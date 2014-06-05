using UnityEngine;
using System.Collections;

public class LevelInfo : MonoBehaviour {

    public static LevelInfo Instance;

    // Game
    public bool Win = false;
    public bool Loose = false;
    public bool Dead = false;

    // Base stats
    public string Name = "Noname";
    public float Time = 300.0f;
    public int Kittens = 4;
    public int AvailableShot = 10;
    private float timeSave;

    // Evolutive stats
    public float CurrentTimer = 0.0f;
    private int currentKittens = 0;
    public int CurrentKittens
    {
        get
        {
            return currentKittens;
        }

        set
        {
            currentKittens = value;

            float newSpeed = 1.0f;

            if (currentKittens == 1)
                newSpeed = 2.0f;
            else if (currentKittens == 2)
                newSpeed = 3.0f;
            else if (currentKittens == 3)
                newSpeed = 5.0f;

            GameObject[] cats = GameObject.FindGameObjectsWithTag("Cat");
            foreach (GameObject o in cats)
                o.GetComponent<MecanimNavAgent>().Speed = newSpeed;
        }
    }

    private bool cinematicLaunched = false;
    public bool saveScreenGUI = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timeSave = Time;
    }

    void Update()
    {
        Time -= UnityEngine.Time.deltaTime;

        if (CurrentKittens >= Kittens)
            Win = true;
        if (Time <= 0.0f)
            Loose = true;

        //if (Dead)
        //{
        //    UnityEngine.Time.timeScale = 0.0f;
        //    saveScreenGUI = true;
        //    Loose = true;
        //}

        if (Win && !cinematicLaunched)
        {
            UnityEngine.Time.timeScale = 0.0f;
            saveScreenGUI = true;
            /*cinematicLaunched = true;
            DoWinCinematic();*/
        }

        if ((Loose && !cinematicLaunched) || (Dead && !cinematicLaunched))
        {
            UnityEngine.Time.timeScale = 0.0f;
            saveScreenGUI = true;
            /*cinematicLaunched = true;
            DoLoseCinematic();*/
        }
    }

    void OnGUI()
    {
        if (saveScreenGUI)
        {
            GUI.Label(new Rect(Screen.width * 0.475f, Screen.height * 0.4f, 100.0f, 20.0f), new GUIContent("Save screens ?"));
            if (GUI.Button(new Rect(Screen.width * 0.425f, Screen.height * 0.45f, 100.0f, 20.0f), new GUIContent("Yes")))
            {
                ScreenshotManager.Instance.SaveScreens(true);
                cinematicLaunched = true;
                saveScreenGUI = false;
                if (Win)
                {
                    UnityEngine.Time.timeScale = 1.0f;
                    DoWinCinematic();
                }
                if (Loose || Dead)
                {
                    UnityEngine.Time.timeScale = 1.0f;
                    DoLoseCinematic();
                }
            }
            if (GUI.Button(new Rect(Screen.width * 0.525f, Screen.height * 0.45f, 100.0f, 20.0f), new GUIContent("No")))
            {
                ScreenshotManager.Instance.SaveScreens(false);
                cinematicLaunched = true;
                saveScreenGUI = false;
                if (Win)
                {
                    UnityEngine.Time.timeScale = 1.0f;
                    DoWinCinematic();
                }
                if (Loose || Dead)
                {
                    UnityEngine.Time.timeScale = 1.0f;
                    DoLoseCinematic();
                }
            }
        }
    }

    void DoWinCinematic()
    {
        if (ProfilManager.Profil)
        {
            ProfilManager.Profil.Current.TimeIngame += (int)(timeSave - Time);
            ProfilManager.Profil.SaveCurrent();
        }
        /*CameraManager.Instance.SetOutroMode(true);
        ControllerManager.Instance.SetOutroMode(true);
        OutroManager.Instance.started = true;*/

        Application.LoadLevel("WinScene");
    }

    void DoLoseCinematic()
    {
        if (ProfilManager.Profil)
        {
            ProfilManager.Profil.Current.TimeIngame += (int)(timeSave - Time);
            ProfilManager.Profil.SaveCurrent();
        }
        CameraManager.Instance.SetOutroMode(true);
        ControllerManager.Instance.SetOutroMode(true);
        DetonatorExplosion.Instance.Activate();
    }

}
