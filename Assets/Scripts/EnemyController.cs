using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 5;
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
        // If the player is in the island then go for it . if not go to the center of the island
        if(player.transform.position.y >-1){ 
            enemyRB.AddForce((player.transform.position - transform.position).normalized * speed);
        }
        else{
            enemyRB.AddForce((new Vector3(0,0,0) - transform.position).normalized * speed);
        }

        // If enemy falls then dies.
        if(transform.position.y<-10){
                Destroy(gameObject);
            }
        
    }
}
