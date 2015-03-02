using UnityEngine;
using System.Collections;

public class enemyControls : MonoBehaviour {

	// Objects & Components
	//CharacterController controller;
	
	
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

	// Attacking
	bool isAttacking = false;

	// Misc
	private CollisionFlags collisionFlags;
	
	
	
	// Use this for initialization
	void Start () {

		moveDirection = transform.TransformDirection(Vector3.forward);
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
				StartCoroutine(waitAtSpot(1f));
			}
			if(wpNum >= waypoints.Length)
				wpNum = 0;
		}


	}

	void attack(){

		transform.position = Vector3.MoveTowards (transform.position, moveTo.transform.position, (chaseSpeed * .01f));
	}

	void OnTriggerEnter(Collider col){

		if (col.tag == "Player") {
		
			isAttacking = true;
			moveTo = col.gameObject;
		
		}

	}

	void OnTriggerExit(Collider col){
		
		if (col.tag == "Player") {
			
			isAttacking = false;
			StartCoroutine(waitAtSpot(2.5f));
		}
		
	}

	IEnumerator waitAtSpot(float seconds){

		waiting = true;
		yield return new WaitForSeconds (seconds);
		waiting = false;
	}
}
