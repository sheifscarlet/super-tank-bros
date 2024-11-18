using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float lagSpeed = 5f;
    [SerializeField] private Camera camera;
    [SerializeField] private float rotationLockDuration = 0.2f; // Длительность блокировки поворота дула

    private bool isRotationLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRotationLocked)
        {
            HandleTurretRotation();
        }
    }

    private void HandleTurretRotation()
    {
        Ray screenRay = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(screenRay, out hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f; // Ensure the direction is horizontal

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * lagSpeed);
        }
    }

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