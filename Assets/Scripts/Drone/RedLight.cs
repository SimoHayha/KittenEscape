using UnityEngine;
using System.Collections;

public class RedLight : MonoBehaviour {

    public Material Red;
    public Material Green;

    public GameObject Led;

	void Start () {
	
	}
	
	void Update () {
        if (CastManager.Instance.CanShootBullet)
            Led.renderer.material = Green;
        else
            Led.renderer.material = Red;
	}
}
