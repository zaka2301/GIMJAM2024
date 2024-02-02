using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public string scene;
    public GameObject PauseMenu;
    public void Play()
    {
        PlayerPrefs.SetInt("Stage", 1);
        DontDestroyOnLoad(PauseMenu);
        SceneManager.LoadScene(scene);
    }
}
