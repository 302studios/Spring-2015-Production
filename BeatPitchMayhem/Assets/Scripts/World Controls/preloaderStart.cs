using UnityEngine;
using System.Collections;

public class preloaderStart : MonoBehaviour {

	public string sceneName;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		//sceneName = PlayerPrefs.GetString ("Last Scene");

		if (Input.GetKey (KeyCode.Space) && sceneName == "Start2") {
			PlayerPrefs.SetString("UsingJoy", "false");
			Application.LoadLevel (sceneName + "Keyboard");
		}
		else if (Input.GetKey (KeyCode.Space)) {
			Application.LoadLevel (sceneName);
			PlayerPrefs.SetString("UsingJoy", "false");
		}
		if (Input.GetKey (KeyCode.JoystickButton7) && sceneName == "Start2") {
			Application.LoadLevel (sceneName + "Controller");
			PlayerPrefs.SetString ("UsingJoy", "true");
		}
		else if (Input.GetKey (KeyCode.JoystickButton7)) {
			Application.LoadLevel (sceneName);
			PlayerPrefs.SetString("UsingJoy", "true");
		}

		if(Input.GetKey(KeyCode.Escape))
			Application.Quit();
		if(Input.GetKey(KeyCode.JoystickButton6))
			Application.Quit();

	}
}
