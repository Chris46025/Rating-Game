using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraVsComputer : MonoBehaviour {

	Animation currentPlayer;
	private string[] animations;

	// Use this for initialization
	void Start () {
		currentPlayer = gameObject.GetComponent<Animation> ();
		currentPlayer.Play ("MoveIn");
	}
}
