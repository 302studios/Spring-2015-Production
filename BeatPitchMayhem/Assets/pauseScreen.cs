using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pauseScreen : MonoBehaviour {

	public characterMovement thePlayer;
	public Image metDetails;
	public Image lcDetails;
	public Image scDetails;


	// Use this for initialization
	void Start () {
	
		thePlayer = GameObject.Find ("First Person Controller").GetComponent<characterMovement> ();
		metDetails.enabled = false;
		lcDetails.enabled = false;
		scDetails.enabled = false;
		this.GetComponent<Text> ().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Return) || Input.GetKey (KeyCode.JoystickButton7)) {

			if (thePlayer.canControl)
				this.GetComponent<Text> ().enabled = false;
			else
				this.GetComponent<Text> ().enabled = true;

			metDetails.enabled = false;
			lcDetails.enabled = false;
			scDetails.enabled = false;
		}

	}
}
