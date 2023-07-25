using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float SpawnRange = 9;
    private int WaveNumber = 1;
    private int EnemyCount;
    public GameObject PowerupPrefab;


    public float spawnInterval = 6f;
    public float minDistance = 4f;
    
    private float spawnTimer = 0f;
    public AudioSource audioSource;
    public AudioClip NewWave;

    public TMP_Text waveText;
    public Material[] materials;
    public GameObject PotionPrefab;
    void Start()
    {
        // starting the first wave for the first time
        waveText.text = "Wave  " + WaveNumber;
        SpawnEnemyWave(WaveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        // If number of enemies become zero, then the new wave should be started
        EnemyCount = FindObjectsOfType<EnemyController>().Length;
        if(EnemyCount ==0){
            audioSource.PlayOneShot(NewWave,0.4f);
            WaveNumber ++;
            waveText.text = "Wave  " + WaveNumber;
            SpawnEnemyWave(WaveNumber);
            // StartCoroutine(SpawnPowerup());
        }


        // every 6 seconds the new powerup spawns
        spawnTimer += Time.deltaTime;     
        if (spawnTimer >= spawnInterval)
        {
            SpawnPowerUp();
            spawnTimer = 0f;
        }

        SpawnPotion();
    }


    // Generate random positions for enemies 
    private Vector3 GenerateRandomPosition(){
        float SpawnPosX = Random.Range(-SpawnRange, SpawnRange);
        float SpawnPosZ = Random.Range(-SpawnRange, SpawnRange);
        Vector3 randomPos = new Vector3(SpawnPosX, 0.4f,SpawnPosZ);
        return randomPos;
    }



    // Creates enemies with different skins
    void SpawnEnemyWave(int enemiesToSpawn){
        for (int i = 0; i < enemiesToSpawn; i++){
            int randomIndex = Random.Range(0, materials.Length);
            GameObject newObject = Instantiate(enemyPrefab,GenerateRandomPosition(),enemyPrefab.transform.rotation);
            newObject.GetComponent<MeshRenderer>().material = materials[randomIndex];
            
        }
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
        if(GameObject.FindGameObjectsWithTag("PowerUp").Length <=4){
            Instantiate(PowerupPrefab, GenerateRandomPositions(), PowerupPrefab.transform.rotation);
        }
    }
    void SpawnPotion(){
        int chance = Random.Range(1,8500);
        if( chance == 23){
         Instantiate(PotionPrefab,GenerateRandomPositions(), PotionPrefab.transform.rotation);
        }
    }

}