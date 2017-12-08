using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRunLineGlow : MonoBehaviour {

	private float AnchorMinX;
	private float AnchorMaxX;

	private float AnchorMinY;
	private float AnchorMaxY;

	private float speed_0 = 0.8f;
	private float speed_1 = 1.5f;

	private int switcher;

	// Use this for initialization
	void Start () {
		AnchorMinX = 0;
		AnchorMaxX = 0;
		AnchorMinY = 0.1f;
		AnchorMaxY = 0.18f;
		switcher = 0;
	}

	// Update is called once per frame
	void Update () {
		switch(switcher){
		case 0:
			AnchorMaxX = Mathf.Clamp (AnchorMaxX + speed_0 * Time.deltaTime, 0f, 1.0f);
			if(AnchorMaxX >= 1.0f){
				switcher = 1;
			}
			break;
		case 1:
			AnchorMinX = Mathf.Clamp (AnchorMinX + speed_1 * Time.deltaTime, 0f, 1.0f);
			if(AnchorMinX >= 1.0f){
				switcher = 0;
				AnchorMaxX = 0;
				AnchorMinX = 0;
			}
			break;
		}

		GetComponent<RectTransform> ().anchorMin = new Vector2 (AnchorMinX,AnchorMinY);
		GetComponent<RectTransform> ().anchorMax = new Vector2 (AnchorMaxX,AnchorMaxY);
	}
}
