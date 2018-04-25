using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joytest : MonoBehaviour {

	public GameObject firstButton;

	void Start(){
		if (firstButton != null)
			EventSystem.current.SetSelectedGameObject (firstButton);
	}

	// Slap this code onto an object in the scene then press buttons
	// I recommend you test with 3-4 controllers (different models) for best effect
	void Awake() {
		string[] names = Input.GetJoystickNames();
		Debug.Log("Connected Joysticks:");
		for(int i = 0; i < names.Length; i++) {
			Debug.Log("Joystick" + (i + 1) + " = " + names[i]);
		}
	}

	void Update() {

		if (Input.GetAxis ("Vertical") != 0)
			Debug.Log ("Vertical : " + Input.GetAxis ("Vertical"));
		if (Input.GetAxis ("Horizontal") != 0)
			Debug.Log ("Horizontal : " + Input.GetAxis ("Horizontal"));
		
		if (Input.GetAxis ("Vertical Arrow") != 0)
			Debug.Log ("Vertical Arrow : " + Input.GetAxis ("Vertical Arrow"));
		if (Input.GetAxis ("Horizontal Arrow") != 0)
			Debug.Log ("Horizontal Arrow : " + Input.GetAxis ("Horizontal Arrow"));

		if (Input.GetAxis ("Vertical Rotate") != 0)
			Debug.Log ("Vertical Rotate : " + Input.GetAxis ("Vertical Rotate"));
		if (Input.GetAxis ("Horizontal Rotate") != 0)
			Debug.Log ("Horizontal Rotate : " + Input.GetAxis ("Horizontal Rotate"));
		
	}
}
