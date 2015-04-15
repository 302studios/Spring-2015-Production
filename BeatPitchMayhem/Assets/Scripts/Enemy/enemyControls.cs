using UnityEngine;
using System.Collections;
using SynchronizerData;

public class enemyControls : MonoBehaviour {

	// Objects & Components
	//CharacterController controller;

	// Player
	public characterMovement thePlayer;
	
	// Movement
	public float patrolSpeed = 7f;
	public float chaseSpeed = 13f;
	float speedSmoothing = 10f;
	public float moveSpeed = 0f;
	public float animSpeed = .2f;
	public float rotateSpeed = 3f;
	private bool movingBack = false;
	private bool isMoving = false;
	bool isChasing = false;
	private bool slammed = false;
	bool slowed = false;
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
	Animator anims;

	public bool stunned = false;
	float stunTime = 3f;

	public bool downBeat = false;
	public bool upBeat = false;
	public bool onBeat = false;
	private BeatObserver beatObserver;
	public bool canAttack = false;
	public float attackPower;
	AudioSource source;
	public bool atPlayerFront = false;
	public Vector3 relativePos;
	GameObject trackPlayer;

	
	
	// Use this for initialization
	void Start () {

		moveDirection = transform.TransformDirection(Vector3.forward);
		thePlayer = GameObject.Find ("First Person Controller").GetComponent<characterMovement>();
		beatObserver = GetComponent<BeatObserver>();
		source = GetComponent<AudioSource>();
		anims = GetComponentInChildren<Animator>();
		if (this.tag == "Brute"){
			attackPower = 25f;
		}
		
		if (this.tag == "Beast"){
			attackPower = 10f;
		}
		
		if (this.tag == "Bat") {
			attackPower = 0f;
		}
		trackPlayer = GameObject.FindGameObjectWithTag ("PlayerFront");
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

		/*if (this.tag == "Brute") {
			if (downBeat && ((beatObserver.beatMask & BeatType.DownBeat) == BeatType.DownBeat)) {
				canAttack = true;
			} else
				canAttack = false;
		}*/
		if (this.tag == "Brute") {
			if (onBeat && ((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat)) {
				canAttack = true;
			} else
				canAttack = false;
		}
		if (this.tag == "Beast") {
			if (upBeat && ((beatObserver.beatMask & BeatType.UpBeat) == BeatType.UpBeat)) {
				canAttack = true;
			} else
				canAttack = false;
		}
		if (this.tag == "Bat") {
			if (onBeat && ((beatObserver.beatMask & BeatType.OnBeat) == BeatType.OnBeat)) {
				canAttack = true;
			} else
				canAttack = false;
		}

	}

	void enemyPatrol(){

		moveTo = waypoints[wpNum];

		if (this.tag == "Brute") {

			anims.SetBool("Walking", true);
			anims.speed = animSpeed * patrolSpeed;

		}

		if(!waiting)
			if(slowed)
				transform.position = Vector3.MoveTowards (transform.position, moveTo.transform.position, (patrolSpeed * .005f));
			else
				transform.position = Vector3.MoveTowards (transform.position, moveTo.transform.position, (patrolSpeed * .01f));

		if (this.transform.position == moveTo.transform.position) {
			if(!waiting){
				wpNum++;
				StartCoroutine(waitAtSpot(patrolWaitTime));
			}
			if(wpNum >= waypoints.Length)
				wpNum = 0;
		}

		relativePos = moveTo.transform.position - transform.position;
		//transform.LookAt(moveTo.transform.position);
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (relativePos), Time.deltaTime);
		
	}

	void attack(){

		target = moveTo.transform.position;

		anims.speed = animSpeed * chaseSpeed;

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

		if(!atPlayerFront){

			if(slowed)
				transform.position = Vector3.MoveTowards (transform.position, target, (chaseSpeed * .005f));
			else
				transform.position = Vector3.MoveTowards (transform.position, target, (chaseSpeed * .01f));
		}


		relativePos = target - transform.position;
		relativePos.Normalize();
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(relativePos), Time.deltaTime);

	}

	public IEnumerator waitAtSpot(float seconds){

		waiting = true;
		yield return new WaitForSeconds (seconds);
		waiting = false;
	}

	public void doStun(){

		StartCoroutine (stunTimer());

	}

	public void slow(bool isSlow){

		slowed = isSlow;
		StartCoroutine (slowTimer ());
	}

	IEnumerator stunTimer(){

		stunned = true;
		yield return new WaitForSeconds (stunTime);
		stunned = false;

	}

	IEnumerator slowTimer(){
		

		yield return new WaitForSeconds (.2f);
		slowed = false;
		
	}

	IEnumerator attackTimer(){
		
		
		yield return new WaitForSeconds (.5f);
		thePlayer.gameObject.GetComponent<playerInfo>().currHealth -= attackPower;
		canAttack = false;
		//source.Play();
		
	}

	public void OnTriggerStay(Collider col){

		if (col.tag == "PlayerFront" && canAttack) {
			canAttack = false;
			anims.SetTrigger("Attacking");
			StartCoroutine(attackTimer());
			source.Play();


		}
		if (col.tag == "PlayerFront") {
			atPlayerFront = true;
		}

	}

	public void OnTriggerExit(Collider col){
	
		if (col.tag == "PlayerFront") {
			atPlayerFront = false;
		}
	}
}
