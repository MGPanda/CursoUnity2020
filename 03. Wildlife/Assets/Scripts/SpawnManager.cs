using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;

    private int animalIndex;

    private float spawnRangeX = 14f, spawnPositionZ;
    
    [SerializeField, Range(0, 3)]
    private float startDelay = 2f, spawnInteval = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPositionZ = transform.position.z;
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInteval);
    }

    void SpawnRandomAnimal()
    {
        animalIndex = Random.Range(0, enemies.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPositionZ);
        Instantiate(enemies[animalIndex], spawnPos, enemies[animalIndex].transform.rotation);
    }
}