using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTest : MonoBehaviour {

	public GameObject target ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<RectTransform> ().position = Camera.main.WorldToScreenPoint (target.transform.position);
	}
}
