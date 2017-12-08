using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMenuEvents : MonoBehaviour {

	public GameObject menuTip;

	// Use this for initialization
	void Start () {
		WorldMenuController.EVENT_PLAYER_PRESS_A += OnPlayerPressA;
		WorldMenuController.EVENT_PLAYER_PRESS_B += OnPlayerPressB;
		WorldMenuController.EVENT_PLAYER_ENTER_MENU += OnEnterMenu;
		WorldMenuController.EVENT_PLAYER_EXIT_MENU += OnExitMenu;
		WorldMenuController.EVENT_PLAYER_PRESS_L += OnPlayerPressL;
		WorldMenuController.EVENT_PLAYER_PRESS_R += OnPlayerPressR;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnPlayerPressA(object sender,PanelArgs args){

	}
	void OnPlayerPressB(object sender,PanelArgs args){

	}

	void OnEnterMenu(object sender,PanelArgs args){
		menuTip.SetActive (false);
	}

	void OnExitMenu(object sender,PanelArgs args){
		menuTip.SetActive (true);
	}

	void OnPlayerPressL(object sender,PanelArgs args){
		
	}

	void OnPlayerPressR(object sender,PanelArgs args){
		
	}
}
