using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Coin : MonoBehaviour {

	// TODO: Create variables to reference the game objects we need access to
	// Declare a GameObject named 'coinPoofPrefab' and assign the 'CoinPoof' prefab to the field in Unity
	public float speed;
	public GameObject coinPoofPrefab;
	public score_board scrbrd; 

	//private AudioSource onclickSound;



	void Update () {
		// OPTIONAL-CHALLENGE: Animate the coin rotating
		// TIP: You could use a method from the Transform class
		transform.Rotate(new Vector3(0,30,0)*Time.deltaTime*speed);//rotates to 30 degrees along y axis per frame.
	}


	public void OnCoinClicked () {
		/// Called when the 'Coin' game object is clicked
		/// - Displays a poof effect (handled by the 'CoinPoof' prefab)
		/// - Plays an audio clip (handled by the 'CoinPoof' prefab)
		/// - Removes the coin from the scene

		// Prints to the console when the method is called
		Debug.Log ("'Coin.OnCoinClicked()' was called");


		// TODO: Display the poof effect and remove the coin from the scene
		// Use Instantiate() to create a clone of the 'CoinPoof' prefab at this coin's position and with the 'CoinPoof' prefab's rotation
		// Use Destroy() to delete the coin after for example 0.5 seconds
	    Instantiate(coinPoofPrefab, transform.position, transform.rotation);
		StartCoroutine (RemoveCoin ());

	}

	IEnumerator RemoveCoin() {
		Debug.Log("removing coins");
	    yield return new WaitForSeconds (0.5f);
		Destroy (gameObject);
		scrbrd.IncrementCoins ();
	}
}
