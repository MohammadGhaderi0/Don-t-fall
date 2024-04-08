
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        
    }

    public void DisablePause()
    {
        
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
    
}
