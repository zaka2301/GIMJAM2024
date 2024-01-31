using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject cardDestination;
    [SerializeField] GameObject cardDestination2;
    private bool isHover = false;
    private GameObject buttonHovered;
    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHover = true;
        buttonHovered = eventData.pointerEnter;

        DebatBottomText.OnHoverCard(buttonHovered.name);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
        buttonHovered = null;

        DebatBottomText.OnHoverCard(" ");
    }

    void Update()
    {
        if(GameLogic.isPlayerTurn && isHover && !GameLogic.isUsingCard && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(UseCard(buttonHovered));

        }
    }

    IEnumerator UseCard(GameObject card)
    {
        GameLogic.isUsingCard = true;
        card.transform.SetParent(card.transform.parent.transform.parent.transform.parent);//LMAO

        Vector2 oPos = card.transform.position;
        float t = 0.0f;
        while(t < 1.0f)
        {
            t += Time.deltaTime;
            card.transform.position = new Vector2(Mathf.SmoothStep(oPos.x, cardDestination.transform.position.x, t),Mathf.SmoothStep(oPos.y, cardDestination.transform.position.y, t));
            yield return null;
        }
        t = 0.0f;
        yield return new WaitForSeconds(1.0f);

        switch(card.name)
        {
            case "Card1":
                /*A1
                int damage = (int) ( (float) GameLogic.enemyHealth * 0.25f); // -1/4
                GameLogic.enemyHealth -= damage;
                */
                /*D1
                GameLogic.timerMultiplier = 0.5f;
                */
                GameLogic.playerHealth = (int) ( (float) GameLogic.playerHealth * 0.80f);
                GameLogic.enemyHealth = (int) ( (float) GameLogic.enemyHealth * 0.60f);
                break;
            case "Card2":
                /*A2
                GameLogic.enemyDamageMultiplier = 0.5f;
                */
                /*D2
                GameLogic.playerDamageMultiplier = 1.2f;
                */
                GameLogic.enemyHealth = (int) ( (float) GameLogic.enemyHealth * 0.60f);
                break;
            case "Card3":
                /*A3
                GameLogic.skips = 1;
                */
                /*D3
                GameLogic.skips = 2;
                */
                GameLogic.playerHealth = (int) ( (float) GameLogic.playerHealth * 1.30f);
                break;

        }




        oPos = card.transform.position;
        while(t < 1.0f)
        {
        t += Time.deltaTime;
            card.transform.position = new Vector2(Mathf.SmoothStep(oPos.x, cardDestination2.transform.position.x, t),Mathf.SmoothStep(oPos.y, cardDestination2.transform.position.y, t));
            yield return null;
        }
 
    }

}
