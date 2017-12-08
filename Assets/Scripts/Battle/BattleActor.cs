using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleActor
{
	public enum ActorType {
		USER,
		NPC
	}
	public ActorType actorType;
	public enum ActorAction {
		NormalAttack,
		Skill,
		Defend,
	}
	public ActorAction actorAction;

	public int actionTarget;
	public int actionSkill;
	public int actionMagic;

	public float TimePoint;
	public float TimeSpeed;

	public int hp { get { return Data.HP; } private set {Data.HP = value ; } }
	public int mp { get { return Data.MP; } private set {Data.MP = value ; } }

	public int m_hp { get { return Data.MHP; } private set {Data.MHP = value ; } }
	public int m_mp { get { return Data.MMP; } private set {Data.MMP = value ; } }

	public int InState;
	private int bindBattle_id;
	private bool alive;

	public StandardActor Data;

	public GameObject Model;
	public GameObject ButtonTag;

	public BattleActor (int battle_id,ActorType type,int id)
	{
		TimePoint = 0f;
		InState = 0;
		actorType = type;
		bindBattle_id = battle_id;

		if (type == ActorType.USER) {
			Data = GlobalData.Instance.ActiveActors [id];
			Model = GameObject.Instantiate (GlobalData.Instance.ActiveActors [id].BattleModel);
			Model.GetComponent<BattleActorUpdate> ().data = this;

			TimeSpeed = Data.SPD;

			alive = true;

		} else {
			Data = MonsterDataBase.MonsterData [id].Clone();
			Model = GameObject.Instantiate (MonsterDataBase.MonsterData [id].BattleModel);
			Model.GetComponent<BattleActorUpdate> ().data = this;

			hp = m_hp;

			foreach (Transform child in Model.transform) {
				if (child.name == "overhead ref") {
					GameObject UItag = GameObject.Instantiate (GameResource.Prefab.UIBattleUnitTag) as GameObject;
					UItag.GetComponent<SpriteReference> ().refLocation = child.gameObject;
					UItag.GetComponent<ButtonCallBack> ().UnitID = battle_id;
					UItag.GetComponent<TargetTagData> ().Init (this.Data);

					UItag.transform.SetParent(BattleUI.current.PanelUnitTag.transform,false);
					BattleUI.current.PanelUnitTag.GetComponent<PanelReference> ().ButtonList.Add (UItag);
					UItag.name = "TAG_" + battle_id;
					ButtonTag = UItag;
					break;
				}
			}

			TimeSpeed = Data.SPD;

			alive = true;
		}
	}

	public bool IsAlive(){
		if (hp <= 0) {
			return false;
		}
		return true;
	}

	public void ResetActorSpeedToPrepare(){
		TimeSpeed = GetPrepareSpeed ();
	}

	public float GetPrepareSpeed(){
		return Data.SPD;
	}

	public void Kill(BattleActor killer){
		if (ButtonTag != null)
			ButtonTag.GetComponent<TargetTagData> ().Break ();
		
		Model.GetComponent<BattleActorUpdate> ().Dead (actorType);
		BattleMain.current.UnitsIcon [bindBattle_id].SetActive (false);
		CheckWinLose(killer);

		if (actorType == ActorType.NPC) {
			BattleMain.current.expGet += Data.EXP;
		}
	}

	public void Heal(BattleActor src, int amount){
		amount = Data.Heal (amount);
		if (BattleMain.EVENT_ANYUNIT_STATUS_UPDATE != null)
			BattleMain.EVENT_ANYUNIT_STATUS_UPDATE (this,new ActionUnitArgs(){ triggerUnit = this });
		
	}

	public int Damaged(BattleActor src, int amount){
		amount = Data.Damaged (amount);
		//hp = hp - amount;

		if (BattleMain.EVENT_ANYUNIT_STATUS_UPDATE != null)
			BattleMain.EVENT_ANYUNIT_STATUS_UPDATE (this,new ActionUnitArgs(){ triggerUnit = this });

		if (hp <= 0) {
			alive = false;
			Kill (src);
		}
		return amount;
	}

	public void CheckWinLose(BattleActor killer){
		Debug.Log ("into win lose");
		int life = 0;
		for (int i = 0; i < BattleMain.current.memberUser; i++) {
			if (BattleMain.Units [i].IsAlive ()) {
				life++;
			}
		}
		if (life == 0) {
			//lose
			BattleMain.ForceStopTimePass ();
			if (BattleMain.EVENT_BATTLE_LOSE != null)
				BattleMain.EVENT_BATTLE_LOSE (this,new ActionUnitArgs(){triggerUnit = killer});
		}
		life = 0;
		for (int i = 0; i < BattleMain.current.memberEnemy; i++) {
			if (BattleMain.Units [BattleMain.current.memberUser + i].IsAlive ()) {
				life++;
			}
		}
		if (life == 0) {
			//win
			Debug.Log ("Win!!");
			BattleMain.ForceStopTimePass ();
			if (BattleMain.EVENT_BATTLE_WIN != null)
				BattleMain.EVENT_BATTLE_WIN (this,new ActionUnitArgs(){triggerUnit = killer});
		}
	}
}

