using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour {

    public static ControllerManager Instance;

    // Player
    public HUD HUD = null;
    public ControllerTest ControllerTest = null;
    public PlayerBehavior PlayerBehavior = null;

    // Drone
    public DroneBehavior DroneBehavior = null;
    public CastManager CastManager = null;
    public DroneHUD DroneHUD = null;

    // Camera controllers
    public FPSCamera FPSCamera = null;
    public SmoothDamp SmoothDamp = null;
    public GalleryCamera GalleryCamera = null;
    public IntroCamera IntroCamera = null;

    void Awake()
    {
        Instance = this;
    }

    public void SetGalleryMode(bool mode)
    {
        HUD.enabled = !mode;
        if (mode)
            ControllerTest.ForceStop();
        ControllerTest.enabled = !mode;
        PlayerBehavior.enabled = !mode;
        DroneBehavior.enabled = !mode;
        CastManager.enabled = !mode;
        FPSCamera.enabled = !mode;
        SmoothDamp.enabled = !mode;
        GalleryCamera.SetEnabled(mode);
    }

    public void SetGUIMode(bool mode)
    {
        HUD.enabled = !mode;
        if (mode)
            ControllerTest.ForceStop();
        ControllerTest.enabled = !mode;
        PlayerBehavior.enabled = !mode;
        DroneBehavior.enabled = !mode;
        CastManager.enabled = !mode;
        FPSCamera.enabled = !mode;
        SmoothDamp.enabled = !mode;
        GalleryCamera.enabled = !mode;
    }

    public void LaunchIntroCameraFall()
    {
        IntroCamera.LaunchFallAnimation();
    }

    public void SetStartLevelAnimMode(bool mode)
    {
        HUD.enabled = !mode;
        ControllerTest.enabled = !mode;
        PlayerBehavior.enabled = !mode;
        DroneBehavior.enabled = !mode;
        CastManager.enabled = !mode;
        FPSCamera.enabled = !mode;
        SmoothDamp.enabled = !mode;
        GalleryCamera.enabled = !mode;
    }

    public void SetDroneMode(bool mode)
    {
        HUD.enabled = mode;
        ControllerTest.ForceAnimationStop();
        //ControllerTest.enabled = !mode;
        PlayerBehavior.enabled = !mode;
    }

    public void SetScreenshotMode(bool mode)
    {
        HUD.enabled = !mode;
        ControllerTest.enabled = !mode;
        PlayerBehavior.enabled = !mode;
        DroneBehavior.enabled = !mode;
        CastManager.enabled = !mode;
        FPSCamera.enabled = !mode;
        SmoothDamp.enabled = !mode;
        GalleryCamera.enabled = !mode;
        DroneHUD.enabled = !mode;
    }

    public void SetOutroMode(bool mode)
    {
        HUD.enabled = !mode;
        ControllerTest.enabled = !mode;
        PlayerBehavior.enabled = !mode;
        DroneBehavior.enabled = !mode;
        CastManager.enabled = !mode;
        FPSCamera.enabled = !mode;
        SmoothDamp.enabled = !mode;
        GalleryCamera.enabled = !mode;
        DroneHUD.enabled = !mode;
    }
}
