using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0,50),Tooltip("Current speed of the plane."), SerializeField]
    private float speed;
    
    public float turnSpeed;

    public int travelSpeed = 50;

    private float horizontalInput, verticalInput, jumpInput;
    
    private GameObject propeller;
    // Start is called before the first frame update
    void Start()
    {
        propeller = GameObject.Find("Propellor");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Jump");
        propeller.transform.Rotate(Vector3.forward * (Time.deltaTime * speed * 100));
        if (jumpInput > 0.1)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * speed));
            speed += 0.1f;
        }
        else
        {
            speed *= 0.999f;
        }

        if (speed >= travelSpeed)
        {
            speed = travelSpeed;
            transform.Rotate(Vector3.right * (Time.deltaTime * verticalInput * turnSpeed));
            transform.Rotate(Vector3.back * (Time.deltaTime * horizontalInput * turnSpeed));
        }
        
    }
}
