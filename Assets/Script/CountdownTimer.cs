using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public int time {get; private set;}
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] int startingTime;
    private int cardTime1;
    private int cardTime2;

    public void StartClock()
    {
        time = startingTime;

        cardTime1 = Random.Range(45, 50);
        cardTime2 = Random.Range(15, 20);
        InvokeRepeating("TimerDecrease", 1f, 1f);
        
    }

    void TimerDecrease()
    {   
        if (time > 0)
        {
            time--;
        }
        if (time == cardTime1 | time == cardTime2)
        {
            GetComponent<CardEvent>().TriggerCardEvent();
        }
        UpdateUI();
        if (time == 0)
        {
            StartCoroutine(GetComponent<PreGame>().EndGame());
        }
    }

    void UpdateUI()
    {
        timeText.text = "" + time;
    }
}
