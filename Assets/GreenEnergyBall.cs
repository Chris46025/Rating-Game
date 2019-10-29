using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnergyBall : Projectile {

	// Update is called once per frame
	void FixedUpdate () {
		gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, target, 1f * Time.deltaTime);
	}
}
