using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;

    private float timeSinceLastSpawn;
    private float timeUntilNextSpawn = 4.0f;

    private int thisBall;

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall ()
    {
        thisBall = Random.Range(0, ballPrefabs.Length);
        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        // instantiate ball at random spawn location
        Instantiate(ballPrefabs[thisBall], spawnPos, ballPrefabs[thisBall].transform.rotation);
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeUntilNextSpawn)
        {
            timeSinceLastSpawn = 0f;
            SpawnRandomBall();
            timeUntilNextSpawn = Random.Range(2, 5);
        }
    }
}
