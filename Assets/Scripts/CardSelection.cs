using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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

    [SerializeField] Image blackScreen;


    // Start is called before the first frame update
    void Start()
    {
        A1.SetActive(PlayerPrefs.GetInt("A1", 1) == 0 ? false : true);
        A2.SetActive(PlayerPrefs.GetInt("A2", 1) == 0 ? false : true);
        A3.SetActive(PlayerPrefs.GetInt("A3", 0) == 0 ? false : true);
        D1.SetActive(PlayerPrefs.GetInt("D1", 1) == 0 ? false : true);
        D2.SetActive(PlayerPrefs.GetInt("D2", 0) == 0 ? false : true);
        D3.SetActive(PlayerPrefs.GetInt("D3", 1) == 0 ? false : true);
        S1.SetActive(PlayerPrefs.GetInt("S1", 0) == 0 ? false : true);
        S2.SetActive(PlayerPrefs.GetInt("S2", 0) == 0 ? false : true);
        S3.SetActive(PlayerPrefs.GetInt("S3", 1) == 0 ? false : true);
        StartCoroutine(UnBlackScreen());
    }

    public void ReadyButton()
    {
        
        for(int i = 1; i <= selection.Count; ++i)
        {
            PlayerPrefs.SetString("CardSlot"+i.ToString(), selection[i-1]);
            //Debug.Log("CardSlot"+i.ToString());
            //Debug.Log(selection[i-1]);
        }

        StartCoroutine(LoadDebat());
        

    }

    IEnumerator UnBlackScreen()
    {
        float a = 1.0f;
        while (a > 0.0f)
        {   a -= Time.deltaTime;
            blackScreen.color = new Color(0.0f, 0.0f, 0.0f, a);
            yield return null;
        }
    }
    IEnumerator LoadDebat()
    {
        float a = 0.0f;
        while (a < 1.0f)
        {   a += Time.deltaTime;
            blackScreen.color = new Color(0.0f, 0.0f, 0.0f, a);
            
            yield return null;
        }
        SceneManager.LoadScene("DebatPhase");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isHover && Input.GetMouseButtonDown(0))
        {

            if(selection.Contains(buttonHovered.name))
            {
                selection.Remove(buttonHovered.name);
                buttonHovered.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                if(selection.Count >= 3)
                {
                    //Debug.Log("cant");
                }
                else
                {
                    selection.Add(buttonHovered.name);
                    buttonHovered.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            /*
            string text = "";
            for(int i = 0; i < selection.Count; ++i)
            {
                text += selection[i] +" ";
            }
            Debug.Log(text);
            */
        }
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerEnter.CompareTag("Cards"))
        {
            
            isHover = true;
            buttonHovered = eventData.pointerEnter;
            buttonHovered.transform.SetAsLastSibling();
            buttonHovered = buttonHovered.transform.GetChild(0).gameObject;
        
            buttonHovered.transform.localScale *= scaler;

        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(buttonHovered != null)
        {
            buttonHovered.transform.localScale /= scaler;
        }
        buttonHovered = null;
        isHover = false;

    }

    IEnumerator ZoomIn(RectTransform card)
    {
        float scale = 1;
    yield break;
        
    }
}
