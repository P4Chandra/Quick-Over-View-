using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_chest_Script : MonoBehaviour {



	// Use this for initialization
	Quaternion chestopen_start;
	Quaternion chestopen_end;

	bool isopening=false;
	float timer=0.0f;

	public GameObject chestlid;
	public score_board scrbrd;

	void Start () {
		chestopen_start = chestlid.gameObject.transform.rotation;
		chestopen_end = chestopen_start * Quaternion.Euler (110.0f, 0.0f, 0.0f);
	}



	void Update()
	{
		if (!isopening)
			return;
		chestlid.transform.rotation = Quaternion.Slerp (chestopen_start, chestopen_end, timer / 8.0f);
		timer = timer + Time.deltaTime;
	}

    public void OnChestClick(){
		if (isopening)
			return;
		isopening = true;
		Debug.Log("chest is opening");
		scrbrd.IncrementChestOpened ();


	}
}
