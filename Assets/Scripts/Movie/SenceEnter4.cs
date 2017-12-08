using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SenceEnter4 : MonoBehaviour {

	GameObject mainCanvas;

	public GameObject anubis;

	public GameObject darkAmit;
	public GameObject darkFire;
	public GameObject Coli;
	public GameObject panelFinal;
	public GameObject panelText;

	public GameObject userInput;

	public GameObject AnuFrom;
	public GameObject anubisTo;


	Animator animator;

	public Camera camera_enter0;
	public Camera camera_enter1;
	public Camera camera_enter2;

	private GameObject tipText;

	private bool isAnuMove = false;

	// Use this for initialization
	IEnumerator Start () {
		
		yield return new WaitForSeconds (0.1f);

		darkAmit.SetActive (false);
		GlobalData.Instance.GameInState = GlobalData.GameStates.IN_MOVIE;
		tipText = WorldMenuController.current.menuTip;
		tipText.SetActive (false);
		WorldMenuController.current.miniMap.SetActive (false);
		userInput.SetActive (false);
		darkFire.SetActive (true);

		GameCamera.ApplyCameraObject (camera_enter0,0.1f);


		Transmission.FromUnit (GlobalData.Instance.ActiveActors[1], "奈芙蒂斯", "只是幻影嗎?......", "Stand", 1, true, 5);
		Transmission.FromUnit (MonsterDataBase.MonsterData[2], "少年", "我先走一步囉~~~", "Stand", 0, true, 3);
		Transmission.FromUnit (GlobalData.Instance.ActiveActors[1], "奈芙蒂斯", "你這傢伙~~~~", "Angry", 1, true, 15);

		yield return new WaitForSeconds (5);

		Coli.SetActive (false);
		anubis.transform.position = AnuFrom.transform.position;
		isAnuMove = true;
		anubis.GetComponent<Animator> ().CrossFade ("walking",0.5f);

		yield return new WaitForSeconds (10);
	
		panelFinal.SetActive (true);
		panelFinal.GetComponent<Image> ().CrossFadeAlpha (0,0,true);
		panelFinal.GetComponent<Image> ().CrossFadeAlpha (1,15,true);
		panelText.GetComponent<Text> ().CrossFadeAlpha (0, 0, true);
		panelText.GetComponent<Text> ().CrossFadeAlpha (1, 15, true);


	}
	
	// Update is called once per frame
	void Update () {
		
		if (isAnuMove) {

			anubis.transform.LookAt (anubisTo.transform);

			Vector3 dir = anubisTo.transform.position - anubis.transform.position;
			Vector3 movement = dir.normalized * 1.5f * Time.deltaTime;
			if (movement.magnitude > dir.magnitude)
				movement = dir;
			anubis.GetComponent<CharacterController> ().Move (movement);

		}

	}

	IEnumerator MovieWork(){
		

		//GameCamera.BlackFadeScreen (2);
		GameCamera.ApplyCameraObject (camera_enter0, 1);




		Transmission.FromUnit (GlobalData.Instance.ActiveActors[0], "荷魯斯", "...", "Stand", 0, true, 3);
		Transmission.FromUnit (GlobalData.Instance.ActiveActors[1], "奈芙蒂斯", "等等，牠要過來了", "Stand", 1, true, 5);

		yield return new WaitForSeconds (16f);
		/*
		Transmission.FromUnit (MonsterDataBase.MonsterData[2], "少年", "嗚......!\n姐姐長的很可愛卻異常的兇悍呢...", "Cry", 0, true, 5);
		Transmission.FromUnit (GlobalData.Instance.ActiveActors[1], "奈芙蒂斯", "......", "Angry", 1, true, 5);
		Transmission.FromUnit (MonsterDataBase.MonsterData[3], "阿米特", "(舔舔舔)", "Stand", 1, true, 5);
		Transmission.FromUnit (GlobalData.Instance.ActiveActors[0], "荷魯斯", "哈哈哈阿米特要告訴我們到宮殿的路嗎?", "Smile", 0, true, 5);
		Transmission.FromUnit (MonsterDataBase.MonsterData[2], "少年", "快把阿米特還我!!", "Stand", 1, true, 5);

		Transmission.FromUnit (GlobalData.Instance.ActiveActors[1], "奈芙蒂斯", "和我一起到宮殿就把阿米特還你", "Smile", 0, true, 5);
		Transmission.FromUnit (GlobalData.Instance.ActiveActors[0], "荷魯斯", "出發~~~!", "Stand", 0, true, 5);
		Transmission.FromUnit (MonsterDataBase.MonsterData[2], "少年", "等等我啊! 可惡!", "Stand", 1, true, 5);
		*/

		userInput.SetActive (true);
		GlobalData.Instance.movie_2_active = false;
		tipText.SetActive (true);
		GlobalData.Instance.GameInState = GlobalData.GameStates.IN_WORLD;


	}
}
