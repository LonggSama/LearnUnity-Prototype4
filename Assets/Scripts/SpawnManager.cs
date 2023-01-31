using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;

    [SerializeField] float spawnRange = 10f;


    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPostion(), enemyPrefabs[enemyIndex].transform.rotation);
    }

    private Vector3 GenerateSpawnPostion()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
