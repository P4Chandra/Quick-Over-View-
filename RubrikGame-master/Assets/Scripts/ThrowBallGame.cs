using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBallGame : MonoBehaviour {

	// Use this for initialization
	// Use this for initialization

	public GameObject poofprefab; //poof effect
	public MainGamePlay gameplay;
	public AudioClip[] audioFiles;
	AudioSource audio;
	//  Remove Routine needs to be called once 
	static bool isRemoved=false;
	static bool isLevelComplete = false;

	public Material active;

	void Start () {
		isRemoved = false;
		isLevelComplete = false;
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {

		//Detect if Rube Game Ball lands on goal  and 
		//if  yes initiate level transition.


		if (other.gameObject.CompareTag ("LevelGoal") && !isLevelComplete) {
			//Debug.Log ("Rubeball hit goal");
			Debug.Log ("Activitating Level Complete");
			this.gameObject.GetComponent<Renderer> ().material = active;
			gameplay.ActivateLevelComplete ();
			isLevelComplete = true;
		} else if (other.gameObject.CompareTag ("Floor")
		         && !isRemoved) {
			isRemoved = true;
			gameplay.ActivateLevelFail ();
			DestroyBall ();
			// play ball destroy clip

		} else if (other.gameObject.name.Contains ("Plank")) {
			audio.clip = audioFiles [0];
			audio.Play ();//play rolling ball script
		}else if (other.gameObject.name.Contains ("Trampoline")) {
			audio.clip = audioFiles [1];
			audio.Play ();//play rolling ball script
		}

		// Destroy ball if it hits floor and reinstantiate the ball in its regular location


	}

	public void DestroyBall(){
		Instantiate (poofprefab, transform.position, transform.rotation);
		isRemoved = true;
		StartCoroutine (RemoveGameBall ());
	}

	IEnumerator RemoveGameBall() {
		//play audio here

		yield return new WaitForSeconds(0.5f);
		Destroy (gameObject);
		//Debug.Log ("Removing GameBall");


	}
}
