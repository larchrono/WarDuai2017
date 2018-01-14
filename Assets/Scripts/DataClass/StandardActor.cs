using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StandardActor : BasicActorClass {

	static private int defendGetMpBase = 10;

	private int hp;
	private int mp;
	private int sp;

	private int mhp;
	private int mmp;
	private int msp;

	private int atk;
	private int matk;
	private int def;
	private int spd;

	//private double critical

	//status equips
	private int hpPlus;
	private double hpRaise;

	private int mpPlus;
	private double mpRaise;

	private int spPlus;
	private double spRaise;

	private int atkPlus;
	private double atkRate;
	private double atkRaise;

	private int matkPlus;
	private double matkRate;
	private double matkRaise;

	private int defPlus;
	private double defRate;
	private double defRaise;

	private int spdPlus;
	private double spdRate;
	private double spdRaise;

	private double mp_costdown_rate;

	private int defendGetMp;

	//status battle buff
	private double buff_hp;
	private double buff_atk;
	private double buff_matk;
	private double buff_vit;
	private double buff_spd;

	private int equippedWeapon;
	private int equippedArmor;
	private int equippedShoes;
	private int equippedRing;

	private List<int> learnSkill;
	private List<int> learnMagic;

	public StandardActor(){}
	//uid to create initial main actor
	public StandardActor(int uid){

		learnSkill = new List<int> ();
		learnMagic = new List<int> ();

		// 荷魯斯 initial status
		if (uid == 1) {

			Icon = GameResource.Sprite.Icon_Horus;
			Photo = GameResource.Sprite.Photo_Horus_Stand;
			BattleIcon = GameResource.Sprite.BattleIcon_Horus;
			BattleModel = GameResource.Prefab.ModelBattleHorus;

			Photos.Add ("Angry",GameResource.Sprite.Photo_Horus_Angry);
			Photos.Add ("Cry",GameResource.Sprite.Photo_Horus_Cry);
			Photos.Add ("Smile",GameResource.Sprite.Photo_Horus_Smile_1);
			Photos.Add ("Smile_2",GameResource.Sprite.Photo_Horus_Smile_2);
			Photos.Add ("Smile_3",GameResource.Sprite.Photo_Horus_Smile_3);

			ActorName = "荷魯斯";
			ActorDescription = "";

			Level = 1;
			BaseHp = 32;
			BaseMp = 12;
			BaseSp = 24;
			BaseStr = 9;
			BaseInt = 6;
			BaseVit = 7;
			BaseAgi = 18;
			StrLvRaise = 3;
			IntLvRaise = 1;
			VitLvRaise = 2;
			AgiLvRaise = 3;
			HPLvRaise = 6;
			MPLvRaise = 3;
			//BaseAgi = 1;

			equippedWeapon = 100;
			equippedArmor = 200;
			equippedShoes = 300;

			learnSkill.Add (11);
			learnSkill.Add (12);
			learnMagic.Add (11);
			learnMagic.Add (12);
			learnMagic.Add (13);
		}
		if (uid == 2) {

			Icon = GameResource.Sprite.Icon_Nephthys;
			Photo = GameResource.Sprite.Photo_Nephthys_Stand;
			BattleIcon = GameResource.Sprite.BattleIcon_Nephthys;
			BattleModel = GameResource.Prefab.ModelBattleNephthys;

			Photos.Add ("Angry",GameResource.Sprite.Photo_Nephthys_Angry);
			Photos.Add ("Cry",GameResource.Sprite.Photo_Nephthys_Cry);
			Photos.Add ("Smile",GameResource.Sprite.Photo_Nephthys_Smile_1);
			Photos.Add ("Smile_2",GameResource.Sprite.Photo_Nephthys_Smile_2);
			Photos.Add ("Smile_3",GameResource.Sprite.Photo_Nephthys_Smile_3);
			Photos.Add ("Hate",GameResource.Sprite.Photo_Nephthys_Hate);

			ActorName = "奈芙蒂斯";
			ActorDescription = "";

			Level = 1;
			BaseHp = 22;
			BaseMp = 18;
			BaseSp = 24;
			BaseStr = 7;
			BaseInt = 8;
			BaseVit = 6;
			BaseAgi = 15;
			StrLvRaise = 1;
			IntLvRaise = 3;
			VitLvRaise = 2;
			AgiLvRaise = 2;
			HPLvRaise = 4;
			MPLvRaise = 6;
			//BaseAgi = 5;

			equippedWeapon = 101;
			equippedArmor = 201;
			equippedShoes = 300;

			learnSkill.Add (21);
			learnSkill.Add (22);
			//learnSkill.Add (23);
			learnSkill.Add (24);
			learnMagic.Add (21);
			learnMagic.Add (22);
			learnMagic.Add (23);
			learnMagic.Add (24);
		}

		UpdateActorStatus ();
		RecoverActorStatus ();
	}

	public void RecoverActorStatus(){
		hp = mhp;
		mp = mmp;
		sp = msp;
	}



	public void UpdateActorStatus(){
		
		ResetEquipmentStatus ();

		UpdateEquipmentStatus (equippedWeapon);
		UpdateEquipmentStatus (equippedArmor);
		UpdateEquipmentStatus (equippedShoes);
		UpdateEquipmentStatus (equippedRing);


		mhp = System.Convert.ToInt32 ((BaseHp + hpPlus) * (1 + hpRaise));
		mmp = System.Convert.ToInt32 ((BaseMp + mpPlus) * (1 + mpRaise));
		msp = System.Convert.ToInt32 ((BaseSp + spPlus) * (1 + spRaise));
		atk = System.Convert.ToInt32 (((BaseStr * (1 + atkRate)) + atkPlus)*(1 + atkRaise));
		matk= System.Convert.ToInt32 (((BaseInt * (1 + matkRate))+ matkPlus)*(1 + matkRaise));
		def = System.Convert.ToInt32 (((BaseVit * (1 + defRate)) + defPlus)*(1 + defRaise));
		spd = System.Convert.ToInt32 (((BaseAgi * (1 + spdRate)) + spdPlus)*(1 + spdRaise));

		hp = Mathf.Min (hp,mhp);
		mp = Mathf.Min (mp,mmp);
		sp = Mathf.Min (sp,msp);
	}

	// tools

	public void UpdateEquipmentStatus(int targetEquipmentID){
		
		if (targetEquipmentID != 0 && ItemsDataBase.ItemData [targetEquipmentID] is EquipmentClass) {
			hpPlus += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).HP_UP;
			mpPlus += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).MP_UP;
			spPlus += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).SP_UP;
			atkPlus += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).ATK_UP;
			matkPlus += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).MATK_UP;
			defPlus += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).DEF_UP;
			spdPlus += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).SPD_UP;

			atkRate += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).ATK_Rate;
			matkRate += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).MATK_Rate;
			defRate += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).DEF_Rate;
			spdRate += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).SPD_Rate;

			hpRaise += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).HP_Raise;
			mpRaise += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).MP_Raise;
			spRaise += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).SP_Raise;

			atkRaise += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).ATK_Raise;
			matkRaise += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).MATK_Raise;
			defRaise += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).DEF_Raise;
			spdRaise += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).SPD_Raise;

			mp_costdown_rate += (ItemsDataBase.ItemData [targetEquipmentID] as EquipmentClass).Mp_Costdown_Rate;
		}

	}

	public void ResetEquipmentStatus(){
		hpPlus = 0;
		mpPlus = 0;
		spPlus = 0;
		atkPlus = 0;
		matkPlus = 0;
		defPlus = 0;
		spdPlus = 0;

		atkRate = 0;
		matkRate = 0;
		defRate = 0;
		spdRate = 0;

		hpRaise = 0;
		mpRaise = 0;
		spRaise = 0;

		atkRaise = 0;
		matkRaise = 0;
		defRaise = 0;
		spdRaise = 0;
	}

	public int ConvertTypeToEquipmentPlace(EquipmentClass.EquipmentTypes type){
		if (type == EquipmentClass.EquipmentTypes.WEAPON) {
			return equippedWeapon;
		} else if (type == EquipmentClass.EquipmentTypes.ARMOR) {
			return equippedArmor;
		} else if (type == EquipmentClass.EquipmentTypes.SHOES) {
			return equippedShoes;
		} else if (type == EquipmentClass.EquipmentTypes.RING) {
			return equippedRing;
		}
		return 0;
	}

	public int Heal(int amount){
		int finalHeal = Mathf.Clamp( amount , 0 , mhp - hp );
		hp = hp + finalHeal;
		return finalHeal;
	}

	public int HealMP(int amount){
		mp = Mathf.Clamp (mp + amount, 0, mmp);
		return mp;
	}

	public int DefendWork(){
		HealMP (defendGetMpBase + defendGetMp);
		return mp;
	}

	public int Damaged(int amount){
		Debug.Log ("damage:" + amount + " , def :" + def);
		int finalDamage = Mathf.Clamp( amount - def , 1 , 9999 );
		hp = Mathf.Clamp (hp - finalDamage, 0 , mhp);
		return finalDamage;
	}

	public void ConsumMP(int cost){
		int finalCost = ReduceMpCost (cost);
		mp = Mathf.Clamp(mp - finalCost, 0, mmp);
	}

	public bool CanCostMp(int cost){
		int finalCost = ReduceMpCost (cost);
		if ((mp - finalCost) >= 0)
			return true;
		return false;
	}
	public int ReduceMpCost(int cost){
		return (int)(cost * Mathf.Max (0, 1 - (float)mp_costdown_rate));
	}

	public void LevelUp(int num = 1){
		Level++;
		BaseStr += StrLvRaise;
		BaseInt += IntLvRaise;
		BaseVit += VitLvRaise;
		BaseAgi += AgiLvRaise;
		BaseHp += HPLvRaise;
		BaseMp += MPLvRaise;
		UpdateActorStatus ();
		RecoverActorStatus ();
	}

	public bool AddExp(int value){
		int nowLv = Level;
		EXP += value;
		bool isLevelUp = false;
		for (int i = nowLv + 1 ; i < 100; i++) {
			if (InitEXPTable.Normal.ExpTable [i] - EXP <= 0) {
				LevelUp ();
				isLevelUp = true;
			} else
				break;
		}
		if (isLevelUp)
			return true;
		return false;
	}

	public StandardActor Clone(){
		return this.MemberwiseClone () as StandardActor;
	}

	/// <summary>
	/// 
	/// Setter & Getter
	/// 
	/// </summary>
	/// <value>The H.</value>

	public int HP {
		get {return hp;}
		set {hp = value;}
	}
	public int MP {
		get {return mp;}
		set {mp = value;}
	}
	public int SP {
		get {return sp;}
		set {sp = value;}
	}
	public int MHP {
		get {return mhp;}
		set {mhp = value;}
	}
	public int MMP {
		get {return mmp;}
		set {mmp = value;}
	}
	public int MSP {
		get {return msp;}
		set {msp = value;}
	}
	public int ATK {
		get {return atk;}
		set {atk = value;}
	}
	public int MATK {
		get {return matk;}
		set {matk = value;}
	}
	public int DEF {
		get {return def;}
		set {def = value;}
	}
	public int SPD {
		get {return spd;}
		set {spd = value;}
	}
	public int EquippedWeapon {
		get {return equippedWeapon;}
		set {equippedWeapon = value;
			UpdateActorStatus ();
		}
	}
	public int EquippedArmor {
		get {return equippedArmor;}
		set {equippedArmor = value;
			UpdateActorStatus ();
		}
	}
	public int EquippedShoes {
		get {return equippedShoes;}
		set {equippedShoes = value;
			UpdateActorStatus ();
		}
	}
	public int EquippedRing {
		get {return equippedRing;}
		set {equippedRing = value;
			UpdateActorStatus ();
		}
	}
	public List<int> LearnSkill {
		get {return learnSkill;}
		set {learnSkill = value;}
	}
	public List<int> LearnMagic {
		get {return learnMagic;}
		set {learnMagic = value;}
	}
}
