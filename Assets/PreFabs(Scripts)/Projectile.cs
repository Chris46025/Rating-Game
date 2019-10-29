using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {
	// Use this for initialization
	private int attackPower;
	public Vector3 target;

	void Start () {
		//AudioSource sound = gameObject.GetComponent<AudioSource> ();
		//sound.Play ();
		StartCoroutine (AutoDestroy(15));
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "ChessPiece") {
			StartCoroutine( other.GetComponentInParent<ChessPiece> ().damage (attackPower));
			StartCoroutine (AutoDestroy(3));
		}
	}
	public void setAttackPower(int a){
		attackPower = a;
	}

	public void setTarget(ChessPiece t){
		target = BoardManager.Instance.getTileCenter (t.CurrentX, t.CurrentY);
		target.y = .5f;
	}

	IEnumerator AutoDestroy(int i){
		yield return new WaitForSeconds (i);
		Destroy (gameObject);
	}


}
