using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pauseScreen : MonoBehaviour {

	public characterMovement thePlayer;
	public Image metDetails;
	public Image lcDetails;
	public Image scDetails;
	public Text Tooltip1Text;
	public Text Tooltip2Text;
	public Text Tooltip3Text;
	public Image TooltipBkgd;


	// Use this for initialization
	void Start () {
	
		thePlayer = GameObject.Find ("First Person Controller").GetComponent<characterMovement> ();
		metDetails.enabled = false;
		lcDetails.enabled = false;
		scDetails.enabled = false;
		Tooltip1Text.enabled = false;
		Tooltip2Text.enabled = false;
		Tooltip3Text.enabled = false;
		TooltipBkgd.enabled = false;
		this.GetComponent<Text> ().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (!(metDetails.enabled ||
			lcDetails.enabled ||
			scDetails.enabled ||
			Tooltip1Text.enabled ||
			Tooltip2Text.enabled ||
			Tooltip3Text.enabled ||
			TooltipBkgd.enabled)) {
			if (thePlayer.canControl)
				this.GetComponent<Text> ().enabled = false;
			else
				this.GetComponent<Text> ().enabled = true;
		}

		if (Input.GetKey (KeyCode.Return) || Input.GetKey (KeyCode.JoystickButton7)) {

			metDetails.enabled = false;
			lcDetails.enabled = false;
			scDetails.enabled = false;
			Tooltip1Text.enabled = false;
			Tooltip2Text.enabled = false;
			Tooltip3Text.enabled = false;
			TooltipBkgd.enabled = false;
		}

	}
}
