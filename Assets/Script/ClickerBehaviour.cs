using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerBehaviour : MonoBehaviour
{
    public static int followers {get; private set; }
    [SerializeField] TextMeshProUGUI followerText;
    [SerializeField] GameObject followerSlider;
    [SerializeField] GameObject popupPrefab;
    [SerializeField] int followerGain;
    [SerializeField] int maxFollowers;
    [SerializeField] float bonusChance;
    [SerializeField] int bonusGain;
    [SerializeField] float popupBloomRecovery;
    [SerializeField] float popupBloomGain;
    [SerializeField] float maxPopupBloom;
    [SerializeField] int nextUpgrade;

    private int upgrade = 1;
    public float popupBloom = 0.2f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) & PreGame.gameStarted)
        {
            float RNG = Random.Range(0f, 1f);
            followers += followerGain;
            if (popupBloom < maxPopupBloom)
            {
                popupBloom += popupBloomGain;
            }
            if (RNG < bonusChance)
            {
                followers += bonusGain;
                CreatePopup((Input.mousePosition * 10f / 1080f) - new Vector3(8.88f + Random.Range(-popupBloom, popupBloom), 5f + Random.Range(-popupBloom, popupBloom), 0f), "+" + (followerGain + bonusGain) + " Bonus");
            }
            else
            {
                CreatePopup((Input.mousePosition * 10f / 1080f) - new Vector3(8.88f + Random.Range(-popupBloom, popupBloom), 5f + Random.Range(-popupBloom, popupBloom), 0f), "+" + followerGain);
            }
            UpdateUI();
            if (followers >= nextUpgrade)
            {
                Upgrade();
            }
            if (GetComponent<CardEvent>().IsCardEvent)
            {
                GetComponent<CardEvent>().UpdateCardEventUI();
            }
        }
        if (popupBloom > 0.2f)
        {
            popupBloom -= popupBloomRecovery * Time.deltaTime;
        }
    }

    void UpdateUI()
    {
        followerText.text = "" + followers;
        followerSlider.GetComponent<Slider>().value = (float) followers / (float) maxFollowers;
    }

    void Upgrade()
    {
        upgrade++;
        nextUpgrade *= 2;
        followerGain++;
        CreatePopup(new Vector2(0, 0), "Setting Upgrade!");
    }

    void CreatePopup(Vector2 position, string text)
    {
        var popup = Instantiate(popupPrefab, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        temp.text = text;
        Destroy(popup, 0.5f);
    }
}
