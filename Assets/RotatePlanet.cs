using UnityEngine;
using System.Collections;

public class RotatePlanet : MonoBehaviour
{
	public GameObject player;
	private float x;
	private float y;
	
	void Start()
	{
	
	}
	
	void Update ()
	{
		
		x = Input.GetAxis("Horizontal");
		y = Input.GetAxis("Vertical");
		
		transform.Rotate(new Vector3(y, 0, x));
	}
}
