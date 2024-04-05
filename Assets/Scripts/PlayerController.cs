using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject FocalPoint, powerUpIndicator;
    
    public SpawnManager spawnManager;
    
    public GameOver GameOverScreen;
    
    private Rigidbody PlayerRB;
    
    // private float speed = 11.7f;
    
    public float strength = 26;
    
    public float potionTime, powerupTime;
    
    public bool gameOver, hasPowerUp,hasPotion;
    
    public AudioSource audioSource;
    
    public AudioClip NormalHit, SpecialHit, potion_drink, Powerup_sfx;

    public InputHandler input;
    


    void Start()          // Start is called before the first frame update
    { 
        audioSource = GetComponent<AudioSource>();
        PlayerRB = GetComponent<Rigidbody>();
    }

    
    void Update()       // Update is called once per frame
    {
        PotionEffectUpdate();
        PowerUpUpdate();
        Gameover();
        
    }

    // If player collides with powerup, turns powerup feature ON
     private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PowerUp")){
            Destroy(other.gameObject);
            TurnOnPowerUpEffect();
        }    
        else if(other.CompareTag("Potion"))
        {
            Destroy(other.gameObject);
            TurnOnPotionEffect();
            
        }
    }



    // If player collides with enemy and has power up, shoot the enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

            if (hasPowerUp)
            {
                if (hasPotion)
                {
                    enemyRB.AddForce((awayFromPlayer - Vector3.up) * 2500, ForceMode.Impulse);
                }
                else
                {
                    enemyRB.AddForce(awayFromPlayer * strength, ForceMode.Impulse);
                }
                audioSource.PlayOneShot(SpecialHit, 1);
                powerupTime = 0;
            }
            else
            {
                if (hasPotion)
                {
                    enemyRB.AddForce((awayFromPlayer - Vector3.up) * 30, ForceMode.Impulse);
                }
                else
                {
                    enemyRB.AddForce(awayFromPlayer * 1.8f, ForceMode.Impulse);
                    audioSource.PlayOneShot(NormalHit, 2);
                }
            }
        }
    }


    void TurnOffPowerUpEffect()
    {
        hasPowerUp = false; 
        powerUpIndicator.SetActive(false);
    }

    void TurnOffPotionEffects()
    {
        transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        PlayerRB.mass = 2;
        input.speed = 15;
        powerUpIndicator.transform.localScale = new Vector3(3.3f,3.3f,3.3f);
        hasPotion = false;
        
    }

    private void Gameover()
    {
        if(transform.position.y <= -10){
            GameOverScreen.ShowGameInfo(spawnManager.WaveNumber);
            spawnManager.waveText.text = "";                      // hiding the ""wave x"" text when player dies
        } 
    }


    private void PotionEffectUpdate()
    {
        if(hasPotion)
        {
            potionTime -= Time.deltaTime;
            if (potionTime<=0)
            {
                TurnOffPotionEffects();
            }
        }
    }

    private void PowerUpUpdate()
    {
        if(hasPowerUp)
        {
            powerUpIndicator.transform.position = transform.position -new Vector3(0,0.45f,0);         // updating the Transform of powerup ring 
            powerupTime -= Time.deltaTime;
            if (powerupTime <= 0)
            {
                TurnOffPowerUpEffect();    
            }
        }

    }

    private void TurnOnPowerUpEffect()
    {
        audioSource.PlayOneShot(Powerup_sfx,1);
        hasPowerUp = true;
        powerupTime = 7;
        powerUpIndicator.SetActive(true);
        if(hasPotion){
            powerUpIndicator.transform.localScale = new Vector3(8.58f,8.58f,8.58f);
        }
    }

    private void TurnOnPotionEffect()
    {
        audioSource.PlayOneShot(potion_drink,1);
        if(hasPowerUp)
        {
            powerUpIndicator.transform.localScale  = new Vector3(8.58f,8.58f,8.58f);
        }
        if(!hasPotion)
        {
            transform.localScale *= 2.5f;
            potionTime = 9;
            PlayerRB.mass = 6;
            input.speed = 35;
            hasPotion = true;   
        }
        else
        {
            potionTime = 9;
        }
    }
}


