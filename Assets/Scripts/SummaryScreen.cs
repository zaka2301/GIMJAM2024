using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryScreen : MonoBehaviour
{
    [SerializeField] SummaryText summaryText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CountFollowers()
    {
        StartCoroutine(summaryText.CountGain());
    }

}
