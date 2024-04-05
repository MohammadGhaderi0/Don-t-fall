using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject PowerupPrefab;
    
    public GameObject smallEnemyPrefab;
    
    public GameObject bigEnemyPrefab;
    
    public GameObject PotionPrefab;
    
    public float SpawnRange = 9;
    
    private const int spawnInterval_powerUp = 6;
    
    private const int spawnInterval_potion = 7;
    
    public float minDistance = 4f;
    
    private float powerUpSpawnTimer;
    
    private float potionSpawnTimer;
    
    public int WaveNumber = 1;
    
    private int EnemyCount;
    
    public AudioSource audioSource;
    
    public AudioClip NewWave;
    
    public TMP_Text waveText;
    
    public Material[] materials;
    
    private int powerUpRandom;
    
    private int potionRandom;


    

    void Start()
    {
        powerUpRandom = Random.Range(0, 6);
        potionRandom = Random.Range(2, 8);
        // starting the first wave for the first time
        waveText.text = "Wave  " + WaveNumber;
        SpawnEnemyWave(WaveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(EnemyCount == 0)                     // If number of enemies become zero, 
        {                                       // then the new wave should be started
            StartNewWave();
        }
        
        SpawnPowerUp();
        SpawnPotion();
    }


    // Generate random positions for enemies 
    private Vector3 GenerateRandomPosition(){
        float SpawnPosX = Random.Range(-SpawnRange, SpawnRange);
        float SpawnPosZ = Random.Range(-SpawnRange, SpawnRange);
        Vector3 randomPos = new Vector3(SpawnPosX, 0.4f,SpawnPosZ);
        return randomPos;
    }



    // Creates enemies with different skins and sizes
    void SpawnEnemyWave(int enemiesToSpawn){
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
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        Vector3 position;
        
        do
        {
            position = new Vector3(Random.Range(-9, 9), 0.2f, Random.Range(-9, 9));
        }
        while (!IsPositionValid(position, powerUps));

        return position;
    }
    
      bool IsPositionValid(Vector3 position, GameObject[] powerUps)
    {
        foreach (GameObject powerUp in powerUps)
        {
            if (Vector3.Distance(position, powerUp.transform.position) < minDistance)
                return false;
        }
        
        return true;
    }


    

      void SpawnPowerUp()
    {
        powerUpSpawnTimer += Time.deltaTime;     
        if (powerUpSpawnTimer >= spawnInterval_powerUp + powerUpRandom)
        {
            if(GameObject.FindGameObjectsWithTag("PowerUp").Length <=2){
                Instantiate(PowerupPrefab, GenerateRandomPositions(), PowerupPrefab.transform.rotation);
            }
            powerUpSpawnTimer = 0f;
            powerUpRandom = Random.Range(0, 6);

        }
    }
    void SpawnPotion(){
        if (potionSpawnTimer >= spawnInterval_potion + potionRandom)
        {
            Instantiate(PotionPrefab,GenerateRandomPositions(), PotionPrefab.transform.rotation);
        }
        potionSpawnTimer = 0;
        powerUpRandom = Random.Range(2, 8);

    }

    void StartNewWave()
    {
        audioSource.PlayOneShot(NewWave,0.4f);
        WaveNumber ++;
        waveText.text = "Wave  " + WaveNumber;
        SpawnEnemyWave(WaveNumber);
    }


}