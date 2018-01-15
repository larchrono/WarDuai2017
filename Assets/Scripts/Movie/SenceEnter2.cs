using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SenceEnter2 : MonoBehaviour {

	GameObject mainCanvas;
	public GameObject actorHorus;
	public GameObject actorNaphy;
	public GameObject actorAnubis;
	public GameObject actorAmit;

	public GameObject anubisTo;
	float anubisToSpeed = 1f;
	public bool isAnuMove = false;


	private GameObject tipText;

	Animator animator;

	public Camera camera_enter0;
	public Camera camera_enter1;
	public Camera camera_enter2;

	private bool isMovieFinish = false;

	// Use this for initialization
	IEnumerator Start () {

		GlobalData.Instance.GameInState = GlobalData.GameStates.IN_MOVIE;
		tipText = WorldMenuController.current.menuTip;
		tipText.SetActive (false);

		GameCamera.ApplyCameraObject (camera_enter0, 0);

		Transmission.FromUnit (MonsterDataBase.MonsterData[2], "少年", "嗚......!\n姐姐長的很可愛卻異常的兇悍呢...", "Cry", 0, false, 5);
		Transmission.FromUnit (GlobalData.Instance.ActiveActors[1], "奈芙蒂斯", "......", "Angry", 1, false, 5);
		Transmission.FromUnit (MonsterDataBase.MonsterData[3], "阿米特", "(舔舔舔)", "Stand", 1, false, 5);
		Transmission.FromUnit (GlobalData.Instance.ActiveActors[0], "荷魯斯", "哈哈哈阿米特要告訴我們到宮殿的路嗎?", "Smile", 0, false, 5);
		Transmission.FromUnit (MonsterDataBase.MonsterData[2], "少年", "快把阿米特還我!!", "Stand", 1, false, 5);

		Transmission.FromUnit (GlobalData.Instance.ActiveActors[1], "奈芙蒂斯", "和我一起到宮殿就把阿米特還你", "Smile", 0, false, 5);
		Transmission.FromUnit (GlobalData.Instance.ActiveActors[0], "荷魯斯", "出發~~~!", "Stand", 0, false, 5);
		Transmission.FromUnit (MonsterDataBase.MonsterData[2], "少年", "等等我啊! 可惡!", "Stand", 1, false, 5, delegate() {
			isMovieFinish = true;
		});

		yield return new WaitUntil (() => isMovieFinish);

		GlobalData.Instance.movie_1_active = false;
		tipText.SetActive (true);
		GlobalData.Instance.GameInState = GlobalData.GameStates.IN_WORLD;
	}
	
	// Update is called once per frame
	void Update () {

	}

}
