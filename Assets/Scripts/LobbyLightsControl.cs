using UnityEngine;
using System.Collections;

public class LobbyLightsControl : MonoBehaviour
{
	private bool		canSwitch;
	public Material		red;
	public Material		green;
	public GameObject	lights;
	
	void Start()
	{
		canSwitch = false;
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (canSwitch)
			{
				audio.Play();
				lights.SetActive(!lights.active);
				if (lights.activeSelf == true)
					transform.FindChild("Button").renderer.material = green;
				else
					transform.FindChild("Button").renderer.material = red;
			}
		}
	}
	
	void OnTriggerEnter()
	{
		canSwitch = true;
		this.renderer.material.shader = Shader.Find("Self-Illumin/Diffuse");
	}
	
	void OnTriggerExit()
	{
		canSwitch = false;
		this.renderer.material.shader = Shader.Find("Diffuse");
	}
	
}
