using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Application = UnityEngine.Device.Application;

public class MainMenu : MonoBehaviour
{
    
    public Slider volume;

    public Slider sensitivity;
    
    public Settings setting;

    public MusicController music;

    public void PlayGame()    // This function called when the start button pressed
    {
        BaseSoundController.Instance.PlaySoundByIndex(0,new Vector3(0,1,-10));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void UpdateSettings()
    {
        setting.ApplySettings(volume,sensitivity);
    }

    public void setSettings()
    {
        setting.SetSettings(volume,sensitivity);
        music.SetVolume();
        
    }
    
    
    
}
