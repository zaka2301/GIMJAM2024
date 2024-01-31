using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{

    [SerializeField] InputDisplay inputDisplay;
    [SerializeField] Animator summaryScreen;
    [SerializeField] GameObject timerBar;
    [SerializeField] public float playerTimer;
    [SerializeField] public float enemyTimer;
    [SerializeField] public int maxRound;
    [SerializeField] Image blackScreen;

    [SerializeField] AudioClip gibberish1;
    [SerializeField] AudioClip gibberish2;
    [SerializeField] AudioClip gibberish3;
    [SerializeField] AudioClip gibberish4;

    AudioSource audioSource;
    public float timer;
    public static bool isPlayerTurn = true;
    public static bool doDebat = false;
    public static bool onBreak = false;
    public static int[] inputs = new int[4]{0,0,0,0};
    public static int currentInput;
    public static int playerHealth;
    public static int playerBaseHealth;

    public static int enemyHealth;


    private int mistakeCount = 0;

    public static int round = 0;
    // Start is called before the first frame update
    void Start()
    {
        //probably use Playerpref here
        playerHealth = 100;
        enemyHealth = 100;
        round = 0;

        playerBaseHealth = playerHealth;

        audioSource = GetComponent<AudioSource>();
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

    // Update is called once per frame
    void Update()
    {
        if(round == 0) //sebelum mulai
        {
           StartCoroutine(SetUpPlayerTurn());
            
        }
        else if(round > maxRound) //debat selesai
        {
            //do something per frame here idk
        }
        else
        {
            if(!onBreak)
            {
                if(isPlayerTurn)
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
                            StartCoroutine(OnFinishPlayerTurn());
                        }
                        else
                        {
                            timer -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            doDebat = true;
                            SetUpDebateInput();

                        }
                    }
                }

                else // enemy turn

                {
                    if(timer <= 0)
                    {
                        timer = 0.0f;
                        StartCoroutine(SetUpPlayerTurn());
                    }
                    else
                    {
                        timer -= Time.deltaTime;
                    }
                }
            }
        }   
  
    }

    private IEnumerator OnFinishPlayerTurn()
    {
        onBreak = true;
        
        timerBar.SetActive(false);

        inputs[0] = 0;
        inputs[1] = 0;
        inputs[2] = 0;
        inputs[3] = 0;

        if(currentInput < 0) //berhasil hit semua
        {
            int damage = (int) ( (float) enemyHealth * 0.33f); // -1/3
            playerHealth += damage; // * x%
            enemyHealth -= damage;
        }
        else
        {
            int damage = (int) ( (float) playerHealth * 0.20f); // -1/5
            playerHealth -= damage;
            enemyHealth += damage;
        }




        yield return new WaitForSeconds(2.0f);
        //set up enemy turn
        onBreak = false;

        timer = enemyTimer; //timer for enemy

        doDebat = false;
        isPlayerTurn = false;
        audioSource.Play();
    }

    private IEnumerator SetUpPlayerTurn()
    {
        audioSource.Stop();
        round += 1;
        onBreak = true;

        yield return new WaitForSeconds(2.0f);

        if(round > maxRound) //debat selesai
        {
            summaryScreen.enabled = true;
            StartCoroutine(BlackScreen());

            yield break; //selesai, gausah setup
        }

        onBreak = false;

        isPlayerTurn = true;
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

    IEnumerator BlackScreen()
    {
        float a = 0.0f;
        while(a < 0.6f)
        {
            blackScreen.color = new Color(0.0f,0.0f,0.0f, a);
            a += Time.deltaTime;
            yield return null;
        }
    }
}
