using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 4;
 
    private GameObject player;
    
    private Rigidbody enemyRB;
    
    private List<GameObject> otherEnemies; // List to store references to other active enemy GameObjects
    
    public SpawnManager spawnManager;
    
    

    void Start()
    {
        spawnManager = GameObject.FindWithTag("SpawnManager")?.GetComponent<SpawnManager>();
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
        SetDirection();
        CheckDying();
    }

    void SetSpeed()
    {
        speed = transform.localScale == new Vector3(3, 3, 3) ? 20 : 6;
    }

    void SetDirection()
    {
        // Check if player is above a certain y-position
        if (player.transform.position.y > -1)
        {
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            // Find the closest active enemy
            foreach (GameObject enemy in otherEnemies)
            {
                if (enemy != null)
                {
                    float distanceToPlayer = Vector3.Distance(player.transform.position, enemy.transform.position);
                    if (distanceToPlayer < closestDistance)
                    {
                        closestEnemy = enemy;
                        closestDistance = distanceToPlayer;
                    }
                }
            }

            if (closestEnemy != null)
            {
                // Move towards the position between the player and the closest enemy
                Vector3 targetPosition = (player.transform.position + closestEnemy.transform.position) / 2;
                enemyRB.AddForce((targetPosition - transform.position).normalized * speed);
            }
            else
            {
                // If no other active enemy is closer, move towards the player
                enemyRB.AddForce((player.transform.position - transform.position).normalized * speed);
            }
        }
        else
        {
            // If player is below a certain y-position, move randomly
            enemyRB.AddForce((new Vector3(Random.Range(0, 3), 0, Random.Range(0, 3)) - transform.position).normalized *
                             speed);
        }
    }



    void CheckDying() // If enemy falls then dies.
    {
        if (transform.position.y < -10)
        {
            spawnManager.enemiesLeft--;
            Destroy(gameObject);
        }
    }



    
}
