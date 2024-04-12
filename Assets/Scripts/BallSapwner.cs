using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSapwner : MonoBehaviour
{

    public GameObject smallBall;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnBall()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
            
        }
    }
}
