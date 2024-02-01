using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLogic : MonoBehaviour
{

    [SerializeField] InputDisplay inputDisplay;
    [SerializeField] Animator summaryScreen;

    public static float playerTimer;
    public static float enemyTimer;
    [SerializeField] Image blackScreen;
    [SerializeField] GameObject timerBar;
    [SerializeField] DebatBar debatBar;
    [SerializeField] TextMeshProUGUI midText;

    [SerializeField] AudioClip gibberish1;
    [SerializeField] AudioClip gibberish2;
    [SerializeField] AudioClip gibberish3;
    [SerializeField] AudioClip gibberish4;

    [SerializeField] AudioClip hit1;
    [SerializeField] AudioClip hit2;
    [SerializeField] AudioClip hit3;

    

    static AudioSource audioSource;
    public static float timer;
    public static bool isPlayerTurn = false;
    public static bool doDebat = false;
    public static bool canUseCard = false;
    public static bool isUsingCard = false;
    public static bool dukunTurn = false;
    public static int[] inputs = new int[4]{0,0,0,0};
    public static int currentInput;
    public static int playerHealth;
    public static int playerBaseHealth;

    public static int enemyHealth;

    public static float playerDamageMultiplier = 1.0f; //player damage to enemy
    public static float enemyDamageMultiplier = 1.0f; // enemy damage to player


    public static string cardUsed = "";
    public static float timerMultiplier = 1.0f;
    public static int skips = 0;

    public static bool enemySkip = false;
    public static bool roundSkip = false;

    public static int playerHealthBonus = 0;
    public static int playerHealthBajer = 0;

    private static bool playerFirst;


    private int mistakeCount = 0;

    public static int turn = 0;

    int maxTurn;
    int stage;
    // Start is called before the first frame update
    void Start()
    {
        stage = PlayerPrefs.GetInt("Stage", 1);
        switch(stage)
        {
            case 1:
            case 2:
                maxTurn = 3;
                break;
            case 3:
            case 4:
                maxTurn = 4;
                break;
            case 5:
                maxTurn = 5;
                break;
            default:
                Debug.Log("No Stage");
                break;
        }
        

        playerHealth = PlayerPrefs.GetInt("Followers", 100);
        enemyHealth = 100;

        playerTimer = 2.0f;
        enemyTimer = 2.0f;

        playerBaseHealth = playerHealth;

        audioSource = GetComponent<AudioSource>();
        StartCoroutine(debatBar.UpdateBar());

        
        StartCoroutine(PlayMidText("Start"));



    }



    IEnumerator PlayMidText(string type, float sec = 2.0f)
    {
        float a = 0.0f;
        switch(type)
        {
            case "Start":
                midText.text = "Stage " + stage.ToString();
                while(a < 1.0f)
                {
                    a += Time.deltaTime;
                    midText.alpha = a;
                    yield return null;

                }

                yield return new WaitForSeconds(sec);

                a = 0.0f;
                while(a < 1.0f)
                {
                    a += Time.deltaTime;
                    midText.alpha = 1.0f - a;
                    yield return null;
                }

                a = 0.0f;

                float rand = Random.Range(0.0f, 0.5f);

                midText.text = rand < 0.5f ? "Player goes first" : "Opponent goes first";
                while(a < 1.0f)
                {
                    a += Time.deltaTime;
                    midText.alpha = a;
                    yield return null;

                }

                yield return new WaitForSeconds(sec);

                a = 0.0f;
                while(a < 1.0f)
                {
                    a += Time.deltaTime;
                    midText.alpha = 1.0f - a;
                    yield return null;
                }

                playerFirst = rand < 0.5f ? true : false;

                StartCoroutine(SetUpTurn());


                break;

            default:

                midText.text = type;
                while(a < 1.0f)
                {
                    a += Time.deltaTime;
                    midText.alpha = a;
                    yield return null;

                }

                yield return new WaitForSeconds(sec);

                a = 0.0f;
                while(a < 1.0f)
                {
                    a += Time.deltaTime;
                    midText.alpha = 1.0f - a;
                    yield return null;
                }
                break;
        }
    }


    IEnumerator SetUpTurn() // 1 = player, 2 = opp
    {    
        playerHealth -= playerHealthBonus;
        playerHealthBonus = 0;

        StartCoroutine(debatBar.UpdateBar());
        turn += 1;
        skips = skips == 0 ? skips : skips - 1;
        isUsingCard = false;

        Debug.Log(turn);

        yield return new WaitForSeconds(2.0f);

        if(turn > maxTurn * 2) //debat selesai
        {
            summaryScreen.enabled = true;
            StartCoroutine(BlackScreen());

            yield break; //selesai, gausah setup
        }
        else if(roundSkip)
        {
            StartCoroutine(PlayMidText("Round Skipped", 1.0f));
            roundSkip = false;
            yield return new WaitForSeconds(4.0f);
            StartCoroutine(SetUpTurn());
            yield break;
        }
        else if(turn % 2 != 0) // round change
        {
            //reset multiplier
            if(!dukunTurn)
            {
                enemyDamageMultiplier = 1.0f;
            }
            else
            {
                dukunTurn = false;

            }
            playerDamageMultiplier = 1.0f;
            timerMultiplier = 1.0f;


            StartCoroutine(PlayMidText("Preparation", 0.5f));
            canUseCard = true;
            audioSource.Play();
            yield return new WaitForSeconds(10.0f);
    
            canUseCard = false;
        }

        


        if(cardUsed != "")
        {
            Debug.Log("cutscene");
            yield return new WaitForSeconds(1.0f);

            //card effects
            
            switch(cardUsed)
            {
                case "A1":
                    enemyHealth = (int) ( (float) enemyHealth * 0.80f); //-0.2%
                    StartCoroutine(debatBar.UpdateBar(5.0f));
                    yield return new WaitForSeconds(5.0f);
                    break;
                case "A2":
                    dukunTurn = true;
                    enemyDamageMultiplier = 0.7f;
                    break;
                case "A3":
                    enemySkip = true;
                    break;
                case "D1":
                    timerMultiplier = 0.5f;
                    break;
                case "D2":
                    playerHealthBonus = (int) ( (float) playerHealth * 0.25f);
                    playerHealth += playerHealthBonus;
                    StartCoroutine(debatBar.UpdateBar());

                    playerDamageMultiplier = 1.3f;
                    break;
                case "D3":

                    roundSkip = true;
                    playerHealth += (int) ( (float) playerHealth * 0.30f);


                    break;
                case "S1":
                    playerHealth = (int) ( (float) playerHealth * 0.70f);
                    playerDamageMultiplier = 2.0f;
                    break;
                case "S2":
                    playerHealthBajer = (int) ( (float) playerHealth * 0.30f);
                    playerHealth += playerHealthBajer;
                    StartCoroutine(debatBar.UpdateBar());
                    break;
                case "S3":
                    playerDamageMultiplier = 2.0f;
                    break;
                default:
                    break;
            }
            

            

            cardUsed  = "";
        }

        if(playerFirst && !roundSkip)
        {
            if(turn % 2 != 0) 
            {

                StartCoroutine(SetUpPlayerTurn());
            }
            else
            {
                if(!enemySkip)
                {
                    StartCoroutine(OnFinishPlayerTurn());
                }
                else
                {
                    StartCoroutine(PlayMidText("Mic Problem"));
                    yield return new WaitForSeconds(2.0f);
                    StartCoroutine(SetUpTurn());
                }

            }
        }
        else
        {
            StartCoroutine(SetUpTurn());
        }

        yield break;
    }


    void PlayHitSound()
    {
        float p = Random.Range(0.0f, 1.0f);
        if(p < 0.33f)
        {
            audioSource.PlayOneShot(hit1, 1.0f);
        }
        else if(p < 0.67)
        {
            audioSource.PlayOneShot(hit2, 1.0f);
        }
        else
        {
            audioSource.PlayOneShot(hit3, 1.0f);
        }
    }
    void PlayGibberishSound()
    {
        float p = Random.Range(0.0f, 1.0f);
        if(p < 0.25f)
        {
            audioSource.PlayOneShot(gibberish1, 0.5f);
        }
        else if(p < 0.5)
        {
            audioSource.PlayOneShot(gibberish2, 1.0f);
        }
        else if(p < 0.75f)
        {
            audioSource.PlayOneShot(gibberish3, 0.5f);
        }
        else
        {
            audioSource.PlayOneShot(gibberish4, 1.0f);
        }
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
        StartCoroutine(PlayMidText("Opponent's turn", 0.1f)); 
        yield return new WaitForSeconds(2.0f);
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
                if(timer % 0.5f < 0.05f) PlayGibberishSound();
                timer -= Time.deltaTime;
                
            }
            yield return null;
        }

        audioSource.Stop();
        

        if(skips == 0)
        {
            PlayHitSound();
            int damage = (int) ( (float) playerHealth * 0.20f * enemyDamageMultiplier); // -1/5
            playerHealth -= damage;
            enemyHealth += damage;
        }

        StartCoroutine(SetUpTurn());

        yield break;
    }

    private IEnumerator SetUpPlayerTurn()
    {
        StartCoroutine(PlayMidText("Player's turn", 0.1f));
        yield return new WaitForSeconds(2.0f);
        isPlayerTurn = true;
        timer = playerTimer;
        
        while(isPlayerTurn)
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
            else 
            {
                if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    doDebat = true;
                    SetUpDebateInput();
                }
            }

            
            yield return null;
        }


        //if(skips == 0)
        {
            PlayHitSound();
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

        doDebat = false;



        StartCoroutine(SetUpTurn());
        yield break;

    }

    bool CheckRoundEnd()
    {
        if(turn == maxTurn * 2) //debat selesai
        {
            summaryScreen.enabled = true;
            StartCoroutine(BlackScreen());

            return true; //selesai, gausah setup
        }
        return false;
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
