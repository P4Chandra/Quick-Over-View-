

	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class OculusController : MonoBehaviour {

		//public SteamVR_TrackedObject trackedobj;
		//public SteamVR_Controller.Device device;

		//Teleporter
		private LineRenderer laser;
		private static float ynudgeamount = 0.5f;// specific to teleportAimerObject height
		private static Vector3 ynudgevector= new Vector3(0f,ynudgeamount,0f);
		public GameObject teleportAimerObject;
		public Vector3 teleportLocation;
		public GameObject player;
		public LayerMask laserMask;
    

    //Dashing
        private bool isdashing=false;
		private Vector3 dashstartposition;
		private float lerptime;
		public float dashspeed=20f;

		//walking 

		/*public Vector3 movementDirection;
		public float movespeed = 4f;
		public Transform playerCam;*/

	    private bool isGameStarted=false;
        private bool isOutofrange = false;
        private Vector3 prevLocation;
	  
	   // For  Cheating Indication
	public UICheatNotif cheat;
	  
        // Use this for initialization
    void Start () {
			//trackedobj = GetComponent<SteamVR_TrackedObject> ();
			laser = GetComponentInChildren<LineRenderer> ();
           prevLocation = player.transform.position;
    }

		//When trigger pressed or Released
		void PressTriggerAction()
        {
                
                if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)) 
			    {
                    prevLocation = player.transform.position;
                    laser.gameObject.SetActive (true);
				    teleportAimerObject.SetActive (true);
                   //set start point of laser beam to hand controller
                    laser.SetPosition (0, gameObject.transform.position);
				    RaycastHit hit;
                //detect the target to teleport to using Raycaster

                    if (Physics.Raycast(transform.position, transform.forward, out hit, 15, laserMask))
                        teleportLocation = hit.point;
                    else
                    {

                        teleportLocation = transform.position + 10 * transform.forward;
                    }
                    teleportLocation.y = 1.50f;

                    laser.SetPosition(1, teleportLocation);
                    teleportAimerObject.transform.position = teleportLocation + ynudgevector;
                   // Debug.Log("Final Teleport Location = "+ teleportLocation.ToString());
                }
		        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)) 
			    {
				    laser.gameObject.SetActive (false);
				    teleportAimerObject.SetActive (false);
				    //player.transform.position = teleportLocation; // This is instantly moving to a point
				    dashstartposition = player.transform.position;// animation movement to a point
                    
                    isdashing = true;

			    }

		}


		// Update is called once per frame
		void Update () {
        if (isOutofrange)
           player.transform.position = prevLocation;

		if (isGameStarted && (OVRInput.Get (OVRInput.Button.SecondaryIndexTrigger) ||
            OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x !=0
            )) {
			cheat.UpdateCheatText ("CAUGHT CHEATING !!!\n\n"+"Cannot move with ThrowBall in hand.");

		}
        if (isGameStarted|| isOutofrange)
        {
            //Disable Teleporting when you touch Ball or when you are outside teleportable area.
            return;
        }
               //cheat.UpdateCheatText("");
                if (isdashing) {
				lerptime += Time.deltaTime * dashspeed;
				player.transform.position = Vector3.Lerp (dashstartposition, teleportLocation, lerptime);
				if (lerptime >= 1) {
					isdashing = false;
					lerptime = 0;
				}
			} else
				PressTriggerAction ();//Perform trigge action

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstick) ||
            OVRInput.GetUp(OVRInput.Button.SecondaryThumbstick))
            prevLocation = player.transform.position;

    }
  
	public void SetGameFlag(bool gameflag) {
		isGameStarted = gameflag;
	}



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moveable") || other.CompareTag("PlayArea"))
            isOutofrange = false;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Moveable"))
        {
            isOutofrange = true;
        }
        
    }

}

