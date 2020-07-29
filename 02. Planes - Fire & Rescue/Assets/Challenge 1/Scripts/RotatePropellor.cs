using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePropellor : MonoBehaviour
{
    public float speed = 185;
    void Update()
    {
        transform.Rotate(Vector3.forward * speed);
    }
}
