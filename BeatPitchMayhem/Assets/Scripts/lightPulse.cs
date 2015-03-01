using UnityEngine;
using System.Collections;
using SynchronizerData;

public class lightPulse : MonoBehaviour {

	private Light lightControls;
	public bool pulseToggle = false;
	public int lowIntensity;
	public int highIntensity;

	public bool onBeat = true;
	public bool downBeat = false;

	private BeatObserver beatObserver;

	public Color[] lightColors;
	public int numOfColors;
	private int colorIndex;
	public int startColor;

	// Use this for initialization
	void Start () {
	
		lightControls = this.gameObject.GetComponent<Light> ();
		//pulseToggle = false;
		beatObserver = GetComponent<BeatObserver>();
		lowIntensity = 0;
		highIntensity = 8;
		colorIndex = startColor;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (onBeat && ((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat)) {
			//Pulse();
			Pulse();
		}
		if (downBeat && ((beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat)) {
			Pulse ();
		}

		if(pulseToggle){
			lightControls.intensity = highIntensity;
		}
		else
			lightControls.intensity = lowIntensity;

	}

	void Pulse () {

		pulseToggle = !pulseToggle;
		if (pulseToggle) {
			lightControls.color = lightColors [colorIndex];
			colorIndex++;
			if (colorIndex >= numOfColors)
					colorIndex = 0;
		}
		//Debug.Log ("Switch: " + pulseToggle);
	}
}
