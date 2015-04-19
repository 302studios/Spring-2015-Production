using UnityEngine;
using System.Collections;

public class sceneTransition : MonoBehaviour {

	public string sceneName;
	public bool isLoadingScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyUp (KeyCode.Space) && isLoadingScene) {
			PlayerPrefs.SetString("Last Scene", sceneName);
			Application.LoadLevel (sceneName);
			//PlayerPrefs.SetString("Last Scene", sceneName);

		}

	}

	void OnTriggerEnter(Collider col){

		if (col.tag == "Player") {
			PlayerPrefs.SetString ("Last Scene", sceneName);
			Application.LoadLevel (sceneName);
		}
	}
}
