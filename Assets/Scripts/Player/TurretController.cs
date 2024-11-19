using System.Collections;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float rotationLockDuration = 0.2f;

    private bool isRotationLocked = false;

    private void Update()
    {
        if (!isRotationLocked)
        {
            // Turret rotation handled by Shooting script
        }
    }

    // Rotate turret manually using input (-1 for left, 1 for right)
    public void RotateTurret(float direction)
    {
        if (isRotationLocked) return;

        float rotationAmount = direction * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotationAmount, 0f);
    }

    // Lock turret rotation temporarily
    public void LockRotation()
    {
        isRotationLocked = true;
        StartCoroutine(UnlockRotationAfterDelay());
    }

    private IEnumerator UnlockRotationAfterDelay()
    {
        yield return new WaitForSeconds(rotationLockDuration);
        isRotationLocked = false;
    }
}