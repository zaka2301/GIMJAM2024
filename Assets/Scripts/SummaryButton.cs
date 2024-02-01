using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SummaryButton : MonoBehaviour
{
    [SerializeField] Image blackScreen;

    [SerializeField] AudioSource musicPlayer;

    
    public void Retry()
    {
        SceneManager.LoadScene("DebatPhase", LoadSceneMode.Single);
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage") + 1);
        StartCoroutine(Cutscene());
        //SceneManager.LoadScene("", LoadSceneMode.Single);
    }

    private IEnumerator Cutscene()
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

    }
}
