
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseAndUnpause()
    {
        if (paused) // unpause the game
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
            paused = false;
        }
        else //pause the game
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
            paused = true;
        }
        

    }
    

    public void ExitGame()
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
    
}
