using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image backgroundImage;
    public Image transitor;
    public TextMeshProUGUI dialogueText;
    public GameObject DialogueUI;
    public bool IsInDialogue = false;
    private bool IsTyping = false;
    private Queue<string> names = new Queue<string>();
    private Queue<Sprite> scenes = new Queue<Sprite>();
    private Queue<string> sentences = new Queue<string>();
    private string sentence = "";
    private int currentSentence;

    private IEnumerator typeCoroutine;
    
    // Start is called before the first frame update
    void Awake()
    {
        
        names = new Queue<string>();
        scenes = new Queue<Sprite>();
        sentences = new Queue<string>();
        typeCoroutine = TypeSentence("");
        
    }

    void Update()
    {
        if(!IsInDialogue) return;

        if(Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue (Dialogue dialogue)
    {
        IsInDialogue = true;
        StartCoroutine(ShowDialogue());

        names.Clear();
        scenes.Clear();
        sentences.Clear();
        currentSentence = 1;

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }
        foreach (Sprite scene in dialogue.scenes)
        {
            scenes.Enqueue(scene);
        }
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    IEnumerator ShowDialogue()
    {
        DialogueUI.SetActive(true);
        Image image = DialogueUI.GetComponent<Image>();
        float alpha = 0.0f;
        while(alpha < 1.0f)
        {
            alpha += Time.deltaTime * 3.0f;
            image.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            yield return null;
        }
    }
    IEnumerator HideDialogue()
    {
        Image image = DialogueUI.GetComponent<Image>();
        float alpha = 1.0f;
        while(alpha > 0.0f)
        {
            alpha -= Time.deltaTime * 3.0f;
            image.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            yield return null;
        }
        DialogueUI.SetActive(false);
    }

    public void DisplayNextSentence()
    {
        Debug.Log(sentences.Count);
        if (sentences.Count == 0)
        {
            StartCoroutine(EndDialogue());
            return;
        }

        if(IsTyping)
        {
            StopCoroutine(typeCoroutine);
            transitor.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            dialogueText.text = sentence;
            IsTyping = false;
        }
        else
        {
            StartCoroutine(Transit());
            nameText.text = names.Dequeue();
            backgroundImage.sprite = scenes.Dequeue();
            sentence = sentences.Dequeue();
            
            if(sentence == "")
            {
                StartCoroutine(HideDialogue());
                return;
            }
            if(!DialogueUI.activeSelf) StartCoroutine(ShowDialogue()); 
            typeCoroutine = TypeSentence(sentence);
            StopCoroutine(typeCoroutine);
            StartCoroutine(typeCoroutine);
        }
    }

    IEnumerator Transit()
    {
        transitor.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        transitor.sprite = backgroundImage.sprite;
        yield return new WaitForSeconds(0.1f);
        float alpha = 1.0f;
        while(alpha > 0.0f)
        {
            alpha -= Time.deltaTime * 3.0f;
            transitor.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            yield return null;
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        IsTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        IsTyping = false;
    }

    

    IEnumerator EndDialogue()
    {
        IsInDialogue = false;
        
        float col = 1.0f;
        while(col > 0.0f)
        {
            col -= Time.deltaTime * 2.0f;
            DialogueUI.GetComponent<Image>().color = new Color(col, col, col, 1.0f);
            backgroundImage.color = new Color(col, col, col, 1.0f);
            yield return null;
        }
        DialogueUI.SetActive(false);


        switch(PlayerPrefs.GetString("Cutscene"))
        {
            case "Opening":
                SceneManager.LoadScene("ClickerPhase");
                break;
            case "DebatMonologue":
                SceneManager.LoadScene("DebatPhase");
                break;
            case "DebatWin":
                SceneManager.LoadScene("ClickerPhase");
                break;
            case "DebatLose":
                SceneManager.LoadScene("ClickerPhase");
                break;
            case "EndWin":
                SceneManager.LoadScene("ClickerPhase");
                break;
            case "EndLose":
                SceneManager.LoadScene("ClickerPhase");
                break;
            default:
                break;

        }
    }
}
