using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsManager : MonoBehaviour {

    // Use this for initialization
    public Slider masterVolume;
    public Slider sfxVolume;
    public Slider musicVolume;
    void Start () {
        if(PlayerPrefs.HasKey("master volume"))
        {
            masterVolume.value = PlayerPrefs.GetInt("master volume");
        }
        else
        {
            masterVolume.value = 100;
            PlayerPrefs.SetInt("master volume", 100);
        }
        if(PlayerPrefs.HasKey("sfx volume"))
        {
            sfxVolume.value = PlayerPrefs.GetInt("sfx volume");
        }
        else
        {
            sfxVolume.value = 100;
            PlayerPrefs.SetInt("sfx volume", 100);
        }
        if (PlayerPrefs.HasKey("music volume"))
        {
            musicVolume.value = PlayerPrefs.GetInt("music volume");
        }
        else
        {
            musicVolume.value = 100;
            PlayerPrefs.SetInt("music volume", 100);
        }

        masterVolume.onValueChanged.AddListener(delegate { changeVolume("master volume",masterVolume); });
        sfxVolume.onValueChanged.AddListener(delegate { changeVolume("sfx volume",sfxVolume); });
        musicVolume.onValueChanged.AddListener(delegate { changeVolume("music volume",musicVolume); });
    }
    private void changeVolume(string key,Slider slider)
    {
        PlayerPrefs.SetInt(key, (int)slider.value);
    }

}
