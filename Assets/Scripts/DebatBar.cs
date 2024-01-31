using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebatBar : MonoBehaviour
{
    [SerializeField] GameLogic gameLogic;
    [SerializeField] Slider playerBar;
    [SerializeField] Slider enemyBar;
    [SerializeField] TextMeshProUGUI PlayerFollowers;
    [SerializeField] TextMeshProUGUI EnemyFollowers;

    private int maxPlayerFollowers;
    private int maxEnemyFollowers;
    // Start is called before the first frame update
    void Start()
    {
        
        maxEnemyFollowers = GameLogic.enemyHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Followers Counter
        maxPlayerFollowers = GameLogic.playerHealth > maxPlayerFollowers ? GameLogic.playerHealth : maxPlayerFollowers;
        maxEnemyFollowers = GameLogic.enemyHealth > maxEnemyFollowers ? GameLogic.enemyHealth : maxEnemyFollowers;
        PlayerFollowers.text = GameLogic.playerHealth.ToString() + "/" + maxPlayerFollowers.ToString();
        EnemyFollowers.text = GameLogic.enemyHealth.ToString() + "/" + maxEnemyFollowers.ToString();    

        //Followers Bar
        playerBar.value = (float)GameLogic.playerHealth/ (float)maxPlayerFollowers;
        enemyBar.value = (float)GameLogic.enemyHealth/ (float)maxEnemyFollowers;


    }
}
