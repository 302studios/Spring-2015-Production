using UnityEngine;
using System.Collections;

public class ForceFieldCheck : MonoBehaviour {

	playerInfo thePlayer;
	worldInfo theWorld;
	public int numberNeededToDisable;

	// Use this for initialization
	void Start () {
	
		thePlayer = GameObject.Find ("First Person Controller").GetComponent<playerInfo>();
		theWorld = GameObject.Find ("World").GetComponent<worldInfo>();
		numberNeededToDisable = theWorld.numberOfSpeakersNeeded;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (thePlayer.speakersComplete >= numberNeededToDisable) {

			this.GetComponent<MeshRenderer>().enabled = false;
			this.GetComponent<MeshCollider>().enabled = false;
		}

	}
}
