using UnityEngine;
using System.Collections;
using SynchronizerData;

public class bossControls : MonoBehaviour {

	// Objects & Components
	//CharacterController controller;

	// Player
	public characterMovement thePlayer;
	
	// Movement
	public float patrolSpeed = 15f;
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
	float stunTime = 5f;

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
	worldInfo theWorld;

	public bool shieldActive;
	public GameObject theShield;
	public bool vulnerable;

	public ParticleSystem left;
	public ParticleSystem mid;
	public ParticleSystem right;

	public GameObject theBlast;
	public Transform boomStart;

	// Use this for initialization
	void Start () {

		moveDirection = transform.TransformDirection(Vector3.forward);
		thePlayer = GameObject.Find ("First Person Controller").GetComponent<characterMovement>();
		beatObserver = GetComponent<BeatObserver>();
		source = GetComponent<AudioSource>();
		anims = GetComponentInChildren<Animator>();
	
		trackPlayer = GameObject.FindGameObjectWithTag ("MainCamera");
		theWorld = GameObject.Find ("World").GetComponent<worldInfo>();

		shieldActive = true;
		vulnerable = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.H))
			damage ();

		if (!stunned) {
			enemyPatrol ();
		} else {
			moveTo = null;
		}

		Vector3 forward = transform.TransformDirection(Vector3.forward) * 5;
		Debug.DrawRay(transform.position, forward, Color.green);

		if (shieldActive) {

			theShield.GetComponent<MeshRenderer>().enabled = true;
			theShield.GetComponent<SphereCollider>().enabled = true;
			vulnerable = false;
		} 
		if(!shieldActive){

			theShield.GetComponent<MeshRenderer>().enabled = false;
			theShield.GetComponent<SphereCollider>().enabled = false;
			if(stunned)
				vulnerable = true;
			else
				vulnerable = false;
		}

	}

	void enemyPatrol(){

		moveTo = waypoints[wpNum];

		if(!waiting)
			if(slowed)
				transform.position = Vector3.MoveTowards (transform.position, moveTo.transform.position, (patrolSpeed * .005f));
			else
				transform.position = Vector3.MoveTowards (transform.position, moveTo.transform.position, (patrolSpeed * .01f));

		if (this.transform.position == moveTo.transform.position) {
			if(!waiting){
				int prev = wpNum;
				wpNum = Random.Range(0,4);
				while(prev == wpNum){
					wpNum = Random.Range(0,4);
				}
				StartCoroutine(bossAttack());
				//StartCoroutine(waitAtSpot(patrolWaitTime));
			}
			if(wpNum >= waypoints.Length)
				wpNum = 0;
		}
	
		relativePos = trackPlayer.transform.position - transform.position;
		//transform.LookAt(moveTo.transform.position);
		if(slowed)
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (relativePos), Time.deltaTime * (rotateSpeed*.5f));
		else
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (relativePos), Time.deltaTime * rotateSpeed);
	}

	public IEnumerator waitAtSpot(float seconds){

		waiting = true;
		yield return new WaitForSeconds (seconds);
		waiting = false;
	}

	public void doStun(){

		StartCoroutine (stunTimer());

	}
	
	public void lightHit(){

		if (shieldActive == false)
			doStun ();

	}

	public void damage(){

		theWorld.bossStage++;



		if (theWorld.bossStage == 4) {
			left.Play();
			mid.Play();
			right.Play();
			Destroy (this.gameObject);
		}

	}

	public IEnumerator bossAttack(){

		waiting = true;
		anims.SetTrigger("Charge");


		for (int i = 1; i <= theWorld.bossStage; i++) {

			//instantiate
			Rigidbody clone;
			clone = Instantiate (theBlast, boomStart.position, boomStart.rotation) as Rigidbody;
			//animation
			anims.SetTrigger("Fire");
			yield return new WaitForSeconds(.5f);
		}

		yield return new WaitForSeconds (1f);
		waiting = false;
		
	}

	public void slow(bool isSlow){

		slowed = isSlow;
		StartCoroutine (slowTimer ());
	}

	IEnumerator stunTimer(){

		stunned = true;
		anims.SetBool ("Stunned", true);
		yield return new WaitForSeconds (stunTime);
		stunned = false;
		anims.SetBool ("Stunned", false);

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
