using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed = 30;
    public float rotationSpeed = 50;
    private float verticalInput, horizontalInput;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's input
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Vector3.right * (rotationSpeed * Time.deltaTime * verticalInput));
        transform.Rotate(Vector3.back * (rotationSpeed * Time.deltaTime * horizontalInput));
    }

    private void OnCollisionEnter(Collision other)
    {
        _rigidbody.useGravity = true;
        speed = 0;
        rotationSpeed = 0;
    }
}
