using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolumeMusic()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        //volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        float loadedVolume = PlayerPrefs.GetFloat("musicVolume");
        volumeSlider.value = loadedVolume;
        AudioListener.volume = loadedVolume; // Đồng bộ âm lượng
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        //PlayerPrefs.Save();
    }

    public void SetVolume(float value)
    {
        volumeSlider.value = value; // Cập nhật Slider
        AudioListener.volume = value; // Cập nhật âm lượng
    }
}
