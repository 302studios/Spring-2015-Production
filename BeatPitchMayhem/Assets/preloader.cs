using UnityEngine;
using System.Collections;

public class preloader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey(KeyCode.Space))
			Application.LoadLevel ("Lounge Test");
		if(Input.GetKey(KeyCode.Escape))
			Application.Quit();

	}
}
