using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameObject FocalPoint;
    public GameObject PowerupIndicator;
    public SpawnManager SpawnManager;
    public GameOver GameOverScreen;
    private Rigidbody PlayerRB;
    private float speed = 11.7f;
    public float PotionTime;
    public float strength = 26;
    public float powerupTime;
    private bool GameOver;
    private bool hasPowerUp;
    private bool hasPotion;
    public AudioSource audioSource;
    public AudioClip NormalHit;
    public AudioClip SpecialHit;
    public AudioClip potion_drink;
    public AudioClip Powerup_sfx;

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


        if(hasPotion){
            PotionTime -= Time.deltaTime;
            scaledown();
        }
        if(hasPowerUp){
            powerupTime -= Time.deltaTime;
            powerdown();
        }

        Gameover();
        
    }

    // If player collides with powerup, turns powerup feature ON
     private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PowerUp")){
            audioSource.PlayOneShot(Powerup_sfx,1);
            hasPowerUp = true;
            Destroy(other.gameObject);
            powerupTime = 7;
            PowerupIndicator.SetActive(true);
            if(hasPotion){
                PowerupIndicator.transform.localScale = new Vector3(8.58f,8.58f,8.58f);
            }
        }    
        if(other.CompareTag("Potion")){
            audioSource.PlayOneShot(potion_drink,1);
            if(hasPowerUp){
                PowerupIndicator.transform.localScale  = new Vector3(8.58f,8.58f,8.58f);
            }
            if(!hasPotion){
                transform.localScale *= 2.5f;
                Destroy(other.gameObject);
                PotionTime = 9;
                PlayerRB.mass = 6;
                speed = 35;
                hasPotion = true;   
            }
            else{
                Destroy(other.gameObject);
                PotionTime = 9;
            }
            
        }
    }


    // If player collides with enemy and has power up, shoot the enemy
    private void OnCollisionEnter(Collision other) {
        Rigidbody EnemyRB = other.gameObject.GetComponent<Rigidbody>();
        Vector3 AwayFromPlayer = other.gameObject.transform.position - transform.position;
        if(other.gameObject.CompareTag("Enemy") && hasPowerUp){   
            EnemyRB.AddForce(AwayFromPlayer * strength,ForceMode.Impulse);
            PowerupIndicator.SetActive(false);
            hasPowerUp = false;
            audioSource.PlayOneShot(SpecialHit,1);
            powerupTime = 0;

        } // If player collides with enemy and does not have powerups, shoot it with little power
        else if (other.gameObject.CompareTag("Enemy") && !hasPowerUp)
        {
            EnemyRB.AddForce(AwayFromPlayer * 1.8f,ForceMode.Impulse);
            audioSource.PlayOneShot(NormalHit,2);
        }
        else if(other.gameObject.CompareTag("Enemy") && hasPowerUp && hasPotion){
            EnemyRB.AddForce((AwayFromPlayer - new Vector3(0,1,0)) * 2500,ForceMode.Impulse);
            powerupTime = 0;
        }
        else if(other.gameObject.CompareTag("Enemy") && !hasPowerUp && hasPotion){
            EnemyRB.AddForce((AwayFromPlayer - new Vector3(0,1,0)) * 30,ForceMode.Impulse);
        }
    }

    // if powerup is activated then it deactivates after 7 seconds
    // IEnumerator PowerupCountdownRoutine(){
    //     yield return new WaitForSeconds(7);
    //     hasPowerUp = false;
    //     PowerupIndicator.SetActive(false);
    // }
    void powerdown(){
        if(powerupTime<=0){
            hasPowerUp = false;
            PowerupIndicator.SetActive(false);
        }
    }
    // IEnumerator scaleup(){
    //     yield return new WaitForSeconds(9);
    //     transform.localScale = new Vector3(1.5f,1.5f,1.5f);
    //     PlayerRB.mass = 2;
    //     speed = 15;
    //     PowerupIndicator.transform.localScale = new Vector3(3.3f,3.3f,3.3f);
    //     hasPotion = false;

    // }

    void scaledown(){
        if(PotionTime <= 0){
            transform.localScale = new Vector3(1.5f,1.5f,1.5f);
            PlayerRB.mass = 2;
            speed = 15;
            PowerupIndicator.transform.localScale = new Vector3(3.3f,3.3f,3.3f);
            hasPotion = false;
        }
    }

    public void Gameover(){
        if(transform.position.y <= -10){
            GameOverScreen.Setup(SpawnManager.WaveNumber);
            SpawnManager.waveText.text = "";
           
           } 
        }
    }


