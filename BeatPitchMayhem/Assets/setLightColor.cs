using UnityEngine;
using System.Collections;

public class setLightColor : MonoBehaviour {

	randomColor theColor;

	// Use this for initialization
	void Start () {
	
		theColor = this.GetComponentInParent<randomColor> ();
		this.GetComponent<Light> ().color = theColor.colors [theColor.selectedCol];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
