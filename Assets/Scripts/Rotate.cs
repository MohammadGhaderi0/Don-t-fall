using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float Yangle = 0;

    // Update is called once per frame
    void Update()
    {
        Yangle = (Yangle+2) % 360;
        transform.eulerAngles = new Vector3(transform.position.x, Yangle,transform.position.z);
    }
}
