using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Device.Application;

public class MainMenu : MonoBehaviour
{
    public AudioSource audiosource;
    public void PlayGame()    // This function called when the start button pressed
    {
        audiosource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
