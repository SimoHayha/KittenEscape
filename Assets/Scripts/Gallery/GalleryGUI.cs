using UnityEngine;
using System.Collections;

public class GalleryGUI : MonoBehaviour {

    public Gallery Gallery = null;
    public CameraManager CameraManager = null;
    public ControllerManager ControllerManager = null;

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width * 0.05f, Screen.height * 0.8f, Screen.width * 0.125f, Screen.height * 0.075f), new GUIContent("Left")))
        {
            Gallery.Right();
        }

        if (GUI.Button(new Rect(Screen.width * 0.825f, Screen.height * 0.8f, Screen.width * 0.125f, Screen.height * 0.075f), new GUIContent("Right")))
        {
            Gallery.Left();
        }

        if (GUI.Button(new Rect(Screen.width * 0.45f, Screen.height * 0.8f, Screen.width * 0.125f, Screen.height * 0.075f), new GUIContent("Remove")))
        {
            Gallery.Remove();
        }

        if (GUI.Button(new Rect(Screen.width * 0.4425f, Screen.height * 0.1f, Screen.width * 0.125f, Screen.height * 0.075f), new GUIContent("Leave")))
        {
            CameraManager.SetGalleryMode(false);
            CameraManager.SetGUIMode(false);
            ControllerManager.SetGalleryMode(false);
            this.enabled = false;
        }
    }
}
