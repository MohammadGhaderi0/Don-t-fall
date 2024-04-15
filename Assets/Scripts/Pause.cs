
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public GameObject pauseUI;
    
    private bool paused;

    private bool settingIsOpened;

    public GameObject settingUI;
    
    public Slider volume;

    public Slider sensitivity;

    public BaseSoundController soundController;

    public InputHandler input;

    public PlayerController playerController;

    public SpawnManager spawnManager;
    

    public void PauseAndUnpause()
    {
        if (paused) // unpause the game
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
            paused = false;
            playerController.ActiveDeActivePlayerKinematic();
            spawnManager.ActiveDeactiveEnemiesKinematic();
            
            
        }
        else //pause the game
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
            paused = true;
            playerController.ActiveDeActivePlayerKinematic();
            spawnManager.ActiveDeactiveEnemiesKinematic();


        }
        

    }
    

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenCloseSettings()
    {
        if (!settingIsOpened)
        {
            settingUI.SetActive(true);
            pauseUI.SetActive(false);
            settingIsOpened = true;
            ApplySettings();
        }
        else
        {
            settingIsOpened = false;
            PlayerPrefs.SetFloat("volume",volume.value);
            PlayerPrefs.SetFloat("sensitivity",sensitivity.value);
            soundController.SetVolume(volume.value / 100);
            input.RotationSpeed = volume.value + 40;
            settingUI.SetActive(false);
            pauseUI.SetActive(true);
        }
    }

    public void ApplySettings()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            volume.value = PlayerPrefs.GetFloat("volume");
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
    
    

    
    
}
