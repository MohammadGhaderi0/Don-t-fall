using System.Collections;
using UnityEngine;
public class BallSpawner : MonoBehaviour
{

    private const int maxProduce = 25;

    private int ballCounter, shootSpeed;
    
    public GameObject smallBall,bigBall;

    public Vector3 forceDirection;
    
    public Material[] materials;

    
    
    private void Start()
    {
        StartCoroutine(SpawnBall());                               //for spawning balls in the main menu
    }
    

    IEnumerator SpawnBall()
    {
        while (ballCounter <=maxProduce)                                 // every ball station produces maxProduce balls
        {
            yield return new WaitForSeconds(1.2f);                       // the minimum time between producing two balls
            int ballType = Random.Range(0, 3);
            shootSpeed = Random.Range(0, 70);                            // random speed when balls are going to be shot
            int materialIndex = Random.Range(0, materials.Length);       // random material for every ball
            ballCounter++;
            GameObject ball = null;
            switch (ballType)
            {
                case 0:          // creates small ball
                    ball = Instantiate(smallBall, transform.position, Quaternion.identity);
                    break;
                case 1:         // creates big ball
                    ball = Instantiate(bigBall, transform.position, Quaternion.identity);
                    break;
            }

            if (ball != null)                       // it returns error if being null is not checked!
            {
                ball.GetComponent<MeshRenderer>().material = materials[materialIndex];
                ball.GetComponent<Rigidbody>().AddForce(forceDirection * shootSpeed, ForceMode.Impulse);
            }

            
            
        }
    }
}
