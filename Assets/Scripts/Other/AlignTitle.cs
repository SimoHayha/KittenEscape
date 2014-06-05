using UnityEngine;
using System.Collections;

public class AlignTitle : MonoBehaviour
{
	public float	pos_x;
	public float	pos_y;
	
	void Start()
	{
		transform.position = new Vector3(pos_x, pos_y, 0.0f);
	}
}
