using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public static CameraManager Instance;

    public ControllerManager ControllerManager = null;

    public Camera TpsCamera = null;
    public Camera FpsCamera = null;
    public Camera GalleryCamera = null;
    public Camera IntroCamera = null;
    public GameObject ScreenCam = null;
    public GameObject OutroCamera = null;

    private bool inGallery = false;
    private bool inGUI = false;
    private bool inCinematique = false;
    private bool inScreen = false;

    void Awake()
    {
        Instance = this;
    }

	void Start () {
        TpsCamera.enabled = true;
        FpsCamera.enabled = false;
        if (GalleryCamera)
            GalleryCamera.enabled = false;
	}
	
	void Update () {
        if (!inGallery && !inGUI && !inCinematique && !inScreen)
        {
            if (Input.GetMouseButtonDown(1))
            {
                TpsCamera.enabled = false;
                FpsCamera.enabled = true;
                FpsCamera.transform.rotation = TpsCamera.transform.rotation;
                ControllerManager.SetDroneMode(true);

            }
            if (Input.GetMouseButtonUp(1))
            {
                TpsCamera.enabled = true;
                FpsCamera.enabled = false;
                ControllerManager.SetDroneMode(false);
            }
        }
	}

    public void SetGalleryMode(bool mode)
    {
        inGallery = mode;
        TpsCamera.enabled = !mode;
        GalleryCamera.enabled = mode;
    }

    public void SetGUIMode(bool mode)
    {
        inGUI = mode;
    }

    public void SetStartLevelAnimMode(bool mode)
    {
        TpsCamera.enabled = !mode;
        IntroCamera.enabled = mode;
        inCinematique = mode;
        ControllerManager.SetStartLevelAnimMode(mode);
    }

    public void SetScreenshotMode(bool mode)
    {
        if (mode)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;
        inScreen = mode;
        ScreenCam.SetActive(mode);
        TpsCamera.enabled = !mode;
    }

    public void SetOutroMode(bool mode)
    {
        TpsCamera.enabled = !mode;
        OutroCamera.SetActive(mode);
    }
}
