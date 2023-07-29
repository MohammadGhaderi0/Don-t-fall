using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionBarScript : MonoBehaviour
{
    public Slider Potion_slider;
    public PlayerController playercontroller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Potion_slider.value  = playercontroller.PotionTime;
        if(Potion_slider.value > 0.1f){
            transform.localScale = Vector3.one;
        }
        else{
           transform.localScale = Vector3.zero;
        }

        }
        
        
    }

