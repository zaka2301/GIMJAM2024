using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SummaryButton : MonoBehaviour
{
    [SerializeField] Image blackScreen;

    
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
        Debug.Log("cutscene");


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
