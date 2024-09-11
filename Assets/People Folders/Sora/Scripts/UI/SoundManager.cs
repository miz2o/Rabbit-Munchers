using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadVol();
        }

        else
        {
            LoadVol();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVol();
    }

    private void LoadVol()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void SaveVol()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
