using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class crossHairCustom : MonoBehaviour {

	public Sprite[] crosshairs;
	Image display;

	public sonicCompressor sC;
	public lightController controller;

	// Use this for initialization
	void Start () {

		display = this.GetComponent<Image>();
		display.sprite = crosshairs [0];
		sC = GameObject.Find ("Sonic Compressor").GetComponent<sonicCompressor>();
		controller = GameObject.Find ("Light Controller").GetComponent<lightController> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (sC && controller) {
			if (sC.active) {
				display.sprite = crosshairs [3];
			}
			else if(controller.active){
				display.sprite = crosshairs [2];
			}
			else
				display.sprite = crosshairs [0];
		}


	}
}
