using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{   
    [Header("Movement Properties")]
    [SerializeField] private float tankSpeed;
    [SerializeField] private float rotationSpeed;
    private float _horizontalInput;
    private float _verticalInput;
    
    //Components
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        
        HandleMovement();
    }


    private void HandleMovement()
    {
        //Move
        Vector3 wantedPosition = transform.position + (transform.forward * _verticalInput * tankSpeed * Time.deltaTime);
        _rb.MovePosition(wantedPosition);
        
        //Rotate
        Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * (rotationSpeed * _horizontalInput * Time.deltaTime));
        _rb.MoveRotation(wantedRotation);
    }
}
