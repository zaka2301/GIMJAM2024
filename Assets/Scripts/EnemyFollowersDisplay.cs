using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFollowersDisplay : MonoBehaviour
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
        display.text = "Enemy Followers : " + GameLogic.enemyHealth.ToString();
    }
}
