using UnityEngine;
using System.Collections;

public class IntroManager : MonoBehaviour {

    public CameraManager CameraManager = null;
    public ControllerManager ControllerManager = null;
    public GameObject SplashScreenPlane = null;
    public LevelInfo LevelInfo = null;
    public Font Font = null;

    public Rect NameDim;
    public int NameFontSize = 50;
    public Rect TimeDim;
    public int TimeFontSize = 15;
    public Rect KittyDim;
    public int KittyFontSize = 15;

    public bool TimedIntro = true;
    public float TimeIntro = 2.5f;
    public bool FadedIntro = true;
    public float FadeIntro = 1.0f;

    private float nextTick = 0.0f;
    private float currentFade = 0.0f;
    private Camera oldCamera = null;
    private bool activeFade = false;
    private Camera introCamera = null;
    private float currentAlpha = 1.0f;


	void Start () {
        introCamera = CameraManager.IntroCamera;
        oldCamera = Camera.main;
        CameraManager.SetStartLevelAnimMode(true);
        ControllerManager.SetStartLevelAnimMode(true);

        nextTick = Time.time + TimeIntro;
	}
	
	void Update () {
	    if (Time.time > nextTick)
        {
            if (FadedIntro)
            {
                activeFade = true;
                ControllerManager.LaunchIntroCameraFall();
            }
            else
            {
                CameraManager.SetStartLevelAnimMode(false);
                ControllerManager.SetStartLevelAnimMode(false);
                enabled = false;
            }
        }

        if (activeFade)
        {
            currentAlpha = Mathf.Lerp(1.0f, 0.0f, currentFade / FadeIntro);

            Color color = SplashScreenPlane.GetComponent<MeshRenderer>().material.color;
            color.a = currentAlpha;
            SplashScreenPlane.GetComponent<MeshRenderer>().material.color = color;

            currentFade += Time.deltaTime;

            if (currentFade > FadeIntro)
            {
                SplashScreenPlane.SetActive(false);
                gameObject.SetActive(false);
            }
        }
	}

    void OnGUI()
    {
        GUI.skin.label.font = Font;
        GUI.skin.label.fontSize = NameFontSize;
        Color tmp = GUI.skin.label.normal.textColor;
        tmp.a = currentAlpha;
        GUI.skin.label.normal.textColor = tmp;
        GUI.Label(new Rect(Screen.width * NameDim.x, Screen.height * NameDim.y, Screen.width * NameDim.width, Screen.height * NameDim.height), new GUIContent(LevelInfo.Name));
        GUI.skin.label.fontSize = TimeFontSize;
        GUI.Label(new Rect(Screen.width * TimeDim.x, Screen.height * TimeDim.y, Screen.width * TimeDim.width, Screen.height * TimeDim.height), new GUIContent(@"Time " + LevelInfo.Time.ToString()));
        GUI.skin.label.fontSize = KittyFontSize;
        GUI.Label(new Rect(Screen.width * KittyDim.x, Screen.height * KittyDim.y, Screen.width * KittyDim.width, Screen.height * KittyDim.height), new GUIContent(@"Kitty " + LevelInfo.Kittens.ToString()));
    }
}
