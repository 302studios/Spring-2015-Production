using UnityEngine;
using System.Collections;

public class worldInfo : MonoBehaviour {

	public enum LevelNames{

		EDM, 
		Rock,
		HipHop,
		Basement,
		Lounge,
		Boss
	};

	public GameObject player;
	public LevelNames levelName;
	string levelNameString;
	public int numberOfSpeakersNeeded;
	public Color theColor;


	// Boss Variables

	bool bossInitialized;
	public Color[] bossColors;
	public int bossStage;

	
	//PlayerRespawn respawnScript;
	
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true; 

		switch (levelName) {

			case LevelNames.EDM:
				levelNameString = "L1-EDM";
				theColor = Color.green;
				break;
			case LevelNames.Rock:
				levelNameString = "L2a-Rock";	
				theColor = Color.red;
				break;
			case LevelNames.HipHop:
				levelNameString = "L2b-Hip-Hop";	
				theColor = Color.cyan;
				break;
			case LevelNames.Basement:
				levelNameString = "L3-Basement";	
				break;
			case LevelNames.Lounge:
				levelNameString = "L4-Lounge";	
				break;
			case LevelNames.Boss:
				levelNameString = "L5-Boss";
				theColor = Color.cyan;
				break;
			default:
				break;

		}

		bossInitialized = false;
		
		//respawnScript = player.GetComponent<PlayerRespawn> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (levelName == LevelNames.Boss) {
			if(!bossInitialized)
				bossInitialization();

			bossLevel();
		}

		if(Input.GetKey(KeyCode.Escape)){
			if(Screen.lockCursor == false){
				Application.Quit();
			} else{
				Screen.lockCursor = false; 
			}
		}

		if (Input.GetKey (KeyCode.R)) {
			Application.LoadLevel(levelNameString);
		}
		
		if((Input.GetMouseButton(0) || Input.GetMouseButton(1))&& Screen.lockCursor == false){
			Screen.lockCursor = true; 
		}
		
		//if(cInput.GetKeyDown("Restart")){
		
		//Application.LoadLevel(0);
		//respawnScript.dead = true;
		
		//}
		
	}

	void bossInitialization(){

		bossColors = new Color[3];

		bossColors [0] = Color.cyan;
		bossColors [1] = Color.red;
		bossColors [2] = Color.green;
		bossStage = 1;


		bossInitialized = true;
	}

	void bossLevel(){

		if (bossStage != 4)
			theColor = bossColors [bossStage - 1];
		else {

			StartCoroutine (endGame ());

		}

	}

	IEnumerator endGame(){

		yield return new WaitForSeconds (5f);
		Debug.Log ("We Made It!!!!");
		Application.LoadLevel ("Load-End");
	}
}
