using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager> 
{
    public AudioSource[] backgroundMusic;
    public AudioSource[] oneshotMusic;

    public void PlayBackgroundMusic(SoundList sound)
    {
        if (sound == SoundList.BackgroundMainMenu)
        {
            backgroundMusic[1].Stop();
            backgroundMusic[0].Play();
        }
        else if (sound == SoundList.BackgroundInGame)
        {
            backgroundMusic[0].Stop();
            backgroundMusic[1].Play();
        }
    }

    public void PlayOneShotMusic(SoundList sound)
    {
        if (sound == SoundList.ButtonClick)
        {
            oneshotMusic[0].Play();
        }
        else if (sound == SoundList.Shot)
        {
            oneshotMusic[1].Play();
        }
        else if (sound == SoundList.Dead)
        {
            oneshotMusic[2].Play();
        }
        else if (sound == SoundList.Win)
        {
            oneshotMusic[3].Play();
        }
    }
}

public enum SoundList
{
    BackgroundMainMenu,
    BackgroundInGame,
    ButtonClick,
    Shot,
    Dead,
    Win
}
