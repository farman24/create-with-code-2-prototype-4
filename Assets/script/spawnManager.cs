
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public GameObject[] powerUp;
    public int enemyCount;
    public int waveNumber=1;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyWave(waveNumber);
    }
    // Update is called once per frame
    void spawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs, generateRandomSpawnPosition(), enemyPrefabs.transform.rotation);

        }
        int index = Random.Range(0, powerUp.Length);
        Instantiate(powerUp[index], generateRandomSpawnPosition(), powerUp[index].transform.rotation);

    }
    void Update()
    {
        enemyCount = FindObjectsOfType<enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            spawnEnemyWave(waveNumber);
        }
    }
    private Vector3 generateRandomSpawnPosition()
    {
        float spawnPosX = Random.Range(6.2f, -6.8f);
        float spawnPosZ = Random.Range(-7.5f, 8f);
        Vector3 randomPosition = new Vector3(spawnPosX, 0.143999994f, spawnPosZ);
        return randomPosition;
    }
}
