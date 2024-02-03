using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    private TextMeshProUGUI name;
    private TextMeshProUGUI dialogue;
    private Color color;
    void Awake()
    {
        name = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        dialogue = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    }
    void Update()
    {
        color.a = GetComponent<Image>().color.a;
        name.color = color;
        dialogue.color = color;
    }
}
