using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebatBottomText : MonoBehaviour
{
    TextMeshProUGUI bottomText;

    private static bool isHover = false;
    private static string displayText;
    // Start is called before the first frame update
    void Start()
    {
        bottomText = gameObject.GetComponent<TextMeshProUGUI>();
        
    }


    public static void OnHoverCard(string card)
    {
        if(card == " ")
        {
            displayText = "Use a card or press any arrow to start debate.";
        }
        else
        {
            switch (card)
            {
                case "Card1":
                    displayText = "A1 NIH BOS";
                    break;
                case "Card2":
                    displayText = "A2 NIH BOS";
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameLogic.isPlayerTurn && !GameLogic.doDebat && !GameLogic.isUsingCard)
        {

            bottomText.text = displayText;

        }
        else
        {
            bottomText.text = " ";
        }
        
    }
}
