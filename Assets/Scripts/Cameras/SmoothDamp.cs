using UnityEngine;
using System.Collections;

public class SmoothDamp : MonoBehaviour {

    public LayerMask Layer;

    public Transform head;
    public Transform target;

    public float minDistance = 0.1f;
    public float maxDistance = 4.5f;
    public float distance = 10.0f;
    public float OffsetFromWall = 0.1f;
    public float ZoomRate = 40.0f;
    public float ZoomDampening = 5.0f;
    public float TargetHeight = 1.7f;

    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20;
    public float yMaxLimit = 80;

    public float offsetDistance = 3.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    private bool isColliding = false;

    private float desiredDistance = 0.0f;
    private float correctedDistance = 0.0f;
    private float currentDistance = 0.0f;

    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        if (rigidbody)
            rigidbody.freezeRotation = true;

        desiredDistance = distance;
    }

    void LateUpdate()
    {
        Vector3 vTargetOffset;

        if (!target)
            return;

        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0.0f);

        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ZoomRate * Mathf.Abs(desiredDistance);
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        correctedDistance = desiredDistance;

        vTargetOffset = new Vector3(0.0f, -TargetHeight, 0.0f);
        Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset);

        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3(target.position.x, target.position.y + TargetHeight, target.position.z);

        bool isCorrected = false;
        if (Physics.Linecast(trueTargetPosition, position, out collisionHit, Layer))
        {
            correctedDistance = Vector3.Distance(trueTargetPosition, collisionHit.point) - OffsetFromWall;
            isCorrected = true;
        }

        currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * ZoomDampening) : correctedDistance;

        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        position = target.position - (rotation * Vector3.forward * currentDistance + vTargetOffset);

        transform.rotation = rotation;
        transform.position = position;

        var targetRot = rotation;

        targetRot.x = 0.0f;
        targetRot.z = 0.0f;
        if (!Input.GetMouseButton(1))
            target.rotation = targetRot;
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
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
