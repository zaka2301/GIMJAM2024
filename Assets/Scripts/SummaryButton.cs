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
            blackScreen.color = new Color(0.0f,0.0f,0.0f, a);
            a += Time.deltaTime;
            yield return null;
        }
        //load cutscne
        cutscene.GetComponent<Image>().sprite = win;
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
