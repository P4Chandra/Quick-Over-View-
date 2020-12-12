using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICheatNotif : MonoBehaviour {

	// Use this for initialization
	public Text cheattext;

	void Start () {
		cheattext.text = "CAUGHT CHEATING !!!\n\n";
        cheattext.gameObject.SetActive(false);
    }

	public void UpdateCheatText(string content)
	{
        cheattext.gameObject.SetActive(true);
		cheattext.text = content;
        MessageDeactivate();

    }

    IEnumerator MessageDeactivate()
    {
        yield return new WaitForSeconds(1.0f);
        cheattext.gameObject.SetActive(false);
    }

}
