using UnityEngine;
using System.Collections;

public class toolTip : MonoBehaviour {

	public bool playerEntered;

	// Use this for initialization
	void Start () {

		playerEntered = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){

		if (col.tag == "Player") {
			playerEntered = true;
		}

	}
}
