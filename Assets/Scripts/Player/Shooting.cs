using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private PlayerType playerType;

    [Header("Shooting")]
    [SerializeField] private bool canShoot = true;
    [SerializeField] private float cooldown;
    [SerializeField] private int missileDamage = 10; 

    [Header("Camera Shake")]
    [SerializeField] private float intensity = 5f;
    [SerializeField] private float time = 0.25f;

    [Header("Components")]
    [SerializeField] private MissleSpawner missleSpawner;
    [SerializeField] private TurretController turretController;

    private void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        switch (playerType)
        {
            case PlayerType.Player1:
                HandlePlayer1Inputs();
                break;

            case PlayerType.Player2:
                HandlePlayer2Inputs();
                break;
        }
    }

    private void HandlePlayer1Inputs()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            turretController.RotateTurret(-1); // Rotate left
        }
        else if (Input.GetKey(KeyCode.E))
        {
            turretController.RotateTurret(1); // Rotate right
        }

        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            Shoot();
        }
    }

    private void HandlePlayer2Inputs()
    {
        if (Input.GetKey(KeyCode.U))
        {
            turretController.RotateTurret(-1); // Rotate left
        }
        else if (Input.GetKey(KeyCode.O))
        {
            turretController.RotateTurret(1); // Rotate right
        }

        if (Input.GetKeyDown(KeyCode.Return) && canShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Camera shake effect
        CameraShake.instance.ShakeCamera(intensity, time);

        // Spawn missile and set damage
        if (missleSpawner != null)
        {
            missleSpawner.Spawn(missileDamage);
        }

        Debug.Log("Shoot");

        if (turretController != null)
        {
            turretController.LockRotation();
        }

        canShoot = false;
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
        Debug.Log("Ready to shoot");
    }
}
