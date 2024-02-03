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

    AudioSource audioSource;
    AudioSource musicSource;
    [SerializeField] AudioClip koranSFX;

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

    void Awake()
    {
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        musicSource = GameObject.Find("Music Player").GetComponent<AudioSource>();

        audioSource.volume *= PlayerPrefs.GetFloat("MasterVolume", 1.0f) *  PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        musicSource.volume *= PlayerPrefs.GetFloat("MasterVolume", 1.0f) *  PlayerPrefs.GetFloat("MusicVolume", 1.0f);
    }
    void Start()
    {
        audioSource.PlayOneShot(koranSFX, 1.0f);
        A1.SetActive(PlayerPrefs.GetInt("A1", 0) == 0 ? false : true);
        A2.SetActive(PlayerPrefs.GetInt("A2", 0) == 0 ? false : true);
        A3.SetActive(PlayerPrefs.GetInt("A3", 0) == 0 ? false : true);
        D1.SetActive(PlayerPrefs.GetInt("D1", 0) == 0 ? false : true);
        D2.SetActive(PlayerPrefs.GetInt("D2", 0) == 0 ? false : true);
        D3.SetActive(PlayerPrefs.GetInt("D3", 0) == 0 ? false : true);
        S1.SetActive(PlayerPrefs.GetInt("S1", 0) == 0 ? false : true);
        S2.SetActive(PlayerPrefs.GetInt("S2", 0) == 0 ? false : true);
        S3.SetActive(PlayerPrefs.GetInt("S3", 0) == 0 ? false : true);
        StartCoroutine(UnBlackScreen());
    }

    public void ReadyButton()
    {
        PlayerPrefs.SetString("CardSlot1", "");
        PlayerPrefs.SetString("CardSlot2", "");
        PlayerPrefs.SetString("CardSlot3", "");

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
        {   
            blackScreen.color = new Color(0.0f, 0.0f, 0.0f, a);
            a -= Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator LoadDebat()
    {
        float a = 0.0f;
        while (a < 1.0f)
        {   
            blackScreen.color = new Color(0.0f, 0.0f, 0.0f, a);
            a += Time.deltaTime;
            yield return null;
        }
        PlayerPrefs.SetString("Cutscene", "DebatMonologue");
        SceneManager.LoadScene("Dialogues");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isHover && Input.GetMouseButtonDown(0))
        {

            if(selection.Contains(buttonHovered.name))
            {
                audioSource.time = 0.25f;
                audioSource.pitch = 3.0f;
                audioSource.Play();
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
                    audioSource.pitch = -3.0f;
                    audioSource.time = 0.2f;
                    audioSource.Play();
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
        
            buttonHovered.GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, 400.0f);
            //buttonHovered.transform.position += new Vector3(0.0f, 1.0f)
            buttonHovered.transform.localScale *= scaler;

        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(buttonHovered != null)
        {
            buttonHovered.transform.localScale /= scaler;
            buttonHovered.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0.0f, 400.0f);
        }
        buttonHovered = null;
        isHover = false;

    }


}
