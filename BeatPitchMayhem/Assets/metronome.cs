using UnityEngine;
using System.Collections;

public class metronome : MonoBehaviour {

	public bool active = false;
	MeshRenderer[] theMesh;
	
	public GameObject metBubble;

	bool cooling = false;
	bool canShoot = true;
	
	public Transform bubbleRot;

	public float coolTime = 5f;



	// Use this for initialization
	void Start () {
	
		theMesh = this.gameObject.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < theMesh.Length; i++) {
		
			theMesh[i].enabled = false;

		}

	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.Alpha1) && !cooling)// && !active)
			active = !active;

		if ((Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown (KeyCode.Alpha3)) && active)// && !active)
			active = !active;

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

	}

	void activateMetronome(){

		Instantiate (metBubble, this.transform.position, bubbleRot.rotation); 
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
