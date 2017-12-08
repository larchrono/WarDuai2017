using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DynamicButonScript : MonoBehaviour {

	public WorldMenuController menuController;

	public Text NameText;
	public Text NumText;
	public Text SPCost;
	public Text MPCost;

	private int number;
	private int linkedActorInventorySlot;
	private int linkedItemID;

	private int linkedSkillSlot;
	private int linkedSkillID;

	private int linkedMagicSlot;
	private int linkedMagicID;

	public EquipmentClass.EquipmentTypes equipmentType;

	public enum ButtonTypes {
		MAGIC,
		ITEM,
		ITEMLABEL_CONSUMABLE,
		ITEMLABEL_EQUIP,
		ITEMLABEL_PRECIOUS,
		EQUIPMENT,
		SKILL,
		EQUIPPED
	}
	public ButtonTypes buttonType;

	// Use this for initialization
	void Awake () {
		if(NameText != null)NameText.text = "Empty";
		if(NumText != null)NumText.text = "0";
	}

	public int Number {
		get{ return number;}
		set{ 
			number = value;
			if(NumText != null) NumText.text = "" + number;
		}
	}

	public void SetButtonCanUse(bool true_false){
		if (true_false) {
			gameObject.SetActive (true);
		} else {
			linkedItemID = 0;
			linkedMagicID = 0;
			linkedSkillID = 0;
			gameObject.SetActive (false);
		}
	}

	public void SetSPText (string value) {
		if (SPCost != null)
			SPCost.text = value;
		else
			Debug.Log ("Error Assess.");
	}

	public void SetMPText (string value) {
		if (MPCost != null)
			MPCost.text = value;
		else
			Debug.Log ("Error Assess.");
	}

	public int LinkedActorInventorySlot{
		get{ return linkedActorInventorySlot;}
		set{ linkedActorInventorySlot = value;}
	}

	public int LinkedItemID{
		get{ return linkedItemID;}
		set{ linkedItemID = value;}
	}
	public int LinkedSkillSlot{
		get{ return linkedSkillSlot;}
		set{ linkedSkillSlot = value;}
	}

	public int LinkedSkillID{
		get{ return linkedSkillID;}
		set{ linkedSkillID = value;}
	}
	public int LinkedMagicSlot{
		get{ return linkedMagicSlot;}
		set{ linkedMagicSlot = value;}
	}
	public int LinkedMagicID{
		get{ return linkedMagicID;}
		set{ linkedMagicID = value;}
	}
}
