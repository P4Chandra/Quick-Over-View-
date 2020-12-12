using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public List<GameObject> objectList;
	public List<GameObject> prefabList;
	public int currentObject = 0;

	//Scene Transition
	//public SteamVR_LoadLevel levelloader;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			objectList.Add (child.gameObject);
			Debug.Log ("adding prefab " + child.gameObject.name);
		}

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MenuLeft(){
		objectList [currentObject].SetActive (false);
		currentObject--;
		if (currentObject < 0)
			currentObject = objectList.Count - 1;
		objectList [currentObject].SetActive (true);
	}

	public void MenuRight(){
		objectList [currentObject].SetActive (false);
		currentObject++;
		if (currentObject > objectList.Count - 1)
			currentObject = 0;
		objectList [currentObject].SetActive (true);
	}

	public void SpawnCurrentObject(){

		Instantiate (prefabList [currentObject], objectList [currentObject].transform.position,
			        objectList [currentObject].transform.rotation);

		//levelloader.Trigger ();
		
	}

}
