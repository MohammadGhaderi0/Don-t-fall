using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 15;
    private Rigidbody PlayerRB;
    private GameObject FocalPoint;
    private bool hasPowerUp;
    public float strength = 10;
    public GameObject PowerupIndicator;
    public AudioClip NormalHit;
    public AudioClip SpecialHit;
    public AudioSource audioSource;
    private bool GameOver;
    private bool hasPotion;
  

    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        PlayerRB = GetComponent<Rigidbody>();
        FocalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        // To move forward the camera
        float forwardInput = Input.GetAxis("Vertical");
        PlayerRB.AddForce(FocalPoint.transform.forward * speed * forwardInput);

        // updating the Transform of powerup ring 
        PowerupIndicator.transform.position = transform.position -new Vector3(0,0.45f,0);
        
    }

    // If player collides with powerup, turns powerup feature ON
     private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PowerUp")){
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            PowerupIndicator.SetActive(true);
            if(hasPotion){
                PowerupIndicator.transform.localScale *= 2.6f;
            }
        }    
        if(other.CompareTag("Potion")){
            if(hasPowerUp){
                PowerupIndicator.transform.localScale *= 2.6f;
            }
            if(!hasPotion){
                transform.localScale *= 2.5f;
                Destroy(other.gameObject);
                StartCoroutine(scaleup());
                PlayerRB.mass = 6;
                speed = 35;
                hasPotion = true;   
            }
            else{
                Destroy(other.gameObject);
                StartCoroutine(scaleup());
            }
            
        }
    }


    // If player collides with enemy and has power up, shoot the enemy
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Enemy") && hasPowerUp){
            Rigidbody EnemyRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 AwayFromPlayer = other.gameObject.transform.position - transform.position;
            EnemyRB.AddForce(AwayFromPlayer * strength,ForceMode.Impulse);
            PowerupIndicator.SetActive(false);
            hasPowerUp = false;
            audioSource.PlayOneShot(SpecialHit,1);

        } // If player collides with enemy and does not have powerups, shoot it with little power
        else if (other.gameObject.CompareTag("Enemy") && !hasPowerUp)
        {
            Rigidbody EnemyRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 AwayFromPlayer = other.gameObject.transform.position - transform.position;
            EnemyRB.AddForce(AwayFromPlayer * 1.7f,ForceMode.Impulse);
            audioSource.PlayOneShot(NormalHit,2);
        }
        else if(other.gameObject.CompareTag("Enemy") && hasPowerUp && hasPotion){
            Rigidbody EnemyRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 AwayFromPlayer = other.gameObject.transform.position - transform.position;
            EnemyRB.AddForce(AwayFromPlayer * 20,ForceMode.Impulse);

        }
    }

    // if powerup is activated then it deactivates after 7 seconds
    IEnumerator PowerupCountdownRoutine(){
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        PowerupIndicator.SetActive(false);
    }
    IEnumerator scaleup(){
        yield return new WaitForSeconds(9);
        transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        PlayerRB.mass = 2;
        speed = 15;
        PowerupIndicator.transform.localScale = new Vector3(3.3f,3.3f,3.3f);
        hasPotion = false;

    }

}
