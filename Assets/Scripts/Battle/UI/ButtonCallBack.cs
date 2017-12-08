using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonCallBack : MonoBehaviour , ISelectHandler , IDeselectHandler  {

	public enum ButtonWorks
	{
		MENU_SKILL,
		MENU_MAGIC,
		MENU_DEFEND,
		MENU_RUN,
		SKILL_DECIDE,
		TARGET_DECIDE,
		MAGIC_DECIDE,
		GAME_RESTART,
	}
	public enum ButtonTypes
	{
		DEFAULT,
		ORDERS,
		SKILL_SLOT,
		UNIT_TAG,
		MAGIC_SLOT,
		GAME_RESTART,
	}

	public ButtonWorks ButtonWork;
	public ButtonTypes ButtonType;

	//public delegate void OnSkillDecideEvent (int skillID);
	//public event OnSkillDecideEvent OnSkillDecide;


	// For skill use
	public GameObject UIText;
	public GameObject UISlotIcon;
	//For Dynammic button
	public int ButtonDynamicID  { get; private set;}

	public int SkillID { get; private set;}
	public void SetSkillID(int id, int bid){
		SkillID = id;
		UIText.GetComponent<Text> ().text = SkillDataBase.SkillData [id].Name;
		UISlotIcon.GetComponent<Image> ().sprite = SkillDataBase.SkillData [id].Icon;
		ButtonDynamicID = bid;
	}
	private int unit_id;

	// For Magic Use
	public int MagicID { get; private set;}
	public void SetMagicID(int id, int bid){
		MagicID = id;
		UIText.GetComponent<Text> ().text = MagicDataBase.MagicData [id].Name;
		UISlotIcon.GetComponent<Image> ().sprite = MagicDataBase.MagicData [id].Icon;
		ButtonDynamicID = bid;
	}


	// For Order button use
	public GameObject AnimImage;

	public void ButtonSubmit(){
		if (BattleUI.EVENT_PLAYER_BUTTON_SUBMIT != null) {
			BattleUI.EVENT_PLAYER_BUTTON_SUBMIT (this);
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		if (BattleUI.EVENT_PLAYER_BUTTON_AIM != null) {
			BattleUI.EVENT_PLAYER_BUTTON_AIM (this);
		}
	}

	public void OnDeselect(BaseEventData eventData)
	{
		if (BattleUI.EVENT_PLAYER_BUTTON_EXITAIM != null) {
			BattleUI.EVENT_PLAYER_BUTTON_EXITAIM (this);
		}
	}

	public int UnitID {
		get{ return unit_id; } 
		set { unit_id = value; ButtonDynamicID = value; }
	}

	void OnDestroy() {
		//print("Script was destroyed , my name is :" + gameObject.name);
	}
}
