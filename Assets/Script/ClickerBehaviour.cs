using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerBehaviour : MonoBehaviour
{
    public static int followers {get; private set; }
    [SerializeField] TextMeshProUGUI followerText;
    [SerializeField] GameObject followerSlider;
    [SerializeField] int followerGain;
    [SerializeField] int maxFollowers;

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
        followerText.text = "" + followers;
        followerSlider.GetComponent<Slider>().value = (float) followers / (float) maxFollowers;
    }
}
