using UnityEngine;
using System.Collections;

public class DroneLamp : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!Input.GetMouseButton(1))
            transform.LookAt(target);
	}
}
