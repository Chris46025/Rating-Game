using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardHighlights : MonoBehaviour {

	public static BoardHighlights Instance{ set; get;}

	public GameObject moveHighlightPrefab;
	public GameObject attackHighlightPrefab;
	private List<GameObject> moveHighlights;
	private List<GameObject> attackHighlights;

	public ChessPiece[,] ChessPieces{ set; get;} 

	private void Start(){
		Instance = this;
		moveHighlights = new List<GameObject> ();
		attackHighlights = new List<GameObject> ();



	}

	private GameObject getMoveHighlightObject(){
		GameObject go = moveHighlights.Find (g => !g.activeSelf);

		if (go == null) {
			go = Instantiate (moveHighlightPrefab);
			moveHighlights.Add (go);

		}

		return go;
	}

	private GameObject getAttackHighlightObject(){
		GameObject go = attackHighlights.Find (g => !g.activeSelf);

		if (go == null) {
			go = Instantiate (attackHighlightPrefab);
			attackHighlights.Add (go);

		}

		return go;
	}

	public void highlightAllowedMoves(bool[,] moves){
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if (moves [i, j]) {
					ChessPiece c = BoardManager.Instance.getChessPiece(i,j);

					if (c != null && c.isWhite != BoardManager.Instance.getTurn ()) {
						GameObject go = getAttackHighlightObject ();
						go.SetActive (true);
						go.transform.position = new Vector3 (i + 0.5f, 0, j);
					}
					else {
						GameObject go = getMoveHighlightObject ();
						go.SetActive (true);
						go.transform.position = new Vector3 (i + 0.5f, 0, j);
					}

				}
			}
		}
	}

	public void hideHighlights(){
		foreach (GameObject go in attackHighlights)
			go.SetActive (false);
		foreach (GameObject go in moveHighlights)
			go.SetActive (false);
	}
}
