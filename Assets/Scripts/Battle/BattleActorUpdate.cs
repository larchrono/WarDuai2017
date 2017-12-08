using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleActorUpdate : MonoBehaviour {

	public GameObject RefHead;
	public GameObject RefOverHead;
	public GameObject RefWeapon;

	private const float defaultSpeedWalkTime = 2f;
	private const float defaultSpeedBurstTime = 0.65f;
	private const float defaultSpeedBackTime = 0.75f;

	private float moveSpeed;
	private float burstSpeed;

	private float SpeedWalkTime = 2f;
	private float SpeedBurstTime = 0.65f;
	private float SpeedBackTime = 0.75f;

	private float animFadeTime = 0.1f;
	private float standToUnitMinRange = 1f;
	private Vector3 originPosition;

	private CharacterController control;
	private Animator anim;
	private Vector3 pointMoveTo;
	private string nowOrder;
	private float nowSpeed;
	private bool inBurst;
	private bool attackHit;

	private float startAttackRange;

	private GameObject nowUseEffect;
	private GameObject nowHitEffect;

	/* not working , for monster fade
	private float deadSpeed = 0.1f;
	private float nowAlpha = 1;
	*/

	private string orderSkillAnim;

	public BattleActor data;
	public BattleActor targetData { get; private set;}

	private float distanceWithTarget;

	//Show message for unity
	public string outputOrder;

	void DebugMsg(){
		outputOrder = nowOrder;
	}


	// Use this for initialization
	void Awake () {
		control = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		pointMoveTo = gameObject.transform.position;
		nowOrder = "stand";
	}

	void Start(){
		originPosition = gameObject.transform.position;
	}

	// fixedUpdate deal with rigidBody
	void Update () {
		
		DebugMsg ();
		float nowDistance = Vector3.Distance (pointMoveTo, transform.position);

		if (nowOrder == "attack") {
			
			//check unit is in range
			if (nowDistance > startAttackRange) {
				nowSpeed = moveSpeed;
			} else if (nowDistance > standToUnitMinRange) {
				if (!inBurst) {
					anim.CrossFade (orderSkillAnim, animFadeTime);
					burstSpeed = nowDistance / SpeedBurstTime;
					inBurst = true;
				}
				nowSpeed = burstSpeed;
			}

			//closeset move moent
			if (nowDistance > standToUnitMinRange) {
				Vector3 dir = pointMoveTo - gameObject.transform.position;
				Vector3 movement = dir.normalized * nowSpeed * Time.deltaTime;
				if (movement.magnitude > dir.magnitude)
					movement = dir;
				gameObject.GetComponent<CharacterController> ().Move (movement);
			} else {
				if (attackHit == false) {
					attackHit = true;
					targetData.Model.GetComponent<BattleActorUpdate>().anim.CrossFade ("hit", animFadeTime);
					if (nowHitEffect != null) {
						GameObject ef = Instantiate (nowHitEffect);
						ef.transform.position = targetData.Model.transform.position;
						Destroy (ef, 3);
					}

					if (BattleMain.EVENT_UNIT_ENTER_ACTION_EFFECT != null)
						BattleMain.EVENT_UNIT_ENTER_ACTION_EFFECT (this,new ActionUnitArgs(){triggerUnit = data, targetUnit = targetData});
				}
			}
		}

		if (nowOrder == "back") {
			//When Order Back , TargetPosition is Origin Point
			if (nowDistance > 0.1) {
				Vector3 dir = pointMoveTo - gameObject.transform.position;
				Vector3 movement = dir.normalized * nowSpeed * Time.deltaTime;
				if (movement.magnitude > dir.magnitude)
					movement = dir;
				gameObject.GetComponent<CharacterController> ().Move (movement);
			} else {
				nowOrder = "stand";
				if (BattleMain.EVENT_PLAYER_UNIT_BACKTO_ORIGIN != null)
					BattleMain.EVENT_PLAYER_UNIT_BACKTO_ORIGIN (this,new ActionUnitArgs(){ triggerUnit = data });
				if (BattleMain.EVENT_UNIT_ENTER_ACTION_FINISHED != null)
					BattleMain.EVENT_UNIT_ENTER_ACTION_FINISHED (this,new ActionUnitArgs(){ triggerUnit = data });
			}
		}
		if (nowOrder == "death") {
			/* not work
			nowAlpha = Mathf.Clamp (nowAlpha - deadSpeed * Time.deltaTime, 0, 1);
			//GetComponent<Renderer> ().material.color.a = 
			foreach (Transform child in transform) {
				if (child.GetComponent<Renderer> () != null){
					Debug.Log (child.GetComponent<Renderer> ().material.color.a);
					child.GetComponent<Renderer> ().material.color = new Color (1, 1, 1, nowAlpha);
				}
			}
			*/
		}
	}

	public void IssueTargetOrder(string order,BattleActor targetUnit) {

		// broadcast event
		if (BattleMain.EVENT_PLAYER_UNIT_ISSUED_ORDER != null)
			BattleMain.EVENT_PLAYER_UNIT_ISSUED_ORDER (this,new ActionUnitArgs());
		if (BattleMain.EVENT_PLAYER_UNIT_ISSUED_TARGET_ORDER != null)
			BattleMain.EVENT_PLAYER_UNIT_ISSUED_TARGET_ORDER (this,new ActionUnitArgs());

		orderSkillAnim = order;
		targetData = targetUnit;
		distanceWithTarget = Vector3.Distance (transform.position, targetUnit.Model.transform.position);

		nowHitEffect = null;

		// Skill anim select
		if (order == "skill_12") {
			nowOrder = "attack";
			startAttackRange = 20;
			SpeedWalkTime = 2;
			SpeedBurstTime = 1.5f;
			SpeedBackTime = defaultSpeedBackTime;
			if (RefWeapon != null) {
				if (nowUseEffect != null)
					Destroy (nowUseEffect);
				nowUseEffect = Instantiate (GameResource.Prefab.EffectSkill_11);
				nowUseEffect.transform.SetParent (RefWeapon.transform,false);
				nowHitEffect = GameResource.Prefab.EffectSkill_11_hit;
			}
		}

		if (order == "attack") {
			nowOrder = "attack";
			startAttackRange = BattleMain.AttackRange_Normal;
			SpeedWalkTime = defaultSpeedWalkTime;
			SpeedBurstTime = defaultSpeedBurstTime;
			SpeedBackTime = defaultSpeedBackTime;

			if(anim.GetBehaviour<attackData> () != null){
				startAttackRange = anim.GetBehaviour<attackData> ().attackRange;
				SpeedWalkTime = anim.GetBehaviour<attackData> ().walkTime;
				SpeedBurstTime = anim.GetBehaviour<attackData> ().burstTime;
				SpeedBackTime = anim.GetBehaviour<attackData> ().backTime;
				standToUnitMinRange = anim.GetBehaviour<attackData> ().standToUnitMinRange;
			}
		}

		if (order == "skill_22") {
			nowOrder = "stand";
			anim.CrossFade (orderSkillAnim,animFadeTime);
			nowHitEffect = GameResource.Prefab.EffectSkill_22_hit;
			StartCoroutine (SpellEffect(targetUnit));
		}

		if (order == "skill_23") {
			nowOrder = "stand";
			anim.CrossFade (orderSkillAnim,animFadeTime);
			nowHitEffect = GameResource.Prefab.EffectSkill_23_hit;
			StartCoroutine (SpellEffect(targetUnit));
		}

		if (order == "skill_24") {
			nowOrder = "stand";
			anim.CrossFade (orderSkillAnim,animFadeTime);
			nowHitEffect = GameResource.Prefab.EffectSkill_24_hit;
			StartCoroutine (SpellEffect(targetUnit));
		}

		if (nowOrder == "attack") {
			pointMoveTo = targetUnit.Model.GetComponent<BattleActorUpdate>().originPosition;
			pointMoveTo.y = 0;

			moveSpeed = distanceWithTarget / SpeedWalkTime ;
				
			anim.CrossFade ("walk",animFadeTime);
			inBurst = false;
			attackHit = false;
			transform.LookAt (targetUnit.Model.transform);

			if (BattleMain.EVENT_ACTOR_BEGIN_ATTACK != null)
				BattleMain.EVENT_ACTOR_BEGIN_ATTACK (data,targetUnit);
		}
	}

	//Call By Animation Script
	public void IssueImmediateOrder(string order){


		nowOrder = order;

		if (order == "stop") {
			pointMoveTo = transform.position;
			anim.CrossFade ("stand", animFadeTime);
		}
		if (order == "back") {
			pointMoveTo = originPosition;
			if (nowUseEffect != null){
				nowUseEffect.GetComponent<ParticleSystem> ().Stop (true, ParticleSystemStopBehavior.StopEmittingAndClear);
				Destroy (nowUseEffect,1f);
			}
			//Animation is call from Animator script
			//this method is call from Animator script
			nowSpeed = Vector3.Distance(transform.position,pointMoveTo) / SpeedBackTime;
		}

	}

	public void IssuePointOrderLoc(string order,Vector3 pos){
		nowOrder = order;


	}


	public void SetUnitPositionLoc(Vector3 pos){
		gameObject.transform.position = pos;
		gameObject.GetComponent<CharacterController> ().Move (Vector3.zero);
	}

	public void Dead(BattleActor.ActorType type){
		anim.CrossFade ("death", animFadeTime);
		// nowAlpha = 1;
		nowOrder = "death";
		if (type == BattleActor.ActorType.NPC) {
			StartCoroutine (DeadEffect ());
		}
	}

	IEnumerator DeadEffect(){
		yield return new WaitForSeconds (1.0f);
		GameObject ef = Instantiate (GameResource.Prefab.EffectDeath);
		ef.transform.position = gameObject.transform.position;
		gameObject.SetActive (false);
	}

	IEnumerator SpellEffect(BattleActor targetUnit){
		yield return new WaitForSeconds (2.0f);

		GameObject ef = Instantiate (nowHitEffect);
		ef.transform.position = targetUnit.Model.transform.position;
		ef.transform.rotation = gameObject.transform.rotation;
		Destroy (ef,3.0f);
		targetData.Model.GetComponent<BattleActorUpdate>().anim.CrossFade ("hit", animFadeTime);

		if (BattleMain.EVENT_UNIT_ENTER_ACTION_EFFECT != null)
			BattleMain.EVENT_UNIT_ENTER_ACTION_EFFECT (this,new ActionUnitArgs(){triggerUnit = data, targetUnit = targetData});

		if (BattleMain.EVENT_PLAYER_UNIT_BACKTO_ORIGIN != null)
			BattleMain.EVENT_PLAYER_UNIT_BACKTO_ORIGIN (this,new ActionUnitArgs(){ triggerUnit = data });
		if (BattleMain.EVENT_UNIT_ENTER_ACTION_FINISHED != null)
			BattleMain.EVENT_UNIT_ENTER_ACTION_FINISHED (this,new ActionUnitArgs(){ triggerUnit = data });

	}
}
