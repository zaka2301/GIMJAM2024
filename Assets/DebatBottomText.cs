using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebatBottomText : MonoBehaviour
{
    TextMeshProUGUI bottomText;


    private static string displayText = "";
    /*
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
    */
    static Dictionary<string, string> cardDescription = new Dictionary<string, string>()
    {
     {"A1", "Sembako Attack"},
     {"A2", "Santet Dukun"},
     {"A3", "Toggle Mute"},
     {"D1", "Rugi dong? Yang bener aja?"},
     {"D2", "Followers Roar"},
     {"D3", "Kita Rehat Sejenak"},
     {"S1", "Ghibah Technique"},
     {"S2", "Bajer Service"},
     {"S3", "Ordal no Jutsu"}
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
            displayText = "";
        }
        else
        {


            displayText = "Use card : " + cardDescription[PlayerPrefs.GetString(card)];
            /*
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
            */
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameLogic.isPlayerTurn && !GameLogic.doDebat)
        {

            bottomText.text = "Press any arrow to start the debate.";

        }
        else
        {
            bottomText.text = displayText;
        }
        
    }
}
