using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummaryButton : MonoBehaviour
{
    
    public void Retry()
    {
        SceneManager.LoadScene("DebatPhase", LoadSceneMode.Single);
    }

    public void Continue()
    {
        Debug.Log("lanjoot");
        //SceneManager.LoadScene("", LoadSceneMode.Single);
    }
}
