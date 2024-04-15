using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject PowerupPrefab, smallEnemyPrefab, bigEnemyPrefab, PotionPrefab, player;
    
    public float SpawnRange = 9;
    
    private const int spawnInterval_powerUp = 6;
    
    private const int spawnInterval_potion = 7;
    
    public float minDistance = 2f;
    
    private float powerUpSpawnTimer;
    
    private float potionSpawnTimer;
    
    public int WaveNumber = 1;
    
    private int EnemyCount;
    
    public TMP_Text waveText;
    
    public Material[] materials;
    
    private int powerUpRandom;
    
    private int potionRandom;

    private int numberOfPotions;

    private int numberOfPowerUps;

    public List<GameObject> inGroundObjects;

    public int enemiesLeft;

    
    

    void Start()
    {
        
        powerUpRandom = Random.Range(0, 6);
        potionRandom = Random.Range(0, 6);
        // starting the first wave for the first time
        waveText.text = "Wave  " + WaveNumber;
        SpawnEnemyWave(WaveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesLeft == 0)                     // If number of enemies become zero, 
        {                                       // then the new wave should be started
            StartNewWave();
        }
        
        SpawnPowerUp();
        SpawnPotion();
        
    }

    public void ActiveDeactiveEnemiesKinematic()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            rb.isKinematic = !rb.isKinematic;
        }

    }
    


    // Generate random positions for enemies 
    private Vector3 GenerateRandomPosition(){
        float SpawnPosX = Random.Range(-SpawnRange, SpawnRange);
        float SpawnPosZ = Random.Range(-SpawnRange, SpawnRange);
        Vector3 randomPos = new Vector3(SpawnPosX, 0.4f,SpawnPosZ);
        return randomPos;
    }



    // Creates enemies with different skins and sizes
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        enemiesLeft = enemiesToSpawn;
        for (int i = 0; i < enemiesToSpawn; i++){
            Material randomMaterial = GetRandomMaterial();
            GameObject newBall;
            if(Random.Range(0,3) ==1)           // // It is 33% possible to spawn big enemy
            { 
                newBall = Instantiate(bigEnemyPrefab,GenerateRandomPosition(),smallEnemyPrefab.transform.rotation);
                newBall.transform.localScale = new Vector3(3,3,3);
                newBall.GetComponent<Rigidbody>().mass = 6;
            }
            else
            { 
                newBall = Instantiate(smallEnemyPrefab,GenerateRandomPosition(),smallEnemyPrefab.transform.rotation);
            }
            newBall.GetComponent<MeshRenderer>().material = randomMaterial;
            
        }
    }
  
    Material GetRandomMaterial() {
        int randomIndex = Random.Range(0, materials.Length);
        return materials[randomIndex];
    }


      // Generate random positions for powerups(they should not be next to each other) 
    Vector3 GenerateRandomPositions()
    {
        Vector3 position;
        do
        {
            position = new Vector3(Random.Range(-9, 9), 0.2f, Random.Range(-9, 9));
        }
        while (!IsPositionValid(position));

        return position;
    }
    
      bool IsPositionValid(Vector3 position)
    {
        foreach (GameObject ability in inGroundObjects)
        {
            if (Vector3.Distance(position, ability.transform.position) < minDistance)
                return false;
        }
        
        return true;
    }


    

      void SpawnPowerUp()
    {
        powerUpSpawnTimer += Time.deltaTime;     
        if (powerUpSpawnTimer >= spawnInterval_powerUp + powerUpRandom)
        {
            if(numberOfPowerUps<=2){
 
                GameObject newObj = Instantiate(PowerupPrefab, GenerateRandomPositions(), PowerupPrefab.transform.rotation);
                numberOfPowerUps++;
                inGroundObjects.Add(newObj);
                
            }
            powerUpSpawnTimer = 0f;
            powerUpRandom = Random.Range(0, 6);

        }
    }
    void SpawnPotion(){
        potionSpawnTimer += Time.deltaTime;     
        if (potionSpawnTimer >= spawnInterval_potion + potionRandom)
        {
            if (numberOfPotions <=2)
            {
                GameObject newObj = Instantiate(PotionPrefab,GenerateRandomPositions(), PotionPrefab.transform.rotation);
                numberOfPotions++;
                inGroundObjects.Add(newObj);

            } 
            potionSpawnTimer = 0;   
            powerUpRandom = Random.Range(2, 8);
       
        }

    }

    void StartNewWave()
    {
        BaseSoundController.Instance.PlaySoundByIndex(6,player.transform.position);
        WaveNumber ++;
        waveText.text = "Wave  " + WaveNumber;
        SpawnEnemyWave(WaveNumber);
    }

    


}