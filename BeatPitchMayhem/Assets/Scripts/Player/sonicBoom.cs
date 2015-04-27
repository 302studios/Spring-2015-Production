using UnityEngine;
using System.Collections;

public class sonicBoom : MonoBehaviour {

	public float speed = .2f;
	GameObject theCam;
	float destroyTime = 1.5f; 
	public float expandRate = 0.1f;
	Vector3 travel;

	// Use this for initialization
	void Start () {

		theCam = GameObject.FindGameObjectWithTag ("MainCamera");
		travel = theCam.transform.forward;
	}
	
	// Update is called once per frame
	void Update () {

		Velocity ();
		Expand ();
		Destroy (this.gameObject, destroyTime);
	}

	void Expand () {

		this.transform.localScale += new Vector3 (expandRate, expandRate, expandRate);

	}

	void Velocity() {

		this.gameObject.transform.position += speed * travel;

	}

	void OnTriggerStay(Collider col) {

		if (col.tag == "Beast" || col.tag == "Brute" || col.tag == "Bat"){
			col.gameObject.GetComponent<enemyControls> ().doStun ();

		}

		if (col.tag == "Glass")
			col.gameObject.SetActive (false);

	}

	void OnTriggerEnter(Collider col){

		if (col.name == "Malkior") {
			if(col.gameObject.GetComponent<bossControls>().vulnerable)
				col.gameObject.GetComponent<bossControls>().damage();

		}

	}

}
