using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI pointsTXT;
    
    public TextMeshProUGUI recordTXT;
    


    public void ShowGameInfo(int score)     // showing record and waves survived
    {
        gameObject.SetActive(true);
        pointsTXT.text = (score-1) + " waves survived";
        SubmitRecord(score);
    }

    private void SubmitRecord(int score)                        // for saving record in playerPrefs 
    {
        if (PlayerPrefs.HasKey("record"))
        {
            if (PlayerPrefs.GetInt("record") > (score - 1))
            {
                recordTXT.text = "Best Record : " + PlayerPrefs.GetInt("record") + " waves";
            }
            else
            {
                recordTXT.text = "Best Record : " + (score-1) + " waves";
                PlayerPrefs.SetInt("record",score-1);
            }
        }
        else
        {
            recordTXT.text = "Best Record : " + (score-1) + " waves";
            PlayerPrefs.SetInt("record",score-1);
        }
    }
    
    
    public void RestartBTN(){          // This function called when restart button pressed
        BaseSoundController.Instance.PlaySoundByIndex(0, new Vector3(0,0,0));
        SceneManager.LoadScene(1);
    }

    public void MainMenu(){            // This function called when main menu button pressed
        BaseSoundController.Instance.PlaySoundByIndex(0,new Vector3(0,0,0));
        SceneManager.LoadScene(0);
    }


}
