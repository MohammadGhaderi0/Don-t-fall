using UnityEngine;
using UnityEngine.UI;

public class PowerBarScript : MonoBehaviour
{
    public Slider slider;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
