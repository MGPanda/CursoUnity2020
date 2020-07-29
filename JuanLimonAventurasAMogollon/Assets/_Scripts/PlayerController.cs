using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;

    private Animator _animator;
    private Rigidbody _rigidbody;

    [SerializeField]
    private float turnSpeed;

    private Quaternion rotation = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal,0,vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal,0);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0);

        bool isWalking = hasHorizontalInput || hasVerticalInput;
        
        _animator.SetBool("IsWalking",isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, 
            movement, turnSpeed*Time.deltaTime,0f);

        rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }
}
