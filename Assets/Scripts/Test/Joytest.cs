using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joytest : MonoBehaviour {

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
		DebugLogJoystickButtonPresses();
	}

	private void DebugLogJoystickButtonPresses() {
		int joyNum = 1; // start at 1 because unity calls them joystick 1 - 4
		int buttonNum = 0;
		int keyCode = 350; // start at joy 1 keycode

		// log button presses on 3 joysticks (20 button inputs per joystick)
		// NOTE THAT joystick 4 is not supported via keycodes for some reason, so only polling 1-3
		for(int i = 0; i < 60; i++) {

			// Log any key press
			//if(Input.GetKeyDown((keyCode+i)))
			//	Debug.Log("Pressed! Joystick " + joyNum + " Button " + buttonNum + " @ " + Time.time);

			buttonNum++; // Increment

			// Reset button count when we get to last joy button
			if(buttonNum == 20) {
				buttonNum = 0;
				joyNum++; // next joystick
			}
		}
	}
}
