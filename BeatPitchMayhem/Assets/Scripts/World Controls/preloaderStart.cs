using UnityEngine;
using System.Collections;

public class preloader : MonoBehaviour {

	public string sceneName;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		sceneName = PlayerPrefs.GetString ("Last Scene");

		if(Input.GetKey(KeyCode.Space))
			Application.LoadLevel (sceneName);
		if(Input.GetKey(KeyCode.Escape))
			Application.Quit();

	}
}
