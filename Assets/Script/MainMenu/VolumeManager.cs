using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] Slider MasterVolumeSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            PlayerPrefs.SetFloat("MasterVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeMasterVolume()
    {
        AudioListener.volume = MasterVolumeSlider.value;
    }
    public void Load()
    {
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterVolumeSlider.value);
    }
}
