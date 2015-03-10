using UnityEngine;
using System.Collections;

public class enemyTrigger : MonoBehaviour {

	enemyControls me;
	public float attackWaitTime;

	// Use this for initialization
	void Start () {
			
		me = this.GetComponentInParent<enemyControls> ();
		attackWaitTime = me.attackWaitTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col){
		
		if (col.tag == "PlayerFront" && !me.thePlayer.isCrouching) {
			
			me.isAttacking = true;
			me.moveTo = col.gameObject;
			
		}
		
	}
	
	void OnTriggerExit(Collider col){
		
		if (col.tag == "PlayerFront" && !me.thePlayer.isCrouching) {
			
			me.isAttacking = false;
			me.waitAtSpot(attackWaitTime);
		}
		
	}
}
