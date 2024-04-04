using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 4;
    private GameObject player;
    private Rigidbody enemyRB;
    private List<GameObject> otherEnemies; // List to store references to other active enemy GameObjects
    
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        // Find and store references to other active enemy GameObjects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        otherEnemies = new List<GameObject>(enemies);
        otherEnemies.Remove(gameObject); // Remove self from the list
    }

    void Update()
    {
        SetSpeed();
        // SetDirection();
        CheckDying();
    }

    void SetSpeed()
    {
        speed = transform.localScale == new Vector3(3, 3, 3) ? 20 : 6;
    }

    void SetDirection()
    {
        if (player.transform.position.y > -1)
        {
            // Check if any other active enemy is closer to the player
            foreach (GameObject enemy in otherEnemies.ToArray()) // Convert to array to avoid concurrent modification
            {
                if (enemy == null)
                {
                    otherEnemies.Remove(enemy); // Remove destroyed enemy from the list
                }
                if (Vector3.Distance(player.transform.position, enemy.transform.position) < Vector3.Distance(player.transform.position, transform.position))
                {
                    // Move towards the position between the player and the closest enemy
                    Vector3 targetPosition = (player.transform.position + enemy.transform.position) / 2;
                    enemyRB.AddForce((targetPosition - transform.position).normalized * speed);
                    return; // Exit the loop once a closer enemy is found
                }
            }
            // If no other active enemy is closer, move towards the player
            enemyRB.AddForce((player.transform.position - transform.position).normalized * speed);
        }
        else
        {
            enemyRB.AddForce((new Vector3(Random.Range(0, 3), 0, Random.Range(0, 3)) - transform.position).normalized * speed);
        }
    }

    void CheckDying()            // If enemy falls then dies.
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
    
    
    
}
