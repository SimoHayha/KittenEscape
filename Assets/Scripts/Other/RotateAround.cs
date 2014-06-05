using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour
{
	public GameObject	target;
	public float		rotationSpeed = 1.0f;
	public bool			isParticle = true;
	
	void Start()
	{
	
	}
	
	void Update()
	{
		if (isParticle)
		{
			transform.RotateAround(target.transform.localPosition, transform.forward, rotationSpeed * Time.deltaTime);
		}
		else
			transform.RotateAround(target.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
	}
}
