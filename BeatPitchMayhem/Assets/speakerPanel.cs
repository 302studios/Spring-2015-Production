using UnityEngine;
using System.Collections;

public class speakerPanel : MonoBehaviour {

	Animator anim;
	bool isOpen;
	public GameObject destroyObj;
	playerInfo thePlayer;
	bool done;
	public ParticleSystem psLeft;
	public ParticleSystem psRight;

	// Use this for initialization
	void Start () {
	
		anim = GetComponent<Animator> ();
		isOpen = false;
		done = false;
		thePlayer = GameObject.Find ("First Person Controller").GetComponent<playerInfo>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col){

		if (col.tag == "Player") {
			if(Input.GetMouseButtonDown(0) && isOpen){
				Destroy(destroyObj);
				if(!done){
					thePlayer.speakersComplete++;
					psLeft.Play();
					psRight.Play();
				}
				done = true;
			}
			if(Input.GetMouseButtonUp(0)){
				anim.SetBool("Panel Open", true);
				isOpen = true;
				thePlayer.speakersComplete++;
			}
		
		}

	}
}
