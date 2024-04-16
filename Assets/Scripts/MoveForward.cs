using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public Rigidbody ballRB;

    private float speed = 31;
    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ballRB.AddForce(transform.forward * speed);
    }
}
