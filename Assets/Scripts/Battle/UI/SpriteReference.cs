using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteReference : MonoBehaviour {

	public GameObject refLocation { set; get; }

	//public GameObject UITagTargetModel {}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 pos = Camera.main.WorldToScreenPoint (refLocation.transform.position);
		GetComponent<RectTransform> ().position = pos;
	}
}
