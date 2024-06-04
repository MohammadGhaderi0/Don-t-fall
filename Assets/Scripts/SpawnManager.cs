using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
	public GameObject PowerupPrefab, smallEnemyPrefab, bigEnemyPrefab, PotionPrefab, player;
	
	public float SpawnRange = 9.2f;
	
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

	public int numberOfPotions;

	public int numberOfPowerUps;

	public List<GameObject> inGroundObjects;

	public int enemiesLeft;

	
	

	void Start()
	{
		powerUpRandom = Random.Range(0, 6);
		potionRandom = Random.Range(0, 6);
		// starting the first wave for the first time
		waveText.text = "Wave  " + WaveNumber;
		SpawnEnemyWave(WaveNumber);         // starting the first wave with one enemy
	}

	void Update()
	{
		if(enemiesLeft == 0)                     // If number of enemies become zero, 
		{                                       // then the new wave should be started
			StartNewWave();
		}
		
		// Spawn Potion
		SpawnAbility(ref potionSpawnTimer,spawnInterval_potion,ref numberOfPotions,2,PotionPrefab);
		
		// Spawn PowerUp
		SpawnAbility(ref powerUpSpawnTimer,spawnInterval_powerUp,ref numberOfPowerUps,2,PowerupPrefab);
		
	}

	public void ActiveDeactiveEnemiesKinematic()   // we activate and disactivate kinematic in pause, because it causes weird movements
	{											   // in balls after changing sensitivity
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemy in enemies)
		{
			Rigidbody rb = enemy.GetComponent<Rigidbody>();
			rb.isKinematic = !rb.isKinematic;
		}

	}
	


	// Generate random positions for enemies 
	private Vector3 GenerateRandomPositionEnemies(Vector3 playerPosition, float minDistance) 
	{
		Vector3 randomPos;
		float distance;
		do {
			float spawnPosX = Random.Range(-SpawnRange, SpawnRange);
			float spawnPosZ = Random.Range(-SpawnRange, SpawnRange);
			randomPos = new Vector3(spawnPosX, 0.4f, spawnPosZ);
			distance = Vector3.Distance(randomPos, playerPosition);
		} while (distance < minDistance);
		return randomPos;
	}



	// Creates enemies with different skins and sizes
	void SpawnEnemyWave(int enemiesToSpawn)
	{
		enemiesLeft = enemiesToSpawn;				// updating number of enemies
		for (int i = 0; i < enemiesToSpawn; i++){
			Material randomMaterial = GetRandomMaterial();
			GameObject newBall;
			if(Random.Range(0,3) ==1)            // It is 33% possible to spawn big enemy
			{ 
				newBall = Instantiate(bigEnemyPrefab,GenerateRandomPositionEnemies(player.transform.position,0.4f),smallEnemyPrefab.transform.rotation);
				newBall.transform.localScale = new Vector3(3,3,3);
				newBall.GetComponent<Rigidbody>().mass = 6;
			}
			else
			{ 
				newBall = Instantiate(smallEnemyPrefab,GenerateRandomPositionEnemies(player.transform.position,0.4f),smallEnemyPrefab.transform.rotation);
			}
			newBall.GetComponent<MeshRenderer>().material = randomMaterial;
			
		}
	}
  
	Material GetRandomMaterial() {
		int randomIndex = Random.Range(0, materials.Length);
		return materials[randomIndex];
	}


	  // Generate random positions for powerups(they should not be next to each other) 
	Vector3 GenerateRandomPositionsPotionAndPowerUp()
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


	// spawn both potion and powerup
void SpawnAbility(ref float spawnTimer, float spawnInterval, ref int numberOfObjects, int maxObjects, GameObject prefab)
{
	int randomValue = Random.Range(0, 5);
	spawnTimer += Time.deltaTime;
	if (spawnTimer >= spawnInterval + randomValue)
	{
		if (numberOfObjects <= maxObjects)
		{
			GameObject newObj = Instantiate(prefab, GenerateRandomPositionsPotionAndPowerUp(), prefab.transform.rotation);
			numberOfObjects++;
			inGroundObjects.Add(newObj);
		}
		spawnTimer = 0f;
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