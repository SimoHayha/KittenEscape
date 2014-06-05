using UnityEngine;
using System.Collections;

public class AudioSettings : MonoBehaviour
{
	private bool	bloom;
	
	// --- Load Game Menu ---
	private float	audioSettingsWindowW;
	private float	audioSettingsWindowH;
	private Rect	audioSettingsRect;
	
	private Rect	labelQualityRect;
	private Rect	qualityRect;
	private float	qualitylevel;
	
	private Rect	labelEffectsRect;
	private Rect	bloomRect;
	// ----------------------
	
	void Start()
	{
		qualitylevel = 3;
		bloom = true;
	}
	
	void OnGUI()
	{
		if (guiTexture.enabled == true)
		{
			audioSettingsRect = GUI.Window(0, audioSettingsRect, DrawAudioSettingsWindow, "Audio Settings");
		}
	}
	
	void DrawAudioSettingsWindow(int id)
	{
        float offX = audioSettingsRect.width * 0.12f;
        float addX = audioSettingsRect.width * 0.07f;
        float width = audioSettingsRect.width * 0.065f;
        float height = audioSettingsRect.width * 0.085f;

        Color origin = GUI.color;
        Color empty = Color.white;
        Color fill = Color.black;

        float currentMusic = ProfilManager.Profil.Current.MusicVolume;
        float currentVoice = ProfilManager.Profil.Current.VoiceVolume;

        GUI.Label(new Rect(audioSettingsRect.width * 0.455f, audioSettingsRect.height * 0.1f, 100.0f, 20.0f), new GUIContent("Music"));
        GUI.Label(new Rect(audioSettingsRect.width * 0.455f, audioSettingsRect.height * 0.4f, 100.0f, 20.0f), new GUIContent("Voice"));

        for (int i = 0; i < 11; ++i)
        {
            if (currentMusic >= (float)(i - 0.1f) / 10.0f)
                GUI.color = fill;
            else
                GUI.color = empty;
            if (GUI.Button(new Rect(offX, audioSettingsRect.height * 0.2f, width, height), new GUIContent("")))
            {
                ProfilManager.Profil.SetCurrentMusic((float)i / 10);
            }

            if (currentVoice >= (float)(i - 0.1f) / 10.0f)
                GUI.color = fill;
            else
                GUI.color = empty;
            if (GUI.Button(new Rect(offX, audioSettingsRect.height * 0.5f, width, height), new GUIContent("")))
            {
                ProfilManager.Profil.SetCurrentVoice((float)i / 10);
            }

            offX += addX;
        }

        GUI.color = origin;
		/*GUI.Label(labelEffectsRect, "Effects");
		bloom = GUI.Toggle(bloomRect, bloom, " Bloom");
		GUI.Label(labelQualityRect, "Quality Settings : " + getQualityName());
		qualitylevel = GUI.HorizontalSlider(qualityRect, qualitylevel, 0, 5); 
		qualitylevel = (int)qualitylevel;*/
	}
	
	string getQualityName()
	{
		int lvl = (int)qualitylevel;
		string name = "";
		
		if (lvl == 0)
			name = "Fastest";
		else if (lvl == 1)
			name = "Fast";
		else if (lvl == 2)
			name = "Simple";
		else if (lvl == 3)
			name = "Good";
		else if (lvl == 4)
			name = "Beautiful";
		else if (lvl == 5)
			name = "Fantastic";
		return name;
	}
	
	void Update()
	{
		// --- Resize handler ---
		audioSettingsWindowW = Screen.width / 4.0f;
		audioSettingsWindowH = Screen.height / 2.6f;
		audioSettingsRect = new Rect(Screen.width / 2 - audioSettingsWindowW / 2, Screen.height / 2 - audioSettingsWindowH / 2, audioSettingsWindowW, audioSettingsWindowH);
		
		labelEffectsRect = new Rect(20.0f, 40.0f, audioSettingsWindowW - 50.0f, 20.0f);
		bloomRect = new Rect(20.0f, 80.0f, audioSettingsWindowW - 20.0f, 20.0f);
		
		labelQualityRect = new Rect(20.0f, 140.0f, audioSettingsWindowW - 50.0f, 20.0f);
		qualityRect = new Rect(20.0f, 180.0f, audioSettingsWindowW - 50.0f, 20.0f);
		
		// ----------------------
		
		// --- audio SETTINGS ---
		if (!bloom)
			Camera.main.GetComponent<Bloom>().enabled = false;
		else
			Camera.main.GetComponent<Bloom>().enabled = true;
		QualitySettings.SetQualityLevel((int)qualitylevel);
		// ----------------------
	}
}
