using UnityEngine;
using System.Collections;

public class bossBlast : MonoBehaviour {

	public float speed = .3f;
	float destroyTime = 3f; 
	public float expandRate = 0.1f;
	Vector3 travel;

	// Use this for initialization
	void Start () {
	
		travel = this.transform.forward;
		//Debug.Log (travel);
		travel = new Vector3 (travel.x, (travel.y - .2f), travel.z);
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

		if (col.tag == "Player")
			col.gameObject.GetComponent<playerInfo> ().currHealth -= 1f;

	}

	void OnTriggerEnter(Collider col){

		if (col.name == "Malkior") {
			if(col.gameObject.GetComponent<bossControls>().vulnerable)
				col.gameObject.GetComponent<bossControls>().damage();
			
		}

	}

}
