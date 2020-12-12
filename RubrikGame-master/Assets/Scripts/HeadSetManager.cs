using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadSetManager : MonoBehaviour {
	public GameObject viveheadset;
	public GameObject oculusheadset;

	private bool hmdchosen = false;
	// Use this for initialization
	void Start () {
		if (XRDevice.model.Equals ("vive")) {
			viveheadset.SetActive (true);
			oculusheadset.SetActive (false);
			hmdchosen = true;
		} else if(XRDevice.model.Equals ("oculus")){
			viveheadset.SetActive (false);
			oculusheadset.SetActive (true);
			hmdchosen = true;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!hmdchosen) {
			if (XRDevice.model.Equals ("vive")) {
				viveheadset.SetActive (true);
				oculusheadset.SetActive (false);
				hmdchosen = true;
			} else if(XRDevice.model.Equals ("oculus")){
				viveheadset.SetActive (false);
				oculusheadset.SetActive (true);
				hmdchosen = true;
			}
		}
		if(!XRDevice.isPresent)
			hmdchosen = false;
	}
}
