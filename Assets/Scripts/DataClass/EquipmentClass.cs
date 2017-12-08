using UnityEngine;
using System.Collections;

public class EquipmentClass : BaseItemClass {

	private string bonusDescription;

	// basic point increase
	private int hp_up;
	private int mp_up;
	private int sp_up;

	private int atk_up;
	private int matk_up;
	private int def_up;
	private int spd_up;

	// equipment rate
	private double atk_rate;
	private double matk_rate;
	private double def_rate;
	private double spd_rate;

	//equipment raise
	private double hp_raise;
	private double mp_raise;
	private double sp_raise;
	private double atk_raise;
	private double matk_raise;
	private double def_raise;
	private double spd_raise;

	private double mp_costdown_rate;

	private bool isEquiped;

	public enum EquipmentTypes {
		WEAPON,
		ARMOR,
		SHOES,
		RING,
		EMPTY
	}
	private EquipmentTypes equipmentType;

	public EquipmentClass(){
		
	}

	public EquipmentClass(string _name,string _desc,int _id,int _rari,bool _throw,ItemTypes it) {
		Name = _name;
		Description = _desc;
		ItemID = _id;
		Rarity = _rari;
		Throwable = _throw;
		ItemType = it;
	}


	public int ATK_UP{
		get {return atk_up;}
		set {
			atk_up = value;
			bonusDescription += "攻擊+" + atk_up + " ";
		}
	}
	public int MATK_UP{
		get {return matk_up;}
		set {
			matk_up = value;
			bonusDescription += "魔力+" + matk_up + " ";
		}
	}
	public int DEF_UP{
		get {return def_up;}
		set {
			def_up = value;
			bonusDescription += "防禦+" + def_up + " ";
		}
	}
	public int SPD_UP{
		get {return spd_up;}
		set {
			spd_up = value;
			bonusDescription += "行動" + NumberSign(spd_up) + " ";
		}
	}
	public int HP_UP{
		get {return hp_up;}
		set {
			hp_up = value;
			bonusDescription += "MHP+" + hp_up + " ";
		}
	}
	public int MP_UP{
		get {return mp_up;}
		set {
			mp_up = value;
			bonusDescription += "MMP+" + mp_up + " ";
		}
	}
	public int SP_UP{
		get {return sp_up;}
		set {
			sp_up = value;
			bonusDescription += "MSP+" + sp_up + " ";
		}
	}
	public double ATK_Rate{
		get {return atk_rate;}
		set {atk_rate = value;}
	}
	public double MATK_Rate{
		get {return matk_rate;}
		set {matk_rate = value;}
	}
	public double DEF_Rate{
		get {return def_rate;}
		set {def_rate = value;}
	}
	public double SPD_Rate{
		get {return spd_rate;}
		set {spd_rate = value;}
	}
	public double HP_Raise{
		get {return hp_raise;}
		set {
			hp_raise = value;
			bonusDescription += "MHP+" + System.String.Format("{0:P0}", hp_raise) + " ";
		}
	}
	public double MP_Raise{
		get {return mp_raise;}
		set {
			mp_raise = value;
			bonusDescription += "MMP+" + System.String.Format("{0:P0}", mp_raise) + " ";
		}
	}
	public double SP_Raise{
		get {return sp_raise;}
		set {
			sp_raise = value;
			bonusDescription += "MSP+" + System.String.Format("{0:P0}", sp_raise) + " ";
		}
	}
	public double ATK_Raise{
		get {return atk_raise;}
		set {
			atk_raise = value;
			bonusDescription += "攻擊+" + System.String.Format("{0:P0}", atk_raise) + " ";
		}
	}
	public double MATK_Raise{
		get {return matk_raise;}
		set {
			matk_raise = value;
			bonusDescription += "魔力+" + System.String.Format("{0:P0}", matk_raise) + " ";
		}
	}
	public double DEF_Raise{
		get {return def_raise;}
		set {
			def_raise = value;
			bonusDescription += "防禦+" + System.String.Format("{0:P0}", def_raise) + " ";
		}
	}
	public double SPD_Raise{
		get {return spd_raise;}
		set {
			spd_raise = value;
			bonusDescription += "行動+" + System.String.Format("{0:P0}", spd_raise) + " ";
		}
	}
	public double Mp_Costdown_Rate{
		get {return mp_costdown_rate;}
		set {
			mp_costdown_rate = value;
			bonusDescription += "MP消耗減少" + System.String.Format("{0:P0}", mp_costdown_rate) + " ";
		}
	}
	public bool IsEquiped{
		get { return isEquiped; }
		set { isEquiped = value; }
	}
	public EquipmentTypes EquipmentType{
		get {return equipmentType;}
		set {equipmentType = value;}
	}

	public override string BonusDescription {
		get { return bonusDescription; }
	}

	//
	// static function
	//
	private static string NumberSign(int number){
		if (number >= 0)
			return "+" + number;
		else
			return "" + number;
	}
	private static string NumberSignPercent(double number){
		if (number >= 0)
			return "+" + System.String.Format("{0:P0}", number);
		else
			return "" + System.String.Format("{0:P0}", number);
	}
	public static EquipmentTypes ConvertIntToType(int slot){
		if (slot == 0) {
			return EquipmentTypes.WEAPON;
		} else if (slot == 1) {
			return EquipmentTypes.ARMOR;
		} else if (slot == 2) {
			return EquipmentTypes.SHOES;
		} else if (slot == 3) {
			return EquipmentTypes.RING;
		}
		return EquipmentTypes.EMPTY;
	}
}
