using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldCameraAct : MonoBehaviour {

	public static WorldCameraAct current;

	public GameObject menuBlurBackground;
	public GameObject battleBlurCard;

	private bool grabBlur = false;
	private bool grabBtBlur = false;
	private GameObject ins;

	void Awake(){
		current = this;
	}

	public bool GrabBlur {
		get{return grabBlur;}
		set{grabBlur = value;}
	}

	public void ShowBlur(bool act){
		//must wait for OnPostRender finishing to show Blur
		StartCoroutine (ShowBlurWork (act));
	}

	public void CreateBattleBlur(){
		ins = Instantiate (battleBlurCard);
		ins.SetActive (false);
		StartCoroutine (ShowBattleBlurWork (ins));
		grabBtBlur = true;
	}

	void OnPostRender() {
		if (grabBlur) {
			Texture2D tex = new Texture2D(Screen.width, Screen.height);
			tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
			tex.Apply();
			//GameObject.Find("Plane").GetComponent<Renderer>().material.mainTexture = tex;
			//GameObject.Find ("Image").GetComponent<Image> ().color = Color.red;

			menuBlurBackground.GetComponent<Image>().sprite = Sprite.Create(tex,new Rect(0,0,Screen.width,Screen.height),new Vector2(0,0));
			grabBlur = false;
		}
		if (grabBtBlur) {
			Texture2D tex = new Texture2D(Screen.width, Screen.height);
			tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
			tex.Apply();
			ins.GetComponent<ScreenBlurAct> ().child.GetComponent<Image>().sprite = Sprite.Create(tex,new Rect(0,0,Screen.width,Screen.height),new Vector2(0,0));
			grabBlur = false;
		}
	}

	IEnumerator ShowBlurWork(bool act){
		yield return new WaitForEndOfFrame();
		menuBlurBackground.SetActive (act);
	}

	IEnumerator ShowBattleBlurWork(GameObject go){
		yield return new WaitForEndOfFrame ();
		go.SetActive (true);
	}
}
