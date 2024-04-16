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
        BaseSoundController.Instance.PlaySoundByIndex(0,new Vector3(0,1,-10));
        Application.Quit();
    }

    public void UpdateSettings()          // updating setting when opening settings in main menu
    {
        BaseSoundController.Instance.PlaySoundByIndex(0,new Vector3(0,1,-10));
        setting.GetSettingsData(volume,sensitivity);
    }

    public void setSettings()           // saving settings when leaving settings in main menu
    {
        setting.SetSettings(volume,sensitivity);
        music.SetVolume();
        
    }
    
    
    
}
