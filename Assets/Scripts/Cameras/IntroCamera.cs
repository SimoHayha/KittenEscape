using UnityEngine;
using System.Collections;

public class IntroCamera : MonoBehaviour
{
    public Camera TpsCamera = null;
    public CameraManager CameraManager = null;
    public Transform FallLookAt = null;

    public float SpeedAccelerator = 1.0f;
    public float FallTimer = 5.0f;

    private float currentFallTimer = float.MaxValue;
    private float currentSpeedAccelerator = 0.0f;
    private Vector3 origin;
    private bool started = false;
    private float currentFOV = 20.0f;

    void Update()
    {
        if (currentFallTimer < FallTimer)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                enabled = false;
                started = false;
                CameraManager.SetStartLevelAnimMode(false);
            }

            currentFOV = Mathf.Lerp(45.0f, 60.0f, currentFallTimer / FallTimer);
            CameraManager.IntroCamera.fieldOfView = currentFOV;

            currentSpeedAccelerator += SpeedAccelerator * Time.deltaTime * currentFallTimer;
            currentFallTimer += Time.deltaTime + currentSpeedAccelerator;

            transform.position = Vector3.Lerp(origin, TpsCamera.transform.position, currentFallTimer / FallTimer);
            transform.LookAt(FallLookAt);
        }
        else
        {
            if (started)
            {
                enabled = false;
                started = false;
                CameraManager.SetStartLevelAnimMode(false);
            }
        }
    }

    public void LaunchFallAnimation()
    {
        currentFallTimer = 0.0f;
        currentFOV = 20.0f;
        started = true;
        origin = transform.position;
    }
}