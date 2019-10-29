using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopFaye : Bishop {

	//Energy Attack
	public GameObject PurpleEnergyBall;

	public override void Start(){
		base.Start ();
		maxHealth = 85;
		setCurrentHealth (maxHealth);
		baseAttackPower = 60;
		base.setAttackPower (baseAttackPower);
	}

	public override IEnumerator attacking(int n, ChessPiece t){

		turning = true;
		var targetRotation = Quaternion.LookRotation (t.transform.position - transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, 5 * Time.deltaTime);

		if (n == 0) {
			animationController.SetBool ("Attack1", true);
			Vector3 projectileSpawnPosition = BoardManager.Instance.getTileCenter (t.CurrentX, t.CurrentY);
			projectileSpawnPosition.y += 3;
			GameObject meteor = Instantiate (PurpleEnergyBall,projectileSpawnPosition , Quaternion.identity) as GameObject;
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

	/*private void setNodes(ChessPiece t){
		setCardinalDirection (t);
		iTweenPath tweenPath = gameObject.GetComponent<iTweenPath> ();

		Vector3 nodePosition1 = new Vector3 (0, 0, 0);
		Vector3 nodePosition2 = new Vector3 (0, 0, 0);
		Vector3 nodePosition3 = new Vector3 (0, 0, 0);
		Vector3 nodePosition4 = new Vector3 (0, 0, 0);
		Vector3 nodePosition5 = new Vector3 (t.CurrentX, 0.8f, t.CurrentY);


		if (south) {
			nodePosition1 = new Vector3 (CurrentX + 0.5f, 0, CurrentY + 0.5f);
			nodePosition2 = new Vector3 (CurrentX + 0.5f, 0.5f, CurrentY + 0.5f);
			nodePosition3 = new Vector3 (CurrentX + 0.5f, 0, CurrentY - 0.5f);
			nodePosition4 = new Vector3 (CurrentX + 0, 0.8f, CurrentY - 0.5f);//in front
		} 
		else if (east) {
			nodePosition1 = new Vector3 (CurrentX - 0.5f, 0, CurrentY - 0.5f);
			nodePosition2 = new Vector3 (CurrentX - 0.5f, 0.5f, CurrentY + 0.5f);
			nodePosition3 = new Vector3 (CurrentX + 0.5f, CurrentY + 0, 0.5f);
			nodePosition4 = new Vector3 (CurrentX + 0.5f, 0.8f, CurrentY + 0);//in front
		} 
		else if (west) {
			nodePosition1 = new Vector3 (CurrentX + 0.5f, 0, CurrentY + 0.5f);
			nodePosition2 = new Vector3 (CurrentX + 0.5f, 0.5f, CurrentY - 0.5f);
			nodePosition3 = new Vector3 (CurrentX -0.5f, 0, CurrentY -0.5f);
			nodePosition4 = new Vector3 (CurrentX -0.5f, 0.8f, CurrentY + 0);
		} 
		else if (northeast) {
			nodePosition1 = new Vector3 (CurrentX + 0.5f, 0, CurrentY + 0.5f);
			nodePosition2 = new Vector3 (CurrentX + 0.5f, 0.5f, CurrentY + 0.5f);
			nodePosition3 = new Vector3 (CurrentX + 0.5f, 0, CurrentY + 0.5f);
			nodePosition4 = new Vector3 (CurrentX + 0.5f, 0.8f, CurrentY + 0.5f);
		}
		else if (northwest) {
			nodePosition1 = new Vector3 (CurrentX + 0.5f, 0, CurrentY + 0.5f);
			nodePosition2 = new Vector3 (CurrentX + 0.5f, 0.5f, CurrentY - 0.5f);
			nodePosition3 = new Vector3 (CurrentX -0.5f, 0, CurrentY -0.5f);
			nodePosition4 = new Vector3 (CurrentX -0.5f, 0.8f, CurrentY + 0.5f);
		} 
		else if (southeast) {
			nodePosition1 = new Vector3 (CurrentX + 0.5f, 0, CurrentY + 0.5f);
			nodePosition2 = new Vector3 (CurrentX -0.5f, 0.5f, CurrentY + 0.5f);
			nodePosition3 = new Vector3 (CurrentX -0.5f, 0, CurrentY -0.5f);
			nodePosition4 = new Vector3 (CurrentX + 0.5f, 0.8f, CurrentY -0.5f);
		} 
		else if (southwest) {
			nodePosition1 = new Vector3 (CurrentX -0.5f, 0, CurrentY + 0.5f);
			nodePosition2 = new Vector3 (CurrentX + 0.5f, 0.5f, CurrentY + 0.5f);
			nodePosition3 = new Vector3 (CurrentX + 0.5f, 0, CurrentY -0.5f);
			nodePosition4 = new Vector3 (CurrentX -0.5f, 0.8f, CurrentY -0.5f);
		}
		else {
			nodePosition1 = new Vector3 (CurrentX + 0.5f, 0, CurrentY + 0.5f);
			nodePosition2 = new Vector3 (CurrentX -0.5f, 0.5f, CurrentY -0.5f);
			nodePosition3 = new Vector3 (CurrentX -0.5f, 0, CurrentY + 0.5f);
			nodePosition4 = new Vector3 (CurrentX + 0, 0.8f, CurrentY + 0.5f);
		}

		tweenPath.nodes [0] = nodePosition1;
		tweenPath.nodes [1] = nodePosition2;
		tweenPath.nodes [2] = nodePosition3;
		tweenPath.nodes [3] = nodePosition4;
		tweenPath.nodes [4] = nodePosition5;
	}*/

	private void setCardinalDirection(ChessPiece t){
		if (CurrentX > t.CurrentX) {
			if (CurrentY > t.CurrentY)
				southwest = true;
			else if (CurrentY == t.CurrentY)
				west = true;
			else
				northwest = true;
		} 
		else if (CurrentX < t.CurrentX) {
			if (CurrentY > t.CurrentY)
				southeast = true;
			else if (CurrentY == t.CurrentY)
				east = true;
			else
				northeast = true;
		} 
		else {
			if (CurrentY > t.CurrentY)
				south = true;
			else
				north = true;
		}
	}
}
