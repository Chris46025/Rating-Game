using UnityEngine;
using System.Collections;

public abstract class Queen : ChessPiece {

	public override void Start(){
		base.Start ();
		base.setKing (false);

		if (isWhite)
			base.setChessPieceName("White Queen");
		else
			base.setChessPieceName("Black Queen");
	}

	public override bool[,] possibleMove(){
		bool[,] r = new bool[8, 8];

		ChessPiece c;
		int i, j;

		//Rook Moves
		//Right
		i = CurrentX;

		while (true) {
			i++;
			if (i >= 8)
				break;

			c = BoardManager.Instance.ChessPieces [i, CurrentY];
			if (c == null) {
				r [i, CurrentY] = true;
			}
			else {
				if (c.isWhite != isWhite) {
					r [i, CurrentY] = true;
				}
				break;
			}	
		}

		//Left
		i = CurrentX;

		while (true) {
			i--;
			if (i < 0)
				break;

			c = BoardManager.Instance.ChessPieces [i, CurrentY];
			if (c == null) {
				r [i, CurrentY] = true;
			}
			else {
				if (c.isWhite != isWhite) {
					r [i, CurrentY] = true;
				}
				break;
			}	
		}

		//Up
		i = CurrentY;

		while (true) {
			i++;
			if (i >= 8)
				break;

			c = BoardManager.Instance.ChessPieces [CurrentX, i];
			if (c == null) {
				r [CurrentX, i] = true;
			}
			else {
				if (c.isWhite != isWhite) {
					r [CurrentX, i] = true;
				}
				break;
			}	
		}

		//Down
		i = CurrentY;

		while (true) {
			i--;
			if (i < 0)
				break;

			c = BoardManager.Instance.ChessPieces [CurrentX, i];
			if (c == null) {
				r [CurrentX, i] = true;
			}
			else {
				if (c.isWhite != isWhite) {
					r [CurrentX, i] = true;
				}
				break;
			}	
		}


		//Bishop Moves
		//Top Left
		i = CurrentX;
		j = CurrentY;

		while (true) {
			i--;
			j++;

			if (i < 0 || j >= 8) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];

			if (c == null) {
				r [i, j] = true;
			}
			else {
				if (isWhite != c.isWhite) {
					r [i, j] = true;
				}
				break;
			}

		}

		//Top Right
		i = CurrentX;
		j = CurrentY;

		while (true) {
			i++;
			j++;

			if (i >= 8 || j >= 8) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];

			if (c == null) {
				r [i, j] = true;
			}
			else {
				if (isWhite != c.isWhite) {
					r [i, j] = true;
				}
				break;
			}

		}

		//Down Left
		i = CurrentX;
		j = CurrentY;

		while (true) {
			i--;
			j--;

			if (i < 0 || j < 0) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];

			if (c == null) {
				r [i, j] = true;
			}
			else {
				if (isWhite != c.isWhite) {
					r [i, j] = true;
				}
				break;
			}

		}

		//Down Right
		i = CurrentX;
		j = CurrentY;

		while (true) {
			i++;
			j--;

			if (i >= 8 || j < 0) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];

			if (c == null) {
				r [i, j] = true;
			}
			else {
				if (isWhite != c.isWhite) {
					r [i, j] = true;
				}
				break;
			}

		}

		return r;

	}
}
