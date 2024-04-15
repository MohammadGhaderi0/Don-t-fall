using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject powerUpIndicator;
    
    public SpawnManager spawnManager;
    
    public GameOver GameOverScreen;
    
    private Rigidbody PlayerRB;
    
    public float strength = 26;
    
    public float potionTime, powerupTime;
    
    public bool gameOver, hasPowerUp,hasPotion;
    
    public InputHandler input;

    public int powerUpChargeTime;
    
    public int potionChargeTime;


    void Start()          // Start is called before the first frame update
    {
        powerUpChargeTime = 7;
        potionChargeTime = 9;
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
                BaseSoundController.Instance.PlaySoundByIndex(3,transform.position);
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
                    BaseSoundController.Instance.PlaySoundByIndex(2,transform.position);
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
        if(transform.position.y < -10)
        {
            transform.position = new Vector3(transform.position.x, -5, transform.position.z);    // freezing position is used to stop the ball going down forever
            PlayerRB.constraints = RigidbodyConstraints.FreezePosition;                            // which may cause errors and setting the y position to -5 to stop this loop running because it is unnecessary;
            // audioSource.Play();                                   
            BaseSoundController.Instance.PlaySoundByIndex(7,new Vector3(0,0,0));
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
        BaseSoundController.Instance.PlaySoundByIndex(5,transform.position);
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
        
        BaseSoundController.Instance.PlaySoundByIndex(4,transform.position);
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

    public void ActiveDeActivePlayerKinematic()
    {
        PlayerRB.isKinematic = !PlayerRB.isKinematic;
    }
}


