using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{

    [SerializeField] InputDisplay inputDisplay;
    [SerializeField] Animator summaryScreen;

    public static float playerTimer;
    public static float enemyTimer;
    [SerializeField] Image blackScreen;
    [SerializeField] GameObject timerBar;
    [SerializeField] DebatBar debatBar;

    [SerializeField] AudioClip gibberish1;
    [SerializeField] AudioClip gibberish2;
    [SerializeField] AudioClip gibberish3;
    [SerializeField] AudioClip gibberish4;

    static AudioSource audioSource;
    public static float timer;
    public static bool isPlayerTurn = false;
    public static bool doDebat = false;
    public static bool onBreak = false;
    public static bool isUsingCard = false;
    public static bool multiplierTurn = false;
    public static int[] inputs = new int[4]{0,0,0,0};
    public static int currentInput;
    public static int playerHealth;
    public static int playerBaseHealth;

    public static int enemyHealth;

    public static float playerDamageMultiplier = 1.0f;
    public static float enemyDamageMultiplier = 1.0f;

    public static float timerMultiplier = 1.0f;
    public static int skips = 0;


    private int mistakeCount = 0;

    public static int turn = 0;

    int maxTurn = 3;
    // Start is called before the first frame update
    void Start()
    {
        //probably use Playerpref here
        playerHealth = 100;
        enemyHealth = 100;

        playerTimer = 2.0f;
        enemyTimer = 2.0f;

        playerBaseHealth = playerHealth;

        audioSource = GetComponent<AudioSource>();
        StartCoroutine(debatBar.UpdateBar());
        StartCoroutine(OnFinishPlayerTurn());

    }

    private void RandomizeInput()
    {
        for (int i = 0; i < 4; ++i) 
        {
            float p = Random.Range(0.0f, 1.0f);
            int input = 0;

                 if (p < 0.25f) {input = 1;}
            else if (p < 0.5f ) {input = 2;}
            else if (p < 0.75f) {input = 3;}
            else                {input = 4;}

            inputs[i] = input;
        }
    }

    private void KeyPress(int key)
    {

        if (key == inputs[currentInput])
        {
            inputs[currentInput] = 0;
            currentInput--;
        }
        else
        {
            mistakeCount++;
            timer *= 0.75f;
        }
    }

    public IEnumerator OnFinishPlayerTurn()
    {
        audioSource.Play();

        timer = enemyTimer;
        while(skips == 0)
        {
            if(timer <= 0)
            {
                timer = 0.0f;
                break;
            }
            else
            {
                timer -= Time.deltaTime;
                
            }
            yield return null;
        }

        audioSource.Stop();
        

        if(skips == 0)
        {
            int damage = (int) ( (float) playerHealth * 0.20f * enemyDamageMultiplier); // -1/5
            playerHealth -= damage;
            enemyHealth += damage;
        }

        turn += 1;
        skips = skips == 0 ? skips : skips - 1;

        StartCoroutine(debatBar.UpdateBar());
  
        yield return new WaitForSeconds(2.0f);

        if(turn == maxTurn * 2) //debat selesai
        {
            summaryScreen.enabled = true;
            StartCoroutine(BlackScreen());

            yield break; //selesai, gausah setup
        }

        multiplierTurn = false;

        StartCoroutine(SetUpPlayerTurn());

        yield break;
    }

    private IEnumerator SetUpPlayerTurn()
    {

        isPlayerTurn = true;
        timer = playerTimer;

        while(isPlayerTurn && skips == 0)
        {
            if(doDebat)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    audioSource.PlayOneShot(gibberish1, 1.0f);
                    KeyPress(1);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(gibberish2, 1.0f);
                    KeyPress(2);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    audioSource.PlayOneShot(gibberish3, 1.0f);
                    KeyPress(3);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(gibberish4, 1.0f);
                    KeyPress(4);
                }
                if(timer <= 0 || currentInput < 0)
                {
                    timer = 0.0f;
                    
                    break;
                    
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
            else if(!isUsingCard)
            {
                if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    doDebat = true;
                    SetUpDebateInput();
                }
            }
            else
            {
                multiplierTurn = true;
                yield return new WaitForSeconds(1.0f);
                break;
            }
            
            yield return null;
        }


        if(!isUsingCard && skips == 0)
        {
            if(currentInput < 0) //berhasil hit semua
            {
                int damage = (int) ( (float) enemyHealth * 0.33f * playerDamageMultiplier); // -1/3
                playerHealth += damage; // * x%
                enemyHealth -= damage;
            }
            else
            {
                int damage = (int) ( (float) playerHealth * 0.20f * enemyDamageMultiplier); // -1/5
                playerHealth -= damage;
                enemyHealth += damage;
            }
        }
        timerBar.SetActive(false);
        
        inputs[0] = 0;
        inputs[1] = 0;
        inputs[2] = 0;
        inputs[3] = 0;
        isPlayerTurn = false;
        isUsingCard = false;
        doDebat = false;
        turn += 1;
        skips = skips == 0 ? skips : skips - 1;
        //reeset multiplier
        if(!multiplierTurn)
        {
        playerDamageMultiplier = 1.0f;
        timerMultiplier = 1.0f;
        enemyDamageMultiplier = 1.0f;
        }
        StartCoroutine(debatBar.UpdateBar());
        
        yield return new WaitForSeconds(2.0f);
        if(turn == maxTurn * 2) //debat selesai
        {
            summaryScreen.enabled = true;
            StartCoroutine(BlackScreen());
            yield break; //selesai, gausah setup
        }
        StartCoroutine(OnFinishPlayerTurn());
        yield break;

    }

    private void SetUpDebateInput()
    {
        timerBar.SetActive(true);
        timer = playerTimer;

        currentInput = 3;
        mistakeCount = 0;

        RandomizeInput();
        inputDisplay.SetUp();
    }

    public IEnumerator BlackScreen()
    {
        float a = 0.0f;
        while(a < 0.6f)
        {
            blackScreen.color = new Color(0.0f,0.0f,0.0f, a);
            a += Time.deltaTime;
            yield return null;
        }
    }
    public IEnumerator UnBlackScreen()
    {
        float a = 0.6f;
        while(a > 0.0f)
        {
            blackScreen.color = new Color(0.0f,0.0f,0.0f, a);
            a -= Time.deltaTime;
            yield return null;
        }
    }
}
