using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] GameObject[] powerupPrefabs;
    [SerializeField] float spawnRange = 10f;
    [SerializeField] int waveSpawnBoss;

    public int enemyCount;
    public int powerupCount;

    private int waveNumber = 1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWay(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnPowerUp();
            if (waveNumber % waveSpawnBoss != 0)
            {
                Debug.Log("Wave " + waveNumber);
                SpawnEnemyWay(waveNumber);
            }
            else if (waveNumber % waveSpawnBoss == 0)
            {
                Debug.Log("Wave " + waveNumber);
                Debug.Log("Wave " + waveSpawnBoss);
                SpawnBoss();
            }
        }
    }

    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPostion(), enemyPrefabs[enemyIndex].transform.rotation);
    }

    void SpawnBoss()
    {
        Instantiate(bossPrefab, GenerateSpawnPostion(), bossPrefab.transform.rotation);
    }

    void SpawnPowerUp()
    {
        int powerupIndex = Random.Range(0, powerupPrefabs.Length);
        powerupCount = FindObjectsOfType<PowerUp>().Length;
        if (powerupCount == 0)
        {
            Instantiate(powerupPrefabs[powerupIndex], GenerateSpawnPostion(), powerupPrefabs[powerupIndex].transform.rotation);
        }
    }

    public Vector3 GenerateSpawnPostion()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    void SpawnEnemyWay(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }
}
