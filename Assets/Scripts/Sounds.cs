using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    
    public PlayerController playerconteoller;
    public AudioSource audioSource;
    public bool gameoverSoundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameOverSound();   
    }
    void GameOverSound(){
        if(playerconteoller.transform.position.y <= -8){
            if(!gameoverSoundPlayed){
                audioSource.Play();
                gameoverSoundPlayed = true;
            }
        }
    }
}
