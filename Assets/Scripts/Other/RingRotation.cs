using UnityEngine;
using System.Collections;

public class RingRotation : MonoBehaviour
{
	public float	rotationSpeed = 1.0f;
	
	void Update()
	{
		transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);
	}
}
