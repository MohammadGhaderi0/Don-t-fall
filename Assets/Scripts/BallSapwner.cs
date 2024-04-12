using System.Collections;
using UnityEngine;
public class BallSapwner : MonoBehaviour
{

    private const int maxProduce = 25;

    private int ballCounter;
    
    public GameObject smallBall;
    
    public GameObject bigBall;

    public Vector3 forceDirection;

    private int power;

    public Material[] materials;

    
    
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnBall());
    }
    

    IEnumerator SpawnBall()
    {
        while (ballCounter <=maxProduce)
        {
            yield return new WaitForSeconds(1.2f);
            int randNum = Random.Range(0, 3);
            power = Random.Range(0, 70);
            int randMat = Random.Range(0, materials.Length);
            switch (randNum)
            {
                case 0:  // spawning small ball
                    GameObject SBall = Instantiate(smallBall, transform.position, Quaternion.identity);
                    SBall.GetComponent<MeshRenderer>().material = materials[randMat];
                    SBall.GetComponent<Rigidbody>().AddForce(forceDirection * power,ForceMode.Impulse);
                    ballCounter++;
                    break;
                case 1: // spawning big ball
                    GameObject XBall = Instantiate(bigBall, transform.position, Quaternion.identity);
                    XBall.GetComponent<MeshRenderer>().material = materials[randMat];
                    XBall.GetComponent<Rigidbody>().AddForce(forceDirection * power,ForceMode.Impulse);
                    ballCounter++;
                    break;
                case 2:  // nothing happens
                    break;
                    
            }
            
            
        }
    }
}
