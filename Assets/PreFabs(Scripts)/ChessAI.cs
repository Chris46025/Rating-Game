using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour {

	public GameObject chessBoard;
	private BoardManager BoardInstance;

	// Use this for initialization
	void Start () {
		BoardInstance = chessBoard.GetComponent<BoardManager>();
	}

	public void movePiece(ChessPiece x , bool[,] moves){
		BoardInstance.selectChessPiece(x.CurrentX, x.CurrentY);
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if (moves [i, j]) {
					//moving Chess Piece x to position i , j after attacking);
					ChessPiece c = BoardInstance.getChessPiece(i,j);
					if (c != null) {
						makeSelectionX (i);
						makeSelectionY (j);
						return;
					}
					else {
						//moving Chess Piece x to position i , j);
						makeSelectionX (i);
						makeSelectionY (j);
						return;
					}

				}
			}
		}
	}

	//This is used to go through the board to select the piece to be moved, once a viable piece is found it will call move piece
	public void getBoard(){
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				ChessPiece currentPiece = chessBoard.GetComponent<BoardManager> ().getChessPiece (i, j);
				if (currentPiece != null && currentPiece.isWhite == false) {
					movePiece(currentPiece, currentPiece.possibleMove());
					return;
				}
			}
		}
	}

	public void makeSelectionX(int x){
		BoardInstance.setSelectionX (x);
	}
	public void makeSelectionY(int y){
		BoardInstance.setSelectionY (y);
	}

}
