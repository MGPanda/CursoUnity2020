using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float minForce = 12,
        maxForce = 16,
        maxTorque = 10,
        xRange = 4,
        ySpawnPos = -6;

    private GameManager gameManager;

    [Range(-100,100)]
    public int pointValue;

    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(),
            ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Genera una fuerza aleatoria basada en un mínimo y máximo.
    /// </summary>
    /// <returns>Devuelve la fuerza en Vector3.</returns>
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    /// <summary>
    /// Genera una posición aleatoria basada en un mínimo y máximo.
    /// </summary>
    /// <returns>Devuelve la posición en Vector3.</returns>
    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    /// <summary>
    /// Genera un torque basado en un mínimo y máximo.
    /// </summary>
    /// <returns>Devuelve el torque en float.</returns>
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private void OnMouseOver()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killzone"))
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Good"))
            {
                gameManager.GameOver();
            }
        }
    }
}