using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelReference : MonoBehaviour {

	public GameObject PanelBack;

	public GameObject PanelWorkPanel;

	public GameObject ButtonAim;
	//For Dynamic Button Aim
	public int ButtonAimDynamicId;

	public List<GameObject> ButtonList;

	// Use this for initialization
	void Awake () {
		ButtonList = new List<GameObject> ();
	}
	
	public void Show(){
		gameObject.SetActive (true);

	}

	public void Hide(){
		gameObject.SetActive (false);
	}


}
