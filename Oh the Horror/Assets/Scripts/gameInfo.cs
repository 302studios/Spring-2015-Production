using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour {
	
	public GameObject player;
	
	//PlayerRespawn respawnScript;
	
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true; 
		
		//respawnScript = player.GetComponent<PlayerRespawn> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.Escape)){
			if(Screen.lockCursor == false){
				Application.Quit();
			} else{
				Screen.lockCursor = false; 
			}
		}
		
		if((Input.GetMouseButton(0) || Input.GetMouseButton(1))&& Screen.lockCursor == false){
			Screen.lockCursor = true; 
		}
		
		//if(cInput.GetKeyDown("Restart")){
		
		//Application.LoadLevel(0);
		//respawnScript.dead = true;
		
		//}
		
	}
}
