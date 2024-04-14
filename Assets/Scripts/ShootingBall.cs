using System.Collections;
using UnityEngine;

public class ShootingBall : MonoBehaviour
{
    public GameObject[] rayStation;

    public GameObject[] shootingBall;

    private bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shootBall());
    }
    
    IEnumerator shootBall()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Random.Range(0,2) == 1 && !isShooting)
            {
                int ran = Random.Range(0, 3);
                initializeShooting(ran);
                isShooting = true;
            }
        }
    }
    
    public void initializeShooting(int index)
    {
        StartCoroutine(ShootSequence(index));
    }

    IEnumerator ShootSequence(int index)
    {
        Vector3 firstPosition = shootingBall[index].transform.position;

        // Activate the ray station
        rayStation[index].SetActive(true);
        yield return new WaitForSeconds(2); // Wait for 2 seconds

        // Deactivate the ray station
        rayStation[index].SetActive(false);
        // yield return new WaitForSeconds(2); // Wait for 2 seconds

        // Activate the shooting ball
        shootingBall[index].SetActive(true);
        yield return new WaitForSeconds(6); // Wait for 4 seconds

        // Deactivate the shooting ball
        shootingBall[index].SetActive(false);

        // Reset the position of the shooting ball
        shootingBall[index].transform.position = firstPosition;

        // Set isShooting back to false
        isShooting = false;
    }



    IEnumerator PauseSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}