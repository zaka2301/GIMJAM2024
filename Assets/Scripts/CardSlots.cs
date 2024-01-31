using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update

    public void OnPointerEnter(PointerEventData eventData)
    {

        Debug.Log(eventData.pointerEnter);
        DebatBottomText.OnHoverCard("sdaf");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        DebatBottomText.OnHoverCard(" ");
    }
}
