using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sonicCompressor : MonoBehaviour {

	public bool active = false;
	MeshRenderer[] theMesh;

	bool cooling = false;
	bool canShoot = true;

	public GameObject theBoom;
	public Transform boomStart;

	public float coolTime = 5f;

	public Text name;
	public Text timer;

	playerInfo thePlayer;

	bool canPad;

	// Use this for initialization
	void Start () {
	
		theMesh = this.gameObject.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < theMesh.Length; i++) {
		
			theMesh[i].enabled = false;
		
		}

		thePlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerInfo> ();
		name = GameObject.Find ("Sonic Compressor Text").GetComponent<Text> ();
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

		if (Input.GetKeyDown (KeyCode.Alpha3) && !cooling && thePlayer.hasSonicCompressor)// && !active)
			active = !active;

		if ((Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown (KeyCode.Alpha2)) && active)// && !active)
			active = !active;

		if ((Input.GetAxis ("PadY") > .75f) && !cooling && thePlayer.hasSonicCompressor && canPad) {
			active = !active;
			canPad = false;
		}
		
		if ((Input.GetAxis ("PadX") > .75f || Input.GetAxis ("PadX") < -.75f) && active && canPad) {
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

			firePulse();
			StartCoroutine(coolDown());
			canShoot = false;
		}

		if ((Input.GetAxis("UseGadget") > .75f) && canShoot && active) {
			
			firePulse();
			StartCoroutine(coolDown());
			canShoot = false;
		}

	}

	void firePulse(){

		Rigidbody clone;
		clone = Instantiate (theBoom, boomStart.position, boomStart.rotation) as Rigidbody;
		//clone.velocity = transform.TransformDirection(Vector3.forward * 10);

	}

	IEnumerator coolDown(){


		yield return new WaitForSeconds (2f);
		active = false;
		cooling = true;
		yield return new WaitForSeconds(coolTime);
		cooling = false;
		canShoot = true;


	}
}
