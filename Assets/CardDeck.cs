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
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Up", true);
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Up", false);
    }

    IEnumerator MoveUp()
    {
        Vector3 oPos = transform.position;
        
        while(transform.position.y < 170)
        {
            Debug.Log(transform.position);
            transform.position = Vector2.MoveTowards(transform.position, oPos + new Vector3(0, 170, 0), 100.0f * Time.deltaTime);
            yield return null;
        }
    }

}
