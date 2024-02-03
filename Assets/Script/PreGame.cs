using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PreGame : MonoBehaviour
{
    public static bool gameStarted {get; private set; }
    [SerializeField] GameObject titleObject;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        titleObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Starting in 2...";
        yield return new WaitForSeconds(1);
        titleObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Starting in 1...";
        yield return new WaitForSeconds(1);
        titleObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Go!!";
        yield return new WaitForSeconds(1);
        titleObject.SetActive(false);
        GetComponent<CountdownTimer>().StartClock();
        gameStarted = true;
    }

    public void End()
    {
        StartCoroutine(EndGame());
    }

    public IEnumerator EndGame()
    {
        gameStarted = false;
        titleObject.SetActive(true);
        titleObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Time's up!";
        PlayerPrefs.SetInt("Followers", ClickerBehaviour.followers);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("PreDebat");
    }
}
