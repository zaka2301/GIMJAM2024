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
    [SerializeField] TextMeshProUGUI cardCountDownText;

    [SerializeField] string[] cardNames;
    [SerializeField] Sprite[] cardSprites;
    [SerializeField] float[] cardWeights;

    public static bool IsCardEvent {get; private set;}
    [SerializeField] float[] followersNeeded;
    private int currentCardEvent = 0;
    private int followerStart;

    private int stageMultiplier;

    void Start()
    {
        stageMultiplier = (int) Mathf.Pow(2, PlayerPrefs.GetInt("Stage", 1) - 1);
        if (PlayerPrefs.GetInt("Stage", 1) != 1)
        {
            int i = 0;
            foreach (string cardName in cardNames)
            {
                if (PlayerPrefs.GetInt(cardName, 0) == 1)
                {
                    cardWeights[i] = 0;
                }
                i++;
            }
        }
    }

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
        StartCoroutine(CardEventCountDown());
    }

    public void EndCardEvent()
    {
        IsCardEvent = false;
        if (ClickerBehaviour.followers - followerStart >= followersNeeded[currentCardEvent] * stageMultiplier)
        {
            int cardIndex = CardRandomizerIndex();
            PlayerPrefs.SetInt(cardNames[cardIndex], 1);
            cardImage.sprite = cardSprites[cardIndex];
            cardObject.SetActive(true);
            cardObject.GetComponent<Animator>().SetTrigger("GotCard");
        }
        cardCountDownText.text = "";
        cardUI.GetComponent<Animator>().SetTrigger("EndEvent");
        currentCardEvent++;
    }

    IEnumerator CardEventCountDown()
    {
        yield return new WaitForSeconds(10);
        EndCardEvent();
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

    public void UpdateCardEventUI()
    {
        cardCountDownText.text = (ClickerBehaviour.followers - followerStart) + "/" + (followersNeeded[currentCardEvent] * stageMultiplier) + " Followers" ;
    }
}
