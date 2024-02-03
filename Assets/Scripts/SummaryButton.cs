using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SummaryButton : MonoBehaviour
{
    [SerializeField] Image blackScreen;

    [SerializeField] AudioSource musicPlayer;


    void Start()
    {
        StartCoroutine(UnBlackScreen());
    }
    
    public void Retry()
    {

        StartCoroutine(LoadScene("Dialogues"));
    }

    public void Continue()
    {
        int s = PlayerPrefs.GetInt("Stage");
        int win = PlayerPrefs.GetInt("Win");
        PlayerPrefs.SetInt("Stage", s + 1);
        PlayerPrefs.SetInt("Win", win + (GameLogic.hasWon ? 1 : 0));
        PlayerPrefs.Save();
        StartCoroutine(LoadScene("Dialogues"));
        //SceneManager.LoadScene("", LoadSceneMode.Single);
    }

    public void skip()
    {
        int s = PlayerPrefs.GetInt("Stage");
        int win = PlayerPrefs.GetInt("Win");
        PlayerPrefs.SetInt("Stage", s + 1);
        PlayerPrefs.SetString("Cutscene", s == 5 ? (win >= 3 ? "EndWin" : "EndLose") : "DebatWin");
        PlayerPrefs.SetInt("Win", win + 1);
        PlayerPrefs.Save();
        StartCoroutine(LoadScene("Dialogues"));       
    }

    private IEnumerator LoadScene(string scene)
    {
        float a = 0.0f;
        while(a <= 1.0f)
        {
            musicPlayer.volume = 1.0f - a;
            blackScreen.color = new Color(0.0f,0.0f,0.0f, a);
            a += Time.deltaTime;
            yield return null;
        }
        
        //loadscene
        SceneManager.LoadScene(scene);
    }

    private IEnumerator UnBlackScreen()
    {
        float a = 1.0f;
        while(a > 0.0f)
        {

            blackScreen.color = new Color(0.0f,0.0f,0.0f, a);
            a -= Time.deltaTime;
            yield return null;
        }
    }
}
