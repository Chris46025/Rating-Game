
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenOrbit : MonoBehaviour {

	public float alpha = 0f;

	public float tilt = 45f;

	void Update ()
	{
		transform.position = new Vector2(0f + (10f * MCos(alpha) * MCos(tilt)) - ( 5f * MSin(alpha) * MSin(tilt)),
			0f + (10f * MCos(alpha) * MSin(tilt)) + ( 5f * MSin(alpha) * MCos(tilt)));
		alpha += 5f;
	}

	float MCos(float value)
	{
		return Mathf.Cos(Mathf.Deg2Rad * value);
	}

	float MSin(float value)
	{
		return Mathf.Sin(Mathf.Deg2Rad * value);
	}

}