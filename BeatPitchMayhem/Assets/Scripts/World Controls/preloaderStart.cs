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

		if(Input.GetKey(KeyCode.Space))
			Application.LoadLevel (sceneName);
		if(Input.GetKey(KeyCode.JoystickButton7))
			Application.LoadLevel (sceneName);

		if(Input.GetKey(KeyCode.Escape))
			Application.Quit();
		if(Input.GetKey(KeyCode.JoystickButton6))
			Application.Quit();

	}
}
