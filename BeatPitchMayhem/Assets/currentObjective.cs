using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class currentObjective : MonoBehaviour {

	worldInfo theWorld;



	// Use this for initialization
	void Start () {
	
		theWorld = GameObject.Find ("World").GetComponent<worldInfo> ();

		switch (theWorld.levelName) {
			
		case worldInfo.LevelNames.EDM:
				this.GetComponent<Text>().text = "Find speaker and disable energy located in the back panel";
				break;
		case worldInfo.LevelNames.Rock:
				this.GetComponent<Text>().text = "Find speaker and disable energy located in the back panel";
				break;
		case worldInfo.LevelNames.HipHop:
				this.GetComponent<Text>().text = "Find speaker and disable energy located in the back panel";
				break;
		case worldInfo.LevelNames.Basement:
				this.GetComponent<Text>().text = "Pick up the next gadget and escape The Basement";
				break;
		case worldInfo.LevelNames.Lounge:
				this.GetComponent<Text>().text = "Make your way up the stairs to the DJ Booth";
				break;
		case worldInfo.LevelNames.Boss:
				this.GetComponent<Text>().text = "Defeat Malkior!";
				break;
			default:
				break;

		}

	}
	
	// Update is called once per frame
	void Update () {

		if((PlayerPrefs.GetString("WhereTo") == "Lounge") && (theWorld.levelName == worldInfo.LevelNames.HipHop || theWorld.levelName == worldInfo.LevelNames.Rock))
			this.GetComponent<Text>().text = "Find the stairs and break the glass wall to The Lounge";

		if((PlayerPrefs.GetString("WhereTo") == "Basemenet") && (theWorld.levelName == worldInfo.LevelNames.HipHop || theWorld.levelName == worldInfo.LevelNames.Rock))
			this.GetComponent<Text>().text = "Find the exit and head to The Basement";
	
	}
}
