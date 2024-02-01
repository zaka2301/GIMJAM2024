using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardSlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject cardSlot1;
    GameObject cardSlot2;
    GameObject cardSlot3;
    [SerializeField] GameObject cardPreview;
    [SerializeField] GameObject cardDestination;
    [SerializeField] GameObject cardDestination2;
    [SerializeField] Sprite A1;
    [SerializeField] Sprite A2;
    [SerializeField] Sprite A3;
    [SerializeField] Sprite D1;
    [SerializeField] Sprite D2;
    [SerializeField] Sprite D3;
    [SerializeField] Sprite S1;
    [SerializeField] Sprite S2;
    [SerializeField] Sprite S3;

    private bool isHover = false;
    private GameObject buttonHovered;

    static Dictionary<string, string> cards = new Dictionary<string, string>()
    {
        {"CardSlot1", "A1"},
        {"CardSlot2", "D2"},
        {"CardSlot3", "S3"}
    };
    static Dictionary<string, Sprite> cardsSprite = new Dictionary<string, Sprite>()
    {
        {"A1", null},
        {"A2", null},
        {"A3", null},
        {"D1", null},
        {"D2", null},
        {"D3", null},
        {"S1", null},
        {"S2", null},
        {"S3", null}
    };

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!GameLogic.isUsingCard)
        {
            isHover = true;
            buttonHovered = eventData.pointerEnter;

            cardPreview.GetComponent<Image>().sprite = cardsSprite[PlayerPrefs.GetString(buttonHovered.name)];
            cardPreview.SetActive(true);

            DebatBottomText.OnHoverCard(buttonHovered.name);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
        buttonHovered = null;
        cardPreview.SetActive(false);
        DebatBottomText.OnHoverCard(" ");
    }




    void Awake()
    {
        PlayerPrefs.SetString("CardSlot1", "A1");
        PlayerPrefs.SetString("CardSlot2", "D2");
        PlayerPrefs.SetString("CardSlot3", "S3");
        cardsSprite["A1"] = A1;
        cardsSprite["A2"] = A2;
        cardsSprite["A3"] = A3;
        cardsSprite["D1"] = D1;
        cardsSprite["D2"] = D2;
        cardsSprite["D3"] = D3;
        cardsSprite["S1"] = S1;
        cardsSprite["S2"] = S2;
        cardsSprite["S3"] = S3;
    }

    void Start()
    {
        cardSlot1 = this.gameObject.transform.GetChild(0).gameObject;
        cardSlot2 = this.gameObject.transform.GetChild(1).gameObject;
        cardSlot3 = this.gameObject.transform.GetChild(2).gameObject;

        cardSlot1.GetComponent<Image>().sprite = cardsSprite["A1"];
        cardSlot2.GetComponent<Image>().sprite = cardsSprite["D2"];
        cardSlot3.GetComponent<Image>().sprite = cardsSprite["S3"];
    }
    void Update()
    {
        if(isHover && GameLogic.canUseCard && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(UseCard(buttonHovered));

        }
    }



    IEnumerator UseCard(GameObject card)
    {

        GameLogic.isUsingCard = true;
        GameLogic.cardUsed = card.name;
        card.transform.SetParent(card.transform.parent.transform.parent.transform.parent);//LMAO

        Vector2 oPos = card.transform.position;
        float t = 0.0f;
        Vector2 scaler = card.GetComponent<RectTransform>().sizeDelta;
        while(t < 1.0f)
        {
            t += Time.deltaTime;
            card.GetComponent<RectTransform>().sizeDelta = scaler * (t * 2.5f + 1.0f);
            card.transform.position = new Vector2(Mathf.SmoothStep(oPos.x, cardDestination.transform.position.x, t),Mathf.SmoothStep(oPos.y, cardDestination.transform.position.y, t));
            yield return null;
        }
        t = 0.0f;
        yield return new WaitForSeconds(1.0f);

        
        /*
        switch(cards[card.name])
        {
            case "A1":
                int damage = (int) ( (float) GameLogic.enemyHealth * 0.25f); // -1/4
                GameLogic.enemyHealth -= damage;
                break;
            case "A2":
                GameLogic.enemyDamageMultiplier = 0.5f;
                break;
            case "A3":
                GameLogic.skips = 1;
                break;
            case "D1":
                GameLogic.timerMultiplier = 0.5f;
                break;
            case "D2":
                GameLogic.playerDamageMultiplier = 1.2f;
                break;
            case "D3":
                GameLogic.skips = 2;
                break;
            case "S1":
                GameLogic.playerHealth = (int) ( (float) GameLogic.playerHealth * 0.80f);
                GameLogic.enemyHealth = (int) ( (float) GameLogic.enemyHealth * 0.60f);
                break;
            case "S2":
                GameLogic.enemyHealth = (int) ( (float) GameLogic.enemyHealth * 0.60f);
                break;
            case "S3":
                GameLogic.playerHealth = (int) ( (float) GameLogic.playerHealth * 1.30f);
                break;

        }
        */



        //scaler = card.GetComponent<RectTransform>().sizeDelta;
        oPos = card.transform.position;
        while(t < 1.0f)
        {
            t += Time.deltaTime;
            //card.GetComponent<RectTransform>().sizeDelta = scaler * ((t-1.0f) * 2.5f + 1.0f);
            card.transform.position = new Vector2(Mathf.SmoothStep(oPos.x, cardDestination2.transform.position.x, t),Mathf.SmoothStep(oPos.y, cardDestination2.transform.position.y, t));
            yield return null;
        }
 
    }

}
