using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Door : MonoBehaviour {

	// TODO: Create variables to reference the game objects we need access to
	// Declare a GameObject named 'leftDoor' and assign the 'Left_Door' game object to the field in Unity
	public GameObject leftdoor;

	// Declare a GameObject named 'rightDoor' and assign the 'Right_Door' game object to the field in Unity
	public GameObject rightdoor;
	public AudioClip doorlockClip;
	public score_board scrbrd; // track of doors opened. 

	// TODO: Create variables to reference the components we need access to
	// Declare an AudioSource named 'audioSource' and get a reference to the audio source in Start()
	private AudioSource audioSource;
	// TODO: Create variables to track the gameplay states
	// Declare a boolean named 'locked' to track if the door has been unlocked and initialize it to 'true'
	// Declare a boolean named 'opening' to track if the door is opening and initialize it to 'false'
	private bool locked=true;
	private bool opening=false;
	// TODO: Create variables to hold rotations used when animating the door opening
	// Declare a Quaternion named 'leftDoorStartRotation' to hold the start rotation of the 'Left_Door' game object
	// Declare a Quaternion named "leftDoorEndRotation" to hold the end rotation of the 'Left_Door' game object
	// Declare a Quaternion named 'rightDoorStartRotation' to hold the start rotation of the 'Right_Door' game object
	// Declare a Quaternion named 'rightDoorEndRotation' to hold the end rotation of the 'Right_Door' game object
	private Quaternion leftDoorStartRotation;
	private Quaternion leftDoorEndRotation;
	private Quaternion rightDoorStartRotation;
	private Quaternion rightDoorEndRotation;
	// TODO: Create variables to control the speed of the interpolation when animating the door opening
	// Declare a float named 'timer' to track the Quaternion.Slerp() interpolation and initialize it to for example '0f'
	// Declare a float named 'rotationTime' to set the Quaternion.Slerp() interpolation speed and initialize it to for example '10f'
	private float timer=0.0f;
	private float rotationTime = 10.0f;

	void Start () {
		// TODO: Get a reference to the audio source
		// Use GetComponent<>() to get a reference to the AudioSource component and assign it to the 'audioSource'
		audioSource = GetComponent<AudioSource>();
		// TODO: Set start and end rotation values used when animating the door opening
		// Use 'leftDoor' to get the start rotation of the 'Left_Door' game object and assign it to 'leftDoorStartRotation'
		// Use 'leftDoorStartRotation' and Quaternion.Euler() to set the end rotation of the 'Left_Door' game object and assign it to 'leftDoorEndRotation'
		// Use 'rightDoor' to get the start rotation of the 'Right_Door' game object and assign it to 'rightDoorStartRotation'
		// Use 'rightDoorStartRotation' and Quaternion.Euler() to set the end rotation of the 'Right_Door' game object and assign it to 'rightDoorEndRotation'
		leftDoorStartRotation = leftdoor.gameObject.transform.rotation;
		leftDoorEndRotation = leftDoorStartRotation*Quaternion.Euler (0.0f, 0.0f, -90.0f);
		rightDoorStartRotation = rightdoor.gameObject.transform.rotation;
		rightDoorEndRotation = rightDoorStartRotation*Quaternion.Euler (0.0f, 0.0f, 90.0f);
	}


	void Update () {
		// TODO: If the door is opening, animate the 'Left_Door' and 'Right_Door' game objects rotating open
		// Use 'opening' to check if the door is opening...
		// ... use Quaternion.Slerp() to interpolate from 'leftDoorStartRotation' to 'leftDoorEndRotation' by the interpolation time 'timer / rotationTime' and assign it to the 'leftDoor' rotation
		// ... use Quaternion.Slerp() to interpolate from 'rightDoorStartRotation' to 'rightDoorEndRotation' by the interpolation time 'timer / rotationTime' and assign it to the 'leftDoor' rotation
		// ... use Time.deltaTime to increment 'timer'

		if (!opening)  return;// do nothing if opening is false
		leftdoor.transform.rotation = Quaternion.Slerp (leftDoorStartRotation, leftDoorEndRotation, timer / rotationTime);
		rightdoor.transform.rotation = Quaternion.Slerp (rightDoorStartRotation, rightDoorEndRotation, timer / rotationTime);
		timer += Time.deltaTime;

	}
		
	public void OnDoorClicked () {
		/// Called when the 'Left_Door' or 'Right_Door' game object is clicked
		/// - Starts opening the door if it has been unlocked
		/// - Plays an audio clip when the door starts opening

		// Prints to the console when the method is called
		Debug.Log ("'Door.OnDoorClicked()' was called");

		// TODO: If the door is unlocked, start animating the door rotating open and play a sound to indicate the door is opening
		// Use 'locked' to check if the door is locked and ...
		// ... start the animation defined in Update() by changing the value of 'opening'
		// ... use 'audioSource' to play the AudioClip assigned to the AudioSource component

		if (locked) {
			// OPTIONAL-CHALLENGE: Play a different sound if the door is locked
			// TIP: You could get a reference to the 'Door_Locked' audio and play it without assigning it to the AudioSource component
			AudioSource.PlayClipAtPoint(doorlockClip,Camera.main.transform.position);
			if(gameObject.name.ToLower().Contains("entry")|| gameObject.name.ToLower().Contains("main"))
			    scrbrd.UpdateHints(" To open this door, find the key." );
			if(gameObject.name.ToLower().Contains("inside"))
				scrbrd.UpdateHints(" To open this door, find the key close to a chest." );
			return;
		}
		opening=true;
		audioSource.Play ();
		/*leftdoor.transform.Rotate (new Vector3 (0, 0, -90) * Time.deltaTime * 0.8f);
		  rightdoor.transform.Rotate (new Vector3 (0, 0, 90) * Time.deltaTime * 0.8f);*/
		
		// OPTIONAL-CHALLENGE: Prevent the door from being interacted with after it has started opening
		// TIP: You could disable the Event Trigger component, or for an extra challenge, try disabling all the Collider components on all children
		DisableCollidersTrigger();
		scrbrd.IncrementDoorOpened ();
		if(gameObject.name.ToLower().Contains("entry"))
			scrbrd.UpdateHints(" Opening Entry Door, Collect coins and find the Chest." );
		if(gameObject.name.ToLower().Contains("inside"))
			scrbrd.UpdateHints(" Opening Internal Door, You are close to your destination" );
		if(gameObject.name.ToLower().Contains("main"))
			scrbrd.UpdateHints("You made it!!Click on the Sign Post if you wish to play again." );

	}


	public void Unlock () {
		/// Called from Key.OnKeyClicked(), i.e. the Key.cs script, when the 'Key' game object is clicked
		/// - Unlocks the door

		// Prints to the console when the method is called
		Debug.Log ("'Door.Unlock()' was called");
		// TODO: Unlock the door 
		// Unlock the door by changing the value of 'locked'
		locked=false;
		scrbrd.UpdateHints("Unlocking the door.... click the door to open." );
	}

	private void DisableCollidersTrigger() {
		EventTrigger current_trig=GetComponent<EventTrigger>();
		current_trig.enabled = false;//disable trigger
		foreach(Collider coll in GetComponents<Collider>())//disable all colliders
			coll.enabled = false;

	}
}
