using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageManager : MonoBehaviour
{
    public static int stage;
    [SerializeField] Sprite[] stageBackgrounds;
    [SerializeField] SpriteRenderer backroundRenderer;

    [SerializeField] GameObject chatPrefab;
    [SerializeField] Vector2 chatPosition;
    public float chatSpeed;
    private Queue<GameObject> chats;

    [SerializeField] int chatNamesCount;
    [SerializeField] string[] chatNames;
    [SerializeField] int chatTextsCount;
    [SerializeField] string[] chatTexts;
    
    void Start()
    {
        Debug.Log("Hi");
        stage = PlayerPrefs.GetInt("Stage", 1);
        chats = new Queue<GameObject>();
        chats.Clear();
        switch (stage)
        {   
            case(1):
                backroundRenderer.sprite = stageBackgrounds[0];
                break;
            case(2):
                backroundRenderer.sprite = stageBackgrounds[1];
                break;
            case(3):
                backroundRenderer.sprite = stageBackgrounds[2];
                break;
            case(4):
                backroundRenderer.sprite = stageBackgrounds[3];
                break;
            case(5):
                backroundRenderer.sprite = stageBackgrounds[4];
                break;

            
        }
    }

    public void buttonTemp()
    {
        AddChat();
        Debug.Log("button pressed");
    }

    public void AddChat()
    {   
        if (chats.Count == 12)
        {
            Destroy(chats.Dequeue());
        }

        foreach (GameObject chat in chats)
        {
            chat.transform.Translate(new Vector2(0, 0.4f));
        }

        int RNG = Random.Range(0, chatNamesCount);
        string name = chatNames[RNG];
        RNG = Random.Range(0, chatTextsCount);
        string text = chatTexts[RNG];

        var chatNew = Instantiate(chatPrefab, chatPosition, Quaternion.identity);
        chatNew.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name + ": " + text;
        chats.Enqueue(chatNew);
    }
}
