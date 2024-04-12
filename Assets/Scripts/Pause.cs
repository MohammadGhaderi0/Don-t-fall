
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public GameObject pausePanel;
    
    private bool paused;

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
            pausePanel.SetActive(false);
            paused = false;
        }
        else //pause the game
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            paused = true;
        }
        

    }
    

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
    
}
