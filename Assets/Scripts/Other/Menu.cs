using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	// --- Menu textures ---
	public Texture		menu_normal;
	public Texture		menu_hover;
	// ---------------------
	
	// --- Menu audio ---
	public AudioClip	menu_sound_hover;
	public AudioClip	menu_sound_click;
	public AudioClip	menu_sound_enter;
	// ------------------
	
	// --- Camera Animations ---
	private Vector3		cameraOrigin;
	private Vector3		cameraCurrent;
	private float		timer;
	public Transform	target;
	public float		animTime = 1.5f;
	// -------------------------
	
	// --- Fading animations ---
	private float		alphaBlack;
	private bool		fadeToBlack;
	private bool		newGame;
	private bool		exitGame;
	public Texture		blackTexture;
	// -------------------------
	
	// --- Load Game Menu ---
	private float	loadWindowW;
	private float	loadWindowH;
	private Rect	loadRect;
	// ----------------------

    public bool b = false;
    public Texture ScreenOn;
    public Texture ScreenOff;
    private Texture currentScreenState;

    void Start()
    {
        newGame = false;
        exitGame = false;
        timer = float.MaxValue;
        cameraOrigin = Camera.main.transform.position;
        cameraCurrent = cameraOrigin;
        timer = float.MaxValue;
        fadeToBlack = false;
        alphaBlack = 0.0f;
        hideSettingsMenu();
        hideVideoSettings();
        hideAudioSettings();
        hideExtrasMenu();
        hideLoadMenu();

    }

	
	void OnGUI()
	{
		// HANDLE FOR DISPLAYING SAVED GAMES
		if (GameObject.Find("Menu_load_back").GetComponent<GUITexture>().enabled == true)
		{
			loadRect = GUI.Window(0, loadRect, DrawLoadWindow, "Choose a savegame");
		}
		
		// SCREEN FADING TO BLACK HANDLES
		if (alphaBlack >= 1.0f && newGame)
			Application.LoadLevel("Test3C");
		else if (alphaBlack >= 1.0f && exitGame)
			Application.Quit();
		
		// BLACK FADING SETTINGS
		if (fadeToBlack)
		{
			if (alphaBlack < 1.0f)
				alphaBlack += 0.025f;
			GUI.color = new Color(0.0f, 0.0f, 0.0f, alphaBlack);
			GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), blackTexture);
		}
	}
	
	void DrawLoadWindow(int id)
	{
        float offY = 40.0f;
        float addY = 50.0f;

        foreach (string s in ProfilManager.Profil.ProfilsName)
        {
            if (GUI.Button(new Rect(loadRect.width * 0.225f, offY, 200.0f, 40.0f), new GUIContent(s)))
                ProfilManager.Profil.SetCurrentProfil(s);

            offY += addY;
        }
	}
	
	void Update()
	{
        if (b)
        {
            if (ProfilManager.Profil.Current.ScreenEnabled)
                currentScreenState = ScreenOn;
            else
                currentScreenState = ScreenOff;

            this.guiTexture.texture = currentScreenState;
        }
		// --- Resize handler ---
		loadWindowW = Screen.width / 4.0f;
		loadWindowH = Screen.height / 2.6f;
		loadRect = new Rect(Screen.width / 2 - loadWindowW / 2, Screen.height / 2 - loadWindowH / 2, loadWindowW, loadWindowH);
		// ----------------------
		
		if (timer < animTime)
		{
            if (target)
            {
                Camera.main.transform.position = Vector3.Lerp(cameraCurrent, target.position, timer / animTime);
                timer += Time.deltaTime;
                if (Camera.main.transform.position == target.transform.position)
                {
                    cameraCurrent = Camera.main.transform.position;
                    timer = float.MaxValue;
                }
            }
		}
		else if (timer >= animTime)
		{
			timer = float.MaxValue;
		}
	}
	
	void OnMouseEnter()
	{
		unselectAllMenus();
		this.guiTexture.texture = menu_hover;
		audio.PlayOneShot(menu_sound_hover);	
	}
	
	void OnMouseExit()
	{
		this.guiTexture.texture = menu_normal;
	}
	
	void OnMouseDown()
	{
		audio.PlayOneShot(menu_sound_click);
		cameraCurrent = Camera.main.transform.position;
		
		// --- MAIN MENU ---
        if (name == "Menu_new_game")
		{
			GameObject.Find("Title").GetComponent<GUITexture>().enabled = false;
			newGame = true;
			hideMainMenu();
			fadeToBlack = true;
				timer = 0.0f;
			audio.PlayOneShot(menu_sound_enter);
		}
		else if (name == "Menu_load_game")
		{
            ProfilManager.Profil.ReloadProfilesNames();
			hideMainMenu();
			showLoadMenu();
		}
		else if (name == "Menu_settings")
		{
			hideMainMenu();
			showSettingsMenu();
		}
		else if (name == "Menu_extras")
		{
			hideMainMenu();
			showExtrasMenu();
		}
		else if (name == "Menu_exit")
		{
			GameObject.Find("Title").GetComponent<GUITexture>().enabled = false;
			audio.PlayOneShot(menu_sound_enter);
			exitGame = true;
			hideMainMenu();
			fadeToBlack = true;
			timer = 0.0f;
            StartCoroutine(Quit());
		}
		// -----------------
		
		// --- LOAD MENU ---
		else if (name == "Menu_load_back")
		{
			hideLoadMenu();
			showMainMenu();
		}
		// -----------------
		
		// --- VIDEO SETTINGS ---
		else if (name == "video_settings_back")
		{
            hideVideoSettings();
			showSettingsMenu();
		}
		// ----------------------
		
		// --- AUDIO SETTINGS ---
		else if (name == "audio_settings_back")
		{
            ProfilManager.Profil.SaveCurrent();
            hideAudioSettings();
			showSettingsMenu();
		}
		// ----------------------
		
		// --- SETTINGS MENU ---
		else if (name == "Menu_settings_video")
		{
			//hideSettingsMenu();
			//showVideoSettings();
		}
		else if (name == "Menu_settings_audio")
		{
			hideSettingsMenu();
			showAudioSettings();
		}
		else if (name == "Menu_settings_back")
		{
			hideSettingsMenu();
			showMainMenu();
			timer = 0.0f;
		}
		// ---------------------
		
		// --- EXTRAS MENU ---
		else if (name == "Menu_extras_gallery")
		{
            ProfilManager.Profil.Current.ScreenEnabled = !ProfilManager.Profil.Current.ScreenEnabled;
            Debug.Log(ProfilManager.Profil.Current.ScreenEnabled);
            if (ProfilManager.Profil.Current.ScreenEnabled)
                currentScreenState = ScreenOn;
            else
                currentScreenState = ScreenOff;
		}
		else if (name == "Menu_extras_back")
		{
            ProfilManager.Profil.SaveCurrent();
			hideExtrasMenu();
			showMainMenu();
			timer = 0.0f;
		}
		// -------------------
		
	}
	
	void unselectAllMenus()
	{
		GameObject.Find("Menu_new_game").guiTexture.texture = GameObject.Find("Menu_new_game").GetComponent<Menu>().menu_normal;
		GameObject.Find("Menu_load_game").guiTexture.texture = GameObject.Find("Menu_load_game").GetComponent<Menu>().menu_normal;
		GameObject.Find("Menu_settings").guiTexture.texture = GameObject.Find("Menu_settings").GetComponent<Menu>().menu_normal;
		GameObject.Find("Menu_extras").guiTexture.texture = GameObject.Find("Menu_extras").GetComponent<Menu>().menu_normal;
		GameObject.Find("Menu_exit").guiTexture.texture = GameObject.Find("Menu_exit").GetComponent<Menu>().menu_normal;
	}
	
	// --- MAIN MENU ---
	void showMainMenu()
	{
		GameObject.Find("Menu_new_game").GetComponent<GUITexture>().enabled = true;
		GameObject.Find("Menu_load_game").GetComponent<GUITexture>().enabled = true;
		GameObject.Find("Menu_settings").GetComponent<GUITexture>().enabled = true;
		GameObject.Find("Menu_extras").GetComponent<GUITexture>().enabled = true;
		GameObject.Find("Menu_exit").GetComponent<GUITexture>().enabled = true;
	}
	
	void hideMainMenu()
	{
		GameObject.Find("Menu_new_game").GetComponent<GUITexture>().enabled = false;
		GameObject.Find("Menu_load_game").GetComponent<GUITexture>().enabled = false;
		GameObject.Find("Menu_settings").GetComponent<GUITexture>().enabled = false;
		GameObject.Find("Menu_extras").GetComponent<GUITexture>().enabled = false;
		GameObject.Find("Menu_exit").GetComponent<GUITexture>().enabled = false;
	}
	// -----------------
	
	// --- LOAD MENU ---
	void showLoadMenu()
	{
		GameObject.Find("Menu_load_back").GetComponent<GUITexture>().enabled = true;
	}
	
	void hideLoadMenu()
	{
		GameObject.Find("Menu_load_back").GetComponent<GUITexture>().enabled = false;
	}
	// -----------------
	
	// --- SETTINGS MENU ---
	void showSettingsMenu()
	{
		GameObject.Find("Menu_settings_video").GetComponent<GUITexture>().enabled = true;
		GameObject.Find("Menu_settings_audio").GetComponent<GUITexture>().enabled = true;
		GameObject.Find("Menu_settings_back").GetComponent<GUITexture>().enabled = true;
	}
	
	void hideSettingsMenu()
	{
		GameObject.Find("Menu_settings_video").GetComponent<GUITexture>().enabled = false;
		GameObject.Find("Menu_settings_audio").GetComponent<GUITexture>().enabled = false;
		GameObject.Find("Menu_settings_back").GetComponent<GUITexture>().enabled = false;
	}
	// ---------------------
	
	// --- SHOW VIDEO OPTIONS ---
	void showVideoSettings()
	{
		GameObject.Find("video_settings_back").GetComponent<GUITexture>().enabled = true;
	}
	// --------------------------
	
	// --- HIDE VIDEO OPTIONS ---
	void hideVideoSettings()
	{
		GameObject.Find("video_settings_back").GetComponent<GUITexture>().enabled = false;
	}
	// --------------------------
	
	// --- SHOW AUDIO OPTIONS ---
	void showAudioSettings()
	{
		GameObject.Find("audio_settings_back").GetComponent<GUITexture>().enabled = true;
	}
	// --------------------------
	
	// --- HIDE AUDIO OPTIONS ---
	void hideAudioSettings()
	{
		GameObject.Find("audio_settings_back").GetComponent<GUITexture>().enabled = false;
	}
	// --------------------------
	
	// --- EXTRAS MENU ---
	void showExtrasMenu()
	{
		GameObject.Find("Menu_extras_gallery").GetComponent<GUITexture>().enabled = true;
        if (ProfilManager.Profil.Current.ScreenEnabled)
            currentScreenState = ScreenOn;
        else
            currentScreenState = ScreenOff;
        this.guiTexture.texture = currentScreenState;
        //GameObject.Find("Menu_extras_gallery").GetComponent<GUITexture>().texture = currentScreenState;
		GameObject.Find("Menu_extras_back").GetComponent<GUITexture>().enabled = true;
	}
	
	void hideExtrasMenu()
	{
		GameObject.Find("Menu_extras_gallery").GetComponent<GUITexture>().enabled = false;
		GameObject.Find("Menu_extras_back").GetComponent<GUITexture>().enabled = false;
	}
	// -------------------

    IEnumerator Quit()
    {
        yield return new WaitForSeconds(3.0f);
        Debug.Log("Leave");
        Application.Quit();
    }
}
