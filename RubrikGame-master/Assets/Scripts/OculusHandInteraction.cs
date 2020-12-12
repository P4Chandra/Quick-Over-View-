using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusHandInteraction : MonoBehaviour
{

    //Grab and throw objects
   // private SteamVR_Controller.Device device;
   // public SteamVR_TrackedObject trackedobj;
    public float throwForce = 1.5f;

    private OVRInput.Controller thisController;
    public bool leftHand;//if true this is the left hand controller.


    //Swip action 

   // public float swipeSum;
   // public float touchLast;
   // public float touchCurrent;
   // public float distance;
   // public bool hasswipedLeft = false;
   // public bool hasswipedRight = false;
    public MenuManager objectManager;

    private bool menuIsSwipable = false;
    private float menuStickX;

    private bool oculus = false; // determine if you are using oculus 

    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        //device = SteamVR_Controller.Input((int)trackedobj.index);

			menuStickX = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;

            if (menuStickX < 0.45f && menuStickX > -0.45f)
                menuIsSwipable = true;

            if (menuIsSwipable && menuStickX >= 0.45f)
            {
                //firefunction that looks at menu
                //disables current item and enables next item
                objectManager.MenuLeft();
                menuIsSwipable = false;

            }
            else if (menuIsSwipable && menuStickX <= -0.45f)
            {
                objectManager.MenuRight();
                menuIsSwipable = false;

            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                objectManager.SpawnCurrentObject();
            }
       
    }

    void SwipeRight()
    {
		Debug.Log("swipt right");
        objectManager.MenuRight();
    }

    void SwipeLeft()
    {
		Debug.Log("swipt Left");
		objectManager.MenuLeft();   
    }



 

}
