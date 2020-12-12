using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour {

	// Use this for initialization
	static bool isFloor =true;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider feet)
    {
        Debug.Log("platform touches " + feet.tag);
        if (   feet.tag.Contains("Floor") || feet.tag.Contains("Ball")
            || feet.tag.Contains("Play")  || feet.tag.Contains("Goal")
            || feet.tag.Contains("Level")
            )
        {
            Debug.Log("Teleport is not on Floor or PlayArea");
            isFloor = true;

        }
        else
            isFloor = false;
    }

    public bool IsFloorSurface()
    {
        return isFloor;
    }

}
