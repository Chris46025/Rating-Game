using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	private GameObject[] mainMenuButtons;
	private GameObject[] playButtons;
	public Image fader;

	//Button play;

	// Use this for initialization
	void Start () {
		fader.enabled = false;
		mainMenuButtons = GameObject.FindGameObjectsWithTag ("MainMenuButtons");
		playButtons = GameObject.FindGameObjectsWithTag ("PlayButtons"); 

		for (int i = 0; i < playButtons.Length; i++) {
			playButtons [i].SetActive(false);
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showGameButtons(){
		for (int i = 0; i < mainMenuButtons.Length; i++) {
			mainMenuButtons [i].SetActive(false);
		}

		for (int i = 0; i < playButtons.Length; i++) {
			playButtons [i].SetActive(true);
		}
	}

	public void hideGameButtons(){
		for (int i = 0; i < mainMenuButtons.Length; i++) {
			mainMenuButtons [i].SetActive(true);
		}

		for (int i = 0; i < playButtons.Length; i++) {
			playButtons [i].SetActive(false);
		}
	}


	public void StartGame(int i){
		StartCoroutine (FadeOut (i));
	}

	IEnumerator FadeOut(int i){
		fader.enabled = true;
		fader.GetComponent<Animator> ().SetBool ("Fade", true);
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (i);
	}
}
