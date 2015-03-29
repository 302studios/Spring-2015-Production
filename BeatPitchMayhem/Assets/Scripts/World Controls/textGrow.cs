using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textGrow : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(this.GetComponent<Text> ().fontSize < 140)
			this.GetComponent<Text> ().fontSize++;

	}
}
