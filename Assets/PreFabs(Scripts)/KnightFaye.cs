using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightFaye : Knight {

	//Energy Attack
	public GameObject Meteor;

	public override void Start(){
		base.Start ();
		maxHealth = 65;
		setCurrentHealth (maxHealth);
		baseAttackPower = 50;
		base.setAttackPower (baseAttackPower);
	}

	public override IEnumerator attacking(int n, ChessPiece t){

		turning = true;
		var targetRotation = Quaternion.LookRotation (t.transform.position - transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, 5 * Time.deltaTime);

		if (n == 0) {
			animationController.SetBool ("Attack1", true);
			Vector3 meteorSpawnPosition = BoardManager.Instance.getTileCenter (t.CurrentX, t.CurrentY);
			meteorSpawnPosition.y += 5;
			GameObject meteor = Instantiate (Meteor,meteorSpawnPosition , Quaternion.identity) as GameObject;
			meteor.GetComponent<Projectile> ().setAttackPower (attackPower);
			yield return new WaitForSeconds(1.5f);
			animationController.SetBool ("Attack1", false);
		} 
		else if (n == 1) {
			animationController.SetBool ("Attack2", true);
			yield return new WaitForSeconds(0.5f);
			animationController.SetBool ("Attack2", false);
		}
		turning = false;
		//Other kinds of attacks beside close and range
	}
}
