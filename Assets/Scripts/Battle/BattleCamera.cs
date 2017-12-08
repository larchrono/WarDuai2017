using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour {

	public enum ViewModes
	{
		USER_PREPARE,
		USER_SET_TARGET,
		ACTOR_ATTACK,
		WAIT_READY,
		ACTOR_HIT,
		WIN_BATTLE,
		STOP,
	}

	public GameObject nowFocus;
	public GameObject nowTarget;
	public Camera targetSelectView;
	public Camera idleView;
	public GameObject idleViewLookAt;

	public ViewModes viewMode;
	public float distance;
	public float angle;
	public float hight;

	public float idleCameraDistance;

	public float rotateSpeed = 15;
	private float minDistance = 1;
	private float maxDistance = 5;

	private Vector3 targetPos;
	private Vector3 nowPos;
	private Quaternion rotationEuler;

	private float attackingCameraHight = 55;
	private float idleCameraHightMin = 35;
	private float idleCameraHightMax = 55;

	private float winHightUpSpeed = 10f;

	private float[] AngleRandom = new float[]{45f , -45f};
	private int selectedAngle;

	// Use this for initialization
	void Start () {
		distance = 3;
		angle = 0;
		hight = 35;
		rotateSpeed = 15;

		idleCameraDistance = Vector3.Distance (idleViewLookAt.transform.position, idleView.transform.position);

		viewMode = ViewModes.WAIT_READY;

		BattleUI.EVENT_PLAYER_SKILLBUTTON_CONFORM += OnSkillDecide;
		BattleUI.EVENT_UI_PLAYER_PANEL_BACK += OnTargetCancel;
		BattleMain.EVENT_ACTOR_BEGIN_ATTACK += OnActorModelStartAttack;
		BattleMain.EVENT_UNIT_ENTER_READYPOINT += OnPlayerReady;
		BattleMain.EVENT_UNIT_ENTER_ACTION_EFFECT += OnModelHitTarget;
		BattleMain.EVENT_PLATER_UNIT_SUBMIT_ORDER += OnPlayerFinishOrder;

		BattleMain.EVENT_BATTLE_WIN += OnWinBattle;
		BattleMain.EVENT_BATTLE_LOSE += OnLostBattle;
	}
	
	// Update is called once per frame
	void LateUpdate () {

		angle = (angle + rotateSpeed * Time.deltaTime) % 360.0f;

		if (viewMode == ViewModes.WAIT_READY) {

			//For rotation use
			rotationEuler = Quaternion.Euler (hight,angle,0);
			nowPos = rotationEuler * new Vector3 (0, 0, -idleCameraDistance) + idleViewLookAt.transform.position;
			idleView.transform.position = nowPos;

			Camera.main.transform.position = idleView.transform.position;
			Camera.main.transform.LookAt (idleViewLookAt.transform);

		}

		if (nowFocus != null) {
			targetPos = nowFocus.transform.position;

			if (viewMode == ViewModes.USER_PREPARE) {

				distance = 2.0f;
				hight = 55f;
				distance = Mathf.Clamp (distance,minDistance,maxDistance);

				//For rotation use
				rotationEuler = Quaternion.Euler (hight,angle,0);
				nowPos = rotationEuler * new Vector3 (0, 0, -distance) + targetPos;

				Camera.main.transform.position = nowPos;
				Camera.main.transform.LookAt (nowFocus.GetComponent<BattleActorUpdate>().RefHead.transform);
			}

			if (viewMode == ViewModes.ACTOR_ATTACK) {

				distance = Mathf.Clamp (distance,minDistance,maxDistance);

				angle = nowFocus.transform.rotation.eulerAngles.y + AngleRandom[selectedAngle];
				hight = attackingCameraHight;

				//Debug.Log ("now use Angle:" + nowFocus.transform.rotation.eulerAngles.y + " + " + AngleRandom[selectedAngle]);

				//For rotation use
				rotationEuler = Quaternion.Euler (hight,angle,0);
				nowPos = rotationEuler * new Vector3 (0, 0, -distance) + targetPos;

				Camera.main.transform.position = nowPos;
				Camera.main.transform.LookAt (nowFocus.GetComponent<BattleActorUpdate>().RefHead.transform);
			}
		}

		if (viewMode == ViewModes.WIN_BATTLE) {

			angle = (angle + rotateSpeed * Time.deltaTime) % 360.0f;

			hight = Mathf.Clamp (hight + winHightUpSpeed * Time.deltaTime, 0, 89f );

			//Debug.Log ("::"+hight);
			//For rotation use
			rotationEuler = Quaternion.Euler (hight,angle,0);
			nowPos = rotationEuler * new Vector3 (0, 0, -idleCameraDistance) + idleViewLookAt.transform.position;

			Camera.main.transform.position = nowPos;
			Camera.main.transform.LookAt (idleViewLookAt.transform);
		}
	}

	void OnSkillDecide(ButtonCallBack data){
		if (data.ButtonWork != ButtonCallBack.ButtonWorks.SKILL_DECIDE)
			return;
		
		viewMode = ViewModes.USER_SET_TARGET;
		GameCamera.ApplyCameraObject (targetSelectView, 0.25f);
	}

	void OnTargetCancel(PanelArgs args){
		if (args.PanelBeforeWork.name != "PanelUnitTag")
			return;
		viewMode = ViewModes.USER_PREPARE;
		Camera.main.transform.position = nowPos;
		Camera.main.transform.LookAt (nowFocus.transform);
	}

	void OnActorModelStartAttack(BattleActor triggerUnit,BattleActor targetUnit){
		
		viewMode = ViewModes.ACTOR_ATTACK;
		nowFocus = triggerUnit.Model;
		nowTarget = targetUnit.Model;
		selectedAngle = Random.Range (0, AngleRandom.Length);
	}

	void OnPlayerReady(object sender,ActionUnitArgs args){
		//如果進入Ready的是玩家
		if (args.triggerUnit.actorType == BattleActor.ActorType.USER) {
			viewMode = ViewModes.USER_PREPARE;
			nowFocus = args.triggerUnit.Model;

		}
	}

	void OnPlayerFinishOrder(object sender,ActionUnitArgs args){
		viewMode = ViewModes.WAIT_READY;
		hight = Random.Range (idleCameraHightMin, idleCameraHightMax);
	}

	void OnModelHitTarget(object sender,ActionUnitArgs args){
		viewMode = ViewModes.ACTOR_HIT;
		StartCoroutine (DelayResetCamera());

	}
	IEnumerator DelayResetCamera(){
		yield return new WaitForSeconds (1f);
		if (viewMode == ViewModes.ACTOR_HIT) {
			viewMode = ViewModes.WAIT_READY;
			hight = Random.Range (idleCameraHightMin, idleCameraHightMax);
		}
	}

	void OnWinBattle(object sender , ActionUnitArgs args){
		StartCoroutine (OnWinBattleWork());

	}
	IEnumerator OnWinBattleWork(){
		yield return new WaitForSeconds (1.0f);
		viewMode = ViewModes.WIN_BATTLE;
		yield return new WaitForSeconds (2.5f);
		viewMode = ViewModes.STOP;
		GameCamera.ShowBlurOnCanvas ();
		yield return new WaitForSeconds (0.1f);
		if(BattleUI.EVENT_SHOW_RESULT_CALL != null)
			BattleUI.EVENT_SHOW_RESULT_CALL (this,new ActionUnitArgs());
	}

	void OnLostBattle(object sender , ActionUnitArgs args) {
		StartCoroutine (OnLostBattleWork ());
	}
	IEnumerator OnLostBattleWork(){
		yield return new WaitForSeconds (4.0f);
		viewMode = ViewModes.STOP;
	}
}
