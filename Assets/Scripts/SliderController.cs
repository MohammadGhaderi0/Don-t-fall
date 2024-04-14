using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private Slider slider;
    public PlayerController playerController;
    public float valueThreshold = 0.1f;

    private void Start()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
        {
            Debug.LogError("Slider component not found on GameObject: " + gameObject.name);
            return;
        }
    }

    void Update()
    {
        slider.value = GetValueFromPlayerController();
        UpdateSliderVisibility();
    }

    private float GetValueFromPlayerController()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController reference not set on SliderController script attached to: " + gameObject.name);
            return 0f;
        }

        return GetValue();
    }

    protected virtual float GetValue()
    {
        return 0f; // Implement this in child classes
    }

    private void UpdateSliderVisibility()
    {
        transform.localScale = slider.value > valueThreshold ? Vector3.one : Vector3.zero;
    }
}