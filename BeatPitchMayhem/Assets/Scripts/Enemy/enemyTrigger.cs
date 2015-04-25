using UnityEngine;
using System.Collections;

public class enemyTrigger : MonoBehaviour {

	enemyControls me;
	public float attackWaitTime;
	public bool fooledByCrouch;
	AudioSource attackAudio;
	worldInfo theWorld;

	// Use this for initialization
	void Start () {
			
		me = this.GetComponentInParent<enemyControls> ();
		attackWaitTime = me.attackWaitTime;
		attackAudio = this.GetComponent<AudioSource> ();
		theWorld = GameObject.Find ("World").GetComponent<worldInfo> ();

		if (this.tag == "Brute"){
			attackAudio.clip = theWorld.currentClips [2];
		}
		
		if (this.tag == "Beast"){
			attackAudio.clip = theWorld.currentClips [6];
		}
		
		if (this.tag == "Bat") {
			attackAudio.clip = theWorld.currentClips [4];
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		if (me.isAttacking) {
			me.gameObject.GetComponent<AudioSource> ().volume = 0f;
			attackAudio.volume = 1f;
		} else {
			me.gameObject.GetComponent<AudioSource> ().volume = 1f;
			attackAudio.volume = 0f;
		}


	}

	void OnTriggerStay(Collider col){
		
		if (col.tag == "PlayerFront" && (!me.thePlayer.isCrouching || !fooledByCrouch)) {
			
			me.isAttacking = true;
			me.moveTo = col.gameObject;
			
		}
		
	}
	
	void OnTriggerExit(Collider col){
		
		if (col.tag == "PlayerFront" && (!me.thePlayer.isCrouching || !fooledByCrouch)) {
			
			me.isAttacking = false;
			me.waitAtSpot(attackWaitTime);
		}
		
	}
}
