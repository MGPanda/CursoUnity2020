using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    public float timeUntilDog = 1.5f;
    private float timeSinceDog;

    private void Start()
    {
        timeSinceDog = timeUntilDog;
    }

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && timeSinceDog >= timeUntilDog)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            timeSinceDog = 0f;
        }

        timeSinceDog += Time.deltaTime;
    }
}
