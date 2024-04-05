using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject FocalPoint, powerUpIndicator;
    
    public SpawnManager spawnManager;
    
    public GameOver GameOverScreen;
    
    private Rigidbody PlayerRB;
    
    public float strength = 26;
    
    public float potionTime, powerupTime;
    
    public bool gameOver, hasPowerUp,hasPotion;
    
    public AudioSource audioSource;
    
    public AudioClip NormalHit, SpecialHit, potion_drink, Powerup_sfx;

    public InputHandler input;

    public int powerUpChargeTime;
    
    public int potionChargeTime;


    void Start()          // Start is called before the first frame update
    {
        powerUpChargeTime = 7;
        potionChargeTime = 7;
        audioSource = GetComponent<AudioSource>();
        PlayerRB = GetComponent<Rigidbody>();
    }

    
    void Update()       // Update is called once per frame
    {
        PotionEffectUpdate();
        PowerUpUpdate();
        Gameover();
        
    }

    // If player collides with powerups or potions,it turns the abilities on
     private void OnTriggerEnter(Collider other)
     {
        if(other.CompareTag("PowerUp"))
        {
            spawnManager.inGroundObjects.Remove(other.gameObject);
            Destroy(other.gameObject);
            TurnOnPowerUpEffect();
        }    
        else if(other.CompareTag("Potion"))
        {

            spawnManager.inGroundObjects.Remove(other.gameObject);
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

    void TurnOffPotionEffects()             // changing the size and mass to the original
    {
        transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        PlayerRB.mass = 2;                                      
        input.speed = 15;
        powerUpIndicator.transform.localScale = new Vector3(3.3f,3.3f,3.3f);
        hasPotion = false;
        
    }

    private void Gameover()
    {
        if(transform.position.y <= -10){                          // when player falls
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
            powerUpIndicator.transform.position = transform.position -new Vector3(0,0.45f,0);         // updating the location of powerup ring 
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
        if (hasPowerUp)
        {
            powerupTime = powerUpChargeTime;
        }
        else
        {
            hasPowerUp = true;
            powerupTime = powerUpChargeTime;
            powerUpIndicator.SetActive(true);

        }
        if(hasPotion){
            powerUpIndicator.transform.localScale = new Vector3(8.58f,8.58f,8.58f);
        }
    }

    private void TurnOnPotionEffect()
    {
        
        audioSource.PlayOneShot(potion_drink,1);
        if(hasPowerUp)                                       // if the player have powerups , the ring size should be different
        {
            powerUpIndicator.transform.localScale  = new Vector3(8.58f,8.58f,8.58f);
        }
        if(!hasPotion)
        {
            transform.localScale *= 2.5f;
            potionTime = potionChargeTime;
            PlayerRB.mass = 6;
            input.speed = 35;
            hasPotion = true;   
        }
        else
        {
            potionTime = potionChargeTime;
        }
    }
}


