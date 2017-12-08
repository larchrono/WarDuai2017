using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SenceEnter3 : MonoBehaviour {

	GameObject mainCanvas;
	public GameObject actorHorus;
	public GameObject actorNaphy;
	public GameObject actorAnubis;
	public GameObject actorAmit;

	public GameObject darkFire;

	public GameObject userInput;
	public GameObject pointFire;

	public GameObject anubisTo;


	Animator animator;

	public Camera camera_enter0;
	public Camera camera_enter1;
	public Camera camera_enter2;

	private GameObject tipText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Enter dark");
		if (GlobalData.Instance.movie_2_active == true) {
			GlobalData.Instance.movie_2_active = false;
			StartCoroutine (MovieWork ());
		}
	}

	IEnumerator MovieWork(){
		GlobalData.Instance.GameInState = GlobalData.GameStates.IN_MOVIE;
		tipText = WorldMenuController.current.menuTip;
		tipText.SetActive (false);
		WorldMenuController.current.miniMap.SetActive (false);

		//GameCamera.BlackFadeScreen (2);
		GameCamera.ApplyCameraObject (camera_enter0, 1);


		GameObject ef = Instantiate (Resources.Load ("Prefabs/effect/GroundHit01") as GameObject);
		ef.transform.position = pointFire.transform.position;
		darkFire.SetActive (true);
		GlobalData.Instance.isDarkFireGateShow = true;
		userInput.SetActive (false);
		Transmission.FromUnit (GlobalData.Instance.ActiveActors[0], "荷魯斯", "哇!!", "Stand", 0, true, 3);
		Transmission.FromUnit (GlobalData.Instance.ActiveActors[1], "奈芙蒂斯", "你這傢伙知道過去的方法嗎?", "Stand", 1, true, 5);
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
