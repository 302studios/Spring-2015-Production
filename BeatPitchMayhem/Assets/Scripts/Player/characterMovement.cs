using UnityEngine;
using System.Collections;

public class characterMovement : MonoBehaviour {


	// Objects & Components
	GameObject theCamera;
	CharacterController controller;


	// Walking & Running
	public bool canControl = true;
	public float walkSpeed = 8f;
	public float runSpeed = 13f;
	float speedSmoothing = 10f;
	private float moveSpeed = 0f;
	private bool movingBack = false;
	private bool isMoving = false;
	bool isRunning = false;
	private bool slammed = false;
	public Vector3 movement = Vector3.zero;
	private Vector3 moveDirection = Vector3.zero;
	public bool grounded = true; // Currently no check for grounded!!!!
	//public Transform groundCheck;
	//float groundRadius = 0.2f;
	//public LayerMask whatIsGround;

	// Crouching
	bool isCrouching = false;
	public Vector3 cameraLocStanding = Vector3.zero;
	public Vector3 cameraLocCrouching = Vector3.zero;
	float crouchSpeed = 5f;
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
	float dodgePower = 40f;
	float dodgeCooldown = 1f;
	public int keyCount = 0;
	public float keyCool = 0.2f;

	// Misc
	private CollisionFlags collisionFlags;



	// Use this for initialization
	void Start () {
	
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
	}
	
	// Update is called once per frame
	void Update () {
	
		basicMovement();
		doubleTapDodge();
		crouchCamMove ();
		leaningLeon ();

		// Calculate actual motion
		movement = moveDirection * moveSpeed;
		movement *= Time.deltaTime;

		collisionFlags = controller.Move (movement);
	
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
		if (isRunning) {
			targetSpeed *= runSpeed;
		} 
		else 
		{
			targetSpeed *= walkSpeed;
		}

		moveSpeed = Mathf.Lerp (moveSpeed, targetSpeed, curSmooth);
		//Animator.doTheThing 


	}

	void doubleTapDodge (){

		// Keyboard Controls
		if (canDodge) {
			// Dodge Left
			if ((Input.GetKeyDown (KeyCode.A)) && !((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D)))) {
			
				if (keyCool > 0 && keyCount == 1/*Number of Taps you want Minus One*/) {
					moveSpeed *= dodgePower;
					StartCoroutine (dodgeCooling ());
				} else {
					keyCool = 0.2f; 
					keyCount += 1;
				}
			}
			// Dodge Right
			if ((Input.GetKeyDown (KeyCode.D)) && !((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.A)))) {
			
				if (keyCool > 0 && keyCount == 1/*Number of Taps you want Minus One*/) {
					moveSpeed *= dodgePower;
					StartCoroutine (dodgeCooling ());
				} else {
					keyCool = 0.2f; 
					keyCount += 1;
				}
			}
			// Dodge Back
			if ((Input.GetKeyDown (KeyCode.S)) && !((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))) {
			
				if (keyCool > 0 && keyCount == 1/*Number of Taps you want Minus One*/) {
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

		if(Input.GetKeyDown(KeyCode.Z))
		{
			isCrouching = true;
			theCamera.transform.localPosition = cameraLocCrouching;

		}

		if(Input.GetKeyUp(KeyCode.Z))
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

}
