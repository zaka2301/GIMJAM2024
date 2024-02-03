using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider slider;
    void Awake()
    {
        gameObject.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        slider.value = GameLogic.timer/GameLogic.playerTimer;
    }

}
