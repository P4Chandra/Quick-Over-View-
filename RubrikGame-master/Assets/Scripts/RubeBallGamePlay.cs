using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubeBallGamePlay : MonoBehaviour {


	public GameObject poofprefab;
	public MainGamePlay gameplay;
	public Material active;
	private AudioSource hitsound;
	//private bool isHit = false;//Is set to true if the current ball object is hit by throwball or RubeBall 
	// This causes REmove Routine to be called only once.
	private bool isRemoved = false;


	// Use this for initialization
	void Start () {
		isRemoved = false;
		hitsound = gameObject.GetComponent<AudioSource> ();
		//isHit = false;

	}


	// Update is called once per frame
	void Update () {

	}

	//Detect if RubeBall hits the Goal,if yes then level complete else destroy it.
	//Detect if ThrowBall hits anything it will be destroyed no matter where it lands.

	void OnTriggerEnter(Collider other) {

			
		//Cheat-Check only Throwball can touch the rube balls Touch with gloves/hands is disabled
		if ( other.gameObject.tag.Contains ("ThrowBall") && !isRemoved){
			hitsound.Play ();
			Debug.Log (this.gameObject.name +" hit by "+other.tag);
			/*Rigidbody rb = this.gameObject.GetComponent<Rigidbody> ();
			rb.useGravity = true;
			rb.isKinematic = false;
			this.gameObject.GetComponent<Collider> ().isTrigger=false;*/
			//if (!isHit) {
			gameplay.IncrementBallHit ();
			this.gameObject.GetComponent<Renderer> ().material = active;
			Instantiate (poofprefab, transform.position, transform.rotation);
			isRemoved = true;
			StartCoroutine (RemoveCurrentBall ());

		}

	}
		
	IEnumerator RemoveCurrentBall() {
		//play audio here
		Debug.Log ("Removing GameBall");
		yield return new WaitForSeconds(0.5f);
		//isHit = false;
		Destroy (gameObject);
		hitsound.Stop ();

	}


		

}
