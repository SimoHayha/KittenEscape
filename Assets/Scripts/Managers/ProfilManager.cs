using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ProfilManager : MonoBehaviour {

    public static ProfilManager Profil;

    public string PathPrefix = @"file://";
    public string PathAppData = null;
    public string PathProfiles = null;

    public List<Profil> Profils = new List<Profil>();
    public Profil Current = null;

    public TextMesh TotalKitten;
    public TextMesh TotalTime;

    public bool displayLoad = false;
    public bool displayNewProfile = false;

    public List<string> ProfilsName = new List<string>();

    private string stringToEdit = "";

    void Awake()
    {
        Profil = this;
    }

	void Start () {
        PathAppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
        PathProfiles = PathAppData + @"\KittenEscape\Saves";

        if (!CheckForDirectory())
        {
            Debug.Log("Profile directory does not exist");

            if (!CreateProfilDirectory())
                Debug.Log("Cannot create profile directory");
        }

        GetProfilesNames();

        if (PlayerPrefs.GetString("Profil") != null && PlayerPrefs.GetString("Profil").Length > 0)
        {
            if (!SetCurrentProfil(PlayerPrefs.GetString("Profil")))
                CreateProfil("Default");
        }
        else
            CreateProfil("Default");

        SetCurrentMusic(Current.MusicVolume);
        SetCurrentVoice(Current.VoiceVolume);
    }

    void GetProfilesNames()
    {
        string[] Files = Directory.GetFiles(PathProfiles);

        foreach (string s in Files)
            ProfilsName.Add(Path.GetFileNameWithoutExtension(s));
    }

    public void ReloadProfilesNames()
    {
        ProfilsName.Clear();
        GetProfilesNames();
    }

    void Update()
    {
        if (TotalKitten && Current != null)
            TotalKitten.text = "Total kittens : " + Current.TotalKitten.ToString();
        if (TotalTime && Current != null)
            TotalTime.text = "Total time : " + Current.TimeIngame.ToString();

        if (Input.GetKeyDown(KeyCode.F6))
            SaveCurrent();
        if (Input.GetKeyDown(KeyCode.F7))
            displayLoad = !displayLoad;
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel("Menu");
    }

    void OnGUI()
    {
        if (displayLoad)
        {
            stringToEdit = GUI.TextField(new Rect(Screen.width * 0.4f, Screen.height * 0.4f, 100.0f, 20.0f), stringToEdit);
            if (GUI.Button(new Rect(Screen.width * 0.4f, Screen.height * 0.5f, 100.0f, 20.0f), new GUIContent("Load")))
            {
                LoadProfil(stringToEdit);
                displayLoad = false;
            }
        }

        if (displayNewProfile)
        {
            stringToEdit = GUI.TextField(new Rect(Screen.width * 0.4f, Screen.height * 0.4f, 100.0f, 20.0f), stringToEdit);
            if (GUI.Button(new Rect(Screen.width * 0.4f, Screen.height * 0.5f, 100.0f, 20.0f), new GUIContent("Create")))
            {
                CreateProfil(stringToEdit);
                displayNewProfile = false;
            }
        }
    }

    public void AddTime(int time)
    {
        if (Current != null)
            Current.TimeIngame += time;
    }

    public void AddKitten(int kitten)
    {
        if (Current != null)
            Current.TotalKitten += kitten;
    }

    public bool SaveCurrent()
    {
        try
        {
            string filename = PathProfiles + "\\" + Current.Name + ".profil";

            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, Current);

                Debug.Log("Profil saved : " + filename);
            }
        }
        catch (IOException e)
        {
            Debug.Log(e.Message);
            return false;
        }

        return true;
    }

    public bool SetCurrentProfil(string profil)
    {
        foreach (Profil p in Profils)
        {
            if (p.Name == profil)
            {
                Current = p;
                PlayerPrefs.SetString("Profil", profil);
                return true;
            }
        }

        if (LoadProfil(profil))
            return true;

        return false;
    }

    public bool SetCurrentProfil(Profil profil)
    {
        Current = profil;
        PlayerPrefs.SetString("Profil", profil.Name);
        SaveCurrent();

        return true;
    }

    string SerializeObject(Object pObject)
    {
        return null;
    }

    bool CheckForDirectory()
    {
        return System.IO.Directory.Exists(PathProfiles);
    }

    bool CreateProfilDirectory()
    {
        try
        {
            System.IO.Directory.CreateDirectory(PathProfiles);
        }
        catch (System.IO.IOException)
        {
            return false;
        }

        return true;
    }

    bool CreateProfil(string profil)
    {
        if (profil != null && profil.Length > 0)
        {
            try
            {
                string filename = PathProfiles + "\\" + profil + ".profil";

                Profil p = new Profil();
                p.Name = profil;

                using (Stream stream = File.Open(filename, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, p);
                    stream.Close();
                    Debug.Log("Profil saved : " + filename);
                }

                Debug.Log("Profile " + profil + " created");

                SetCurrentProfil(p);
            }
            catch (IOException e)
            {
                Debug.Log(e.Message);
                return false;
            }

            return true;
        }

        return false;
    }

    bool LoadAllProfil()
    {
        try
        {
            string filename = PathProfiles + "\\";

            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();

                Profil p = (Profil)bin.Deserialize(stream);
                Current = p;
                stream.Close();

                Debug.Log("Profil " + p.Name + " loaded");
            }
        }
        catch (IOException e)
        {
            Debug.Log(e.Message);
            return false;
        }

        return true;
    }

    bool LoadProfil(string profil)
    {
        if (profil != null || profil.Length > 0)
        {
            try
            {
                string filename = PathProfiles + "\\" + profil + ".profil";

                using (Stream stream = File.Open(filename, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    Profil p = (Profil)bin.Deserialize(stream);

                    stream.Close();
                    SetCurrentProfil(p);
                }
            }
            catch (IOException e)
            {
                Debug.Log(e.Message);
                return false;
            }

            return true;
        }

        return false;
    }

    public void SetCurrentMusic(float f)
    {
        Current.MusicVolume = f;

        GameObject[] sources = GameObject.FindGameObjectsWithTag("Music");

        foreach (GameObject o in sources)
            o.GetComponent<AudioSource>().volume = f;
    }

    public void SetCurrentVoice(float f)
    {
        Current.VoiceVolume = f;

        GameObject[] sources = GameObject.FindGameObjectsWithTag("Voice");

        foreach (GameObject o in sources)
            o.GetComponent<AudioSource>().volume = f;
    }
}

[System.Serializable]
public class Profil
{
    public string Name;
    public int TimeIngame;
    private int totalKitten;
    public int TotalKitten
    {
        get
        {
            return totalKitten;
        }

        set
        {
            totalKitten = value;
            if (totalKitten >= 4)
                canSprint = true;
            if (totalKitten >= 8)
                canShoot = true;
        }
    }
    public float VoiceVolume;
    public float MusicVolume;
    public bool ScreenEnabled;
    public bool canSprint;
    public bool canShoot;

    public Profil()
    {
        Name = "Default";
        TimeIngame = 0;
        TotalKitten = 0;
        VoiceVolume = 1.0f;
        MusicVolume = 1.0f;
        ScreenEnabled = true;
        canSprint = false;
        canShoot = false;
    }
}