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
			target = GameObject.Find ("Player-Front");
			playerReset = target.transform.localPosition;
			target.GetComponent<SphereCollider> ().radius = 20f;
			target.transform.parent = this.transform;
			target.transform.position = this.transform.position;
			Vector3 tempPos = this.transform.position;
			tempPos.y = 9f;
			if (theWorld.levelName == worldInfo.LevelNames.Boss) {
				target.transform.parent = null;
				target.transform.position = tempPos;
			}
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
