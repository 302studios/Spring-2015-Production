using UnityEngine;
using System.Collections;

public class sonicCompressor : MonoBehaviour {

	public bool active = false;
	MeshRenderer[] theMesh;

	bool cooling = false;
	bool canShoot = true;

	public GameObject theBoom;
	public Transform boomStart;

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
