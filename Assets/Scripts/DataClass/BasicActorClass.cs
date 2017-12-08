using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class BasicActorClass {

	public BasicActorClass(){
		Photos = new Dictionary<string, Sprite> ();
		Icon = GameResource.Sprite.Icon_Default;
		Photo = GameResource.Sprite.Icon_Default;
	}

	//Basic value

	private string actorName;
	private string actorDescription;

	//UI
	public Sprite Icon {get;set;}
	public Sprite Photo {get;set;}
	public Dictionary<string, Sprite> Photos {get;set;}
	public Sprite BattleIcon {get;set;}

	//3D
	public GameObject BattleModel {get;set;}

	//new Dictionary<string, Customer>();

	//Stats
	private int level;
	private int baseHp;
	private int baseMp;
	private int baseSp;
	private int baseStr;
	private int baseInt;
	private int baseVit;
	private int baseAgi;
	private int exp;

	private int strLvRaise;
	private int intLvRaise;
	private int vitLvRaise;
	private int agiLvRaise;
	private int hpLvRaise;
	private int mpLvRaise;

	public string ActorName{
		get {return actorName;}
		set {actorName = value;}
	}

	public string ActorDescription{
		get {return actorDescription;}
		set {actorDescription = value;}
	}

	public int Level {
		get {return level;}
		set {level = value;}
	}

	public int BaseHp {
		get {return baseHp;}
		set {baseHp = value;}
	}

	public int BaseMp {
		get {return baseMp;}
		set {baseMp = value;}
	}

	public int BaseSp {
		get {return baseSp;}
		set {baseSp = value;}
	}

	public int BaseStr {
		get {return baseStr;}
		set {baseStr = value;}
	}

	public int BaseInt {
		get {return baseInt;}
		set {baseInt = value;}
	}

	public int BaseVit {
		get {return baseVit;}
		set {baseVit = value;}
	}

	public int BaseAgi {
		get {return baseAgi;}
		set {baseAgi = value;}
	}

	public int StrLvRaise {
		get {return strLvRaise;}
		set {strLvRaise = value;}
	}

	public int IntLvRaise {
		get {return intLvRaise;}
		set {intLvRaise = value;}
	}

	public int VitLvRaise {
		get {return vitLvRaise;}
		set {vitLvRaise = value;}
	}

	public int AgiLvRaise {
		get {return agiLvRaise;}
		set {agiLvRaise = value;}
	}

	public int HPLvRaise {
		get {return hpLvRaise;}
		set {hpLvRaise = value;}
	}

	public int MPLvRaise {
		get {return mpLvRaise;}
		set {mpLvRaise = value;}
	}

	public int EXP {
		get {return exp;}
		set {exp = value;}
	}
}
