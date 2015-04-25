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
	
		if (Input.GetKey(KeyCode.Return) && isLoadingScene) {
			PlayerPrefs.SetString("Last Scene", sceneName);
			Application.LoadLevel (sceneName);
			//PlayerPrefs.SetString("Last Scene", sceneName);
		}
		if (Input.GetKey(KeyCode.Joystick1Button7) && isLoadingScene) {
			PlayerPrefs.SetString("Last Scene", sceneName);
			Application.LoadLevel (sceneName);
			//PlayerPrefs.SetString("Last Scene", sceneName);
		}
		if(Input.GetKey(KeyCode.Escape))
			Application.Quit();
		if(Input.GetKey(KeyCode.Joystick1Button6))
			Application.Quit();

	}

	void OnTriggerEnter(Collider col){

		if (col.tag == "Player") {
			PlayerPrefs.SetString ("Last Scene", sceneName);
			Application.LoadLevel (sceneName);
		}
	}
}
