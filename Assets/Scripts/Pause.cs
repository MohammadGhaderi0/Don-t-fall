
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public GameObject pauseUI;
    
    public  bool paused;

    private bool settingIsOpened;

    public GameObject settingUI;
    
    public Slider volume;

    public Slider sensitivity;

    public BaseSoundController soundController;

    public InputHandler input;

    public PlayerController playerController;

    public SpawnManager spawnManager;

    public Settings setting;
    

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
            setting.ApplySettings(volume,sensitivity);
        }
        else
        {
            setting.SetSettings(volume,sensitivity);
            settingIsOpened = false;
            settingUI.SetActive(false);
            pauseUI.SetActive(true);
        }
    }

    
    
    

    
    
}
