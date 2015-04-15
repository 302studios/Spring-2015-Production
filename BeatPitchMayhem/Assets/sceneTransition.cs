using UnityEngine;
using System.Collections;

public class sceneTransition : MonoBehaviour {

	public string sceneName;
	public bool isLoadingScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKey(KeyCode.Space) && isLoadingScene)
			Application.LoadLevel(sceneName);

	}

	void OnTriggerEnter(Collider col){

		if (col.tag == "Player")
			Application.LoadLevel (sceneName);
	}
}
