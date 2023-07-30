using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI pointsTXT;
    public AudioSource audioSource;


    public void Setup(int score){
        gameObject.SetActive(true);
        pointsTXT.text = (score-1).ToString() + " waves survived";
    }

    public void RestartBTN(){
        audioSource.Play();
        SceneManager.LoadScene(1);
    }

    public void MainMenu(){
        audioSource.Play();
        SceneManager.LoadScene(0);
    }


}
