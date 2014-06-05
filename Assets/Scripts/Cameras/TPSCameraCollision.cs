using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider))]

public class TPSCameraCollision : MonoBehaviour {

    public float MaxDIstance = 4.5f;
    public float MinDistance = 0.5f;

    private bool isColliding = false;

    public GameObject Player = null;

    void Update()
    {
        if (isColliding)
        {
            transform.position += transform.forward;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        isColliding = true;
    }

    void OnTriggerExit(Collider collider)
    {
        isColliding = false;
    }
}
