using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookReji : Rook {

	public GameObject GreenEnergyBall;

	public override void Start(){
		base.Start ();
		maxHealth = 75;
		setCurrentHealth (maxHealth);
		baseAttackPower = 70;
		base.setAttackPower (baseAttackPower);
	}

	public override IEnumerator attacking(int n, ChessPiece t){

		turning = true;
		var targetRotation = Quaternion.LookRotation (t.transform.position - transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, 5 * Time.deltaTime);

		if (n == 0) {
			animationController.SetBool ("Attack1", true);
			Vector3 projectileSpawnPosition = BoardManager.Instance.getTileCenter (CurrentX, CurrentY);
			GameObject projectile = Instantiate (GreenEnergyBall,projectileSpawnPosition , Quaternion.identity) as GameObject;
			projectile.GetComponent<GreenEnergyBall> ().setAttackPower (attackPower);
			projectile.GetComponent<GreenEnergyBall> ().setTarget (t);
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
