using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardEvent : MonoBehaviour
{

    [SerializeField] float[] rarityChance;
    [SerializeField] TextMeshProUGUI cardCountDownText;
    private bool IsCardEvent;
    private int followerStart;

    public void TriggerCardEvent()
    {
        IsCardEvent = true;
        followerStart = ClickerBehaviour.followers;
        StartCoroutine(CardEventCountDown(10));
    }

    public void EndCardEvent()
    {
        IsCardEvent = false;
        cardCountDownText.text = "You have gained " + (ClickerBehaviour.followers - followerStart) + "new followers!";
    }

    IEnumerator CardEventCountDown(int time)
    {
        UpdateCardEventUI(time);
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            UpdateCardEventUI(time);
        }
        EndCardEvent();
    }

    void UpdateCardEventUI(int time)
    {
        cardCountDownText.text = "Card Event: " + time;
    }

    public void CardRandomizer()
    {
        float RNG = Random.Range(0f, 1f);
        if (RNG <= rarityChance[0])
        {
            float RNG2 = Random.Range(0, 3);
            switch (RNG2)
            {
                case 0:
                    Debug.Log ("Attack tier 1");
                    break;
                case 1:
                    Debug.Log ("Defend tier 1");
                    break;
                case 2:
                    Debug.Log ("Heal tier 1");
                    break;
            }
        }
        else if (RNG <= rarityChance[0] + rarityChance[1])
        {
            float RNG2 = Random.Range(0, 3);
            switch (RNG2)
            {
                case 0:
                    Debug.Log ("Attack tier 2");
                    break;
                case 1:
                    Debug.Log ("Defend tier 2");
                    break;
                case 2:
                    Debug.Log ("Heal tier 2");
                    break;
            }
        }
        else if (RNG <= rarityChance[0] + rarityChance[1] + rarityChance[2])
        {
            float RNG2 = Random.Range(0, 3);
            switch (RNG2)
            {
                case 0:
                    Debug.Log ("Attack tier 3");
                    break;
                case 1:
                    Debug.Log ("Defend tier 3");
                    break;
                case 2:
                    Debug.Log ("Heal tier 3");
                    break;
            }
        }
        else
        {
            Debug.Log ("Special");
        }
    }
}
