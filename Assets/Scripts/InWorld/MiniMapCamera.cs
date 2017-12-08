using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapCamera : MonoBehaviour {

	public GameObject mainActor;
	public GameObject miniArrow;

	bool doWeHaveFogInScene;

	// Use this for initialization
	void Start () {
		doWeHaveFogInScene = RenderSettings.fog;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (mainActor.transform.position.x,transform.position.y,mainActor.transform.position.z);
		miniArrow.GetComponent<RectTransform> ().localRotation = Quaternion.Euler (0,0,-mainActor.transform.rotation.eulerAngles.y);
	}

	private void OnPreRender() {
		//RenderSettings.fog = false;
	}
	private void OnPostRender() {
		//RenderSettings.fog = doWeHaveFogInScene;
	}
}

