using UnityEngine;
using System.Collections;

public class FightScene : MonoBehaviour {

	public GameObject char1;
	public GameObject char2;
	static Quaternion char1Orientation = Quaternion.Euler(0,90,0);
	static Quaternion char2Orientation = Quaternion.Euler(0,-90,0);

	// Use this for initialization
	void Start () {
		GameObject go1 = Instantiate (char1,new Vector3(-0.3f,0,0), char1Orientation) as GameObject;
		char1.GetComponent<Bishop> ().enabled = false;

		GameObject go2 = Instantiate (char2,new Vector3(0.3f,0,0), char2Orientation) as GameObject;
		char2.GetComponent<Bishop> ().enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
