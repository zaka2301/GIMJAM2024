using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLogic : MonoBehaviour
{

    [SerializeField] Animator enemyAnimator;

    [SerializeField] InputDisplay inputDisplay;
    [SerializeField] Animator summaryScreen;

    public static float playerTimer;
    public static float enemyTimer;

    public static bool showTips;
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

    [SerializeField] AudioClip koran;

    [SerializeField] AudioSource musicSource;

    [SerializeField] Stopwatch stopwatch;

    [SerializeField] float lifeStealMultiplier;

    [SerializeField] GameObject playerBubble;
    [SerializeField] GameObject enemyBubble;

    Sprite chatp1;
    Sprite chatp2;
    Sprite chatp3;
    Sprite chate1;
    Sprite chate2;
    Sprite chate3;

    Transform cardDestination;
    Transform cardDestination2;
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

    public static float playerDamageMultiplier; //player damage to enemy
    public static float enemyDamageMultiplier;// enemy damage to player


    public static string cardUsed = "";
    public static float timerMultiplier;


    public static bool enemySkip; 
    public static bool roundSkip;

    public static int playerHealthBonus;
    public static int playerHealthBajer;



    private int mistakeCount;

    public static int turn;

    int maxTurn;
    static int stage;
    // Start is called before the first frame update
    void Start()
    {


        chatp1 = Resources.Load<Sprite>("chat debat");
        chatp2 = Resources.Load<Sprite>("chat debat2");
        chatp3 = Resources.Load<Sprite>("chat debat attack");
        chate1 = Resources.Load<Sprite>("chat debat enemy");
        chate2 = Resources.Load<Sprite>("chat debat2 enemy");
        chate3 = Resources.Load<Sprite>("chat debat attack enemy");

        playerDamageMultiplier = 1.0f;
        enemyDamageMultiplier = 1.0f;
        timerMultiplier = 1.0f;

        roundSkip = false;
        enemySkip = false;
        dukunTurn = false;
        showTips = false;

        playerHealthBajer = 0;
        playerHealthBonus = 0;

        mistakeCount = 0;
        turn = 0;

        


        stage = PlayerPrefs.GetInt("Stage", 1);

        enemyAnimator.SetInteger("Alien", stage);
        switch(stage)
        {
            case 1:
                enemyHealth = 500;
                maxTurn = 3;
                break;
            case 2:
                enemyHealth = 1000;
                maxTurn = 3;
                break;
            case 3:
                enemyHealth = 1500;
                maxTurn = 3;
                break;
            case 4:
                enemyHealth = 2000;
                maxTurn = 4;
                break;
            case 5:
                enemyHealth = 3000;
                maxTurn = 5;
                break;
            default:
                Debug.Log("No Stage");
                break;
        }


        playerHealth = PlayerPrefs.GetInt("Followers", 0);
        

        playerTimer = 2.0f;
        enemyTimer = 2.0f;

        playerBaseHealth = playerHealth;

        audioSource = GetComponent<AudioSource>();
        audioSource.volume *= PlayerPrefs.GetFloat("MasterVolume", 1.0f) * PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        musicSource.volume *= PlayerPrefs.GetFloat("MasterVolume", 1.0f) * PlayerPrefs.GetFloat("MusicVolume", 1.0f);


        cardDestination = GameObject.Find("Card Destination").transform;
        cardDestination2 = GameObject.Find("Card Destination 2").transform;

        StartCoroutine(debatBar.UpdateBar());

        
        StartCoroutine(PlayMidText("Start"));



    }


    IEnumerator StartBubble()
    {
        while(true)
        {
            float chance = Random.Range(0.0f, 1.0f);
            yield return new WaitForSeconds(2.0f);
        }
        yield return new WaitForSeconds(2.0f);sad
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
                    a += Time.deltaTime * 2.0f;
                    midText.alpha = a;
                    yield return null;

                }

                yield return new WaitForSeconds(sec);

                a = 0.0f;
                while(a < 1.0f)
                {
                    a += Time.deltaTime * 2.0f;
                    midText.alpha = 1.0f - a;
                    yield return null;
                }

                a = 0.0f;

                float rand = Random.Range(0.0f, 1.0f);

                midText.text = rand < 0.5f ? "Player goes first" : "Opponent goes first";
                while(a < 1.0f)
                {
                    a += Time.deltaTime * 2.0f;
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

                isPlayerTurn = rand > 0.5f ? true : false; //a lil bit unintuitive, but because its inverted in first turn

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

        playerHealth = Mathf.Max(playerHealth, 0);
        enemyHealth = Mathf.Max(enemyHealth, 0);

        StartCoroutine(debatBar.UpdateBar());
        turn += 1;
        //skips = skips == 0 ? skips : skips - 1;
        isUsingCard = false;
        
        

        
        yield return new WaitForSeconds(2.0f);
        isPlayerTurn = !isPlayerTurn;
        if(turn > maxTurn * 2) //debat selesai
        {
            
            audioSource.PlayOneShot(koran, 1.0f);
            summaryScreen.enabled = true;

            playerHealth -= playerHealthBajer;

            StartCoroutine(BlackScreen());

            yield break; //selesai, gausah setup
        }
        else if(roundSkip)
        {
            StartCoroutine(PlayMidText("Round Skipped", 1.0f));
            roundSkip = false;
            yield return new WaitForSeconds(2.0f);
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

            StartCoroutine(PlayMidText("Ready", 0.5f));
            yield return new WaitForSeconds(2.0f);

            stopwatch.gameObject.SetActive(true);
            StartCoroutine(StartStopwatch());
            
            canUseCard = true;
            audioSource.Play();
            float t = 0.0f;
            while(t < 10.0f &&!isUsingCard && !(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
            {
                t += Time.deltaTime;
                yield return null;
            }
            audioSource.Stop();
            stopwatch.gameObject.SetActive(false);
    
            canUseCard = false;

            if(isUsingCard)
            {
                while(cardUsed == "")
                {
                    yield return null;
                }
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

        }

        



        if(!roundSkip)
        {
 
            
                if(isPlayerTurn)
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
                    enemySkip = false;
                    StartCoroutine(PlayMidText("Mic Problem", 1.0f));
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

    IEnumerator StartStopwatch()
    {
        StartCoroutine(stopwatch.StartStopwatch());

        float t = 0.0f;
        Vector2 oPos = stopwatch.gameObject.transform.position;
        while(t < 1.0f)
        {
            t += Time.deltaTime * 2.0f;
            stopwatch.gameObject.transform.position = new Vector2(Mathf.SmoothStep(oPos.x, cardDestination.position.x, t),Mathf.SmoothStep(oPos.y, cardDestination.position.y + 200.0f, t));
            yield return null;
        }

        yield return new WaitForSeconds(9.5f);

        t = 0.0f;

        oPos = stopwatch.gameObject.transform.position;
        while(t < 1.0f)
        {
            t += Time.deltaTime * 2.0f;
            stopwatch.gameObject.transform.position = new Vector2(Mathf.SmoothStep(oPos.x, cardDestination2.position.x, t),Mathf.SmoothStep(oPos.y, cardDestination2.position.y, t));
            yield return null;
        }

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
            audioSource.PlayOneShot(gibberish2, 0.7f);
        }
        else if(p < 0.75f)
        {
            audioSource.PlayOneShot(gibberish3, 0.5f);
        }
        else
        {
            audioSource.PlayOneShot(gibberish4, 0.7f);
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
        while(true)
        {
            if(timer <= 0)
            {
                timer = 0.0f;
                break;
            }
            else
            {
                if(timer % 0.5f < 0.02f) PlayGibberishSound();
                timer -= Time.deltaTime;
                
            }
            yield return null;
        }

        audioSource.Stop();
        

        PlayHitSound();
        float damage =  ( (float) playerHealth * 0.33f * enemyDamageMultiplier); // -1/5
        playerHealth -= (int) damage;
        enemyHealth += (int)(damage * lifeStealMultiplier);


        StartCoroutine(SetUpTurn());

        yield break;
    }

    private IEnumerator SetUpPlayerTurn()
    {

        

        StartCoroutine(PlayMidText("Player's turn", 0.1f));
        yield return new WaitForSeconds(2.0f);
        showTips = true;

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
                float damage =  ( (float) enemyHealth * 0.33f * playerDamageMultiplier); // -1/3
                playerHealth += (int) (damage * lifeStealMultiplier); // * x%
                enemyHealth -= (int) damage;
            }
            else
            {
                float damage = ( (float) playerHealth * 0.20f * enemyDamageMultiplier); // -1/5
                playerHealth -= (int) damage;
                enemyHealth += (int) (damage * lifeStealMultiplier);
            }
        }
        timerBar.SetActive(false);
        
        inputs[0] = 0;
        inputs[1] = 0;
        inputs[2] = 0;
        inputs[3] = 0;


        doDebat = false;
        showTips = false;



        StartCoroutine(SetUpTurn());
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
