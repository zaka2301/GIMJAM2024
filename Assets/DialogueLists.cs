using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLists : MonoBehaviour
{
    public Dialogue opening;
    public Dialogue debatWin;
    public Dialogue debatLose;
    public Dialogue endWin;
    public Dialogue endLose;

    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        string cutscene = PlayerPrefs.GetString("Cutscene", "Opening");
        
        switch(cutscene)
        {
            case "Opening":
                FindObjectOfType<DialogueManager>().StartDialogue(opening);
                break;
            default:
                Debug.Log("no cutscene loaded");
                break;
        }
    }
}
