using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class metronome : MonoBehaviour {

	public bool active = false;
	MeshRenderer[] theMesh;
	
	public GameObject metBubble;

	bool cooling = false;
	bool canShoot = true;
	
	public Transform bubbleRot;

	public float coolTime;

	public Text name;
	public Text timer;

	worldInfo theWorld;
	playerInfo thePlayer;

	bool canPad;



	// Use this for initialization
	void Start () {
	
		theMesh = this.gameObject.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < theMesh.Length; i++) {
		
			theMesh[i].enabled = false;

		}

		theWorld = GameObject.Find ("World").GetComponent<worldInfo>();
		if (theWorld.levelName == worldInfo.LevelNames.Boss) {
			coolTime = 15f;
			Debug.Log ("Boss Met");
		} else
			coolTime = 15f;

		thePlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerInfo> ();
		name = GameObject.Find ("Metronome Text").GetComponent<Text> ();
		timer = name.gameObject.GetComponentInChildren<Text> ();

		canPad = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (active) {
			name.color = Color.yellow;	
		} else
			name.color = Color.white;

		if (cooling) {
			name.color = Color.grey;
		} else
			timer.text = "";

		if (Input.GetKeyDown (KeyCode.Alpha1) && !cooling && thePlayer.hasMetronome)// && !active)
			active = !active;

		if ((Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown (KeyCode.Alpha3)) && active)// && !active)
			active = !active;

		if ((Input.GetAxis ("PadX") < -.75f) && !cooling && thePlayer.hasMetronome && canPad) {
			active = !active;
			canPad = false;
		}
		
		if ((Input.GetAxis ("PadX") > .75f || Input.GetAxis ("PadY") > .75f) && active && canPad) {
			active = !active;
			canPad = false;
		}

		if ((Input.GetAxis ("PadX") == 0f) && (Input.GetAxis ("PadY") == 0f)) {
			canPad = true;
		}


		if (active) {
			for (int i = 0; i < theMesh.Length; i++) {
				
				theMesh[i].enabled = true;
				
			}



		}

		if (!active) {
			for (int i = 0; i < theMesh.Length; i++) {
				
				theMesh[i].enabled = false;
				
			}


		}


		if (Input.GetMouseButtonDown (0) && canShoot && active) {

			activateMetronome();
			StartCoroutine(coolDown());
			canShoot = false;
		}

		if ((Input.GetAxis("UseGadget") > .75f) && canShoot && active) {
			
			activateMetronome();
			StartCoroutine(coolDown());
			canShoot = false;
		}

	}

	void activateMetronome(){

		Instantiate (metBubble, this.transform.position, bubbleRot.rotation); 

	}

	IEnumerator coolDown(){


		yield return new WaitForSeconds (.5f);
		active = false;
		cooling = true;
		for (float i = coolTime; i >= 0; i--) {
			timer.text = ("| " + i);
			timer.color = Color.red;
			yield return new WaitForSeconds (1);
		}
		cooling = false;
		canShoot = true;


	}



}
