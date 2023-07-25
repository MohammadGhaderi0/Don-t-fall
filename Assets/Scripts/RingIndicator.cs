using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingIndicator : MonoBehaviour
{
    // Update is called once per frame
    void Update() {
        // Get the object's current rotation.
        Vector3 rotation = transform.localEulerAngles;

        // Set the x and z components of the rotation to 0.
        rotation.x = 0;
        rotation.z = 0;

        // Set the object's rotation to the new value.
        transform.localEulerAngles = rotation;
    }
}
