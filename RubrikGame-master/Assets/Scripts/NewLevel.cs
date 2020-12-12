using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevel : MonoBehaviour {

	// Use this for initialization
	public SteamVR_LoadLevel loadlevel;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//if player enters teleport goal then they transition to next level
	void  OnTriggerEnter(Collider other){
       // Debug.Log(" teleport colliding with " + other.gameObject.name + " and tag "+ other.gameObject.tag);
        if (other.tag.Contains("Player")) {
			loadlevel.Trigger ();
			Debug.Log ("going to next level");
		}

	}
}
