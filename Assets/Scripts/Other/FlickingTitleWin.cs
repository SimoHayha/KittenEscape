using UnityEngine;
using System.Collections;

public class FlickingTitleWin : MonoBehaviour
{
	public Color flickingColor;
	
	private float	r;
	private float	g;
	private float	b;
	private float	mod;
	public float	flickeringSpeed = 1.0f;
	
	void Start()
	{
		r = 1.0f;
		g = 1.0f;
		b = 1.0f;
		mod = -0.5f;
		flickingColor = new Color(r, g, b, 0.5f);
	}
	
	void Update()
	{
		if (r >= 0.65f)
			mod = -0.5f;
		else if (r <= 0.35f)
			mod = 0.5f;
		
		r += mod * Time.deltaTime * flickeringSpeed;
		g += mod * Time.deltaTime * flickeringSpeed;
		b += mod * Time.deltaTime * flickeringSpeed;
		
		flickingColor = new Color(r, g, b, 0.5f);
		renderer.material.color = flickingColor;
	}
}
