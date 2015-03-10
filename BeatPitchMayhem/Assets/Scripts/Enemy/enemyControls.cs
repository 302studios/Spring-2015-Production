﻿using UnityEngine;
using System.Collections;

public class enemyControls : MonoBehaviour {

	// Objects & Components
	//CharacterController controller;

	// Player
	public characterMovement thePlayer;
	
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
	public bool isAttacking = false;
	public float attackWaitTime = 2.5f;
	Vector3 target;

	// Misc
	private CollisionFlags collisionFlags;

	public bool stunned = false;
	float stunTime = 3f;
	
	
	
	// Use this for initialization
	void Start () {

		moveDirection = transform.TransformDirection(Vector3.forward);
		thePlayer = GameObject.Find ("First Person Controller").GetComponent<characterMovement>();
	}
	
	// Update is called once per frame
	void Update () {

		if (!stunned) {
			if (!isAttacking)
				enemyPatrol ();
			else
				attack ();
		} else
			moveTo = null;

		Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
		Debug.DrawRay(transform.position, forward, Color.green);

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
		if (this.tag == "Brute"){
			target = new Vector3 (target.x, transform.position.y, target.z);
		}

		if (this.tag == "Beast"){
			target = new Vector3 (target.x, transform.position.y, target.z);
		}

		if (this.tag == "Bat") {
			if(transform.position.y < target.y)
				transform.position = new Vector3(transform.position.x, target.y, transform.position.z);
		}

		transform.position = Vector3.MoveTowards (transform.position, target, (chaseSpeed * .01f));
	}

	public IEnumerator waitAtSpot(float seconds){

		waiting = true;
		yield return new WaitForSeconds (seconds);
		waiting = false;
	}

	public void doStun(){

		StartCoroutine (stunTimer());

	}

	IEnumerator stunTimer(){

		stunned = true;
		yield return new WaitForSeconds (stunTime);
		stunned = false;

	}
}
