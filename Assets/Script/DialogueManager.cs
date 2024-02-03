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
    private Queue<AudioClip> sounds = new Queue<AudioClip>();
    private string sentence = "";

    private IEnumerator typeCoroutine;
    private AudioSource audioSource;
    public GameObject replayScreen;
    
    // Start is called before the first frame update
    void Awake()
    {
        
        names = new Queue<string>();
        scenes = new Queue<Sprite>();
        sentences = new Queue<string>();
        sounds = new Queue<AudioClip>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
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
        sounds.Clear();

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
        foreach (AudioClip sound in dialogue.sounds)
        {
            sounds.Enqueue(sound);
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


            audioSource.PlayOneShot(sounds.Dequeue(), 1.0f);
            
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
            case "EndLose":
                replayScreen.SetActive(true);
                backgroundImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

                break;
            default:
                break;

        }
    }


    public void LoadSceneButton(string scene)
    {
        if(scene == "ClickerPhase")
        {
            PlayerPrefs.SetInt("Stage", 1);
            PlayerPrefs.SetInt("Followers", 0);

            PlayerPrefs.SetInt("A1", 0);
            PlayerPrefs.SetInt("A2", 0);
            PlayerPrefs.SetInt("A3", 0);
            PlayerPrefs.SetInt("D1", 0);
            PlayerPrefs.SetInt("D2", 0);
            PlayerPrefs.SetInt("D3", 0);
            PlayerPrefs.SetInt("S1", 0);
            PlayerPrefs.SetInt("S2", 0);
            PlayerPrefs.SetInt("S3", 0);
        }
        SceneManager.LoadScene(scene);
    }
}
