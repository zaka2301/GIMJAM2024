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
        maxPlayerFollowers = GameLogic.playerHealth;
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


    }

    public IEnumerator UpdateBar()
    {

        float ipv = playerBar.value;
        float epv = enemyBar.value;
        float time = 0.0f;
        float m = 2.0f;
        while(time < m)
        {
            time += Time.deltaTime;
            playerBar.value = Mathf.Lerp(ipv, (float)GameLogic.playerHealth/ (float)maxPlayerFollowers, time/m);
            enemyBar.value = Mathf.Lerp(epv, (float)GameLogic.enemyHealth/ (float)maxEnemyFollowers, time/m);
            yield return null;
        }
        //PlayerFollowers.text = GameLogic.playerHealth.ToString() + "/" + maxPlayerFollowers.ToString();
        //EnemyFollowers.text = GameLogic.enemyHealth.ToString() + "/" + maxEnemyFollowers.ToString(); 
    }
}
