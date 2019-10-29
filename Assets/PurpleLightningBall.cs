using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleLightningBall : Projectile {

	// Update is called once per frame
	void FixedUpdate () {
		gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, target, 2f * Time.deltaTime);
	}


}
