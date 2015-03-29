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

	public float coolTime = 15f;

	public Text name;
	public Text timer;



	// Use this for initialization
	void Start () {
	
		theMesh = this.gameObject.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < theMesh.Length; i++) {
		
			theMesh[i].enabled = false;

		}

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
