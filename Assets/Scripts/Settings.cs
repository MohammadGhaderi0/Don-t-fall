using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public void ApplySettings(Slider volume,Slider sensitivity)
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            volume.value = PlayerPrefs.GetFloat("volume") * 100 ;
        }
        else
        {
            volume.value = 70;
        }
        if (PlayerPrefs.HasKey("sensitivity"))
        {
            sensitivity.value = PlayerPrefs.GetFloat("sensitivity");
        }
        else
        {
            sensitivity.value = 70;
        }


    }

    public void SetSettings(Slider volume,Slider sensitivity)
    {
        PlayerPrefs.SetFloat("volume",volume.value /100);
        PlayerPrefs.SetFloat("sensitivity",sensitivity.value);
        BaseSoundController.Instance.SetVolume(PlayerPrefs.GetFloat("volume"));
    }


}
