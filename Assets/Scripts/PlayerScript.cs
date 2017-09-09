using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
	public Transform groundTest;
	public LayerMask layerMask;
	public GameObject road;
	public Quaternion angleYOfRoad;

	private Animator animator;
	private Rigidbody rigidbody;
	private AudioSource bgmOfFalling;
	private Animator animatorOfChaser;
	private GameObject chaser;
	private CharacterController characterController;
	private AudioSource bgmOfGame;
	private float surviveTime;
	private bool isRunning;
	private float transformSpeed = 3f;
	private float forwardSpeed=0f;
	private float gravity=20.0f;
	private float jumpSpeed=8f;
	private Vector3 moveVelocity;
	private float actionTime;
	private float obstacleLength=7.0f;

	private readonly float deadFallingTime=4f;

	void Start(){
		animator = GetComponent<Animator> ();
		rigidbody = GetComponent<Rigidbody> ();
		bgmOfFalling = GetComponent<AudioSource> ();
		chaser = GameObject.Find ("Chaser");
		animatorOfChaser = chaser.GetComponent<Animator> ();
		characterController=GetComponent<CharacterController>();
		bgmOfGame = GameObject.Find ("GameController").GetComponent<AudioSource> ();
		surviveTime = 0f;
		isRunning = false;
		angleYOfRoad= transform.rotation;
	}

	void Update(){
		if (!isRunning) {
			if (Input.anyKeyDown) {
				animator.SetTrigger ("Start");
				animatorOfChaser.SetTrigger ("Start");
				isRunning = true;

				if (GameControllerScript.gameController != null) {
					GameControllerScript.gameController.StartCounting();
				}
			}
			return;
		}
		surviveTime += Time.deltaTime;
		if (forwardSpeed < 10f) {
			forwardSpeed = 6f+surviveTime / 60.0f;
			animatorOfChaser.speed =0.5f+surviveTime / 60.0f;
		}

		actionTime = obstacleLength /forwardSpeed;

		//这一段代码是根据勾股定理，使人物上坡减速的代码
		Vector3 newPosition = transform.position + (angleYOfRoad * Vector3.forward * forwardSpeed);
		Vector3 nowPosition = transform.position;
		//Debug.DrawLine (nowPosition,newPosition,Color.red);
		float c = forwardSpeed;
		float b = newPosition.y - nowPosition.y;
		if (b > 0) {
			float a = Mathf.Sqrt (Mathf.Pow (c, 2) - Mathf.Pow (b, 2));
			forwardSpeed = a;
		}


		if (characterController.isGrounded) {
			moveVelocity = transform.forward;
			moveVelocity *= forwardSpeed;
			//ButtonCallBack ();
		}
		ButtonCallBack ();
		moveVelocity.y -= gravity*Time.deltaTime;
		characterController.Move (moveVelocity*Time.deltaTime);
	}

	void FixedUpdate(){
		if (!Physics.CheckSphere (groundTest.position, 0.5f, layerMask)) {
			animator.SetBool ("Grounded", false);
			rigidbody.isKinematic = false;

			//隐藏道路对象，让相机看到人物掉落的场景
			road.SetActive (false);

			GameOver ();
		}
		if (!chaser.activeSelf) {
			GameOver ();
		}
	}

	public void GameOver(){
		if (GameControllerScript.gameController != null) {
			GameControllerScript.gameController.StopCounting ();
		}
		bgmOfGame.Stop ();
		if (!bgmOfFalling.isPlaying) {
			bgmOfFalling.Play ();
		}
		animator.SetTrigger ("Stop");
		moveVelocity = new Vector3 (0, 0, 0);
		characterController.Move (moveVelocity);
		StartCoroutine (ReStart ());
	}

	IEnumerator ReStart(){
		yield return new WaitForSeconds (deadFallingTime);
		SceneManager.LoadScene ("Main",LoadSceneMode.Single);
	}

	//通过Input绑定wasd控制人物动作
	void ButtonCallBack(){
		if (Input.GetButtonDown ("TurnLeft")) {
			TurnLeft ();
		}
		if (Input.GetButtonDown ("TurnRight")) {
			TurnRight ();
		}
		if (Input.GetButtonDown ("Slide")) {
			StartCoroutine(SlideAction ());
		}
		if (Input.GetButtonDown ("Jump")) {
			JumpAction ();
		}
		if (Input.GetButton ("LeftTransform")) {
			LeftTransformAction ();
		}
		if (Input.GetButton ("RightTransform")) {
			RightTransformAction ();
		}
	}

	public void  TurnLeft(){
		transform.Rotate (0,-90.0f,0);
	}

	public void TurnRight(){
		transform.Rotate (0,90.0f,0);
	}

	public IEnumerator SlideAction(){
		animator.SetTrigger ("Slide");
		characterController.center=new Vector3 (0f, 0.5f, 0f);
		characterController.height = 0f;
		yield return new WaitForSeconds (actionTime);
		characterController.center=new Vector3 (0f, 1f, 0f);
		characterController.height = 2f;
	}

	public void JumpAction(){
		//if(characterController.isGrounded){
		animator.SetTrigger ("Jump");
		moveVelocity.y = jumpSpeed;
		//}
	}

	public void LeftTransformAction(){
		moveVelocity += transform.right*(-transformSpeed);
	}

	public void RightTransformAction(){
		moveVelocity += transform.right*transformSpeed;
	}
}
