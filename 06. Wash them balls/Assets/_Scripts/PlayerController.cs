using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float moveForce;

    public GameObject focalPoint;

    public bool hasPowerUp;
    public float powerUpForce;
    public float powerUpTime;
    public GameObject[] powerUpIndicators;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(focalPoint.transform.forward * (moveForce * forwardInput));
        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdown());
        }

        if (other.gameObject.name.CompareTo("Killzone") == 0)
        {
            SceneManager.LoadScene("Prototype 4");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountdown()
    {
        foreach (GameObject indicator in powerUpIndicators)
        {
            indicator.SetActive(true);
            yield return new WaitForSecondsRealtime(powerUpTime/powerUpIndicators.Length);
            indicator.SetActive(false);
        }
        hasPowerUp = false;
    }
    
}