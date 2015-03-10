using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stunUI : MonoBehaviour {

	enemyControls me;
	Text stunText;

	// Use this for initialization
	void Start () {
	
		me = this.gameObject.GetComponentInParent<enemyControls> ();
		stunText = this.GetComponentInChildren<Text> ();
		stunText.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	
		if (me.stunned) {
			stunText.enabled = true;
		} else
			stunText.enabled = false;

	}
}
