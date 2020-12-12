using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGamePlay : MonoBehaviour {

	// Use this for initialization

	public GameObject teleportTarget, gameGoal;
	//public GoalTracker goal;

	public GameObject door_right,door_left,poofprefab,winningsign;
	public GameObject rubePrefab,throwPrefab;
	public List<GameObject> RubeBallObject = new List<GameObject> ();

	public UIInterface uihelp;
	public UICheatNotif uicheat;
	public bool isFinalLevel=false;

    public GameObject winningroom;
	public AudioClip[] audiofiles;
	private GameObject ThrowBallObject;

	private List<Vector3> startPosRubeBall = new List<Vector3>();
	private Vector3  startPosThrowBall,startPosMainBall;


	private bool transitionComplete = false;
    public SteamVR_LoadLevel loadlevel;
    static int countBallItems=1;
	private AudioSource playsuccess;

	static int ballsPlayed = 0;
	static int prizeCount = 0;

	static bool isLevelComplete = false;
	static bool isLevelFail=false;

	void Start () {
		gameGoal.SetActive (true);
		teleportTarget.SetActive (false);
		countBallItems = RubeBallObject.Count;
		ThrowBallObject= this.transform.Find ("ThrowBall").gameObject;
		startPosThrowBall = ThrowBallObject.transform.position;
		playsuccess = this.GetComponent<AudioSource> ();
        if (winningsign != null)
            winningsign.SetActive(false);
        if (winningroom != null)
            winningroom.SetActive(false);

        for (int i = 0; i < countBallItems; i++) {
			startPosRubeBall.Add (RubeBallObject [i].transform.position);
			Debug.Log ("Count of Rube balls : " + countBallItems);
		}
        isLevelComplete = false;
        transitionComplete = false;
        ballsPlayed = 0;
        isLevelFail = false;
    }

	// Update is called once per frame
	void Update () {

		if (isLevelComplete && !transitionComplete) {
            playsuccess.clip = audiofiles[0];
            playsuccess.Play();
            transitionComplete = true;
			teleportTarget.SetActive (true);
			uihelp.UpdateStatus ("LEVEL COMPLETED. !!!!");
			uihelp.UpdateHint ("Access Teleport Port to go to next level");
			Debug.Log ("LEVEL COMPLETED. !!!!");
            StartCoroutine(WaitForAudioFinish()); 



        }

		if (isLevelFail  && ThrowBallObject==null && !isLevelComplete) {
			ReInitLevel ();
			isLevelFail = false;
			uihelp.UpdateStatus ("Level Failed.Try Again");
			playsuccess.clip = audiofiles [1];
			playsuccess.Play ();
		}

	}
    IEnumerator WaitForAudioFinish()
    {
        yield return new WaitForSeconds(1.0f);
        if (isFinalLevel)
        {
            teleportTarget.SetActive(false);
            OpenDoors();

        }
        else
            loadlevel.Trigger();
    }

    public void ActivateLevelComplete() {

		uihelp.UpdateStatus ("Collected "+ ballsPlayed + " out of "+countBallItems);
		if (ballsPlayed >= countBallItems) {
			isLevelComplete = true;
			ballsPlayed = 0;

	        

		} else {
			ballsPlayed = 0;
			isLevelFail = true;
			uihelp.UpdateStatus ("Level Failed.Try Again");
			uihelp.UpdateHint ("Main Ball must hit all Red Balls and finally hit the Goal/Target.");


		}
			
	}

	public void ActivateLevelFail(){
		if (!isLevelComplete) {
			Debug.Log ("Activiting Level Failed.");
			ballsPlayed = 0;
			isLevelFail = true;
		}

	}
	public void IncrementBallHit() {

		ballsPlayed++;

	}

	public void IncrementCollectibles() {

		prizeCount++;
		uihelp.UpdateStatus("Prizes collected ="+prizeCount);
	}

	void OpenDoors() {
		Instantiate (poofprefab, door_right.transform.position, door_right.transform.rotation);
		Instantiate (poofprefab, door_left.transform.position, door_left.transform.rotation);
		StartCoroutine (DestroyDoors ());
	}

	IEnumerator DestroyDoors() {
		yield return new WaitForSeconds(0.5f);
		door_right.SetActive (false);
		door_left.SetActive (false);
		winningsign.SetActive (true);
        winningroom.SetActive(true);

    }


	 void ReInitLevel() {
		Debug.Log ("Reinitalizing");
		if (ThrowBallObject == null) {
			ReCreateBall ("ThrowBall", 0);
			//ballsPlayed = 0;
		}
		
		for (int i = 0; i < countBallItems; i++) {
		    if (RubeBallObject [i] == null) {
					ReCreateBall ("RubeBall", i);
					//Debug.Log (" Rube Balls initialzed" + RubeBallObject.Count);
			}
		 }
		 isLevelFail = false;
		 transitionComplete = false;
	        
	    }
	void ReCreateBall(string name,int index) {

			
			if (name.Equals ("ThrowBall")) {
				ThrowBallObject = Instantiate (throwPrefab, startPosThrowBall, throwPrefab.transform.rotation);
				//Debug.Log ("Instantiating Throw Ball");
			}  else {
				RubeBallObject[index] = Instantiate (rubePrefab, startPosRubeBall[index], rubePrefab.transform.rotation);
				//Debug.Log ("Instantiating Rube Ball"+index);
			}
				
	}

}
