using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebatBottomText : MonoBehaviour
{
    TextMeshProUGUI bottomText;

    private static bool isHover = false;
    // Start is called before the first frame update
    void Start()
    {
        bottomText = gameObject.GetComponent<TextMeshProUGUI>();
        
    }


    public static void OnHoverCard(string card)
    {
        if(card == " ")
        {
            isHover = false;
        }
        else
        {
            isHover = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameLogic.isPlayerTurn && !GameLogic.doDebat)
        {
            if(isHover)
            {
                bottomText.text = "ah";
            }
            else
            {
                bottomText.text = "Use a card or press any arrow to start debate.";
            }
        }
        else
        {
            bottomText.text = " ";
        }
        
    }
}
