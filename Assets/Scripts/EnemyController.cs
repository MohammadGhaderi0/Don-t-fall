using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 4;
    private GameObject player;
    private Rigidbody enemyRB;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        setSpeed();
        // If the player is in the island then go for it . if not go to the center of the island
        if(player.transform.position.y >-1){ 
            enemyRB.AddForce((player.transform.position - transform.position).normalized * speed);
        }
        else{
            enemyRB.AddForce((new Vector3(Random.Range(0,3),0,Random.Range(0,3)) - transform.position).normalized * speed);
        }

        // If enemy falls then dies.
        if(transform.position.y<-10){
                Destroy(gameObject);
            }
        
    }
    void setSpeed(){
        if(transform.localScale == new Vector3(3,3,3)){
            speed = 20;
        }
        else{
            speed = 6;
        }
    }
}
