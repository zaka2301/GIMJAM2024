using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFollowersDisplay : MonoBehaviour
{
    private Text display;
    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        display.text = "Your Followers : " + GameLogic.playerHealth.ToString();
    }
}
