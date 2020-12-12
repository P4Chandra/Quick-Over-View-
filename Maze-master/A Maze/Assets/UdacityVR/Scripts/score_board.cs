using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class score_board : MonoBehaviour {

	public Text DoorText;
	public Text KeyText;
	public Text ChestText;
	public Text HintText;
	public Text coinText;
	// private variables only for internal use to keep track of objects that was interacted with
	private int coins=0;
	private int keys=0;
	private int chests=0;
	private int doors=0;
	private string hints=" Keep Looking for hints.";
	private Vector3 offset;

	void Start () {
		DoorText.text = " Doors Opened : " + doors + " / 3";
		KeyText.text = " Keys Found : " + keys + " /3";
		ChestText.text = "Chests Opened : " + chests + " /2";
		coinText.text = " Coins Collected : " + coins + " /26";
		HintText.text = "Hints: " + hints;


	}

	void Update() {

		transform.LookAt(Camera.main.transform);
	}
	public void IncrementCoins() {
		coins++;
		coinText.text = " Coins Collected : " + coins + " /26";

	}

	public void IncrementChestOpened(){
		chests++;
		ChestText.text = "Chests Opened : " + chests + " /2";

	}

	public void IncrementKeysFound() {
		keys++;
		KeyText.text = " Keys Found : " + keys + " /3";

	}

	public void IncrementDoorOpened() {
		doors++;
		DoorText.text = " Doors Opened : " + doors + " / 3";
	}

	public void UpdateHints(string strhint) {
	  HintText.text = "Hints: " + strhint;
	}

}
