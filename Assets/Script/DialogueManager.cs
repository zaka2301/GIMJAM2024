using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        
    }

    public void StartDialogue (Dialogue dialogue)
    {
        IsInDialogue = true;
        DialogueUI.SetActive(true);

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

    public void DisplayNextSentence()
    {
        Debug.Log(sentences.Count);
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if(IsTyping)
        {
            StopAllCoroutines();
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
            alpha -= Time.deltaTime * 2.0f;
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

    void EndDialogue()
    {
        IsInDialogue = false;
        DialogueUI.SetActive(false);
    }
}
