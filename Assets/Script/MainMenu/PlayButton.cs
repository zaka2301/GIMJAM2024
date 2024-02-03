using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public string scene;
    public GameObject PauseMenu;
    [SerializeField] CoverScreen screen;

    public void PlayButtonFunc()
    {
        StartCoroutine(Play());
    }
    public IEnumerator Play()
    {
        StartCoroutine(screen.Fade());
        yield return new WaitForSeconds(1.0f);
        int stage = PlayerPrefs.GetInt("Stage", 0);
        if(stage == 0)
        {
            PlayerPrefs.SetInt("Win", 0);
            PlayerPrefs.SetInt("Stage", 1);
            PlayerPrefs.SetInt("Followers", 0);
            PlayerPrefs.SetString("Cutscene", "Opening");
        }
        DontDestroyOnLoad(PauseMenu);
        SceneManager.LoadScene(scene);
    }
}
