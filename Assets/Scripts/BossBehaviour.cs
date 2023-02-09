using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] GameObject minionPrefab;
    [SerializeField] int numMinion;
    [SerializeField] float spawnCooldown;
    [SerializeField] float firstSpawn;

    [SerializeField] GameObject boss;

    private bool _isSpawn = false;

    private void Start()
    {
        StartCoroutine(SpawnCooldown(firstSpawn));
    }
    // Update is called once per frame
    void Update()
    {
        if (boss != null)
        {
            if (_isSpawn)
            {
                SpawnMinion(numMinion);
                _isSpawn = false;
                StartCoroutine(SpawnCooldown(spawnCooldown));
            }
        }
        else
            this.enabled = false;
    }

    void SpawnMinion(int minionToSpawn)
    {
        for (int i = 0; i < minionToSpawn; i++)
        {
            Instantiate(minionPrefab, SpawnManager.Instance.GenerateSpawnPostion(), minionPrefab.transform.rotation);
        }
    }

    IEnumerator SpawnCooldown(float spawnCooldown)
    {
        yield return new WaitForSeconds(spawnCooldown);
        _isSpawn = true;
    }
}
