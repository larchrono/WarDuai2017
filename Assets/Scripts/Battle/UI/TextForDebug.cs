using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextForDebug : MonoBehaviour {

	private Text context;
	// Use this for initialization
	void Start () {
		context = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		//context.text = BattleUI.current.GetNowPanelName () + "\n" + Camera.main.gameObject.name + "\n";

		if (Input.GetKeyDown (KeyCode.F12)) {
			gameObject.SetActive (true);
		}

		context.text = "" + GlobalData.Instance.PositionBeforeBattle;
	}
}
