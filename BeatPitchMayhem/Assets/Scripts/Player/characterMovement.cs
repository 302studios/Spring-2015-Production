using UnityEngine;
using System.Collections;

public class characterMovement : MonoBehaviour {


	// Objects & Components
	GameObject theCamera;
	CharacterController controller;


	// Walking & Running
	public bool canControl = true;
	public float walkSpeed = 8f;
	public float runSpeed;
	float speedSmoothing = 10f;
	private float moveSpeed = 0f;
	private bool movingBack = false;
	private bool isMoving = false;
	bool isRunning = false;
	private bool slammed = false;
	public Vector3 movement = Vector3.zero;
	private Vector3 moveDirection = Vector3.zero;
	public bool grounded = true; 
	float distToGround;
	public bool vertException = false;
	public float groundFloor = 1.6f;
	public float gravity = .05f;
	//public Transform groundCheck;
	//float groundRadius = 0.2f;
	//public LayerMask whatIsGround;

	// Crouching
	public bool isCrouching = false;
	public Vector3 cameraLocStanding = Vector3.zero;
	public Vector3 cameraLocCrouching = Vector3.zero;
	float crouchSpeed;
	float crouchDist = .5f;

	// Leaning Leon
	bool isLeaning = false;
	bool leaningLeft = false;
	bool leaningRight = false;
	Vector3 cameraRotStanding = Vector3.zero;
	Vector3 cameraLocLeanLeft = Vector3.zero;
	Vector3 cameraRotLeanLeft = Vector3.zero;
	Vector3 cameraLocLeanRight = Vector3.zero;
	Vector3 cameraRotLeanRight = Vector3.zero;

	// Dodging
	bool isDodging = false;
	bool canDodge = true;
	public float dodgePower = 20f;
	float dodgeCooldown = 1f;
	public int keyCount = 0;
	public float keyCool = 0.2f;

	// Misc
	private CollisionFlags collisionFlags;



	// Use this for initialization
	void Start () {
	
		distToGround = collider.bounds.extents.y;
		theCamera = GameObject.Find ("Main Camera");
		moveDirection = transform.TransformDirection(Vector3.forward);
		controller = GetComponent<CharacterController>();
		cameraLocStanding = theCamera.transform.localPosition;
		cameraLocCrouching = Vector3.Scale((theCamera.transform.localPosition), (new Vector3 (0f, crouchDist, 0f))); 
		cameraLocLeanLeft = new Vector3 (-1.5f, 1f, 0f);
		cameraLocLeanRight = new Vector3 (1.5f, 1f, 0f);
		cameraRotLeanLeft = new Vector3 (10f, 0f, 0f);
		cameraRotLeanRight = new Vector3 (0f, -10f, 0f);
		cameraRotStanding = theCamera.transform.localRotation.eulerAngles;

		runSpeed = walkSpeed * 1.6f;
		crouchSpeed = walkSpeed * 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
	
		basicMovement();
		//doubleTapDodge();
		spaceDodge ();
		crouchCamMove ();
		leaningLeon ();

		// Calculate actual motion
		movement = moveDirection * moveSpeed;
		movement *= Time.deltaTime;

		collisionFlags = controller.Move (movement);

		if(!vertException){
			Vector3 temp = this.transform.position;
			temp.y = groundFloor;
			this.transform.position = temp;
		}else if(!IsGrounded()){
			Vector3 temp = this.transform.position;
			temp.y = temp.y - gravity;
			this.transform.position = temp;
		}
	
	}

