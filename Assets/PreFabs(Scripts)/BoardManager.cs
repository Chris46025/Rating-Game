using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {

	public Image fader;
	private GameObject winText;

	public bool CPU;
	public bool CPUTurn;
	public GameObject ComputerAI;
	private ChessAI AI;

	public static BoardManager Instance{ set; get;}
	private bool[,] allowedMoves{ set; get;}

	public ChessPiece[,] ChessPieces{ set; get;} 
	private ChessPiece selectedChessPiece;

	private const float TILE_SIZE =  1.0f;
	private const float TILE_OFFSET =  0.5f;

	private int selectionX = -1;
	private int selectionY = -1;

	public List<GameObject> chessPieces;
	private List<GameObject> activeChessPieces;

	public bool isWhiteTurn = true;
	public bool gameOver;

	static Quaternion whiteOrientation = Quaternion.Euler(0,0,0);
	static Quaternion blackOrientation = Quaternion.Euler(0,180,0);

	private bool movingUp;
	private bool movingDown;
	private bool movingLeft;
	private bool movingRight;
	private Vector3 movingDirection;
	private Vector3 turningDirection;

	private bool battleSequence;

	public Canvas boardInfo;
	private Camera gameCamera;

	private void Start(){
		StartCoroutine (FadeIn ());
		Instance = this;
		spawnAllChessPieces ();
		winText = GameObject.Find ("WinText");
		winText.SetActive (false);
		gameCamera = Camera.main;

		if (CPU) {
			ComputerAI.SetActive (true);
			AI = ComputerAI.GetComponent<ChessAI> ();
		
		}
	}

	private void Update(){
		
		drawChessBoard ();

		if (!battleSequence && !CPUTurn) {
			UpdateSelection ();
			if (selectionX >= 0 && selectionY >= 0) {
				updateChessPieceInfo (selectionX, selectionY);
			}
		}
		if (Input.GetMouseButtonDown (0) && !movingUp && !movingDown && !movingLeft && !movingRight && !battleSequence && !CPUTurn) {
			if (selectionX >= 0 && selectionY >= 0) {
				if (selectedChessPiece == null) {
					//Select Chess Piece
					selectChessPiece(selectionX, selectionY);
				}
				else {
					//move piece
					startMoveChessPiece(selectionX, selectionY);
				}
			}
		}

		if (CPUTurn && !movingUp && !movingDown && !movingLeft && !movingRight && !battleSequence) {
			AI.getBoard ();

			if (selectionX >= 0 && selectionY >= 0) {
				updateChessPieceInfo (selectionX, selectionY);
				if (selectedChessPiece == null) {
					//Select Chess Piece
					selectChessPiece(selectionX, selectionY);
				}
				else {
					//move piece
					startMoveChessPiece(selectionX, selectionY);
				}
			}
		}
	}

	private void setMovingDirection(Vector3 d){
		movingDirection = d;
	}

	public Vector3 getMovingDirection(){
		return movingDirection;
	}

	private void FixedUpdate(){
		if (movingLeft || movingRight || movingUp || movingDown) {
			print ("entered here");

			float tempPosX = selectedChessPiece.transform.position.x; 
			float tempPosY = selectedChessPiece.transform.position.z; 
			float tempSelectionX = (float)selectionX + .5f ;
			float tempSelectionY = (float)selectionY + .5f;

			Vector3 newPosition = new Vector3 (selectedChessPiece.transform.position.x, selectedChessPiece.transform.position.y, selectedChessPiece.transform.position.z);
			Vector3 newDirection = new Vector3 (0, 0, 0);

			if (movingLeft) {
				if (tempPosX > tempSelectionX) {
					newPosition.x = newPosition.x - 1;
					newDirection.x = -1;
				}
				else {
					newPosition.x = newPosition.x + 1;
					newDirection.x = 0;
					movingLeft = false;
				}
			
			}
			
			if (movingRight) {
				if (tempPosX < tempSelectionX) {
					newPosition.x = newPosition.x + 1;
					newDirection.x = 1;
				}
				else {
					newPosition.x = newPosition.x - 1;
					newDirection.x = 0;
					movingRight = false;
				}
			
			}
			
			if (movingUp) {
				if (tempPosY < tempSelectionY) {
					newPosition.z = newPosition.z + 1;
					newDirection.z = 1;
				}
				else {
					newPosition.z = newPosition.z - 1;
					newDirection.z = 0;
					movingUp = false;
				}
			}
			
			if (movingDown) {
				if (tempPosY > tempSelectionY) {
					newPosition.z = newPosition.z - 1;
					newDirection.z = -1;
				}
				else {
					newPosition.z = newPosition.z + 1;
					newDirection.z = 0;
					movingDown = false;
				}
			}
			setMovingDirection (newDirection);
			if (Mathf.Abs (tempSelectionX - tempPosX) > 2 || Mathf.Abs (tempSelectionY - tempPosY) > 2) {
				selectedChessPiece.GetComponent<ChessPiece> ().movement (true, false, true);
				selectedChessPiece.transform.position = Vector3.Lerp (selectedChessPiece.transform.position, newPosition, 1.5f * Time.deltaTime);
			}
			else {
				selectedChessPiece.GetComponent<ChessPiece> ().movement (true, true, false);
				selectedChessPiece.transform.position = Vector3.Lerp (selectedChessPiece.transform.position, newPosition, 1f * Time.deltaTime);
			}

			if (movingLeft == false && movingRight == false && movingUp == false && movingDown == false) {
				selectedChessPiece.GetComponent<ChessPiece> ().movement(false, false, false);
				moveChessPiece(selectionX, selectionY);
			}
		}
	}
		
	public void selectChessPiece(int x, int y){
		print ("select chess piece called");
		if (!CPUTurn) {
			if (ChessPieces [x, y] == null) {
				return;
			}

			if (ChessPieces [x, y].isWhite != isWhiteTurn) {
				chessPieceInfoOpponent (getChessPiece (x, y));
				return;
			}
		}

		selectedChessPiece = ChessPieces [x, y];
		allowedMoves = selectedChessPiece.possibleMove();
		chessPieceInfo (selectedChessPiece);

		if(selectedChessPiece != null)
			BoardHighlights.Instance.highlightAllowedMoves (allowedMoves);
	}

	private void updateChessPieceInfo(int x, int y){
		if (ChessPieces [x, y] == null) {
			return;
		}
		else if (ChessPieces [x, y].isWhite != isWhiteTurn) {
			chessPieceInfoOpponent (getChessPiece (x, y));
			return;
		}
		else {
			if (selectedChessPiece == null)
				chessPieceInfo (getChessPiece (x, y));
			else
				return;
		}
	}

	public void startMoveChessPiece(int x, int y){
		//Don't attack the piece just move
		if (allowedMoves [x, y] && getChessPiece (x, y) == null) {
			//Move Left
			movingLeft = true;

			//Move Right
			movingRight = true;

			//Move Up
			movingUp = true;

			//Move Down
			movingDown = true;
		} 

		//Attack and Move
		else if (selectedChessPiece.getAttackPower () >= getChessPiece (x, y).getCurrentHealth () && allowedMoves [x, y]) {
			if (getChessPiece (x, y).getKingStatus ())
				StartCoroutine (GameOver ());
			battleSequence = true;
			StartCoroutine (attackAndMove (selectedChessPiece, getChessPiece (x, y)));
		}
		//Don't move the piece just attack
		else if (allowedMoves [x, y]) {
			battleSequence = true;
			StartCoroutine (attack (selectedChessPiece, getChessPiece (x, y)));
			isWhiteTurn = !isWhiteTurn;
			BoardHighlights.Instance.hideHighlights ();
			selectedChessPiece = null;
		} 
		else {
			BoardHighlights.Instance.hideHighlights ();
			selectedChessPiece = null;
		}



	}

	private IEnumerator attack(ChessPiece a, ChessPiece b){
		b.turnColliderOn ();
		StartCoroutine( a.attacking (0, b));//Close attack
		yield return new WaitForSeconds(5);
		battleSequence = false;

		//Change if playing locally
		if (!CPU)
			switchSidesLocalGame ();
		else {
			if (CPUTurn)
				CPUTurn = false;
			else
				ComputerMove ();
		}
	}

	private IEnumerator attackAndMove(ChessPiece a, ChessPiece b){
		b.turnColliderOn ();
		if (CPUTurn)
			yield return new WaitForSeconds (1);
		int nextX = b.CurrentX;
		int nextY = b.CurrentY;
		StartCoroutine( a.attacking (0, b));//Close attack
		yield return new WaitForSeconds(5);
		battleSequence = false;
		selectedChessPiece = a;
		startMoveChessPiece (nextX, nextY);
	}

	public void moveChessPiece(int x, int y){
		if (allowedMoves[x,y]) {
			ChessPiece c = ChessPieces [x, y];
			ChessPieces [selectedChessPiece.CurrentX, selectedChessPiece.CurrentY] = null;
			selectedChessPiece.transform.position = getTileCenter (x, y);
			selectedChessPiece.setPosition (x, y);
			ChessPieces [x, y] = selectedChessPiece;
			//Changes Turns
			isWhiteTurn = !isWhiteTurn;

			//Change if playing locally
			if (!CPU)
				switchSidesLocalGame ();
			else {
				if (CPUTurn)
					CPUTurn = false;
				else
					ComputerMove ();
			}

		}

		BoardHighlights.Instance.hideHighlights ();
		selectedChessPiece = null;
	}

	private void UpdateSelection(){
		if (!Camera.main)
			return;

		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask ("ChessPlane"))) {
			if (!movingUp && !movingDown  && !movingLeft  && !movingRight) {
				selectionX = (int)hit.point.x;
				selectionY = (int)hit.point.z;
			}
		}
	}

	private void spawnChessPieces(int index, int x, int y, Quaternion orientation){
		GameObject go = Instantiate (chessPieces [index], getTileCenter(x,y), orientation) as GameObject;
		go.transform.SetParent (transform);
		ChessPieces [x, y] = go.GetComponent<ChessPiece> ();
		ChessPieces [x, y].setPosition (x, y);
		activeChessPieces.Add (go);
	}

	private void spawnAllChessPieces(){
		activeChessPieces = new List<GameObject> ();
		ChessPieces = new ChessPiece[8, 8];

		//Spawn White Side

		for (int i = 0; i < 8; i++) {
			spawnChessPieces(0,i,1,whiteOrientation);//Pawns
		}
			
		spawnChessPieces(1,0,0,whiteOrientation);//Rook
		spawnChessPieces(1,7,0,whiteOrientation);//Rook
		spawnChessPieces(2,6,0,whiteOrientation);//Knight
		spawnChessPieces(2,1,0,whiteOrientation);//Knight
		spawnChessPieces(3,2,0,whiteOrientation);//Bishop
		spawnChessPieces(3,5,0,whiteOrientation);//Bishop
		spawnChessPieces(4,3,0,whiteOrientation);//King
		spawnChessPieces(5,4,0,whiteOrientation);//Queen

		//Spawn Black Side
		for (int i = 0; i < 8; i++) {
			spawnChessPieces(6,i,6,blackOrientation);//Pawns
		}

		spawnChessPieces(7,0,7,blackOrientation);//Rook
		spawnChessPieces(7,7,7,blackOrientation);//Rook
		spawnChessPieces(8,6,7,blackOrientation);//Knight
		spawnChessPieces(8,1,7,blackOrientation);//Knight
		spawnChessPieces(9,2,7,blackOrientation);//Bishop
		spawnChessPieces(9,5,7,blackOrientation);//Bishop
		spawnChessPieces(10,3,7,blackOrientation);//King
		spawnChessPieces(11,4,7,blackOrientation);//Queen
	}

	public Vector3 getTileCenter(int x, int y){
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFSET;
		origin.z += (TILE_SIZE * y) + TILE_OFFSET;
		return origin;
	}

	private void drawChessBoard(){
		
		Vector3 widthLine = Vector3.right * 8;
		Vector3 heightLine = Vector3.forward * 8;

		for (int i = 0; i <= 8; i++) {
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine);
			for (int j = 0; j <= 8; j++) {
				start = Vector3.right * j;
				Debug.DrawLine (start, start + heightLine);
			}
		}

		if (selectionX >= 0 && selectionY >= 0) {
			Debug.DrawLine (
				Vector3.forward * selectionY + Vector3.right * selectionX,
				Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
			Debug.DrawLine (
				Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
				Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
		}
	}

	private void chessPieceInfo(ChessPiece c){
		Text[] infoText = boardInfo.GetComponentsInChildren<Text>();
		infoText[1].text = (string)c.getChessPieceName();
		infoText [2].text = "Atk : " + c.getAttackPower ();
		infoText [3].text = c.getCurrentHealth().ToString() + " / " + (string)c.getMaxHealth().ToString();
		Image health = GameObject.Find ("Health").GetComponent<Image>();
		health.fillAmount = (float)c.getCurrentHealth() / (float)c.getMaxHealth ();
		Image chessPieceAvatar = GameObject.Find ("ChessPieceImage").GetComponent<Image> ();
		chessPieceAvatar.sprite = c.getAvatar ();
	}

	private void chessPieceInfoOpponent(ChessPiece c){
		Text[] infoText = boardInfo.GetComponentsInChildren<Text>();
		infoText[4].text = (string)c.getChessPieceName();
		infoText [5].text = "Atk : " + c.getAttackPower ();
		infoText [6].text = c.getCurrentHealth().ToString() + " / " + (string)c.getMaxHealth().ToString();
		Image health = GameObject.Find ("OpponentHealth").GetComponent<Image>();
		health.fillAmount = (float)c.getCurrentHealth() / (float)c.getMaxHealth ();
		Image chessPieceAvatar = GameObject.Find ("OpponentChessPieceImage").GetComponent<Image> ();
		chessPieceAvatar.sprite = c.getAvatar ();
	}

	public bool getTurn(){
		return isWhiteTurn;
	}

	public ChessPiece getChessPiece(int x, int y){
		ChessPiece c = ChessPieces [x, y];
		return c;
	}

	//Only used when you have to switch camera angles because you are playing locally
	private void switchSidesLocalGame(){
		gameCamera.GetComponent<GameCameraLocally> ().SwitchSides ();
	}

	//Fades into the scene
	IEnumerator FadeIn(){
		fader.GetComponent<Animator> ().SetBool ("Fade", false);
		yield return new WaitForSeconds (3);
		fader.enabled = false;
	}

	private IEnumerator GameOver(){
		battleSequence = true;
		gameOver = true;
		winText.SetActive (true);
		if (isWhiteTurn) {
			winText.GetComponent<Text> ().text = "White Team Wins";
		}
		else {
			winText.GetComponent<Text>().text = "Black Team Wins";
		}
		yield return new WaitForSeconds(2);
	}

	private void ComputerMove(){
		CPUTurn = true;
	}

	public void setSelectionX(int x){
		selectionX = x;
	}

	public void setSelectionY(int y){
		selectionY = y;
	}

	public void setBattleSequence(bool b){
		battleSequence = b;
	}

}
