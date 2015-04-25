using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class lightController : MonoBehaviour {

	public bool active = false;
	MeshRenderer[] theMesh;

	public GameObject lightReticle;
	public GameObject theCam;
	public GameObject lightDrop;
	public GameObject target;

	bool cooling = false;
	bool canShoot = true;

	public GameObject theBoom;
	public Transform lightRot;

	public float coolTime = 5f;

	public Text name;
	public Text timer;
	worldInfo theWorld;
	playerInfo thePlayer;

	bool canPad;


	// Use this for initialization
	void Start () {
	
		theMesh = this.gameObject.GetComponentsInChildren<MeshRenderer>();
		theWorld = GameObject.Find ("World").GetComponent<worldInfo>();
		lightReticle = GameObject.Find ("lightReticle");
		for (int i = 0; i < theMesh.Length; i++) {
		
			theMesh[i].enabled = false;

		}
		lightReticle.GetComponent<Light> ().enabled = false;

		thePlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<playerInfo> ();

		name = GameObject.Find ("Light Controller Text").GetComponent<Text> ();
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

		reticlePosition ();

		if (Input.GetKeyDown (KeyCode.Alpha2) && !cooling && thePlayer.hasLightController)// && !active)
			active = !active;

		if ((Input.GetKeyDown (KeyCode.Alpha1) || Input.GetKeyDown (KeyCode.Alpha3)) && active)// && !active)
			active = !active;

		if ((Input.GetAxis ("PadX") > .75f) && !cooling && thePlayer.hasLightController && canPad) {
			active = !active;
			canPad = false;
		}
		
		if ((Input.GetAxis ("PadX") < -.75f || Input.GetAxis ("PadY") > .75f) && active && canPad) {
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

			lightReticle.GetComponent<Light> ().enabled = true;

		}

		if (!active) {
			for (int i = 0; i < theMesh.Length; i++) {
				
				theMesh[i].enabled = false;
				
			}

			lightReticle.GetComponent<Light> ().enabled = false;
		}


		if (Input.GetMouseButtonDown (0) && canShoot && active) {

			activateLight();
			StartCoroutine(coolDown());
			canShoot = false;
		}

		if ((Input.GetAxis("UseGadget") > .75f) && canShoot && active) {
			
			activateLight();
			StartCoroutine(coolDown());
			canShoot = false;
		}

	}

	void activateLight(){

		Vector3 tempPos = lightReticle.transform.position;
		tempPos.y = 7f;
		Instantiate (lightDrop, tempPos, lightRot.rotation);
		tempPos.y = 1f;

		if (theWorld.levelName == worldInfo.LevelNames.Boss) {
			target.transform.parent = null;
			target.transform.position = tempPos;
		}
	}

	void reticlePosition(){
	
		Vector3 tempPos = lightReticle.transform.localPosition;
		float tempZ;
		float camRot = theCam.transform.eulerAngles.x;
		if (camRot > 60 && camRot < 180) {

			tempZ = 1.5f;
		} else if (camRot < 0 || camRot > 180) {

			tempZ = 17.5f;
		} else {

			tempZ = ((((60f - camRot) * 3f) * .1f)+ 1.5f);
		}
		tempPos.z = tempZ;
		lightReticle.transform.localPosition = tempPos;
	
	}

	IEnumerator coolDown(){


		yield return new WaitForSeconds (3f);
		active = false;
		cooling = true;
		yield return new WaitForSeconds(coolTime);
		cooling = false;
		canShoot = true;


	}
}
