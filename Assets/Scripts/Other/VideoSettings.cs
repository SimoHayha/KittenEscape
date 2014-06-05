using UnityEngine;
using System.Collections;

public class VideoSettings : MonoBehaviour
{
	private bool	bloom;
	
	// --- Load Game Menu ---
	private float	videoSettingsWindowW;
	private float	videoSettingsWindowH;
	private Rect	videoSettingsRect;
	
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
			videoSettingsRect = GUI.Window(0, videoSettingsRect, DrawVideoSettingsWindow, "Video Settings");
		}
	}
	
	void DrawVideoSettingsWindow(int id)
	{
		GUI.Label(labelEffectsRect, "Effects");
		bloom = GUI.Toggle(bloomRect, bloom, " Bloom");
		GUI.Label(labelQualityRect, "Quality Settings : " + getQualityName());
		qualitylevel = GUI.HorizontalSlider(qualityRect, qualitylevel, 0, 5); 
		qualitylevel = (int)qualitylevel;
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
		videoSettingsWindowW = Screen.width / 4.0f;
		videoSettingsWindowH = Screen.height / 2.6f;
		videoSettingsRect = new Rect(Screen.width / 2 - videoSettingsWindowW / 2, Screen.height / 2 - videoSettingsWindowH / 2, videoSettingsWindowW, videoSettingsWindowH);
		
		labelEffectsRect = new Rect(20.0f, 40.0f, videoSettingsWindowW - 50.0f, 20.0f);
		bloomRect = new Rect(20.0f, 80.0f, videoSettingsWindowW - 20.0f, 20.0f);
		
		labelQualityRect = new Rect(20.0f, 140.0f, videoSettingsWindowW - 50.0f, 20.0f);
		qualityRect = new Rect(20.0f, 180.0f, videoSettingsWindowW - 50.0f, 20.0f);
		
		// ----------------------
		
		// --- VIDEO SETTINGS ---
		if (!bloom)
			Camera.main.GetComponent<Bloom>().enabled = false;
		else
			Camera.main.GetComponent<Bloom>().enabled = true;
		// ----------------------
		QualitySettings.SetQualityLevel((int)qualitylevel);
	}
}
