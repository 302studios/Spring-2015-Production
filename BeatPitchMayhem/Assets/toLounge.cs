using UnityEngine;
using System.Collections;

public class toLounge : MonoBehaviour {


	bool loungeAccess;
	public bool glassBroken;
	bool done;

	// Use this for initialization
	void Start () {
	
		if (PlayerPrefs.GetString ("WhereTo") == "Lounge")
			loungeAccess = true;
		else
			loungeAccess = false;

		glassBroken = false;
		done = false;


	}
	
	// Update is called once per frame
	void Update () {
	
		/*if (glassBroken && !done) {
			this.GetComponent<MeshRenderer> ().enabled = false;
			this.GetComponent<MeshCollider> ().enabled = false;
			done = true;
		}*/

	}
}
