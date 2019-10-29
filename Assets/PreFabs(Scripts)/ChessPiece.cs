using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class ChessPiece : MonoBehaviour {

	public int CurrentX{ set; get;}
	public int CurrentY{ set; get;}
	public bool isWhite;
	private bool isKing;
	public int baseAttackPower;
	public int attackPower;
	public int maxHealth;
	private int currentHealth;
	private string chessPieceName;

	public Animator animationController;
	private Rigidbody rigidBody;
	private CapsuleCollider capsuleCollider;
	private ChessPiece opponent;
	private bool fighting;
	private bool isMoving;
	private bool running;
	private bool walking;
	public bool turning;

	//CardinalDirection
	public bool north, south, west, east, northwest, northeast, southeast, southwest;

	static Quaternion whiteOrientation = Quaternion.Euler(0,0,0);
	static Quaternion blackOrientation = Quaternion.Euler(0,180,0);

	//Image
	public Sprite avatar;


	public virtual void Start(){
		animationController = gameObject.GetComponent<Animator> ();
		rigidBody = gameObject.GetComponent<Rigidbody> ();
		capsuleCollider = gameObject.GetComponent<CapsuleCollider> ();

		capsuleCollider.enabled = false;
	}

	private void FixedUpdate(){
		if (getCurrentHealth () <= 0) {
			StartCoroutine (Death ());
		}

		if (getIsMoving()) {
			if(walking)
				animationController.SetBool ("Walk", true);
			else if (running)
				animationController.SetBool ("Run", true);
			Vector3 movement = BoardManager.Instance.getMovingDirection ();
			Quaternion targetRotation = Quaternion.LookRotation (movement, Vector3.up);
			Quaternion newRotation = Quaternion.Lerp (rigidBody.rotation, targetRotation, 3 * Time.deltaTime);
			rigidBody.MoveRotation(newRotation);
		}
		else if(!turning) {
			if(isWhite)
				rigidBody.MoveRotation (whiteOrientation);
			else
				rigidBody.MoveRotation (blackOrientation);

			animationController.SetBool ("Walk", false);
			animationController.SetBool ("Run", false);
		}
	}

	public void setPosition(int x, int y){
		CurrentX = x;
		CurrentY = y;
	}

	public virtual bool[,] possibleMove(){
		return new bool[8,8];
	}

	public virtual void setOpponent(ChessPiece c){
		opponent = c;
	}

	public virtual ChessPiece getOpponent(){
		return opponent;
	}

	public void movement(bool m, bool w, bool r){
		setIsMoving (m);
		walking = w;
		running = r;
	}

	public virtual IEnumerator attacking(int n, ChessPiece t){
		yield return new WaitForSeconds (5);
	}

	public IEnumerator damage(int d){
		animationController.SetBool ("Damage", true);
		setCurrentHealth (getCurrentHealth() - d);
		yield return new WaitForSeconds(0.5f);
		animationController.SetBool ("Damage", false);
		capsuleCollider.enabled = false;
	}

	private IEnumerator Death(){
		animationController.SetBool ("Alive", false);
		yield return new WaitForEndOfFrame ();
		yield return new WaitForSeconds (animationController.GetCurrentAnimatorStateInfo (0).length * 2);
		Destroy (gameObject);
	}

	private void setIsMoving(bool m){
		isMoving = m;
	}

	public bool getIsMoving(){
		return isMoving;
	}

	void OnCollisionEnter(Collision collision){
		//if (collision.gameObject.tag == "ChessPiece") {
		//	Physics.IgnoreCollision (collision.gameObject., gameObject);
		//}
	}

	public int getCurrentHealth(){
		return currentHealth;
	}

	public int getMaxHealth(){
		return maxHealth;
	}

	public virtual void setCurrentHealth(int h){
		currentHealth = h;
	}

	public virtual void setAttackPower(int h){
		attackPower = h;
	}

	public int getAttackPower(){
		return attackPower;
	}

	public virtual void setKing(bool k){
		isKing = k;
	}
	public bool getKingStatus(){
		return isKing;
	}

	public virtual void setChessPieceName(string n){
		chessPieceName = n;
	}

	public string getChessPieceName(){
		return chessPieceName;
	}

	public Sprite getAvatar(){
		return avatar;
	}

	public void setCardinalDirectionsToFalse(){
		north = false;
		south = false;
		west = false; 
		east = false; 
		northwest = false;
		northeast = false; 
		southeast = false; 
		southwest = false;
	}

	public void turnColliderOn(){
		capsuleCollider.enabled = true;
	}
}
