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
     {"A1", "Sembako Throw : Send a groceries attack bought by emak-emak, deals damage to opponent"},
     {"A2", "Santet Dukun Klenik : Throw your opponent off, decreases opponent damage"},
     {"A3", "Mic Mute : Skips 1 enemy turn"},
     {"D1", "Rugi dong / yang bener aje : Slows down the timer bar on next turn"},
     {"D2", "Followers Roar : Increases player damage for the next turn"},
     {"D3", "We'll be right back : Increases player's follower and skips a whole round (2 turns)"},
     {"S1", "Fitnah : Decreases player's followers, deals great amount of damage to opponent"},
     {"S2", "Summon Ketua Partai : Deals great damage to opponent"},
     {"S3", "Buzzer Service : Increases followers greatly"}
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
                case "CardSlot1":
                    displayText = cardDescription["A1"];
                    break;
                case "CardSlot2":
                    displayText = cardDescription["D2"];
                    break;
                case "CardSlot3":
                    displayText = cardDescription["S3"];
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
