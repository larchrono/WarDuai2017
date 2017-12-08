using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleMain : MonoBehaviour {

	public static BattleMain current;

	//一場決鬥最多有幾個單位
	public const int MaxBattleUnitNum = 12;
	//一場決鬥最多有我方單位
	public const int MaxBattlePlayerTeamUnitNum = 4;

	public const float RunLineReadyPointAnchor = 0.76f;
	public const float RunLineIconHeight = 0.6f;
	public const float ActionCostTime = 240.0f;

	public const float MaxSkillCostTime = 5.0f;

	//about unit action
	public const float AttackRange_Normal = 4f;

	//發布Event
	//建立單位應該擁有的Event
	public static EventHandler<ActionUnitArgs> EVENT_PLAYER_UNIT_ISSUED_ORDER;
	public static EventHandler<ActionUnitArgs> EVENT_PLAYER_UNIT_ISSUED_TARGET_ORDER;
	public static EventHandler<ActionUnitArgs> EVENT_PLAYER_UNIT_BACKTO_ORIGIN;
	public static EventHandler<ActionUnitArgs> EVENT_BATTLE_WIN;
	public static EventHandler<ActionUnitArgs> EVENT_BATTLE_LOSE;
	public static EventHandler<ActionUnitArgs> EVENT_EXIT_BATTLE;
	public static EventHandler<ActionUnitArgs> EVENT_UNIT_ENTER_READYPOINT;
	public static EventHandler<ActionUnitArgs> EVENT_UNIT_ENTER_PREPARE;
	public static EventHandler<ActionUnitArgs> EVENT_UNIT_ENTER_ACTION;
	public static EventHandler<ActionUnitArgs> EVENT_UNIT_ENTER_ACTION_EFFECT;
	public static EventHandler<ActionUnitArgs> EVENT_UNIT_ENTER_ACTION_FINISHED;
	public static EventHandler<ActionUnitArgs> EVENT_PLATER_UNIT_SUBMIT_ORDER;
	public static EventHandler<ActionUnitArgs> EVENT_ANYUNIT_STATUS_UPDATE;

	public delegate void OnActorStartAttackEvent(BattleActor triggerUnit,BattleActor targetUnit);
	public static OnActorStartAttackEvent EVENT_ACTOR_BEGIN_ATTACK;

	//用來管理現在的時間線是否會前進
	private static bool timepass;
	private static bool forceStop = false;

	public static BattleActor[] Units;

	public GameObject walingLine;
	public GameObject MainCanvas;

	public GameObject[] UnitsIcon;

	//我方的初始登場地點
	public GameObject[] playerPositions;
	//敵方的初始登場地點
	public GameObject[] enemyPositions;

	//本場戰鬥有幾個人
	public int memberUser = 1;
	public int memberEnemy = 3;
	public int memberTotal;
	public int monsterTypeId = 1;

	public int expGet = 0;

	//控制時間軸變數
	float timelineNow = 0;
	float timelineReady = 760;
	float timelineAction = 1000;

	//調整單位速度
	int totalSpeed;
	float timeSpeedRate;
	public List<BattleActor> unitsInPrepare;

	bool startLeaving = false;

	void Awake() {
		EVENT_PLAYER_UNIT_ISSUED_ORDER = null;
		EVENT_PLAYER_UNIT_ISSUED_TARGET_ORDER = null;
		EVENT_PLAYER_UNIT_BACKTO_ORIGIN = null;
		EVENT_BATTLE_WIN = null;
		EVENT_BATTLE_LOSE = null;
		EVENT_EXIT_BATTLE = null;
		EVENT_UNIT_ENTER_READYPOINT = null;
		EVENT_UNIT_ENTER_PREPARE = null;
		EVENT_UNIT_ENTER_ACTION = null;
		EVENT_UNIT_ENTER_ACTION_EFFECT = null;
		EVENT_UNIT_ENTER_ACTION_FINISHED = null;
		EVENT_PLATER_UNIT_SUBMIT_ORDER = null;
		EVENT_ANYUNIT_STATUS_UPDATE = null;
		EVENT_ACTOR_BEGIN_ATTACK = null;

		current = this;
	}

	// Use this for initialization
	void Start () {

		EVENT_EXIT_BATTLE += OnExitBattle;

		memberUser = GlobalData.Instance.ActiveActors.Count;
		memberEnemy = GlobalData.Instance.BattleMonsterNumber;
		memberEnemy = GlobalData.Instance.BattleMonsterNumber;
		monsterTypeId = GlobalData.Instance.BattleMonsterType;



		expGet = 0;
		timepass = true;
		forceStop = false;

		
		//Unit 前方儲存玩家，後方儲存NPC
		memberTotal = memberUser + memberEnemy;
		totalSpeed = 0;

		Units = new BattleActor[memberUser + memberEnemy];
		UnitsIcon = new GameObject[memberUser + memberEnemy];
		unitsInPrepare = new List<BattleActor> ();
		
		// Create User actor , 撈資料不用改
		for (int i = 0; i < memberUser; i++) {
			Units [i] = new BattleActor (i,BattleActor.ActorType.USER,i);
			Units [i].Model.transform.position = playerPositions [i].transform.position;
			UnitsIcon[i] = MonoBehaviour.Instantiate (GameResource.Prefab.UIBattleRunLineIcon, Vector3.zero, Quaternion.identity) as GameObject;
			UnitsIcon[i].GetComponent<UIRunlineReference> ().SetIconImageByActor (Units [i]);
			UnitsIcon[i].transform.SetParent (walingLine.GetComponent<RectTransform>(),false);
			totalSpeed += Units [i].Data.SPD;
		}
		//Create enemy actor , 撈資料不用改
		for (int i = 0; i < memberEnemy; i++) {
			Units [memberUser + i] = new BattleActor (memberUser + i,BattleActor.ActorType.NPC, monsterTypeId);
			Units [memberUser + i].Model.transform.position = enemyPositions [i].transform.position;
			UnitsIcon[memberUser + i] = MonoBehaviour.Instantiate (GameResource.Prefab.UIBattleRunLineIcon, Vector3.zero, Quaternion.identity) as GameObject;
			UnitsIcon[memberUser + i].GetComponent<UIRunlineReference> ().SetIconImageByActor (Units[memberUser + i]);
			UnitsIcon[memberUser + i].transform.SetParent (walingLine.GetComponent<RectTransform>(),false);
			totalSpeed += Units [memberUser + i].Data.SPD;
		}

		timeSpeedRate = timelineAction/totalSpeed;
		timepass = true;
	}
	
	// Update is called once per frame
	void Update () {

		timelineNow++;

		if (timepass) {

			for(int i=0;i<memberTotal;i++){
				if (Units[i] == null || !Units[i].IsAlive())
					continue;
				
				if (unitsInPrepare.Count > 0)
					Units [i].TimePoint = Mathf.Clamp (Units [i].TimePoint + Units [i].TimeSpeed * Time.deltaTime, 0f, 1000f);
				else
					Units [i].TimePoint = Mathf.Clamp (Units [i].TimePoint + Units [i].TimeSpeed * timeSpeedRate * Time.deltaTime, 0f, 1000f);
				
				UnitsIcon [i].GetComponent<RectTransform> ().anchorMin = new Vector2 ((Units[i].TimePoint/timelineAction),RunLineIconHeight);
				UnitsIcon [i].GetComponent<RectTransform> ().anchorMax = new Vector2 ((Units[i].TimePoint/timelineAction),RunLineIconHeight);

				//Debug.Log ("player:"+Units[0].TimePoint);

				//發布Ready Event
				if (Units[i].TimePoint > timelineReady & Units[i].InState == 0) {
					Units[i].InState = 1;
					EVENT_UNIT_ENTER_READYPOINT.Invoke (this, new ActionUnitArgs (){ triggerUnit = Units[i] });
				}

				//發佈Action Event
				if (Units[i].TimePoint >= timelineAction & Units[i].InState == 1) {
					Units[i].InState = 2;
					EVENT_UNIT_ENTER_ACTION.Invoke (this, new ActionUnitArgs (){ triggerUnit = Units[i] });
				}


			}

		}
	}

	public static void StopTimePass(){
		timepass = false;
	}

	public static void ResumeTimePass(){
		if (forceStop)
			return;
		timepass = true;
	}

	public static void ForceStopTimePass(){
		forceStop = true;
		timepass = false;
	}

	void OnExitBattle(object sender,ActionUnitArgs args){

		if (monsterTypeId == 5) {
			GlobalData.Instance.movie_3_active = true;
		}

		GlobalData.Instance.goldCarry += 15;

		GlobalData.Instance.GameInState = GlobalData.GameStates.IN_WORLD;
		//Store infomation to back world
		///GlobalData.Instance.BattleMonsterId = myId;
		//GlobalData.Instance.BattleMonsterNumber = monsterNum;
		//GlobalData.Instance.BattleMonsterType = monsterType;
		//GlobalData.Instance.PositionBeforeBattle = other.gameObject.transform.position;

		//mapData.mainCamera.GetComponent<WorldCameraAct> ().CreateBattleBlur ();



		GameCamera.FadeOut (1.0f,20f);
		MusicController.current.FadeOut();

		StartCoroutine (BackToWorld ());
	}

	IEnumerator BackToWorld(){
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadSceneAsync (0);
	}
}
