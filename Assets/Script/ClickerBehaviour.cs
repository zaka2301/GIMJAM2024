using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickerBehaviour : MonoBehaviour
{
    public static int followers {get; private set; }
    [SerializeField] TextMeshProUGUI followerText;
    [SerializeField] int followerGain;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            followers += followerGain;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        followerText.text = "Followers: " + followers;
    }
}
