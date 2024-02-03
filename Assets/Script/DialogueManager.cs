using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image backgroundImage;
    public TextMeshProUGUI dialogueText;
    public GameObject DialogueUI;
    public bool IsInDialogue = false;
    private bool IsTyping = false;
    private Queue<string> names;
    private Queue<Sprite> scenes;
    private Queue<string> sentences;
    private string sentence;
    private int currentSentence;
    
    // Start is called before the first frame update
    void Start()
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
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        if(IsTyping)
        {
            StopAllCoroutines();
            dialogueText.text = sentence;
            IsTyping = false;
        }
        else
        {
            nameText.text = names.Dequeue();
            backgroundImage.sprite = scenes.Dequeue();
            sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
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
