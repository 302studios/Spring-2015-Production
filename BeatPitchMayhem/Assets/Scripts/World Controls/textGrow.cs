using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textGrow : MonoBehaviour {

	public int size = 140;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(this.GetComponent<Text> ().fontSize < size)
			this.GetComponent<Text> ().fontSize++;

	}
}
