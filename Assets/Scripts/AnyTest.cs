using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AnyTest : MonoBehaviour {

	public GameObject panelFinal;
	public GameObject panelText;

	public Camera testCamera;
	public Camera testCamera2;

	public GameObject obj;

	public GameObject effect;

	// Use this for initialization
	void Start () {

		//GlobalData.Instance.ActiveActors [0].ATK = 3000;

		//GameCamera.FadeOut(2.0f,10f);

		//Debug.Log ("My name is " + gameObject.name);


		//BlackFade bf = new GameObject ().AddComponent<BlackFade> ();
		//bf.StartFadeOutIn (2.0f);

		return;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			Instantiate (effect);
		}

	}
}
