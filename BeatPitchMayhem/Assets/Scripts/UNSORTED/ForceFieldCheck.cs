using UnityEngine;
using System.Collections;

public class ForceFieldCheck : MonoBehaviour {

	playerInfo thePlayer;
	worldInfo theWorld;
	public int numberNeededToDisable;
	bool canDisable;

	// Use this for initialization
	void Start () {
	
		thePlayer = GameObject.Find ("First Person Controller").GetComponent<playerInfo>();
		theWorld = GameObject.Find ("World").GetComponent<worldInfo>();
		numberNeededToDisable = theWorld.numberOfSpeakersNeeded;
		canDisable = false;
	}
	
	// Update is called once per frame
	void Update () {
	

		if (theWorld.levelName == worldInfo.LevelNames.EDM && this.gameObject.name == "ForceField_L" && (PlayerPrefs.GetString("WhereTo") == "Start")) {
			canDisable = true;
		}

		if (theWorld.levelName == worldInfo.LevelNames.EDM && this.gameObject.name == "ForceField_R" && (PlayerPrefs.GetString("WhereTo") == "Start")) {
			canDisable = true;
		}

		if (theWorld.levelName == worldInfo.LevelNames.HipHop && this.gameObject.name == "ForceField_L" && (PlayerPrefs.GetString("WhereTo") == "Basement")) {
			canDisable = true;
		}

		if (theWorld.levelName == worldInfo.LevelNames.Rock && this.gameObject.name == "ForceField_R" && (PlayerPrefs.GetString("WhereTo") == "Basement")) {
			canDisable = true;
		}


		if (thePlayer.speakersComplete >= numberNeededToDisable && canDisable) {

			this.GetComponent<MeshRenderer>().enabled = false;
			this.GetComponent<MeshCollider>().enabled = false;
		}

	}
}
