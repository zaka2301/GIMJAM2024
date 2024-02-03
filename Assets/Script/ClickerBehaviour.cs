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
    private int stageMultiplier;

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

    [SerializeField] AudioClip tapSound;
    [SerializeField] AudioClip milestoneSound;

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
        currentUpgrade = 0;
        stageMultiplier = (int) Mathf.Pow(2, PlayerPrefs.GetInt("Stage", 1) - 1);
        UpdateUI();
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
            UpdateChat();
            GetComponent<AudioSource>().PlayOneShot(tapSound, 1f);
        }
        if (popupBloom > 0f)
        {
            popupBloom -= popupBloomRecovery * Time.deltaTime;
        }
    }

    void GainFollowers()
    {
        followers += followerGain * stageMultiplier;
        float RNG = Random.Range(0f, 1f);
        if (RNG < bonusChance)
            {
                followers += bonusGain * stageMultiplier;
                CreatePopup(popupPrefab, (Input.mousePosition * 10f / 1080f) - new Vector3(8.88f + Random.Range(-popupBloom, popupBloom), 5f + Random.Range(-popupBloom, popupBloom), 0f), "+" + ((followerGain + bonusGain) * stageMultiplier) + " Bonus");
            }
            else
            {
                CreatePopup(popupPrefab, (Input.mousePosition * 10f / 1080f) - new Vector3(8.88f + Random.Range(-popupBloom, popupBloom), 5f + Random.Range(-popupBloom, popupBloom), 0f), "+" + (followerGain * stageMultiplier));
            }
    }

    void UpdateUI()
    {
        followerText.text = "" + followers;
        followerSlider.GetComponent<Slider>().value = (float) followers / (float) (maxFollowers*stageMultiplier);
        if (CardEvent.IsCardEvent)
        {
            GetComponent<CardEvent>().UpdateCardEventUI();
        }
    }

    void UpdateChat()
    {
        if (StageManager.stage == 3)
        {
            for (int i = 0; i < currentUpgrade + 1; i++)
            {
                GameObject.FindWithTag("StageManager").GetComponent<StageManager>().AddChat();
            }
        }
    }

    void CheckForUpgrade()
    {
        if (currentUpgrade < (maxUpgrade - 1))
        {
            if (followers - startingFollowers >= upgradeMilestone[currentUpgrade] * stageMultiplier)
            {
                currentUpgrade++;
                Upgrade();
            }
        }
    }

    void Upgrade()
    {
        followerGain++;
        GetComponent<AudioSource>().PlayOneShot(milestoneSound, 1f);
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
