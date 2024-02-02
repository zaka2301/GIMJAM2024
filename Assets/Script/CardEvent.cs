using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardEvent : MonoBehaviour
{
    [SerializeField] GameObject cardObject;
    [SerializeField] Image cardImage;
    [SerializeField] GameObject cardUI;
    [SerializeField] GameObject stopwatchObject;
    [SerializeField] string[] cardNames;
    [SerializeField] Sprite[] cardSprites;
    [SerializeField] float[] cardWeights;
    [SerializeField] TextMeshProUGUI cardCountDownText;
    [SerializeField] float followersNeeded;
    public bool IsCardEvent;
    private int followerStart;

    public void TriggerCardEvent()
    {
        IsCardEvent = true;
        followerStart = ClickerBehaviour.followers;
        cardCountDownText.text = "Card Event!!";
        cardUI.GetComponent<Animator>().SetTrigger("StartEvent");
    }

    public void StartCardEvent()
    {
        StartCoroutine(stopwatchObject.GetComponent<Stopwatch>().StartStopwatch());
        StartCoroutine(CardEventCountDown(10));
    }

    IEnumerator CardEventCountDown(int time)
    {
        yield return new WaitForSeconds(time);
        EndCardEvent();
    }

    public void UpdateCardEventUI()
    {
        cardCountDownText.text = (ClickerBehaviour.followers - followerStart) + "/100 Followers" ;
    }

    public void EndCardEvent()
    {
        IsCardEvent = false;
        if (ClickerBehaviour.followers - followerStart >= followersNeeded)
        {
            int cardIndex = CardRandomizerIndex();
            PlayerPrefs.SetInt(cardNames[cardIndex], 1);
            cardImage.sprite = cardSprites[cardIndex];
            cardObject.SetActive(true);
            cardObject.GetComponent<Animator>().SetTrigger("GotCard");
        }
        cardUI.GetComponent<Animator>().SetTrigger("EndEvent");
        cardCountDownText.text = "";
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
