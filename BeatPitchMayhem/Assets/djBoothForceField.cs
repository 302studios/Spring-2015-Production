using UnityEngine;
using System.Collections;

public class djBoothForceField : MonoBehaviour {

	GameObject forcefieldR;
	GameObject forcefieldL;

	// Use this for initialization
	void Start () {
	
		forcefieldR = GameObject.Find ("ForceField_R");
		forcefieldL = GameObject.Find ("ForceField_L");

	}
	
	// Update is called once per frame
	void Update () {
	


	}


}
