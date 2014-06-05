using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
        transform.LookAt(Camera.main.transform.position);
	}
}
