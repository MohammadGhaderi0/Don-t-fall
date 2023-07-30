using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI pointsTXT;


    public void Setup(int score){
        gameObject.SetActive(true);
        pointsTXT.text = (score-1).ToString() + " waves survived";
    }

    public void RestartBTN(){
        SceneManager.LoadScene(1);
    }

    public void MainMenu(){
        SceneManager.LoadScene(0);
    }


}
