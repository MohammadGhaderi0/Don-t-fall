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
    

    public void PauseAndUnpause()            // handling the pause
    {
        if (paused) // unpause the game
        {
            BaseSoundController.Instance.PlaySoundByIndex(0,new Vector3(0,0,0));
            Time.timeScale = 1;
            pauseUI.SetActive(false);
            paused = false;
            playerController.ActiveDeActivePlayerKinematic();             // we activate and disactivate kinematic in pause, because it causes weird movements 
            spawnManager.ActiveDeactiveEnemiesKinematic();                // in balls after changing sensitivity
            
            
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
        BaseSoundController.Instance.PlaySoundByIndex(0,new Vector3(0,0,0));
        SceneManager.LoadScene(0);
    }

    public void OpenCloseSettings()
    {
        BaseSoundController.Instance.PlaySoundByIndex(0,new Vector3(0,0,0));
        if (!settingIsOpened)
        {
            settingUI.SetActive(true);
            pauseUI.SetActive(false);
            settingIsOpened = true;
            setting.GetSettingsData(volume,sensitivity);
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
