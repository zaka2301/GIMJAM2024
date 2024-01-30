using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public int time {get; private set;}
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] int startingTime;

    void Start()
    {
        time = startingTime;
        InvokeRepeating("TimerDecrease", 1f, 1f);
    }

    void TimerDecrease()
    {
        time--;
        UpdateUI();
    }

    void UpdateUI()
    {
        timeText.text = "" + time;
    }
}
