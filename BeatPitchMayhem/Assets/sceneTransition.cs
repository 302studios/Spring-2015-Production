using UnityEngine;
using System.Collections;

public class sceneTransition : MonoBehaviour {

	public string sceneName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){

		if (col.tag == "Player")
			Application.LoadLevel ("L2a-Rock");
	}
}
