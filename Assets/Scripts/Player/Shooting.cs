using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private bool canShoot = true;
    [SerializeField] private float cooldown;
    
    [Header("Camera Shake")] 
    [SerializeField] private float intensity = 5f;
    [SerializeField] private float time = 0.25f;
    
    [Header("Components")]
    [SerializeField] private MissleSpawner missleSpawner;
    [SerializeField] private TurretController turretController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.L) && canShoot)
        {
            Shoot();
        }

        if (Input.GetButtonDown("Fire1")&& canShoot)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        CameraShake.instance.ShakeCamera(intensity,time);
        missleSpawner.Spawn();
        Debug.Log("Shoot");

        // Блокируем поворот дула на короткое время
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