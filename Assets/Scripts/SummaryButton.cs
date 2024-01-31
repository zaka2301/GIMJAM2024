using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SummaryButton : MonoBehaviour
{
    [SerializeField] Image blackScreen;
    [SerializeField] GameObject cutscene;
    [SerializeField] Sprite win;
    [SerializeField] Sprite lose;
    [SerializeField] AudioClip winSFX;
    [SerializeField] AudioClip loseSFX;

    [SerializeField] AudioSource musicPlayer;

    
    public void Retry()
    {
        SceneManager.LoadScene("DebatPhase", LoadSceneMode.Single);
    }

    public void Continue()
    {
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
        //load cutscne
        if(GameLogic.playerHealth >= GameLogic.enemyHealth){
            cutscene.GetComponent<Image>().sprite = win;
            cutscene.GetComponent<AudioSource>().clip = winSFX;
        }
        else
        {
            cutscene.GetComponent<Image>().sprite = lose;
            cutscene.GetComponent<AudioSource>().clip = loseSFX;
        }
        cutscene.SetActive(true);


        while(a >= 0.0f)
        {
            blackScreen.color = new Color(0.0f,0.0f,0.0f, a);
            a -= Time.deltaTime;
            yield return null;
        }
        while(!Input.anyKey)
        {
            yield return null;
        }

        while(a <= 1.0f)
        {
            blackScreen.color = new Color(0.0f,0.0f,0.0f, a);
            a += Time.deltaTime;
            yield return null;
        }

        //loadscene

    }
}
