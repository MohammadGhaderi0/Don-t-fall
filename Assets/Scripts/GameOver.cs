using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI pointsTXT;
    public AudioSource audioSource;


    public void ShowGameInfo(int score)
    {
        gameObject.SetActive(true);
        pointsTXT.text = (score-1).ToString() + " waves survived";
    }

    public void RestartBTN(){          // This function called when restart button pressed
        audioSource.Play();
        SceneManager.LoadScene(1);
    }

    public void MainMenu(){            // This function called when main menu button pressed
        audioSource.Play();
        SceneManager.LoadScene(0);
    }


}
