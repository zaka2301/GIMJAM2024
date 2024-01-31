using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDeck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Up", true);
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Up", false);
    }



}
