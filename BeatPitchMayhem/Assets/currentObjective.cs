using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class currentObjective : MonoBehaviour {

	worldInfo theWorld;
	playerInfo thePlayer;



	// Use this for initialization
	void Start () {
	
		theWorld = GameObject.Find ("World").GetComponent<worldInfo> ();
		thePlayer = GameObject.Find ("First Person Controller").GetComponent<playerInfo> ();

		switch (theWorld.levelName) {
			
		case worldInfo.LevelNames.EDM:
				this.GetComponent<Text>().text = "Find speaker and disable energy located in the back panel";
				break;
		case worldInfo.LevelNames.Rock:
				this.GetComponent<Text>().text = "Same as the last. Careful though, there's more beasts here...";
				break;
		case worldInfo.LevelNames.HipHop:
				this.GetComponent<Text>().text = "Same as the last. Careful though, there's more beasts here...";
				break;
		case worldInfo.LevelNames.Basement:
				this.GetComponent<Text>().text = "Pick up the next gadget and escape The Basement";
				break;
		case worldInfo.LevelNames.Lounge:
				this.GetComponent<Text>().text = "How relaxing. Make your way up the stairs to the DJ Booth";
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

		if((PlayerPrefs.GetString("WhereTo") == "Lounge") && (theWorld.levelName == worldInfo.LevelNames.HipHop || theWorld.levelName == worldInfo.LevelNames.Rock) && (thePlayer.speakersComplete >= 1))
			this.GetComponent<Text>().text = "Find the stairs and break the glass wall to The Lounge";

		if((PlayerPrefs.GetString("WhereTo") == "Lounge") && (theWorld.levelName == worldInfo.LevelNames.HipHop || theWorld.levelName == worldInfo.LevelNames.Rock) && (thePlayer.speakersComplete == 0))
			this.GetComponent<Text>().text = "Speaker. Panel. Energy. Last one to power your Sonic Compressor";

		if((PlayerPrefs.GetString("WhereTo") == "Basement") && (theWorld.levelName == worldInfo.LevelNames.HipHop || theWorld.levelName == worldInfo.LevelNames.Rock) && (thePlayer.speakersComplete >= 1))
			this.GetComponent<Text>().text = "Find the exit and head to The Basement";

		if((theWorld.levelName == worldInfo.LevelNames.EDM) && (thePlayer.speakersComplete >= 1))
			this.GetComponent<Text>().text = "Head to the next room. Hip-Hop or Rock, which will it be?";
	
	}
}
