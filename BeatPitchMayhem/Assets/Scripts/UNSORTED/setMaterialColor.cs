using UnityEngine;
using System.Collections;

public class setMaterialColor : MonoBehaviour {
	
	randomColor theColor;
	Material theGlow;

	// Use this for initialization
	void Start () {
		
		theColor = this.GetComponentInParent<randomColor> ();
		theGlow = renderer.materials[1];
		theGlow.color = theColor.colors [theColor.selectedCol];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
