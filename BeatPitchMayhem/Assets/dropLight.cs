using UnityEngine;
using System.Collections;

public class dropLight : MonoBehaviour {

	
	float destroyTime = 7.2f; 
	public GameObject target;
	public Vector3 playerReset;
	public GameObject player;

	// Use this for initialization
	void Start () {
	
		target = GameObject.Find ("Player-Front");
		player = GameObject.FindGameObjectWithTag ("Player");
		StartCoroutine (targetReposition ());
		Debug.Log ("Reset: " + playerReset);
		target.GetComponent<SphereCollider> ().radius = 20f;

	}
	
	// Update is called once per frame
	void Update () {


	}

	IEnumerator targetReposition() {

		yield return new WaitForSeconds (destroyTime);
		target.transform.parent = player.transform;
		target.transform.localPosition = playerReset;
		target.GetComponent<SphereCollider> ().radius = 0.5f;
		Destroy (this.gameObject);

	}


}
