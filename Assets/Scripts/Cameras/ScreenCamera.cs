using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MouseOrbit))]

public class ScreenCamera : MonoBehaviour {

    public static ScreenCamera Instance;

    public MouseOrbit Orbit;
    public GameObject Cat;
    public GameObject Trap;

    void Awake()
    {
        Instance = this;
    }

	void Start () {
    }
	
	void Update () {
        bool validate = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (validate)
        {
            ScreenshotManager.Instance.takeHiResShot = true;
            ScreenshotManager.Instance.ObjectToDisable = Cat;
            CameraManager.Instance.SetScreenshotMode(false);
            ControllerManager.Instance.SetScreenshotMode(false);
            Trap.SetActive(true);
        }
	}
}
