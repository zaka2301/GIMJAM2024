using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryScreen : MonoBehaviour
{
    [SerializeField] SummaryText summaryText;


    public void CountFollowers()
    {
        StartCoroutine(summaryText.CountGain());
    }

}
