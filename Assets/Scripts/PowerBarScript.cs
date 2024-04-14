using UnityEngine;
using UnityEngine.UI;

public class PowerBarScript : MonoBehaviour
{
    public Slider slider;
    public PlayerController playerController;
    
    void Update()
    {
        slider.value  = playerController.powerupTime;
        if(slider.value > 0.1f){
            transform.localScale = Vector3.one;
        }
        else{
           transform.localScale = Vector3.zero;
           
        }

        
    }



}
