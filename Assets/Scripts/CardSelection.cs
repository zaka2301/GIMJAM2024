using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject buttonHovered;
    bool isHover = false;

    public List<string> selection = new List<string>();
    [SerializeField] float scaler;

    [SerializeField] GameObject A1;
    [SerializeField] GameObject A2;
    [SerializeField] GameObject A3;
    [SerializeField] GameObject D1;
    [SerializeField] GameObject D2;
    [SerializeField] GameObject D3;
    [SerializeField] GameObject S1;
    [SerializeField] GameObject S2;
    [SerializeField] GameObject S3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ReadyButton()
    {
        
        for(int i = 1; i <= selection.Count; ++i)
        {
            PlayerPrefs.SetString("CardSlot"+i.ToString(), selection[i-1]);
            Debug.Log("CardSlot"+i.ToString());
            Debug.Log(selection[i-1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isHover && Input.GetMouseButtonDown(0))
        {

            if(selection.Contains(buttonHovered.name))
            {
                selection.Remove(buttonHovered.name);
            }
            else
            {
                if(selection.Count >= 3)
                {
                    Debug.Log("cant");
                }
                else
                {
                    selection.Add(buttonHovered.name);
                }
            }
            Debug.Log(selection.Count);

        }
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

        isHover = true;
        buttonHovered = eventData.pointerEnter;
        buttonHovered.transform.SetAsLastSibling();
        buttonHovered = buttonHovered.transform.GetChild(0).gameObject;
        buttonHovered.transform.localScale *= scaler;
        Debug.Log(buttonHovered.name);


    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(buttonHovered != null)
        {
            isHover = false;
            buttonHovered.transform.localScale /= scaler;
        }

    }

    IEnumerator ZoomIn(RectTransform card)
    {
        float scale = 1;
    yield break;
        
    }
}
