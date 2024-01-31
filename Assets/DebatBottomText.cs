using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebatBottomText : MonoBehaviour
{
    TextMeshProUGUI bottomText;


    private static string displayText = "Use a card or press any arrow to start debate.";
    static Dictionary<string, string> cardDescription = new Dictionary<string, string>()
    {
     {"A1", "Lempar Sembako : Throws object to your opponent, deals damage to opponent"},
     {"A2", "Hypnotize : Throw your opponent off, decreases opponent damage"}
    };
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
                    displayText = cardDescription["A1"];
                    break;
                case "Card2":
                    displayText = cardDescription["A2"];
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
