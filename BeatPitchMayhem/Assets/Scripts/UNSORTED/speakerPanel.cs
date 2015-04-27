using UnityEngine;
using System.Collections;

public class speakerPanel : MonoBehaviour {

	Animator anim;
	bool isOpen;
	public GameObject destroyObj;
	playerInfo thePlayer;
	bool done;
	public ParticleSystem psLeft;
	public ParticleSystem psRight;
	public worldInfo theWorld;

	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animator> ();
		isOpen = false;
		done = false;
		theWorld = GameObject.Find ("World").GetComponent<worldInfo>();
		thePlayer = GameObject.Find ("First Person Controller").GetComponent<playerInfo>();
		psLeft.startColor = theWorld.theColor;
		psRight.startColor = theWorld.theColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col){

		if (col.tag == "Player") {
			if(Input.GetMouseButtonDown(0) && isOpen){
				Destroy(destroyObj);
				if(!done){
					thePlayer.speakersComplete++;
					psLeft.Play();
					psRight.Play();
				}
				done = true;
			}
			if(Input.GetMouseButtonUp(0)){
				anim.SetBool("Panel Open", true);
				isOpen = true;
				thePlayer.speakersComplete++;

				if(PlayerPrefs.GetString("WhereTo") == "Lounge"){
					PlayerPrefs.SetInt ("Sonic Compressor", 1);
					thePlayer.gadgetCheck();
				}

			}
		
		}

		if (col.tag == "Player") {
			if(Input.GetKeyDown(KeyCode.JoystickButton0) && isOpen){
				Destroy(destroyObj);
				if(!done){
					thePlayer.speakersComplete++;
					psLeft.Play();
					psRight.Play();
				}
				done = true;
			}
			if(Input.GetKeyUp(KeyCode.JoystickButton0)){
				anim.SetBool("Panel Open", true);
				isOpen = true;
				thePlayer.speakersComplete++;

				if(PlayerPrefs.GetString("WhereTo") == "Lounge"){
					PlayerPrefs.SetInt ("Sonic Compressor", 1);
					thePlayer.gadgetCheck();
				}
			}
			
		}

	}
}
