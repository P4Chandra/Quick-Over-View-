using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGamePlay : MonoBehaviour {

	// Use this for initialization

	public OVRPlayerController controller;//enable or disable movement via left thumbstick
	public OculusController teleport;//enable or disable teleportation via left trigger
	public UIInterface uihelp;
	public UICheatNotif uicheat;

	//Object Menu tracking
	//private int countBallItems;
	private bool isinPlayArea=false;
    private bool isBallDisabled=false;
    private Collider tempball;
    private bool ischeatingcaught = false;

	void OnTriggerStay(Collider other){//was stay previously
         if (other.gameObject.CompareTag ("ThrowBall") && isinPlayArea) {
            //CHEAT CHECK - Disable teleporting and  movement when Throw Ball is grabbed by player
            Debug.Log("You are now playing");
            controller.SetGameFlag( true);
            teleport.SetGameFlag( true);
            uihelp.UpdateStatus ("Started playing you cannot move with the ball");
			uihelp.UpdateHint("Get rid of the Ball if you wish to move");// UI hint here
        }
        if (other.gameObject.CompareTag("ThrowBall") && !isinPlayArea)
        {
            DisableBallGrab(other);
            uicheat.UpdateCheatText("CAUGHT CHEATING !!!\n\n" + 
                                    "You are allowed to grab GameBall from Platform only\n\n"+
                                    "Stand behind the Pedestal with ThrowBall to grab the ball");
            Debug.Log("You cannot play game 'cause you arent in playarea");
            ischeatingcaught = true;
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("NoAction"))
        {
            isinPlayArea = false;
            Debug.Log("Detected Floor");
        }
        else if (other.CompareTag("PlayArea"))
        {
            isinPlayArea = true;
            if (tempball != null)
                EnableBallGrab(tempball);
            Debug.Log("Detected PlayArea");
            ischeatingcaught = false;
        }


	}
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ThrowBall"))
        {

            //CHEAT CHECK - Enable teleporting and  movement when Throw Ball is released
            if(!ischeatingcaught)
                uicheat.UpdateCheatText("");
            controller.SetGameFlag(false);
            teleport.SetGameFlag(false);
            uihelp.UpdateStatus("Player Movement Enabled");
        }

    }
    private void DisableBallGrab(Collider ball){
        if (isBallDisabled)
        {
            return;
        }
        tempball = ball;
        Rigidbody rbball = ball.gameObject.GetComponentInParent<Rigidbody>();
        rbball.isKinematic = true;
        rbball.useGravity = false;
        rbball.detectCollisions = false;
        isBallDisabled = true;

    }
	private void EnableBallGrab(Collider ball){

        Rigidbody rbball = ball.gameObject.GetComponentInParent<Rigidbody>();
        rbball.isKinematic = false;
		rbball.useGravity = true;
        rbball.detectCollisions = true;
        isBallDisabled = false;
        uicheat.UpdateCheatText("");
    }

}
