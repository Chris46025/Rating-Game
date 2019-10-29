using UnityEngine;
using System.Collections;

public abstract class King : ChessPiece {

	public override void Start(){
		base.Start ();
		base.setKing (true);

		if (isWhite)
			base.setChessPieceName("White King");
		else
			base.setChessPieceName("Black King");
	}

	public override bool[,] possibleMove()
	{
		bool[,] r = new bool[8, 8];

		KingMove(CurrentX + 1, CurrentY, ref r); // up
		KingMove(CurrentX - 1, CurrentY, ref r); // down
		KingMove(CurrentX, CurrentY - 1, ref r); // left
		KingMove(CurrentX, CurrentY + 1, ref r); // right
		KingMove(CurrentX + 1, CurrentY -1, ref r); // up left
		KingMove(CurrentX - 1, CurrentY -1, ref r); // down left
		KingMove(CurrentX +1, CurrentY + 1, ref r); // up right
		KingMove(CurrentX - 1, CurrentY + 1, ref r); // down right

		return r;
	}

	public void KingMove(int x, int y, ref bool[,] r)
	{
		ChessPiece c;
		if (x >= 0 && x < 8 && y >= 0 && y < 8)
		{
			c = BoardManager.Instance.ChessPieces[x, y];
			if (c == null)
				r[x, y] = true;
			else if (isWhite != c.isWhite)
				r[x, y] = true;
		}
	}
}﻿

