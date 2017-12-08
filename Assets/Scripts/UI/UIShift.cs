using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShift : MonoBehaviour {

	private static GameObject shiftObj;

	public float LRLeft;
	public float LRRight;
	public Vector3 size;

	private bool isLRShow;

	private GameObject LShift;
	private GameObject RShift;

	void Awake() {
		if (shiftObj == null)
			shiftObj = Resources.Load ("Prefabs/UI/LRShiftArrow") as GameObject;
	}

	// Use this for initialization
	void Start () {
		//OpenLR ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenLR(){
		if (LShift != null)
			return;
		LShift = Instantiate (shiftObj,Vector3.zero,Quaternion.identity);
		RShift = Instantiate (shiftObj,Vector3.zero,Quaternion.identity);
		LShift.GetComponent<RectTransform> ().localScale = new Vector3 (size.x, size.y, size.z);
		RShift.GetComponent<RectTransform> ().localScale = new Vector3 (-size.x, size.y, size.z);
		LShift.transform.SetParent (this.GetComponent<RectTransform>(),false);
		RShift.transform.SetParent (this.GetComponent<RectTransform>(),false);
		LShift.GetComponent<RectTransform> ().anchorMin = new Vector2 (LRLeft,0.5f);
		LShift.GetComponent<RectTransform> ().anchorMax = new Vector2 (LRLeft,0.5f);
		RShift.GetComponent<RectTransform> ().anchorMin = new Vector2 (LRRight,0.5f);
		RShift.GetComponent<RectTransform> ().anchorMax = new Vector2 (LRRight,0.5f);
	}

	public void CloseLR(){
		Destroy (LShift);
		Destroy (RShift);
	}
}
