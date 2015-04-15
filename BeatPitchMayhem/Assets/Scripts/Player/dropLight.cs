using UnityEngine;
using System.Collections;

public class dropLight : MonoBehaviour {

	
	float destroyTime = 7.2f; 
	public GameObject target;
	public Vector3 playerReset;
	public GameObject player;
	worldInfo theWorld;

	// Use this for initialization
	void Start () {
	
		theWorld = GameObject.Find ("World").GetComponent<worldInfo>();
		if (theWorld.levelName == worldInfo.LevelNames.Boss) {
			Destroy (this.gameObject, destroyTime);
		} else {
			StartCoroutine (targetReposition ());
			Debug.Log("Target repo?");
			target.GetComponent<SphereCollider> ().radius = 20f;
			target = GameObject.Find ("Player-Front");
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		Debug.Log ("Reset: " + playerReset);
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

	void OnTriggerStay(Collider col){

		if (col.name == "Malkior") {
			col.gameObject.GetComponent<bossControls>().lightHit();
		}
	}


}
