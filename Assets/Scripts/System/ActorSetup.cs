using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSetup : MonoBehaviour {

	public static void SaveToMemory(DataMemory memo){
		if (memo != null)
			SaveActorDataTo (memo);
	}

	public static void SaveToMemory(){
		if (SaveLoad.nowWorkingMemory != null)
			SaveActorDataTo (SaveLoad.nowWorkingMemory);
	}

	public static void SetupFromMemory(DataMemory memo){
		for (int i = 0; i < memo.actors.Count ; i++) {
			SetupActorData (GlobalData.Instance.Actors [i], memo.actors[i]);
		}
	}

	public static void SetupFromMemory(){
		DataMemory memo = SaveLoad.nowWorkingMemory;
		for (int i = 0; i < memo.actors.Count ; i++) {
			SetupActorData (GlobalData.Instance.Actors [i], memo.actors[i]);
		}
	}

	private static void SaveActorDataTo(DataMemory memo){
		memo.actors.Clear ();

		int actorNum = GlobalData.Instance.Actors.Count;
		for (int i = 0; i < actorNum; i++) {
			DataMemory.ActorData actor = new DataMemory.ActorData ();
			StandardActor target = GlobalData.Instance.Actors [i];

			if (target != null) {
				actor.level = target.Level;
				actor.baseHp = target.BaseHp;
				actor.baseMp = target.BaseMp;
				actor.baseSp = target.BaseSp;
				actor.baseStr = target.BaseStr;
				actor.baseInt = target.BaseInt;
				actor.baseVit = target.BaseVit;
				actor.baseAgi = target.BaseAgi;
				actor.exp = target.EXP;

				actor.equippedWeapon = target.EquippedWeapon;
				actor.equippedArmor = target.EquippedArmor;
				actor.equippedShoes = target.EquippedShoes;
				actor.equippedRing = target.EquippedRing;

				actor.learnMagic = target.LearnMagic;
				actor.learnSkill = target.LearnSkill;

				actor.hp = target.HP;
				actor.mp = target.MP;
				actor.sp = target.SP;
			}
			memo.actors.Add(actor);
		}
	}

	private static void SetupActorData(StandardActor actor,DataMemory.ActorData data){
		if (actor != null && data != null) {

			actor.Level = data.level;
			actor.BaseHp = data.baseHp;
			actor.BaseMp = data.baseMp;
			actor.BaseSp = data.baseSp;
			actor.BaseStr = data.baseStr;
			actor.BaseInt = data.baseInt;
			actor.BaseVit = data.baseVit;
			actor.BaseAgi = data.baseAgi;
			actor.EXP = data.exp;

			actor.HP = data.hp;
			actor.MP = data.mp;
			actor.SP = data.sp;

			actor.EquippedWeapon = data.equippedWeapon;
			actor.EquippedArmor = data.equippedArmor;
			actor.EquippedShoes = data.equippedShoes;
			actor.EquippedRing = data.equippedRing;

			actor.LearnMagic = data.learnMagic;
			actor.LearnSkill = data.learnSkill;

			actor.UpdateActorStatus ();
		}
	}
}
