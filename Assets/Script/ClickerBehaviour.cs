using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI followerText;
    [SerializeField] GameObject followerSlider;
    [SerializeField] GameObject popupPrefab;
    [SerializeField] GameObject popupPrefab2;
    [SerializeField] SpriteRenderer backroundRenderer;
    [SerializeField] Sprite[] backgroundSprites1;
    [SerializeField] Sprite[] backgroundSprites2;
    [SerializeField] Sprite[] backgroundSprites4;
    [SerializeField] Sprite[] backgroundSprites5;

    public static int followers {get; private set; }
    [SerializeField] int maxFollowers;
    private int startingFollowers;

    [SerializeField] int followerGain;
    [SerializeField] float bonusChance;
    [SerializeField] int bonusGain;

    private float popupBloom = 0.2f;
    [SerializeField] float maxPopupBloom;
    [SerializeField] float popupBloomGain;
    [SerializeField] float popupBloomRecovery;

    public static int currentUpgrade {get; private set;}
    [SerializeField] int maxUpgrade;
    [SerializeField] int[] upgradeMilestone;

    void Start()
    {
        if (PlayerPrefs.GetInt("Stage", 1) != 1)
        {
            followers = PlayerPrefs.GetInt("Followers", 0);
        }
        else
        {
            followers = 0;
        }
        UpdateUI();
        currentUpgrade = 0;
        startingFollowers = followers;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) & PreGame.gameStarted & Time.timeScale != 0f)
        {
            if (popupBloom < maxPopupBloom)
            {
                popupBloom += popupBloomGain;
            }
            GainFollowers();
            CheckForUpgrade();
            UpdateUI();
        }
        if (popupBloom > 0f)
        {
            popupBloom -= popupBloomRecovery * Time.deltaTime;
        }
    }

    void GainFollowers()
    {
        followers += followerGain;
        float RNG = Random.Range(0f, 1f);
        if (RNG < bonusChance)
            {
                followers += bonusGain;
                CreatePopup(popupPrefab, (Input.mousePosition * 10f / 1080f) - new Vector3(8.88f + Random.Range(-popupBloom, popupBloom), 5f + Random.Range(-popupBloom, popupBloom), 0f), "+" + (followerGain + bonusGain) + " Bonus");
            }
            else
            {
                CreatePopup(popupPrefab, (Input.mousePosition * 10f / 1080f) - new Vector3(8.88f + Random.Range(-popupBloom, popupBloom), 5f + Random.Range(-popupBloom, popupBloom), 0f), "+" + followerGain);
            }
    }

    void UpdateUI()
    {
        followerText.text = "" + followers;
        followerSlider.GetComponent<Slider>().value = (float) followers / (float) maxFollowers;
        if (CardEvent.IsCardEvent)
        {
            GetComponent<CardEvent>().UpdateCardEventUI();
        }
    }

    void CheckForUpgrade()
    {
        Debug.Log("Ceccking " + currentUpgrade + " " + maxUpgrade);
        if (currentUpgrade < (maxUpgrade - 1))
        {
            Debug.Log("available" + startingFollowers + " " + upgradeMilestone[currentUpgrade]);
            if (followers - startingFollowers >= upgradeMilestone[currentUpgrade])
            {
                currentUpgrade++;
                Upgrade();
            }
        }
    }

    void Upgrade()
    {
        followerGain++;
        int stage = PlayerPrefs.GetInt("Stage", 1);
        switch (stage)
        {
            case 1:
                backroundRenderer.sprite = backgroundSprites1[currentUpgrade-1];
                break;
            case 2:
                backroundRenderer.sprite = backgroundSprites2[currentUpgrade-1];
                break;
            case 3:
                break;
            case 4:
                backroundRenderer.sprite = backgroundSprites4[currentUpgrade-1];
                break;
            case 5:
                backroundRenderer.sprite = backgroundSprites5[currentUpgrade-1];
                break;
        }
            
        CreatePopup(popupPrefab2, new Vector2(-4.5f, 0), "Setting Upgrade!");
    }

    void CreatePopup(GameObject chosenPrefab, Vector2 position, string text)
    {
        var popup = Instantiate(chosenPrefab, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        temp.text = text;
        Destroy(popup, 0.5f);
    }
}
