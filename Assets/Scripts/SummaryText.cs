using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummaryText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI followersGained;
    [SerializeField] TextMeshProUGUI followersTotal;
    // Start is called before the first frame update
    private int baseFollowers;
    private int followerGain = 0;


    public IEnumerator CountGain()
    {
        baseFollowers = GameLogic.playerBaseHealth;
        followersTotal.text = baseFollowers.ToString();
        
        followerGain = GameLogic.playerHealth - baseFollowers;
        int i = (int) Mathf.Sign(followerGain);



        int follower = 0;

        while(follower != followerGain)
        {
            if(Input.GetMouseButtonDown(0))
            {
                followersGained.text = followerGain.ToString();
                break;
            }
            follower += i;
            followersGained.text = follower.ToString();
            yield return null;
        }
        StartCoroutine(CountTotal());
    }

    public IEnumerator CountTotal()
    {
        int follower = baseFollowers;

        int i = (int) Mathf.Sign(followerGain);

        while(follower != GameLogic.playerHealth)
        {
            if(Input.GetMouseButtonDown(0))
            {
                followersTotal.text = GameLogic.playerHealth.ToString();
                break;
            }
            follower += i;
            followersTotal.text = follower.ToString();
            yield return null;
        }
    }
}
