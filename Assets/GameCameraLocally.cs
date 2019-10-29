using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraLocally : MonoBehaviour {

	Animation currentPlayer;
	private string[] animations;

	// Use this for initialization
	void Start () {
		currentPlayer = gameObject.GetComponent<Animation> ();
		currentPlayer.Play ("MoveIn");
	}

	public void SwitchSides(){
		if(BoardManager.Instance.isWhiteTurn && !BoardManager.Instance.gameOver)
			currentPlayer.Play ("RotateToWhiteSide");
		else
			currentPlayer.Play ("RotateToBlackSide");
	}
}
