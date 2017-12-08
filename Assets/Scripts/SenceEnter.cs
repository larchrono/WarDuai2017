using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SenceEnter : MonoBehaviour {

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

	// Use this for initialization
	IEnumerator Start () {

		GlobalData.Instance.GameInState = GlobalData.GameStates.IN_MOVIE;
		tipText = WorldMenuController.current.menuTip;
		tipText.SetActive (false);
		WorldMenuController.current.miniMap.SetActive (false);

		animator = actorHorus.GetComponent<Animator>();
		animator.Play ("standing_up");
		BlackFade blackFade = new GameObject("blackFade").AddComponent<BlackFade>();
		GameCamera.ApplyCameraObject (camera_enter0, 0);
		yield return new WaitForSeconds (0.1f);
		animator.enabled = false;

		Transmission.FromUnit (GlobalData.Instance.ActiveActors[0], "荷魯斯", "好痛啊~", "Cry", 0, true, 5);
		Transmission.FromUnit (GlobalData.Instance.ActiveActors[1], "奈芙蒂斯", "快起來!", "Angry", 1, true, 5);
		Transmission.FromUnit (MonsterDataBase.MonsterData[2], "少年", "阿米特，那個還不能吃喔", "Stand", 1, true, 5);
		Transmission.FromUnit (MonsterDataBase.MonsterData[3], "阿米特", "(哈哈喘氣)", "Stand", 0, true, 5);

		Transmission.FromUnit (GlobalData.Instance.ActiveActors[0], "荷魯斯", "你是這裡的居民嗎?", "Stand", 0, true, 5);
		Transmission.FromUnit (MonsterDataBase.MonsterData[2], "少年", "我才不住貧民窟呢!只是帶阿米特來這裡散步!", "Stand", 0, true, 5);
		Transmission.FromUnit (GlobalData.Instance.ActiveActors[0], "荷魯斯", "哦?那你知道這裡的宮殿該往哪裡走嗎?", "Stand", 0, true, 5);
		Transmission.FromUnit (MonsterDataBase.MonsterData[2], "少年", "嗯~現在我沒空跟你聊天。哈哈哈阿米特我們繼續走~", "Stand", 0, true, 5);

		Transmission.FromUnit (GlobalData.Instance.ActiveActors[1], "奈芙蒂斯", "站住!不准你走!","Stand",0,true,5);



		yield return new WaitForSeconds (5f);
		blackFade.StartFadeIn (10f);
		yield return new WaitForSeconds (2.5f);
		animator.enabled = true;
		yield return new WaitForSeconds (27.5f);
		isAnuMove = true;
		actorAnubis.GetComponent<Animator> ().CrossFade ("walking",0.5f);
		actorAmit.GetComponent<Animator> ().CrossFade ("walk",0.5f);

		yield return new WaitForSeconds (5.0f);

		actorNaphy.GetComponent<Animator> ().CrossFade ("leg_sweep", 0.25f);
		GameCamera.ApplyCameraObject (camera_enter1, 2.5f);

		yield return new WaitForSeconds (5.0f);

		GlobalData.Instance.movie_0_active = false;
		GlobalData.Instance.movie_1_active = true;

		GlobalData.Instance.BattleMonsterId = 0;
		GlobalData.Instance.BattleMonsterNumber = 1;
		GlobalData.Instance.BattleMonsterType = 2;
		//GlobalData.Instance.PositionBeforeBattle;

		GameCamera.IsAimCharactor = true;

		WorldCameraAct.current.CreateBattleBlur ();

		GlobalData.Instance.movie_1_active = true;

		SceneManager.LoadSceneAsync (1);

	}
	
	// Update is called once per frame
	void Update () {

		if (isAnuMove) {

			actorNaphy.transform.LookAt (actorAnubis.transform);
			actorAnubis.transform.LookAt (anubisTo.transform);
			actorAmit.transform.LookAt(anubisTo.transform);

			Vector3 dir = anubisTo.transform.position - actorAnubis.transform.position;
			Vector3 movement = dir.normalized * anubisToSpeed * Time.deltaTime;
			if (movement.magnitude > dir.magnitude)
				movement = dir;
			actorAnubis.GetComponent<CharacterController> ().Move (movement);
			actorAmit.GetComponent<CharacterController> ().Move (movement);

		}



	}

}
