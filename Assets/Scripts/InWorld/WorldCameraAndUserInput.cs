using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class WorldCameraAndUserInput : MonoBehaviour {

	public GameObject mainCam;
	public GameObject targetActor;

	public bool useLookAt;

	//Camera
	private Vector3 mainCamForward;
	private Vector3 camViewActorMove;
	private GameObject aimForCamera;

	private Vector3 cameraDistance;
	private float camYRotate = 0f;
	private float camYRotateSpeed = 80f;
	private float camVerticalAngle = 30f;

	private float camXRotate = 0f;
	private float camXRotateMax = 54.0f;
	private float camXRotateMin = -15.0f;

	private Vector3 dragOrigin;

	void Awake(){
		
	}

	// Use this for initialization
	void Start () {
		cameraDistance = new Vector3 (0, 3, -5);
		mainCam.transform.position = targetActor.transform.position + cameraDistance;
		aimForCamera = targetActor.GetComponent<WorldActorAct> ().aimForCamera;
		cameraDistance = mainCam.transform.position - targetActor.transform.position;
		camXRotate = GlobalData.Instance.WorldCameraRotateAngle.x;
		camYRotate = GlobalData.Instance.WorldCameraRotateAngle.y;
	}
	
	// Update is called once per frame
	void Update () {



		//Camera for lookAt
		if (targetActor != null) {

			if (GameCamera.IsAimCharactor && GlobalData.Instance.GameInState == GlobalData.GameStates.IN_WORLD) {
				mainCam.transform.position = targetActor.transform.position + Quaternion.Euler (camXRotate, camYRotate, 0) * cameraDistance;
				mainCam.transform.eulerAngles = new Vector3 (camVerticalAngle, camYRotate, 0);

				if (useLookAt)
					mainCam.transform.LookAt (aimForCamera.transform);
			}
		}
		
		// read inputs for move
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
		if (mainCam != null)
		{
			// calculate camera relative direction to move:
			mainCamForward = Vector3.Scale(mainCam.transform.forward, new Vector3(1, 0, 1)).normalized;
			camViewActorMove = v*mainCamForward + h*mainCam.transform.right;
		}
		targetActor.GetComponent<WorldActorAct> ().Move (camViewActorMove);

		//for rotate
		//if (Input.GetKey (KeyCode.Keypad4)) {
		if (Input.GetButton("L")) {
			camYRotate -= camYRotateSpeed * Time.deltaTime;
		}
		//if (Input.GetKey (KeyCode.Keypad6)){
		if (Input.GetButton("R")) {
			camYRotate += camYRotateSpeed * Time.deltaTime;
		}
		if (Input.GetMouseButton (0)) {
			camYRotate += camYRotateSpeed * Time.deltaTime * Input.GetAxis ("Mouse X");
		}
		//if (Input.GetKey (KeyCode.Q)) {
		if (Input.GetButton("L2")) {
			camXRotate -= camYRotateSpeed * Time.deltaTime;
			if(camXRotate < camXRotateMin) camXRotate = camXRotateMin;
		}
		//if (Input.GetKey (KeyCode.E)){
		if (Input.GetButton("R2")) {
			camXRotate += camYRotateSpeed * Time.deltaTime;
			if(camXRotate > camXRotateMax) camXRotate = camXRotateMax;
		}
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			camXRotate += camYRotateSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel") * 10f;
			camXRotate = Mathf.Clamp (camXRotate, camXRotateMin, camXRotateMax);
		}
			
		GlobalData.Instance.WorldCameraRotateAngle = new Vector2 (camXRotate,camYRotate);
	}

	public Vector2 GetCameraFace(){
		return new Vector2 (camXRotate,camYRotate);
	}

}
