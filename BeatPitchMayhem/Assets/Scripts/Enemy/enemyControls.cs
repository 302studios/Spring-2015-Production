using UnityEngine;
using System.Collections;

public class enemyControls : MonoBehaviour {

	// Objects & Components
	//CharacterController controller;

	// Player
	characterMovement thePlayer;
	
	// Movement
	public float patrolSpeed = 7f;
	public float chaseSpeed = 13f;
	float speedSmoothing = 10f;
	private float moveSpeed = 0f;
	private bool movingBack = false;
	private bool isMoving = false;
	bool isChasing = false;
	private bool slammed = false;
	public Vector3 movement = Vector3.zero;
	private Vector3 moveDirection = Vector3.zero;
	public bool grounded = true; // Currently no check for grounded!!!!
	//public Transform groundCheck;
	//float groundRadius = 0.2f;
	//public LayerMask whatIsGround;

	// Patrolling
	public GameObject[] waypoints;
	int wpNum = 0;
	public GameObject moveTo;
	bool waiting = false;
	public float patrolWaitTime = 1f;

	// Attacking
	bool isAttacking = false;
	public float attackWaitTime = 2.5f;
	Vector3 target;

	// Misc
	private CollisionFlags collisionFlags;
	
	
	
	// Use this for initialization
	void Start () {

		moveDirection = transform.TransformDirection(Vector3.forward);
		thePlayer = GameObject.Find ("First Person Controller").GetComponent<characterMovement>();
	}
	
	// Update is called once per frame
	void Update () {

		if (!isAttacking)
			enemyPatrol ();
		else
			attack ();

	}


	void enemyPatrol(){

		moveTo = waypoints[wpNum];		

		if(!waiting)
			transform.position = Vector3.MoveTowards (transform.position, moveTo.transform.position, (patrolSpeed * .01f));

		if (this.transform.position == moveTo.transform.position) {
			if(!waiting){
				wpNum++;
				StartCoroutine(waitAtSpot(patrolWaitTime));
			}
			if(wpNum >= waypoints.Length)
				wpNum = 0;
		}


	}

	void attack(){

		target = moveTo.transform.position;
		if (this.tag == "Giant"){
			target = new Vector3 (target.x, moveTo.transform.position.y, target.z);
		}

		if (this.tag == "Bat") {
			if(transform.position.y < target.y)
				transform.position = new Vector3(transform.position.x, target.y, transform.position.z);
		}

		transform.position = Vector3.MoveTowards (transform.position, moveTo.transform.position, (chaseSpeed * .01f));
	}

	void OnTriggerStay(Collider col){

		if (col.tag == "PlayerFront" && !thePlayer.isCrouching) {
		
			isAttacking = true;
			moveTo = col.gameObject;
		
		}

	}

	void OnTriggerExit(Collider col){
		
		if (col.tag == "PlayerFront" && !thePlayer.isCrouching) {
			
			isAttacking = false;
			StartCoroutine(waitAtSpot(attackWaitTime));
		}
		
	}

	IEnumerator waitAtSpot(float seconds){

		waiting = true;
		yield return new WaitForSeconds (seconds);
		waiting = false;
	}
}
