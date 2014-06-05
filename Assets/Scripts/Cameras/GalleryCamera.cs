using UnityEngine;
using System.Collections;

public class GalleryCamera : MonoBehaviour {

    public Transform StartPoint = null;
    public Transform EndPoint = null;
    public Transform Board = null;

    public float TimeForLerp = 5.0f;

    private float lerpTime = 0.0f;
    private Camera camera = null;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Board)
        {
            if (lerpTime <= TimeForLerp)
            {
                Vector3 dir = Board.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpTime / TimeForLerp);
                transform.position = Vector3.Lerp(StartPoint.position, EndPoint.position, lerpTime / TimeForLerp);

                lerpTime += Time.deltaTime;
            }
            else
            {
                transform.position = EndPoint.position;
                transform.rotation = EndPoint.rotation;

                if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
                    camera.fieldOfView += 2.0f;
                if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
                    camera.fieldOfView -= 2.0f;

                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 40.0f, 80.0f);
            }
        }
    }

    public void SetEnabled(bool b)
    {
        enabled = b;

        lerpTime = 0.0f;
        transform.rotation = StartPoint.rotation;
        if (camera)
            camera.fieldOfView = 60.0f;
    }
}
