using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{

    [SerializeField] InputDisplay inputDisplay;
    [SerializeField] GameObject timerBar;
    [SerializeField] public float playerTimer;
    [SerializeField] public float enemyTimer;
    [SerializeField] public int maxRound;
    public float timer;
    public static bool isPlayerTurn = true;
    public static int[] inputs = new int[4]{0,0,0,0};
    public static int currentInput;
    public static int playerHealth;

    public static int enemyHealth;
    private int mistakeCount = 0;

    public static int round = 0;
    // Start is called before the first frame update
    void Start()
    {
        //probably use Playerpref here
        playerHealth = 100;
        enemyHealth = 100;
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
            if (Input.anyKey)
            {
                SetUpPlayerTurn();
            }
        }
        else if(round > maxRound) //debat selesai
        {
            //do something per frame here idk
        }
        else
        {
            if(isPlayerTurn)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    KeyPress(1);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    KeyPress(2);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    KeyPress(3);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    KeyPress(4);
                }


                if(timer <= 0 || currentInput < 0)
                {
                    timer = 0.0f;
                    OnFinishPlayerTurn();
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }

            else // enemy turn
            
            {
                if(timer <= 0)
                {
                    timer = 0.0f;
                    SetUpPlayerTurn();
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
        }
  
    }

    private void OnFinishPlayerTurn()
    {
        timerBar.SetActive(false);

        if(currentInput < 0) //berhasil hit semua
        {
            enemyHealth = (int) ( (float) enemyHealth * 0.66f); // -1/3
        }
        else
        {
            playerHealth = (int) ( (float) playerHealth * 0.80f); // -1/5
        }

        //set up enemy turn

        timer = enemyTimer; //timer for enemy


        isPlayerTurn = false;
    }

    private void SetUpPlayerTurn()
    {
        round += 1;

        if(round > maxRound) //debat selesai
        {


            return; //selesai, gausah setup
        }

        isPlayerTurn = true;

        timerBar.SetActive(true);
        timer = playerTimer;

        currentInput = 3;
        mistakeCount = 0;

        RandomizeInput();
        inputDisplay.SetUp();
    }
}
