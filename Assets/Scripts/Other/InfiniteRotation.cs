using UnityEngine;
using System.Collections;

public class InfiniteRotation : MonoBehaviour
{
	public float	rotationSpeed = 1.0f;
	public string	direction = "left";
	
	void Update()
	{
		if (direction == "left")
		{
			var xRotation = transform.rotation.eulerAngles.x + (Time.deltaTime * rotationSpeed);
    		var yRotation = transform.rotation.eulerAngles.y + (Time.deltaTime * rotationSpeed);
			transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		}
		else if (direction == "right")
		{
			var xRotation = transform.rotation.eulerAngles.x - (Time.deltaTime * rotationSpeed);
    		var yRotation = transform.rotation.eulerAngles.y - (Time.deltaTime * rotationSpeed);
			transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
		}
	}
}
