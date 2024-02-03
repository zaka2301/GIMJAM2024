using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLists : MonoBehaviour
{
    public Dialogue opening;
    public Dialogue debatWin;
    public Dialogue debatLose;
    public Dialogue debatMonologue;
    public Dialogue endWin;
    public Dialogue endLose;

    private DialogueManager dialogueManager;

    public void Start()
    {

        
        dialogueManager = FindObjectOfType<DialogueManager>();

        string cutscene = "DebatMonologue";//PlayerPrefs.GetString("Cutscene", "Opening");
        
        switch(cutscene)
        {
            case "Opening":
                FindObjectOfType<DialogueManager>().StartDialogue(opening);
                break;
            case "DebatWin":
                FindObjectOfType<DialogueManager>().StartDialogue(debatWin);
                break;
            case "DebatLose":
                FindObjectOfType<DialogueManager>().StartDialogue(debatLose);
                break;
            case "EndWin":
                FindObjectOfType<DialogueManager>().StartDialogue(endWin);
                break;
            case "EndLose":
                FindObjectOfType<DialogueManager>().StartDialogue(endLose);
                break;
            case "DebatMonologue":
                FindObjectOfType<DialogueManager>().StartDialogue(debatMonologue);
                break;
            default:
                Debug.Log("no cutscene loaded");
                break;
        }
    }
}
