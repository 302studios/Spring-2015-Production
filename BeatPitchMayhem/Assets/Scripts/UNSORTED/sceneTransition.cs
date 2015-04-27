using UnityEngine;
using System.Collections;

public class sceneTransition : MonoBehaviour {

	public string sceneName;
	public bool isLoadingScene;
	public worldInfo theWorld;

	// Use this for initialization
	void Start () {
	

		theWorld = GameObject.Find ("World").GetComponent<worldInfo>(); 

	}
	
	// Update is called once per frame
	void Update () {
	
		if ((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space)) && isLoadingScene) {
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

			if((sceneName == "Load-PB-Hiphop") && theWorld.levelName == worldInfo.LevelNames.EDM){
				PlayerPrefs.SetInt ("Basement Location", 1);
				PlayerPrefs.SetString ("WhereTo", "Basement");
			}

			if((sceneName == "Load-PA-Rock") && theWorld.levelName == worldInfo.LevelNames.EDM){
				PlayerPrefs.SetInt ("Basement Location", 2);
				PlayerPrefs.SetString ("WhereTo", "Basement");
			}

			if(theWorld.levelName == worldInfo.LevelNames.Basement){
				PlayerPrefs.SetString ("WhereTo", "Lounge");
			}


			PlayerPrefs.SetString ("Last Scene", sceneName);
			Application.LoadLevel (sceneName);
		}
	}
}
