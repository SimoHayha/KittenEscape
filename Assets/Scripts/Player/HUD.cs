using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    public static HUD Instance;

    public CastManager CastManager;

    public float StrenghtBarFadingRatio = 2.5f;

    public bool CatchCatActive = false;

    public float CatchCatRatio = 0.0f;

    public GUIStyle MyStyle;

    public bool CatTrapEnabled = false;

    public bool ShowTime = true;

    private float timeShow = 0.0f;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (ProfilManager.Profil.Current.canShoot)
            UpdateStrengthBar();

        if (CatchCatActive)
            UpdateCatchBar();

        UpdateMinimalGUI();
    }

    void UpdateMinimalGUI()
    {
        if (ShowTime)
            GUI.Label(new Rect(Screen.width * 0.445f, Screen.height * 0.05f, 100.0f, 20.0f), new GUIContent(LevelInfo.Instance.Time.ToString()), MyStyle);
        if (CatTrapEnabled)
            GUI.Label(new Rect(Screen.width * 0.35f, Screen.height * 0.2f, 100.0f, 20.0f), new GUIContent("Bring it to the teleporter"), MyStyle);
    }

    void UpdateCatchBar()
    {
        GUI.Label(new Rect(Screen.width * 0.38f, Screen.height * 0.2f, Screen.width * 0.7f, Screen.height * 0.3f), new GUIContent("Press E to catch it !"), MyStyle);
        //GUI.HorizontalScrollbar(new Rect(Screen.width * 0.0825f, Screen.height * 0.1f, Screen.width * 0.8f, 10.0f), 0.0f, CatchCatRatio, 0.0f, 1.0f);
    }

    void UpdateStrengthBar()
    {
        float r, g, b, a;

        if (Input.GetMouseButton(1) && CastManager.CanShootBullet)
        {
            // show bar
            timeShow += Time.deltaTime;
        }
        else
        {
            // hide bar
            timeShow -= Time.deltaTime;
        }

        //Debug.Log(timeShow);
        timeShow = Mathf.Clamp(timeShow, 0.0f, StrenghtBarFadingRatio);
        a = Mathf.Lerp(0.0f, 1.0f, timeShow / StrenghtBarFadingRatio);

        if (Input.GetMouseButton(1))
        {
            r = Mathf.Lerp(0.0f, 1.0f, CastManager.BulletVelocity / CastManager.MaxBulletVelocity);
            g = Mathf.Lerp(1.0f, 0.0f, CastManager.BulletVelocity / CastManager.MaxBulletVelocity);
            b = 0.25f;
        }
        else
        {
            r = 0.0f;
            g = 1.0f;
            b = 0.25f;
        }

        Color origin = GUI.color;

        GUI.color = new Color(r, g, b, a);
        GUI.VerticalScrollbar(new Rect(Screen.width * 0.95f, Screen.height * 0.45f, 10.0f, Screen.height * 0.5f), 0.0f, CastManager.BulletVelocity, 10.0f, 0.0f);

        GUI.color = origin;
    }

    void Aiming()
    {
        //if (timeShow < 0.0f)
        timeShow = 0.0f;
    }

    void ShootBullet()
    {
        //if (timeShow > 2.5f)
        timeShow = StrenghtBarFadingRatio;
    }
}
