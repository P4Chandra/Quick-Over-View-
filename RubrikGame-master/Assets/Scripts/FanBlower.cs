using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBlower : MonoBehaviour {

	// Use this for initialization
	public float throwForce= 1.1f;
	public GameObject Blades;
	private bool isInRange = false;
	private Rigidbody rb =null;
	private Vector3 forward = new Vector3 (0, 0, 0);

	private AudioSource windfile;

	void Start () {
		windfile = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (isInRange && rb != null) {
			//forward = (forward - transform.forward) * throwForce;
			rb.AddForce ((forward - transform.forward) * throwForce);
			//Debug.Log ("Fan effect added");
		}
		Blades.transform.Rotate(new Vector3(0,30,0)*Time.deltaTime*throwForce);
		//windfile.Stop();

	}
	void OnCollisionStay(Collision other){
		if (other.collider.tag.Contains ("Ball")) {
			windfile.Play ();
			rb = other.gameObject.GetComponent<Rigidbody> ();
			isInRange = true;

		}
	}

	void OnCollisionExit(Collision other){
		if (other.collider.tag.Contains ("Ball")) {
			
			rb = null;
			isInRange = false;
		}
	}
}
