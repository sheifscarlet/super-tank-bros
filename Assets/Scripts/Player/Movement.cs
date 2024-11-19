using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [Header("Movement Properties")]
    [SerializeField] private float tankSpeed = 5f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private PlayerType playerType;
    private float _horizontalInput;
    private float _verticalInput;
    

    // Components
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        switch (playerType)
        {
            case PlayerType.Player1:
                _horizontalInput = Input.GetAxis("Horizontal");
                _verticalInput = Input.GetAxis("Vertical");
                break;
            case PlayerType.Player2:
                _horizontalInput = Input.GetAxis("Horizontal2");
                _verticalInput = Input.GetAxis("Vertical2");
                break;
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Move
        Vector3 movement = transform.forward * _verticalInput * tankSpeed;
        _rb.MovePosition(_rb.position + movement * Time.fixedDeltaTime);

        // Rotate
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(Vector3.up * (_horizontalInput * rotationSpeed * Time.fixedDeltaTime));
        _rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));

        // Reset unwanted physics forces
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}