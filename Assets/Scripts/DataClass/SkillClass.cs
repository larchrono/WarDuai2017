using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillClass {

	private string name;
	private string description;
	private string bonusDescription;
	public Sprite Icon {get;set;}
	private string animationName;

	private double bonusDamage;

	private double atkRate;
	private double matkRate;
	private float timeCost;

	private GemData.GemType type;
	private SkillNode shape;

	// declare is X , Y
	private Vector2 size;
	private Vector2 startAt;

	private int spCost;

	public enum SkillTargets {
		ALLY,
		ENEMY,
		NOTARGET
	}
	private SkillTargets skillTarget;


	//skill cost table

	public string Name{
		get {return name;}
		set {name = value;}
	}
	public string Description{
		get {return description;}
		set {description = value;}
	}
	public string BonusDescription {
		get { return bonusDescription; }
	}
	public double BonusDamage{
		get {return bonusDamage;}
		set {
			bonusDamage = value;
			bonusDescription += "額外傷害:" + bonusDamage + " ";
		}
	}
	public double AtkRate{
		get {return atkRate;}
		set {
			atkRate = value;
			bonusDescription += "攻擊倍率:" + System.String.Format("{0:P0}", atkRate) + " ";
		}
	}
	public double MatkRate{
		get {return matkRate;}
		set {
			matkRate = value;
			bonusDescription += "魔法攻擊倍率:" + System.String.Format("{0:P0}", matkRate) + " ";
		}
	}
	public int SpCost{
		get {return spCost;}
		set {spCost = value;}
	}
	public string AnimationName {
		get {return animationName;}
		set {animationName = value;}
	}
	public float TimeCost{
		get {return timeCost;}
		set {timeCost = value;}
	}
	public SkillTargets SkillTarget{
		get {return skillTarget;}
		set {skillTarget = value;}
	}
	public GemData.GemType Type{
		get {return type;}
		set {type = value;}
	}
	public SkillNode Shape{
		get {return shape;}
		set {shape = value;}
	}
	public Vector2 Size{
		get {return size;}
		set {size = value;}
	}
	public Vector2 StartAt{
		get {return startAt;}
		set {startAt = value;}
	}
}
