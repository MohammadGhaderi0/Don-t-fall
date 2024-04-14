using UnityEngine;

public class YRotate : MonoBehaviour
{
    private float Yangle;

    [Range(1,10)]
    public int speed;

    void Update()
    {
        Yangle = (Yangle + speed) % 360; // Increment the rotation angle around the y-axis
        transform.localEulerAngles = new Vector3(0, Yangle, 0); // Set the object's rotation to the new value
    }
}