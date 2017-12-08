using UnityEngine;
using System.Collections;

public class ConsumableClass : BaseItemClass {

	private string bonusDescription;
	private int healHp;
	private int healMp;
	private int healSP;

	public ConsumableClass(){
	}
	public ConsumableClass(string _name,string _desc,int _id,int _rari,bool _throw,ItemTypes it) {

	}

	public int HealHP{
		get {return healHp;}
		set {
			healHp = value;
			bonusDescription += "恢復HP " + healHp + " ";
		}
	}
	public int HealMP{
		get {return healMp;}
		set {
			healMp = value;
			bonusDescription += "恢復MP " + healMp + " ";
		}
	}
	public int HealSP{
		get {return healSP;}
		set {
			healSP = value;
			bonusDescription += "恢復SP " + healSP + " ";
		}
	}
	public override string BonusDescription {
		get { return bonusDescription; }
	}
}