	bool IsGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}

	void basicMovement () {

		Transform cameraTransform = Camera.main.transform;

		Vector3 forward = cameraTransform.TransformDirection (Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;

		Vector3 right = new Vector3 (forward.z, 0, -forward.x);

		float v = Input.GetAxisRaw ("Vertical");
		float h = Input.GetAxisRaw ("Horizontal");

		if (v < -0.2)
			movingBack = true;
		else
			movingBack = false;

		bool wasMoving = isMoving;
		isMoving = Mathf.Abs (h) > 0.1 || Mathf.Abs (v) > 0.1;


		Vector3 targetDirection = (h * right) + (v * forward);

		if (this.transform.position.y > 1.6f) {
			moveDirection.y = -1f;
		}

		float targetSpeed = 0f;
		if (grounded) {

			if(targetDirection !=  Vector3.zero)
			{
				
				moveDirection = targetDirection.normalized;

			}

		}

		float curSmooth = speedSmoothing * Time.deltaTime;

		targetSpeed = Mathf.Min (targetDirection.magnitude, 1.0f);
		isRunning = Input.GetKey(KeyCode.LeftShift);
		if (isRunning && !isCrouching) {
			targetSpeed *= runSpeed;
		} 
		else if(isCrouching)
		{
			targetSpeed *= crouchSpeed; 
		}
		else
		{
			targetSpeed *= walkSpeed;
		}

		moveSpeed = Mathf.Lerp (moveSpeed, targetSpeed, curSmooth);

		//Animator.doTheThing 


	}

	void spaceDodge (){
		
		// Keyboard Controls
		if (canDodge) {
			
			if ((Input.GetKey (KeyCode.W)) && Input.GetKeyDown (KeyCode.Space) && (!((Input.GetKey (KeyCode.A)) || (Input.GetKey (KeyCode.S)) || (Input.GetKey (KeyCode.D))))) {
				
				moveSpeed *= dodgePower;
				canDodge = false;
				StartCoroutine (dodgeCooling ());
			}
			if ((Input.GetKey (KeyCode.A)) && Input.GetKeyDown (KeyCode.Space) && (!((Input.GetKey (KeyCode.W)) || (Input.GetKey (KeyCode.S)) || (Input.GetKey (KeyCode.D))))) {
				
				moveSpeed *= dodgePower;
				canDodge = false;
				StartCoroutine (dodgeCooling ());
			}
			if ((Input.GetKey (KeyCode.S)) && Input.GetKeyDown (KeyCode.Space) && (!((Input.GetKey (KeyCode.W)) || (Input.GetKey (KeyCode.A)) || (Input.GetKey (KeyCode.D))))) {
				
				moveSpeed *= dodgePower;
				canDodge = false;
				StartCoroutine (dodgeCooling ());
			}
			if ((Input.GetKey (KeyCode.D)) && Input.GetKeyDown (KeyCode.Space) && (!((Input.GetKey (KeyCode.W)) || (Input.GetKey (KeyCode.S)) || (Input.GetKey (KeyCode.A))))) {
				
				moveSpeed *= dodgePower;
				canDodge = false;
				StartCoroutine (dodgeCooling ());
			}

		}
	}
	void doubleTapDodge (){

		// Keyboard Controls
		if(canDodge){
			// Dodge Left
			if ((Input.GetKeyDown (KeyCode.A)) && !((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D)))) {
			
				if (keyCool > 0 && keyCount == 1) {
					moveSpeed *= dodgePower;
					StartCoroutine (dodgeCooling ());
				} else {
					keyCool = 0.2f; 
					keyCount += 1;
				}
			}
			// Dodge Right
			if ((Input.GetKeyDown (KeyCode.D)) && !((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)))) {
			
				if (keyCool > 0 && keyCount == 1) {
					moveSpeed *= dodgePower;
					StartCoroutine (dodgeCooling ());
				} else {
					keyCool = 0.2f; 
					keyCount += 1;
				}
			}
			// Dodge Back
			if ((Input.GetKeyDown (KeyCode.S)) && !((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))) {
			
				if (keyCool > 0 && keyCount == 1) {
					moveSpeed *= dodgePower;
					StartCoroutine (dodgeCooling ());
				} else {
					keyCool = 0.2f; 
					keyCount += 1;
				}
			}
			if ((Input.GetKeyDown (KeyCode.W)) && !((Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))) {
				
				if (keyCool > 0 && keyCount == 1) {
					moveSpeed *= dodgePower;
					StartCoroutine (dodgeCooling ());
				} else {
					keyCool = 0.2f; 
					keyCount += 1;
				}
			}

			// Joystick Controls needed
		}

		if (keyCool > 0)
		{
			
			keyCool -= 1 * Time.deltaTime;	
		}
		else{
			keyCount = 0;
		}
	}

	IEnumerator dodgeCooling()
	{
		canDodge = false;
		yield return new WaitForSeconds (dodgeCooldown);
		canDodge = true;

	}

	void crouchCamMove(){

		if(Input.GetKeyDown(KeyCode.LeftControl))
		{
			isCrouching = true;
			theCamera.transform.localPosition = cameraLocCrouching;

		}

		if(Input.GetKeyUp(KeyCode.LeftControl))
		{
			isCrouching = false;	
			theCamera.transform.localPosition = cameraLocStanding;
			
		}

	}

	void leaningLeon(){


		 /*
		  * Need to work on rotating the camera angle. Current methods are failing, not sure why.
		  */
		// Left Lean
		if ((Input.GetKeyDown (KeyCode.Q)) && !(Input.GetKey(KeyCode.E))) {
			theCamera.transform.localPosition = cameraLocLeanLeft;
			//theCamera.transform.Rotate(cameraRotLeanLeft);
		}
		if ((Input.GetKeyUp (KeyCode.Q))  && !(Input.GetKey(KeyCode.E))) {
			theCamera.transform.localPosition = cameraLocStanding;
			//theCamera.transform.Rotate(cameraRotStanding);
		}

		// Right Lean
		if ((Input.GetKeyDown (KeyCode.E)) && !(Input.GetKey(KeyCode.Q))) {
			theCamera.transform.localPosition = cameraLocLeanRight;
			//theCamera.transform.Rotate(cameraRotLeanRight);
		}
		if ((Input.GetKeyUp (KeyCode.E)) && !(Input.GetKey(KeyCode.Q))) {
			theCamera.transform.localPosition = cameraLocStanding;
			//theCamera.transform.Rotate(cameraRotStanding);
		}
	}

	void OnTriggerStay(Collider col){

		if(col.name == "vertExceptionTrigger"){
			vertException = true;
		}

	}

	void OnTriggerExit(Collider col){
		
		if(col.name == "vertExceptionTrigger"){
			//vertException = false;
			Debug.Log("No more box");
		}
		
	}

}
