using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static float timer = 10.0f;
    public static bool isPlayerTurn = true;
    public static int[] inputs = new int[4]{1,3,2,4};
    public static int currentInput;
    private int playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;

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
            timer *= 0.75f;
            Debug.Log("tolol");
        }
    }

    // Update is called once per frame
    void Update()
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
        }

        if(timer > 0)
        {
        timer -= Time.deltaTime;
        }
        else
        {
            isPlayerTurn = false;
        }
        
    }
}
