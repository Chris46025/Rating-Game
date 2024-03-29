﻿using UnityEngine;
using System.Collections;

public abstract class Bishop : ChessPiece {

	public override void Start(){
		base.Start ();
		base.setKing (false);

		if (isWhite)
			base.setChessPieceName("White Bishop");
		else
			base.setChessPieceName("Black Bishop");
	}

	public override bool[,] possibleMove(){
		bool[,] r = new bool[8, 8];

		ChessPiece c;
		int i, j;

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
