using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlackFade : MonoBehaviour {

	GameObject mainCanvas;
	GameObject clone;
	public GameCamera.ActionCallback callback;

	// Use this for initialization
	void Awake () {
		mainCanvas = GameObject.Find ("Canvas");
		clone = (GameObject)Instantiate(GameResource.Prefab.BlackScreen, Vector3.zero, Quaternion.identity);
		clone.transform.SetParent (mainCanvas.GetComponent<RectTransform>(),false);
		//clone.GetComponent<RectTransform> ().SetSiblingIndex (2);
	}


	public void StartFadeIn(float t){
		clone.GetComponent<Image> ().CrossFadeAlpha (0f, t, false);
		Destroy (this.gameObject);
		Destroy (clone,t);
	}

	public void StartFadeOut(float t,float until = 0){
		clone.GetComponent<Image> ().CrossFadeAlpha (0, 0, true);
		clone.GetComponent<Image> ().CrossFadeAlpha (1f, t, false);
		Destroy (clone,t + until);
		StartCoroutine (CallBackWork(t));
	}

	public void StartFadeOutIn(float t){
		StartCoroutine (FadeInOutWork (t));
	}

	IEnumerator FadeInOutWork(float t){
		clone.GetComponent<Image> ().CrossFadeAlpha (0, 0, true);
		clone.GetComponent<Image> ().CrossFadeAlpha (1f, t/2, false);
		yield return new WaitForSeconds (t/2);
		clone.GetComponent<Image> ().CrossFadeAlpha (0f, t/2, false);
		Destroy (this.gameObject,t);
		Destroy (clone,t);
	}

	IEnumerator CallBackWork(float t){
		yield return new WaitForSeconds (t);
		Destroy (gameObject);
		if (callback != null) {
			callback ();
		}
	}

}
