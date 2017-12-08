using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMapCamera : MonoBehaviour {

	public Camera move_1;
	public Camera move_2;
	public Camera move_3;
	public Camera move_4;
	public Camera move_5;

	// Use this for initialization
	IEnumerator Start () {
		GlobalData.Instance.GameInState = GlobalData.GameStates.IN_MOVIE;

		//tipText.SetActive (false);

		BlackFade blackFade = new GameObject("blackFade").AddComponent<BlackFade>();
		blackFade.StartFadeIn (1f);

		GameCamera.ApplyCameraObject (move_1,0);
		yield return new WaitForSeconds (0.01f);
		GameCamera.ApplyCameraObject (move_2,3f);
		yield return new WaitForSeconds (3.0f);
		GameCamera.ApplyCameraObject (move_3,3f);
		yield return new WaitForSeconds (3.0f);
		GameCamera.ApplyCameraObject (move_4,3f);
		yield return new WaitForSeconds (3.0f);
		GameCamera.ApplyCameraObject (move_5,3f);
		yield return new WaitForSeconds (3.0f);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
