using UnityEngine;
using System.Collections;

public class teleporter : MonoBehaviour {

	public GameObject Target;
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			other.gameObject.transform.position = Target.transform.position;
		}
	}
}
