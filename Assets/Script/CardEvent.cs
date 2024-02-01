using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardEvent : MonoBehaviour
{
    [SerializeField] string[] cardNames;
    [SerializeField] float[] cardWeights;
    [SerializeField] TextMeshProUGUI cardCountDownText;
    [SerializeField] GameObject cardObject;
    private bool IsCardEvent;
    private int followerStart;

    public void TriggerCardEvent()
    {
        IsCardEvent = true;
        followerStart = ClickerBehaviour.followers;
        StartCoroutine(CardEventCountDown(10));
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

    public void EndCardEvent()
    {
        IsCardEvent = false;
        cardObject.SetActive(true);
        cardObject.GetComponent<Animator>().SetTrigger("GotCard");
        cardCountDownText.text = "You got " + (cardNames[CardRandomizerIndex()]);
    }

    int CardRandomizerIndex()
    {
        float weightSum = 0f;

        foreach (float weight in cardWeights)
        {
            weightSum += weight;
        }

        int selectedIndex = 0;
        float RNG = Random.Range(0f, weightSum);
        while (cardWeights[selectedIndex] <= RNG | cardWeights[selectedIndex] == 0)
        {
            RNG -= cardWeights[selectedIndex];
            selectedIndex++;
        }
        cardWeights[selectedIndex] = 0;
        return selectedIndex;
    }
}
