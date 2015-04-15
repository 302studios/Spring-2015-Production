using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class metronomeBubble : MonoBehaviour {


	public float destroyTime = 15f; 
	public Image bubbleFade;
	public AudioSource theMusic;
	public BeatSynchronizer sync;
	public float origBPM;
	GameObject forcefieldR;
	GameObject forcefieldL;
	GameObject djBooth;
	Animator[] djAnims;
	worldInfo theWorld;



	// Use this for initialization
	void Start () {
	
		bubbleFade = GameObject.Find ("metronomeFade").GetComponent<Image> ();
		theMusic = GameObject.Find ("World").GetComponent<AudioSource> ();
		sync = GameObject.Find ("World").GetComponent<BeatSynchronizer> ();
		origBPM = sync.bpm;
		theWorld = GameObject.Find ("World").GetComponent<worldInfo>();
		if (theWorld.levelName == worldInfo.LevelNames.Boss) {
			destroyTime = 15f;
			Debug.Log("Boss Met");
		}
		else
			destroyTime = 15f;
		StartCoroutine (beforeDestroy ());
		//forcefieldR = GameObject.Find ("ForceField_R");
		//forcefieldL = GameObject.Find ("ForceField_L");
		djBooth = GameObject.Find ("DJ Booth");


	}
	
	// Update is called once per frame
	void Update () {


	}

	IEnumerator beforeDestroy(){
	
		yield return new WaitForSeconds(destroyTime);
		bubbleFade.enabled = false;
		theMusic.pitch = 1f;
		sync.bpm = origBPM;
		//forcefieldR.SetActive(true);
		//forcefieldL.SetActive(true);
		//djAnims = djBooth.GetComponentsInChildren<Animator>();
		//for(int i = 0; i < djAnims.Length; i++){
		//	djAnims[i].speed = 1f;
		//}

		if (theWorld.levelName == worldInfo.LevelNames.Boss) {
			destroyTime = 15f;

		}

		Destroy (this.gameObject);

	}

	void OnTriggerStay(Collider col){

		if (col.tag == "Beast" || col.tag == "Brute" || col.tag == "Bat"){
			col.gameObject.GetComponent<enemyControls> ().slow (true);
			
		}
		if (col.tag == "Player") {
			bubbleFade.enabled = true;
			theMusic.pitch = 0.5f;
			sync.bpm = origBPM/2;
		}

		if (col.name == "DJ Booth") {
			Debug.Log("Bubble hit booth!");
			forcefieldR.SetActive(false);
			forcefieldL.SetActive(false);
			djAnims = djBooth.GetComponentsInChildren<Animator>();
			for(int i = 0; i < djAnims.Length; i++){
				djAnims[i].speed = 0.5f;
			}
		}

		if (col.name == "DJ Booth Metronome Detect") {
			col.gameObject.GetComponent<djBooth>().metronomeCheck();
		}



	}

	void OnTriggerExit(Collider col){
		
		if (col.tag == "Beast" || col.tag == "Brute" || col.tag == "Bat"){
			col.gameObject.GetComponent<enemyControls> ().slow (false);
		
		}
		if (col.tag == "Player") {
			bubbleFade.enabled = false;
			theMusic.pitch = 1f;
			sync.bpm = origBPM;
		}
		
		
	}


}
