using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class toolTipsMain : MonoBehaviour {


	public toolTip toolTip1;
	public toolTip toolTip2Left;
	public toolTip toolTip2Right;
	public toolTip toolTip3;

	bool tip1Done;
	bool tip2Done;
	bool tip3Done;

	public pauseScreen pause;
	public mouseLookBPM camRotX;
	public mouseLookBPM camRotY;


	// Use this for initialization
	void Start () {
	
		pause = GameObject.Find ("Paused").GetComponent<pauseScreen> ();
		camRotX = GameObject.Find ("First Person Controller").GetComponent<mouseLookBPM> ();
		camRotY = GameObject.Find ("Main Camera").GetComponent<mouseLookBPM> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (!tip1Done && toolTip1.playerEntered) {

			pause.TooltipBkgd.enabled = true;
			pause.Tooltip1Text.enabled = true;
			pause.ContinueText.enabled = true;
			pause.thePlayer.canControl = false;
			camRotX.canControl = false;
			camRotY.canControl = false;
			tip1Done = true;
		}

		if (!tip2Done && (toolTip2Left.playerEntered || toolTip2Right.playerEntered)) {
			
			pause.TooltipBkgd.enabled = true;
			pause.Tooltip2Text.enabled = true;
			pause.ContinueText.enabled = true;
			pause.thePlayer.canControl = false;
			camRotX.canControl = false;
			camRotY.canControl = false;
			tip2Done = true;
		}

		if (!tip3Done && toolTip3.playerEntered) {
			
			pause.TooltipBkgd.enabled = true;
			pause.Tooltip3Text.enabled = true;
			pause.ContinueText.enabled = true;
			pause.thePlayer.canControl = false;
			camRotX.canControl = false;
			camRotY.canControl = false;
			tip3Done = true;
		}

	}
}
