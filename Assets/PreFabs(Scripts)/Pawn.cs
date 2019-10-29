using UnityEngine;
using System.Collections;

public abstract class Pawn : ChessPiece {

	public override void Start(){
		base.Start ();
		base.setKing (false);

		if (isWhite)
			base.setChessPieceName("White Pawn");
		else
			base.setChessPieceName("Black Pawn");
	}

	public override bool[,] possibleMove(){
		bool[,] r = new bool[8,8];

		ChessPiece c, c2;

		//White team moves
		if (isWhite) {
			//Diagonal left
			if (CurrentX != 0 && CurrentY != 7) {
				c = BoardManager.Instance.ChessPieces [CurrentX - 1, CurrentY + 1];
				if (c != null && !c.isWhite) {
					r [CurrentX - 1, CurrentY + 1] = true;
				}
			}
			//Diagonal right
			if (CurrentX != 7 && CurrentY != 7) {
				c = BoardManager.Instance.ChessPieces [CurrentX + 1, CurrentY + 1];
				if (c != null && !c.isWhite) {
					r [CurrentX + 1, CurrentY + 1] = true;
				}
			}
			//Middle
			if (CurrentY != 7) {
				c = BoardManager.Instance.ChessPieces [CurrentX, CurrentY + 1];
				if (c == null) {
					r [CurrentX, CurrentY + 1] = true;
				}
			}
			//Middle on first move
			if (CurrentY == 1) {
				c = BoardManager.Instance.ChessPieces [CurrentX, CurrentY + 1];
				c2 = BoardManager.Instance.ChessPieces [CurrentX, CurrentY + 2];

				if (c == null && c2 == null) {
					r [CurrentX, CurrentY + 2] = true;
				}
			}
		}
		//Black team moves
		else {
			//Diagonal left
			if (CurrentX != 0 && CurrentY != 0) {
				c = BoardManager.Instance.ChessPieces [CurrentX - 1, CurrentY - 1];
				if (c != null && c.isWhite) {
					r [CurrentX - 1, CurrentY - 1] = true;
				}
			}
			//Diagonal right
			if (CurrentX != 7 && CurrentY != 0) {
				c = BoardManager.Instance.ChessPieces [CurrentX + 1, CurrentY - 1];
				if (c != null && c.isWhite) {
					r [CurrentX + 1, CurrentY - 1] = true;
				}
			}
			//Middle
			if (CurrentY != 0) {
				c = BoardManager.Instance.ChessPieces [CurrentX, CurrentY - 1];
				if (c == null) {
					r [CurrentX, CurrentY - 1] = true;
				}
			}
			//Middle on first move
			if (CurrentY == 6) {
				c = BoardManager.Instance.ChessPieces [CurrentX, CurrentY - 1];
				c2 = BoardManager.Instance.ChessPieces [CurrentX, CurrentY - 2];

				if (c == null && c2 == null) {
					r [CurrentX, CurrentY - 2] = true;
				}
			}
		}			
		return r;
	}	



}
