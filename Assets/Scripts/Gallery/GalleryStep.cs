using UnityEngine;
using System.Collections;

public class GalleryStep : MonoBehaviour {

    public bool ShowGUI = false;

    public CameraManager CameraManager = null;
    public ControllerManager ControllerManager = null;
    public GalleryGUI GalleryGUI = null;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            ControllerManager.SetGUIMode(true);
            CameraManager.SetGUIMode(true);
            ShowGUI = true;
        }
    }

    void OnGUI()
    {
        if (ShowGUI)
        {
            if (GUI.Button(new Rect(Screen.width * 0.35f, Screen.height * 0.3f, Screen.width * 0.3f, Screen.height * 0.1f), new GUIContent("Enter gallery")))
            {
                ShowGUI = false;
                CameraManager.SetGalleryMode(true);
                ControllerManager.SetGalleryMode(true);
                GalleryGUI.enabled = true;
            }

            if (GUI.Button(new Rect(Screen.width * 0.35f, Screen.height * 0.5f, Screen.width * 0.3f, Screen.height * 0.1f), new GUIContent("Leave")))
            {
                ShowGUI = false;
                ControllerManager.SetGUIMode(false);
                CameraManager.SetGUIMode(false);
            }
        }
    }
}
