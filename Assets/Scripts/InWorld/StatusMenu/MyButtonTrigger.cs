using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MyButtonTrigger : EventTrigger {

	private WorldMenuController _menuController;

	// Use this for initialization
	void Awake () {
		_menuController = GetComponent<DynamicButonScript> ().menuController;
	}

	public override void OnDeselect(BaseEventData eventData) {
		//Debug.Log (">>"+gameObject.name + ": Im deselected");
	}
	
	public override void OnSelect (BaseEventData eventData)
	{
		//Debug.Log (">>"+gameObject.name + ": Selected");
		if (GetComponent<DynamicButonScript> ().buttonType == DynamicButonScript.ButtonTypes.ITEMLABEL_CONSUMABLE) {
			_menuController.ItemTypeButtonFocus (GlobalData.Instance.InventoryConsumable);
		}
		if (GetComponent<DynamicButonScript> ().buttonType == DynamicButonScript.ButtonTypes.ITEMLABEL_EQUIP) {
			_menuController.ItemTypeButtonFocus (GlobalData.Instance.InventoryEquipment);
		}
		if (GetComponent<DynamicButonScript> ().buttonType == DynamicButonScript.ButtonTypes.ITEMLABEL_PRECIOUS) {
			_menuController.ItemTypeButtonFocus (GlobalData.Instance.InventoryPrecious);
		}
		if (GetComponent<DynamicButonScript> ().buttonType == DynamicButonScript.ButtonTypes.ITEM) {
			_menuController.ItemNameSlotButtonForcus (GetComponent<DynamicButonScript> ());
		}

		if (GetComponent<DynamicButonScript> ().buttonType == DynamicButonScript.ButtonTypes.EQUIPMENT) {
			_menuController.EquipmentButtonFocus (GetComponent<DynamicButonScript> ());
		}

		if (GetComponent<DynamicButonScript> ().buttonType == DynamicButonScript.ButtonTypes.SKILL) {
			_menuController.SkillButtonFocus (GetComponent<DynamicButonScript> ());
		}

		if (GetComponent<DynamicButonScript> ().buttonType == DynamicButonScript.ButtonTypes.MAGIC) {
			_menuController.MagicButtonFocus (GetComponent<DynamicButonScript> ());
		}

		if (GetComponent<DynamicButonScript> ().buttonType == DynamicButonScript.ButtonTypes.EQUIPPED) {
			_menuController.EquippedButtonFocus (GetComponent<DynamicButonScript> ());
		}
	}
}
