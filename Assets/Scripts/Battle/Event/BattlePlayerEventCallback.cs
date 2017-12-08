using UnityEngine;
using System;
using System.Collections;

public class BattlePlayerEventCallback : MonoBehaviour {

	//事件開始
	void Start () {
		BattleMain.EVENT_UNIT_ENTER_READYPOINT += OnUnitReady;
		BattleMain.EVENT_UNIT_ENTER_ACTION += OnUnitBeginAction;
		BattleMain.EVENT_UNIT_ENTER_PREPARE += OnEnterPrepare;
		BattleMain.EVENT_UNIT_ENTER_ACTION_FINISHED += OnUnitFinishAction;
		BattleMain.EVENT_UNIT_ENTER_ACTION_EFFECT += OnUnitActionEffect;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("A")) {
			
		}
		if (Input.GetButtonDown ("B")) {
			
		}
	}

	void OnUnitReady(object sender,ActionUnitArgs args){

		//如果進入Ready的是玩家
		if (args.triggerUnit.actorType == BattleActor.ActorType.USER) {
			BattleMain.StopTimePass ();
		}

		// 如果進入Ready的是電腦
		if (args.triggerUnit.actorType == BattleActor.ActorType.NPC) {
			
			//挑選單位
			args.triggerUnit.actionSkill = 1;
			int users = BattleMain.current.memberUser;
			for (int i = 0; i < users; i++) {
				if (BattleMain.Units [i].IsAlive ()) {
					args.triggerUnit.actionTarget = i;
					break;
				}
			}

			if (BattleMain.EVENT_UNIT_ENTER_PREPARE != null)
				BattleMain.EVENT_UNIT_ENTER_PREPARE (this, args );
			
		}
	}

	void OnEnterPrepare(object sender,ActionUnitArgs args){
		BattleMain.current.unitsInPrepare.Add (args.triggerUnit);
		float costTime = Mathf.Clamp (SkillDataBase.SkillData [args.triggerUnit.actionSkill].TimeCost, 0.01f, BattleMain.MaxSkillCostTime);
		args.triggerUnit.TimeSpeed = BattleMain.ActionCostTime / costTime;

		BattleMain.ResumeTimePass();
	}

	void OnUnitBeginAction(object sender,ActionUnitArgs args){
		//if (args.triggerUnit.actorType != BattleActor.ActorType.USER)
			//return;
		if (args.triggerUnit.actionSkill == 10) {
			args.triggerUnit.Data.DefendWork();

			if (BattleMain.EVENT_ANYUNIT_STATUS_UPDATE != null)
				BattleMain.EVENT_ANYUNIT_STATUS_UPDATE (this,args);

			args.triggerUnit.InState = 0;
			args.triggerUnit.TimePoint = 0;
			args.triggerUnit.ResetActorSpeedToPrepare ();
			BattleMain.current.unitsInPrepare.Remove (args.triggerUnit);

			return;
		}
		print("Prepare skill :" + SkillDataBase.SkillData[args.triggerUnit.actionSkill].Name);
		string animationName = SkillDataBase.SkillData[args.triggerUnit.actionSkill].AnimationName;
		args.triggerUnit.Model.GetComponent<BattleActorUpdate> ().IssueTargetOrder (animationName,BattleMain.Units[args.triggerUnit.actionTarget]);
		BattleMain.StopTimePass ();
	}

	void OnUnitActionEffect(object sender,ActionUnitArgs args){
		//if (SkillDataBase.SkillData [args.triggerUnit.actionSkill].SkillTarget == SkillClass.SkillTargets.ENEMY) {
		int dmg = Skill.GetSkillPower (args.triggerUnit, args.triggerUnit.actionSkill);
		dmg = args.targetUnit.Damaged (args.triggerUnit, dmg);

		Vector2 pos = Camera.main.WorldToScreenPoint (args.targetUnit.Model.GetComponent<BattleActorUpdate> ().RefHead.transform.position);
		UIHelp.CreateDamageTextCanvas (GameResource.Prefab.UITextDamaged, pos, dmg);

		SoundCollect.current.Play (SoundCollect.current.SNDHit);

		GameObject ef = Instantiate (GameResource.Prefab.EffectAttackHit);
		ef.transform.position = args.targetUnit.Model.GetComponent<BattleActorUpdate> ().RefHead.transform.position;
		//}
		/*
		if (SkillDataBase.SkillData [args.triggerUnit.actionSkill].SkillTarget == SkillClass.SkillTargets.ALLY) {
			int heal = Skill.GetSkillPower (args.triggerUnit, args.triggerUnit.actionSkill);
			foreach(BattleActor act in BattleMain.Units){
				if (act.actorType == BattleActor.ActorType.USER) {
					act.Heal (args.triggerUnit,heal);
				}
			}
		}
		*/
	}

	void OnUnitFinishAction(object sender,ActionUnitArgs args){
		
		args.triggerUnit.InState = 0;
		args.triggerUnit.TimePoint = 0;
		args.triggerUnit.ResetActorSpeedToPrepare ();

		BattleMain.current.unitsInPrepare.Remove (args.triggerUnit);

		BattleMain.ResumeTimePass();
	}

	public void OrderAttack(){
		
		BattleMain.ResumeTimePass ();
		BattleMain.Units[0].TimeSpeed = BattleMain.Units[0].TimeSpeed/2.0f;

		BattleMain.Units [0].actorAction = BattleActor.ActorAction.NormalAttack;

	}
}
