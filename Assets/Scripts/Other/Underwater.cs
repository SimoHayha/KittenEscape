using UnityEngine;
using System.Collections;

public class Underwater : MonoBehaviour
{
    public static Underwater Instance;

    public bool IsUnderwater = false;
	public Color color;
	
    void Awake()
    {
        Instance = this;
    }

	void Start()
	{
	}
	
	void Update()
	{
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "MainCamera")
        {
            DroneBehavior.Instance.IsUnderwater = true;
            // Camera splash in
			GameObject.Find("water_enter").GetComponent<AudioSource>().enabled = true;
			GameObject.Find("water_exit").GetComponent<AudioSource>().enabled = false;
			
			// Underwater sound effect : ON
			GameObject.Find("ocean_plane").GetComponent<AudioSource>().enabled = true;
			
			// Underwater visual effects : ON
			GameObject.Find("TPS").GetComponent<Blur>().enabled = true;
			GameObject.Find("TPS").GetComponent<GlobalFog>().enabled = true;
			GameObject.Find("TPS").GetComponent<GlobalFog>().globalFogColor = color;
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "MainCamera")
		{
            DroneBehavior.Instance.IsUnderwater = false;
            // Camera splash out
			GameObject.Find("water_exit").GetComponent<AudioSource>().enabled = false;
			GameObject.Find("water_exit").GetComponent<AudioSource>().enabled = true;
			
			// Underwater sound effect : OFF
			GameObject.Find("ocean_plane").GetComponent<AudioSource>().enabled = false;
			
			// Underwater visual effects : OFF
			GameObject.Find("TPS").GetComponent<Blur>().enabled = false;
			GameObject.Find("TPS").GetComponent<GlobalFog>().enabled = false;
		}
	}
}
