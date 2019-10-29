using UnityEngine;
using System.Collections;

public abstract class Knight : ChessPiece {

	public override void Start(){
		base.Start ();
		base.setKing (false);

		if (isWhite)
			base.setChessPieceName("White Knight");
		else
			base.setChessPieceName("Black Knight");
	}

	public override bool[,] possibleMove(){

		bool[,] r = new bool[8, 8];

		//Up Left
		knightMove(CurrentX - 1, CurrentY + 2, ref r);

		//Up Right
		knightMove(CurrentX + 1, CurrentY + 2, ref r);

		//Right Up
		knightMove(CurrentX + 2, CurrentY + 1, ref r);

		//Right Down
		knightMove(CurrentX + 2, CurrentY - 1, ref r);

		//Down Left
		knightMove(CurrentX - 1, CurrentY - 2, ref r);

		//Down Right
		knightMove(CurrentX + 1, CurrentY - 2, ref r);

		//Left Up
		knightMove(CurrentX - 2, CurrentY + 1, ref r);

		//Left Down
		knightMove(CurrentX - 2, CurrentY - 1, ref r);


		return r;


	}

	public void knightMove(int x, int y, ref bool[,] r){

		ChessPiece c;
		if (x >= 0 && x < 8 && y >= 0 && y < 8) {
			c = BoardManager.Instance.ChessPieces [x, y];
			if (c == null) {
				r [x, y] = true;
			}
			else if (isWhite != c.isWhite) {
				r [x, y] = true;
			}
		}
	}
		

}
