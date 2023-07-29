using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bar : MonoBehaviour
{
    public Slider slider;
    private float PowerUp_time;
    public PlayerController playerController;
    public GameObject powerupUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value  = playerController.powerupTime;
        if(slider.value > 0.1f){
            powerupUI.transform.localScale = Vector3.one;
        }
        else{
           powerupUI.transform.localScale = Vector3.zero;
           
        }
        
        
    }
}
