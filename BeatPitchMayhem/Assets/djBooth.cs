using UnityEngine;
using System.Collections;

public class djBooth : MonoBehaviour {

	bossControls theBoss;
	worldInfo theWorld;
	public int bossStage;

	// Use this for initialization
	void Start () {
	
		theBoss = GameObject.Find ("Malkior").GetComponent<bossControls>();
		theWorld = GameObject.Find ("World").GetComponent<worldInfo>();

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void metronomeCheck(){

		if (bossStage == theWorld.bossStage)
			StartCoroutine(metActive ());

	}

	IEnumerator metActive(){

		theBoss.shieldActive = false;
		yield return new WaitForSeconds(15f);
		theBoss.shieldActive = true;

	}
}
