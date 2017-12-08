using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIEffect : MonoBehaviour {

	public bool fadeInOut;
	public float fadeTime;
	private bool fadeNavi;
	private float fadeElapse = 0;


	private float _elapseTime = 0;

	// Use this for initialization
	void Start () {
		fadeElapse = fadeTime;
	}
	
	// Update is called once per frame
	void Update () {
		_elapseTime += Time.deltaTime;


		// Fade in out
		if (fadeInOut) {
			fadeElapse += Time.deltaTime;
			if (fadeElapse > fadeTime) {
				fadeElapse -= fadeTime;
				if (fadeNavi)
					GetComponent<Text> ().CrossFadeAlpha (1f, fadeTime, false);
				else
					GetComponent<Text> ().CrossFadeAlpha (0f, fadeTime, false);
				fadeNavi = !fadeNavi;
			}
		}




	}

}