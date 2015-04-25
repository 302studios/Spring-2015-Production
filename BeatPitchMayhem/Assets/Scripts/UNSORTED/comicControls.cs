using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class comicControls : MonoBehaviour {

	public Vector3[] theComicLocs;
	public Vector3[] topBorderLocs;
	public Vector3[] bottomBorderLocs;
	public Vector3[] leftBorderLocs;
	public Vector3[] rightBorderLocs;
	
	public float[] topBorderRots;
	public float[] bottomBorderRots;
	public float[] leftBorderRots;
	public float[] rightBorderRots;

	public float[] theComicScales;

	public GameObject theComic;
	public GameObject topBorder;
	public GameObject bottomBorder;
	public GameObject leftBorder;
	public GameObject rightBorder;

	public int numPanels;
	public int currPanel;

	public bool introDone;

	public Text descriptionText;
	public string[] descriptions;

	public float transitionSpeed;
	public string nextScene;

	bool canPad;
	bool canStick;

	// Use this for initialization
	void Start () {
	
		currPanel = 0;
		numPanels = theComicLocs.Length;
		//introDone = false;

		canPad = true;
		canStick = true;

	}
	
	// Update is called once per frame
	void Update () {

		if (introDone && (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow) || ((Input.GetAxis("Horizontal") > .75f) && canStick))) {
			if ((currPanel < numPanels)) {
				currPanel++;
				//panelTransition ();
			} 
			if(currPanel == numPanels)
			{
				Application.LoadLevel(nextScene);
			}

			canStick = false;

		} else if(introDone && (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow) || ((Input.GetAxis("Horizontal") < -.75f) && canStick))){
			if((currPanel > 0)) {
				currPanel--;
			}
			canStick = false;
		}
		else {
			if ((Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow) || ((Input.GetAxis("Horizontal") > .75f) && canStick))) {
				panelTransition ();
				introDone = true;
				canStick = false;
			}
		}

		if (introDone && ((Input.GetAxis("PadX") > .75f) && canPad)) {
			if ((currPanel < numPanels)) {
				currPanel++;
				//panelTransition ();
			} 
			if(currPanel == numPanels)
			{
				Application.LoadLevel(nextScene);
			}
			
			canPad = false;
			
		} else if(introDone && ((Input.GetAxis("PadX") < -.75f) && canPad)){
			if((currPanel > 0)) {
				currPanel--;
			}
			canPad = false;
		}
		else {
			if (((Input.GetAxis("PadX") > .75f) && canPad)) {
				panelTransition ();
				introDone = true;
				canPad = false;
			}
		}


		if(introDone)
			panelTransition ();

		if (Input.GetAxis ("Horizontal") == 0f) {
			canStick = true;
		}
		if (Input.GetAxis ("PadX") == 0f) {
			canPad = true;
		}

	}

	void panelTransition(){

		// Positions
		theComic.transform.localPosition = Vector3.Lerp(theComic.transform.localPosition, theComicLocs [currPanel], Time.deltaTime*transitionSpeed);
		topBorder.transform.localPosition = Vector3.Lerp(topBorder.transform.localPosition, topBorderLocs [currPanel], Time.deltaTime*transitionSpeed);
		bottomBorder.transform.localPosition = Vector3.Lerp(bottomBorder.transform.localPosition, bottomBorderLocs [currPanel], Time.deltaTime*transitionSpeed);
		leftBorder.transform.localPosition = Vector3.Lerp(leftBorder.transform.localPosition, leftBorderLocs [currPanel], Time.deltaTime*transitionSpeed);
		rightBorder.transform.localPosition = Vector3.Lerp(rightBorder.transform.localPosition, rightBorderLocs [currPanel], Time.deltaTime*transitionSpeed);

		// Rotations
		/*Quaternion topRot = new Quaternion(0,0,0,0);
		topRot.eulerAngles = new Vector3(topBorder.transform.localRotation.eulerAngles.x,
		                             topBorder.transform.localRotation.eulerAngles.y,
		                             topBorderRots[currPanel]);
		Quaternion bottomRot = new Quaternion(0,0,0,0);
		bottomRot.eulerAngles = new Vector3(bottomBorder.transform.localRotation.eulerAngles.x,
		                                bottomBorder.transform.localRotation.eulerAngles.y, 
		                                bottomBorderRots[currPanel]);
		Quaternion leftRot = new Quaternion(0,0,0,0);
		leftRot.eulerAngles = new Vector3(leftBorder.transform.localRotation.eulerAngles.x,
		                              leftBorder.transform.localRotation.eulerAngles.y,
		                              leftBorderRots[currPanel]);
		Quaternion rightRot = new Quaternion(0,0,0,0);
		rightRot.eulerAngles = new Vector3(rightBorder.transform.localRotation.eulerAngles.x,
		                               rightBorder.transform.localRotation.eulerAngles.y,
		                               rightBorderRots[currPanel]);
		*/

		Quaternion topRot = new Quaternion(0,0,0,0);
		topRot.eulerAngles = new Vector3(topBorder.transform.localRotation.eulerAngles.x,
		                                 topBorder.transform.localRotation.eulerAngles.y,
		                                 topBorderRots[currPanel]);
		Quaternion bottomRot = new Quaternion(0,0,0,0);
		bottomRot.eulerAngles = new Vector3(bottomBorder.transform.localRotation.eulerAngles.x,
		                                    bottomBorder.transform.localRotation.eulerAngles.y, 
		                                    bottomBorderRots[currPanel]);
		Quaternion leftRot = new Quaternion(0,0,0,0);
		leftRot.eulerAngles = new Vector3(leftBorder.transform.localRotation.eulerAngles.x,
		                                  leftBorder.transform.localRotation.eulerAngles.y,
		                                  leftBorderRots[currPanel]);
		Quaternion rightRot = new Quaternion(0,0,0,0);
		rightRot.eulerAngles = new Vector3(rightBorder.transform.localRotation.eulerAngles.x,
		                                   rightBorder.transform.localRotation.eulerAngles.y,
		                                   rightBorderRots[currPanel]);

		topBorder.transform.localRotation = topRot;
		bottomBorder.transform.localRotation = bottomRot;
		leftBorder.transform.localRotation = leftRot;
		rightBorder.transform.localRotation = rightRot;

		// Scales
		Vector3 comicScaling = new Vector3 (theComicScales [currPanel],
		                                            theComicScales [currPanel],
		                                            theComicScales [currPanel]);
		theComic.transform.localScale = Vector3.Lerp(theComic.transform.localScale, comicScaling, Time.deltaTime*transitionSpeed);

		// Text
		descriptionText.text = descriptions [currPanel];

	}
}
