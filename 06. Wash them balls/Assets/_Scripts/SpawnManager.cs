using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    private float spawnRange = 9;

    public int enemyCount;
    public int enemyWave;

    public GameObject powerUpPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(enemyWave);
    }

    private void Update()
    {
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            enemyWave++;
            SpawnEnemyWave(enemyWave);
            
            int numberOfPowerUps = Random.Range(0, 3);
            for (int i = 0; i < numberOfPowerUps; i++)
            {
                Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
            }
        }
    }

    /// <summary>
    /// Genera una posición aleatoria dentro de la zona de juego.
    /// </summary>
    /// <returns>Devuelve el vector de posición.</returns>
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    void SpawnEnemyWave(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
