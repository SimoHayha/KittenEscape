using UnityEngine;
using System.Collections;

public class FPSCamera : MonoBehaviour {

    public Texture Crosshair;
    public Transform head;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

    void Update()
    {
        transform.position = head.position;

        if (!camera.enabled)
            return;

        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }

        head.transform.rotation = transform.rotation;
    }

    void Start()
    {

    }

    void OnGUI()
    {
        if (camera.enabled)
        {
            if (Time.time != 0 && Time.timeScale != 0)
                GUI.DrawTexture(new Rect(Screen.width / 2 - (Crosshair.width * 0.5f), Screen.height / 2 - (Crosshair.height * 0.5f), Crosshair.width, Crosshair.height), Crosshair);
        }
    }
}
