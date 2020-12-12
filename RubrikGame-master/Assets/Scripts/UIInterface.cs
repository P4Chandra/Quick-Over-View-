using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIInterface : MonoBehaviour {

	// Use this for initialization
	public Text hintText;
	public Text statusText; 

	void Start () {

		//Set Font,Font size and Color.
	
			
		hintText.color=Color.cyan;
		statusText.color=Color.yellow;

		hintText.text="Look here for hints !! ";
		statusText.text = "Refer here for any status";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateHint(string text) {
		hintText.text = text;
		//play audio
	}

	public void UpdateStatus(string text) {
		statusText.text = text;
	}

}
