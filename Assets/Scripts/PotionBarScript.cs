using UnityEngine;
using UnityEngine.UI;

public class PotionBarScript : MonoBehaviour
{
    private Slider potionSlider;
    
    public PlayerController playerController;

    private GameObject potionUI;


    private void Start()
    {
        potionSlider = GetComponent<Slider>();
        potionUI = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        PotionSliderController();
    }

    private void PotionSliderController()
    {
        if (playerController.hasPotion)
        { 
            potionUI.SetActive(potionSlider.value > 0.1f);
            potionSlider.value = playerController.potionTime;
        }
    }

}

