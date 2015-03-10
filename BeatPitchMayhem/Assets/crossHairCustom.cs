using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class crossHairCustom : MonoBehaviour {

	public Sprite[] crosshairs;
	Image display;

	public sonicCompressor sC;

	// Use this for initialization
	void Start () {

		display = this.GetComponent<Image>();
		display.sprite = crosshairs [0];

	}
	
	// Update is called once per frame
	void Update () {
	
		if (sC.active) {
			display.sprite = crosshairs [3];
		} else
			display.sprite = crosshairs [0];


	}
}
