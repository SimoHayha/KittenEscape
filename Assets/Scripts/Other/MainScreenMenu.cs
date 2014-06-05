using UnityEngine;
using System.Collections;

public class MainScreenMenu : MonoBehaviour
{
	public AudioClip	theme;
	private float		offset_top;
	private float		button_offset;
	private float		button_w;
	private float		button_h;
	
	void Start()
	{
		AudioSource.PlayClipAtPoint(theme, Vector3.zero);
		offset_top = Screen.height * 0.3f;
		button_offset = 45.0f;
		button_w = Screen.width * 0.2f;
		button_h = Screen.height * 0.08f;
	}
	
	void Update()
	{
	}
}
