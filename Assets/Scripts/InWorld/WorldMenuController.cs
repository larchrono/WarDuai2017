using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldMenuController : MonoBehaviour {

	public static WorldMenuController current;

	public static EventHandler<PanelArgs> EVENT_PLAYER_ENTER_MENU;
	public static EventHandler<PanelArgs> EVENT_PLAYER_EXIT_MENU;

	public static EventHandler<PanelArgs> EVENT_PLAYER_PRESS_A;
	public static EventHandler<PanelArgs> EVENT_PLAYER_PRESS_B;

	public static EventHandler<PanelArgs> EVENT_PLAYER_PRESS_L;
	public static EventHandler<PanelArgs> EVENT_PLAYER_PRESS_R;

	private GlobalData globalData;
	public AllTextLink allTextObject;

	public GameObject _debugMessage;

	public GameObject MainActorObject;
	public GameObject MainActorController;
	// Refference for UI control
	public GameObject mainCam;
	public GameObject worldUserInput;
	public GemController gemController;
	public EventSystem eventSystem;
	public GameObject mainMenu;

	public GameObject miniMap;
	public GameObject menuTip;

	//parent panel
	public GameObject mainPanel;
	public GameObject magicPanel;
	public GameObject itemPanel;
	public GameObject equipPanel;
	public GameObject skillPanel;
	public GameObject statusPanel;
	public GameObject systemPanel;

	//memory buttom
	private List<GameObject> buttonPanelHistort;
	private GameObject currentButtonPanel;
	public GameObject firstMenuButtonAim;
	public GameObject firstSlotButtonAim;
	public GameObject firstMagicUseAim;
	public GameObject firstMagicUseTargetAim;
	public GameObject firstItemTypeAim;
	public GameObject firstSaveSlotAim;
	public GameObject firstItemAim;
	public GameObject firstNowEqAim;
	public GameObject firstEqAim;
	public GameObject firstSkillAim;
	public GameObject firstStatusAim;

	//all button menus ,for set menu interactable
	public GameObject commandButtoms;
	public GameObject actorButtoms;
	public GameObject magicButtoms;
	public GameObject magicTargetButtoms;
	public GameObject itemTypeButtons;
	public GameObject SaveGameSlots;
	public GameObject itemsButtons;
	public GameObject nowEquipButtons;
	public GameObject equipmentButtons;
	public GameObject skillButtons;
	public GameObject statusButtons;

	//gold ,item
	public Text goldValue;
	public Text TimeValue;

	//element gem
	public GameObject magicGenPanel;
	public GameObject skillGemPanel;
	public GameObject skillGenUseRange;
	private List<GameObject> magicGemImageTable;
	private List<GameObject> skillGemTempShow;

	// menu effects
	private Animator anim;
	private bool isMenuShow;
	private bool menuTurnLock;

	// menu controller
	bool focusLocked;
	int focusActorSlot;
	EquipmentClass.EquipmentTypes focusType;
	private string inPanelName;
	public string willBeUseMagic;
	public string targetItemType;
	public string targetEquipment;

	//item use
	private int itemSelectSlot;

	//actor info
	private const int totalSlot = 4;

	//For sound use , let button dosen't play sound when fade menu
	public bool noSelect = false;


	void Awake(){
		EVENT_PLAYER_ENTER_MENU = null;
		EVENT_PLAYER_EXIT_MENU = null;
		EVENT_PLAYER_PRESS_A = null;
		EVENT_PLAYER_PRESS_B = null;
		EVENT_PLAYER_PRESS_L = null;
		EVENT_PLAYER_PRESS_R = null;

		current = this;
	}

	// Use this for initialization
	void Start () {
		globalData = GlobalData.Instance;

		isMenuShow = false;
		menuTurnLock = false;
		mainMenu.SetActive (false);
		buttonPanelHistort = new List<GameObject> ();
		anim = mainMenu.GetComponent<Animator> ();

		commandButtoms.AddComponent<PanelData> ().firstAim = firstMenuButtonAim;
		commandButtoms.GetComponent<PanelData> ().panelName = "Main";

		actorButtoms.AddComponent<PanelData> ().firstAim = firstSlotButtonAim;
		actorButtoms.GetComponent<PanelData> ().panelName = "";

		magicButtoms.AddComponent<PanelData> ().firstAim = firstMagicUseAim;
		magicButtoms.GetComponent<PanelData> ().panelName = "InMagic";

		magicTargetButtoms.AddComponent<PanelData> ().firstAim = firstMagicUseTargetAim;
		magicTargetButtoms.GetComponent<PanelData> ().panelName = "InMagicTarget";

		itemTypeButtons.AddComponent<PanelData> ().firstAim = firstItemTypeAim;
		itemTypeButtons.GetComponent<PanelData> ().panelName = "InItem";

		SaveGameSlots.AddComponent<PanelData> ().firstAim = firstSaveSlotAim;
		SaveGameSlots.GetComponent<PanelData>().panelName = "InSystem";

		itemsButtons.AddComponent<PanelData> ().firstAim = firstItemAim;
		itemsButtons.GetComponent<PanelData> ().panelName = "InItemList";

		nowEquipButtons.AddComponent<PanelData> ().firstAim = firstNowEqAim;
		nowEquipButtons.GetComponent<PanelData> ().panelName = "InEquip";

		equipmentButtons.AddComponent<PanelData> ().firstAim = firstEqAim;
		equipmentButtons.GetComponent<PanelData> ().panelName = "InEquipList";

		skillButtons.AddComponent<PanelData> ().firstAim = firstSkillAim;
		skillButtons.GetComponent<PanelData> ().panelName = "InSkill";

		statusButtons.AddComponent<PanelData> ().firstAim = firstStatusAim;
		statusButtons.GetComponent<PanelData> ().panelName = "InStatus";

		// Gem is Create By gem controller , attach it to global data
		globalData.InPanelGems = gemController.InPanelGems;

		if (magicGemImageTable == null) {
			magicGemImageTable = gemController.CreateImageGems (magicGenPanel);
		}
		skillGemTempShow = new List<GameObject> ();

		InactiveAllPanel ();





	}
	
	// Update is called once per frame
	void Update () {

		//Debug Msg
		_debugMessage.GetComponent<Text>().text = inPanelName;

		//Text Msg
		goldValue.text = GlobalData.Instance.goldCarry.ToString();
		TimeValue.text = string.Format("{0:00}:{1:00}:{2:00}",
			Mathf.Floor(GlobalData.Instance.playTime / 3600),//hours
			Mathf.Floor((GlobalData.Instance.playTime % 3600) / 60),//minutes
			Mathf.Floor(GlobalData.Instance.playTime % 60));//seconds

		// not in animation
		if (!menuTurnLock) {
			
			// call menu
			if (Input.GetButtonDown("Cancel")){

				if (GlobalData.Instance.GameInState == GlobalData.GameStates.IN_WORLD) {
					// for menu update data
					if (isMenuShow) {
						noSelect = true;
						SoundCollect.current.SNDMenuClose.Play ();
						MenuCancel ();
					} else {
						noSelect = true;
						SoundCollect.current.SNDMenuOpen.Play ();
						MenuShow ();
					}
				}
			}

			if (isMenuShow) {
				if (Input.GetButtonDown ("B") || Input.GetMouseButtonDown(1)) {
					MenuBack();
				}
				if (Input.GetButtonDown ("L") && !focusLocked){
					if (focusActorSlot == 0)
						focusActorSlot = globalData.ActiveActors.Count - 1;
					else
						focusActorSlot = focusActorSlot - 1;
					RefreshCurrentActorInfo ();
					if (EVENT_PLAYER_PRESS_L != null)
						EVENT_PLAYER_PRESS_L (this,new PanelArgs(){});
				}
				if (Input.GetButtonDown ("R") && !focusLocked ) {
					if (focusActorSlot == globalData.ActiveActors.Count - 1)
						focusActorSlot = 0;
					else
						focusActorSlot = focusActorSlot + 1;
					RefreshCurrentActorInfo ();
					if (EVENT_PLAYER_PRESS_R != null)
						EVENT_PLAYER_PRESS_R (this,new PanelArgs(){});
				}
				if (Input.GetButtonDown ("A") && !focusLocked) {
					if (EVENT_PLAYER_PRESS_A != null)
						EVENT_PLAYER_PRESS_A (this,new PanelArgs(){});
				}
			}
			
		}
	}

	public void MenuShow(){

		// stop world actor control
		worldUserInput.SetActive (false);

		//Save global Actor Position
		GlobalData.Instance.PositionBeforeBattle = MainActorObject.transform.position;

		//set basic panel section
		mainMenu.SetActive(true);
		mainPanel.SetActive (true);

		EnableButtoms (commandButtoms);
		DisableButtoms (actorButtoms);
		DisableButtoms (magicButtoms);
		DisableButtoms (magicTargetButtoms);
		DisableButtoms (itemTypeButtons);
		DisableButtoms (itemsButtons);
		DisableButtoms (nowEquipButtons);
		DisableButtoms (equipmentButtons);
		DisableButtoms (skillButtons);

		// active Menu and aim last selection
		StartCoroutine(MenuEffectTurn(true));
		StartCoroutine(highlightBtn (firstMenuButtonAim));

		//clear button aim memory
		buttonPanelHistort.Clear ();

		//set Now page Position
		inPanelName = "Main";

		// menu effects
		mainCam.GetComponent<WorldCameraAct> ().ShowBlur (true);
		mainCam.GetComponent<WorldCameraAct> ().GrabBlur = true;
		anim.SetTrigger ("FadeIn");

		// pause Game
		Time.timeScale = 0;

		//update all actors data
		RefreshAllMenuInfo();
		focusLocked = false;

		isMenuShow = true;

		if (EVENT_PLAYER_ENTER_MENU != null)
			EVENT_PLAYER_ENTER_MENU (this,new PanelArgs(){PanelNow = mainPanel});
	}

	public void MenuCancel(){
		// continue world actor control
		worldUserInput.SetActive (true);

		// cancel Menu and memory last aim buttom
		StartCoroutine(MenuEffectTurn(false));
		//menuButtonAim = eventSystem.currentSelectedGameObject;

		// menu effects
		mainCam.GetComponent<WorldCameraAct> ().ShowBlur (false);
		anim.SetTrigger ("FadeOut");

		// continue game
		Time.timeScale = 1;

		isMenuShow = false;

		if (EVENT_PLAYER_EXIT_MENU != null)
			EVENT_PLAYER_EXIT_MENU (this,new PanelArgs(){});
	}

	public void MenuBack(){

		//selection back

		if (inPanelName == "InMagic") {
			inPanelName = "Magic";
			magicPanel.SetActive (false);
			mainPanel.SetActive (true);
		}
		if (inPanelName == "InItem") {
			inPanelName = "Main";
			itemPanel.SetActive (false);
			mainPanel.SetActive (true);
		}
		if (inPanelName == "InSystem") {
			inPanelName = "Main";
			systemPanel.SetActive (false);
			mainPanel.SetActive (true);
		}
		if (inPanelName == "InEquip") {
			inPanelName = "Equip";
			equipPanel.SetActive (false);
			mainPanel.SetActive (true);
		}
		if (inPanelName == "InSkill") {
			inPanelName = "Skill";
			skillPanel.SetActive (false);
			mainPanel.SetActive (true);
		}
		if (inPanelName == "InStatus") {
			inPanelName = "Status";
			statusPanel.SetActive (false);
			mainPanel.SetActive (true);
		}
		if (inPanelName == "ItemUse") {
			commandButtoms.SetActive (true);
			mainPanel.SetActive (false);

			itemPanel.SetActive (true);
		}

		BackPanel();
	}

	public void MenuToMemberSelect(string btn){
		inPanelName = btn;
		FromPanelToPanel (commandButtoms,actorButtoms);
	}

	public void MenuToItem(){
		FromPanelToPanel (commandButtoms, itemTypeButtons);
		allTextObject.itemCurrentInfo.text = "";
		mainPanel.SetActive (false);
		itemPanel.SetActive (true);
	}

	public void MenuToSystem(){
		FromPanelToPanel (commandButtoms, SaveGameSlots);
		mainPanel.SetActive (false);
		systemPanel.SetActive (true);
		CheckSaveDataList ();
	}

	public void CheckSaveDataList(){
		for (int i = 0; i < 5; i++) {
			SaveLoad.memorySlot = i;
			DataMemory memo = SaveLoad.M_LoadGame ();
			MemoryGameSlotAttach slot = systemPanel.GetComponent<LoadGamePanelAttach> ().memorySlot [i].GetComponent<MemoryGameSlotAttach> ();

			//Save is null when chapter is zero . when not setup , it will be null
			if (memo.chapter != 0)
				slot.SetupMemory (memo);
		}
	}

	public void SaveSlotSelected(int slot){
		SaveTool.SaveNowGlobalToFile (slot);
		CheckSaveDataList ();
		Instantiate(GameResource.Prefab.UIMessageSaveOK).transform.SetParent(GameObject.Find ("Canvas").GetComponent<RectTransform>(),false);
		//clone.transform.SetParent (mainCanvas.GetComponent<RectTransform>(),false);
	}

	public void ActorMemberSlotSelected(int slot){

		SetPanelCurrentActor (slot);

		//2017.08.03 應該先把Panel會出現的資訊設定好，而不是等Button Callback再去設定，這樣容易使PanelToPanel出現錯誤

		if (inPanelName == "Magic") {
			magicPanel.SetActive (true);
			RefreshCurrentActorLearnedMagic ();

			FromPanelToPanel (actorButtoms,magicButtoms);
			mainPanel.SetActive (false);
		}

		if (inPanelName == "Equip") {
			//From actor slot to selected actor page
			FromPanelToPanel (actorButtoms,nowEquipButtons);
			mainPanel.SetActive (false);
			equipPanel.SetActive (true);
		}

		if (inPanelName == "Skill") {
			skillPanel.SetActive (true);
			RefreshCurrentActorLearnedSkill ();

			FromPanelToPanel (actorButtoms,skillButtons);
			mainPanel.SetActive (false);
		}

		if (inPanelName == "Status") {
			FromPanelToPanel(actorButtoms,statusButtons);
			mainPanel.SetActive (false);
			statusPanel.SetActive (true);
		}

		if (inPanelName == "ItemUse") {

			// item effects
			// slot = 施展對象

			GlobalData.Instance.ActiveActors [slot].Heal (30);
			GlobalData.Instance.InventoryConsumable.RemoveAt (itemSelectSlot);

			int itemNumber = GlobalData.Instance.InventoryConsumable.Count;
			for (int i = 0; i < GlobalData.MAX_ITEM_CARRY_NUM ; i++) {
				if (i < itemNumber) {
					int itemID = GlobalData.Instance.InventoryConsumable [i];
					allTextObject.itemSlotEachButton [i].GetComponent<DynamicButonScript> ().SetButtonCanUse (true);
					allTextObject.itemSlotEachButton [i].GetComponent<DynamicButonScript> ().NameText.text = ItemsDataBase.ItemData [itemID].Name;
					allTextObject.itemSlotEachButton [i].GetComponent<DynamicButonScript> ().LinkedItemID = itemID;
					//Debug.Log ("I have " + ItemsDataBase.ItemData [itemID].Name);
				} else {
					allTextObject.itemSlotEachButton [i].GetComponent<DynamicButonScript> ().SetButtonCanUse (false);
				}
			}

			RefreshCurrentActorInfo ();
			RefreshSlotsInfo ();

			Debug.Log ("item slot:" + slot);

			BackPanel ();
			commandButtoms.SetActive (true);
			mainPanel.SetActive (false);

			itemPanel.SetActive (true);
		}
	}

	/// <summary>
	/// Panel implement
	/// </summary>

	public void MagicSelected(){
		int magicID = EventSystem.current.currentSelectedGameObject.GetComponent<DynamicButonScript> ().LinkedMagicID;

		gemController.UseMagicToGem (globalData.ActiveActors[focusActorSlot],magicID,magicGemImageTable);


		RefreshCurrentActorInfo ();
		RefreshSlotsInfo ();

		//Debug.Log ("" + MagicDataBase.MagicData[magicID].Name + " is used");

		//willBeUseMagic = targetMg;
		//FromPanelToPanel (magicButtoms, magicTargetButtoms);
	}

	/*
	public void MagicTargetSelected(){
		BackPanel ();
		if (willBeUseMagic == "fire") {

		}
	}
	*/

	public void ItemTypeSelected(string itemType){
		targetItemType = itemType;
		FromPanelToPanel (itemTypeButtons, itemsButtons);
	}

	public void ItemSelected(int itemTableSlot){
		inPanelName = "ItemUse";
		if (targetItemType == "Consumable") {

			itemSelectSlot = itemTableSlot;

			FromPanelToPanel (itemsButtons, actorButtoms);
			itemPanel.SetActive (false);

			mainPanel.SetActive (true);
			commandButtoms.SetActive (false);
		}
	}

	public void EquipSlotSelected(string targetEq){
		targetEquipment = targetEq;
		FromPanelToPanel (nowEquipButtons, equipmentButtons);
	}

	public void EquipmentSelected(DynamicButonScript datas){
		BackPanel ();
		if (targetEquipment == "weapon") {
			globalData.InventoryEquipment.Remove(datas.LinkedItemID);
			globalData.InventoryEquipment.Add (globalData.ActiveActors [focusActorSlot].EquippedWeapon);
			globalData.ActiveActors [focusActorSlot].EquippedWeapon = datas.LinkedItemID;

			RefreshAllMenuInfo ();
		}
		if (targetEquipment == "armor") {
			//DATA:equip slot weapon change
			//DATA:item slot change

			globalData.InventoryEquipment.Remove(datas.LinkedItemID);
			if(globalData.ActiveActors [focusActorSlot].EquippedArmor != 0)
				globalData.InventoryEquipment.Add (globalData.ActiveActors [focusActorSlot].EquippedArmor);
			globalData.ActiveActors [focusActorSlot].EquippedArmor = datas.LinkedItemID;

			RefreshAllMenuInfo ();
		}
		if (targetEquipment == "shoes") {
			//DATA:equip slot weapon change
			//DATA:item slot change

			globalData.InventoryEquipment.Remove(datas.LinkedItemID);
			if(globalData.ActiveActors [focusActorSlot].EquippedShoes != 0)
				globalData.InventoryEquipment.Add (globalData.ActiveActors [focusActorSlot].EquippedShoes);
			globalData.ActiveActors [focusActorSlot].EquippedShoes = datas.LinkedItemID;

			RefreshAllMenuInfo ();
		}
		if (targetEquipment == "ring") {
			//DATA:equip slot weapon change
			//DATA:item slot change

			globalData.InventoryEquipment.Remove(datas.LinkedItemID);
			if(globalData.ActiveActors [focusActorSlot].EquippedRing != 0)
				globalData.InventoryEquipment.Add (globalData.ActiveActors [focusActorSlot].EquippedRing);
			globalData.ActiveActors [focusActorSlot].EquippedRing = datas.LinkedItemID;

			RefreshAllMenuInfo ();
		}
	}

	public void ItemNameSlotButtonForcus(DynamicButonScript obj){
		int itemID = obj.LinkedItemID;
		allTextObject.itemCurrentInfo.text = "" + ItemsDataBase.ItemData [itemID].Description + "\n" + ItemsDataBase.ItemData [itemID].BonusDescription;
	}

	public void RefreshFocusSkillInfo (DynamicButonScript obj){
		SkillClass skill = SkillDataBase.SkillData [obj.LinkedSkillID];
		allTextObject.skillCurrentInfo.text = skill.Description + "\n" + skill.BonusDescription;
		int rangeX = System.Convert.ToInt32(skill.Size.x);
		int rangeY = System.Convert.ToInt32(skill.Size.y);
		skillGenUseRange.GetComponent<RectTransform>().anchorMin = new Vector2 (0 , 1f - 0.2f * rangeY);
		skillGenUseRange.GetComponent<RectTransform>().anchorMax = new Vector2 (0.2f * rangeX , 1f);
		foreach (GameObject temp in skillGemTempShow) {
			Destroy (temp);
		}
		skillGemTempShow.Clear ();

		skillGemTempShow = gemController.CreateImageGemByNode (skillGemPanel,skill.Shape, skill.StartAt, skill.Type);

	}

	public void RefreshFocusMagicInfo(DynamicButonScript obj){
		MagicClass magic = MagicDataBase.MagicData [obj.LinkedMagicID];
		allTextObject.magicCurrentInfo.text = magic.Description;
	}

	public void RefreshFocusEquippedInfo(DynamicButonScript obj){
		int itemID = globalData.ActiveActors[focusActorSlot].ConvertTypeToEquipmentPlace(obj.equipmentType);
		allTextObject.equipmentCurrentInfo.text = ItemsDataBase.ItemData[itemID].Description + "\n" + ItemsDataBase.ItemData[itemID].BonusDescription;
	}

	public void ItemTypeButtonFocus(List<int> inventoryList){
		allTextObject.itemCurrentInfo.text = "";
		// max item number is 16 (MAX_ITEM_CARRY_NUM)
		int itemNumber = inventoryList.Count;
		for (int i = 0; i < GlobalData.MAX_ITEM_CARRY_NUM ; i++) {
			if (i < itemNumber) {
				int itemID = inventoryList [i];
				allTextObject.itemSlotEachButton [i].GetComponent<DynamicButonScript> ().SetButtonCanUse (true);
				allTextObject.itemSlotEachButton [i].GetComponent<DynamicButonScript> ().NameText.text = ItemsDataBase.ItemData [itemID].Name;
				allTextObject.itemSlotEachButton [i].GetComponent<DynamicButonScript> ().LinkedItemID = itemID;
				//Debug.Log ("I have " + ItemsDataBase.ItemData [itemID].Name);
			} else {
				allTextObject.itemSlotEachButton [i].GetComponent<DynamicButonScript> ().SetButtonCanUse (false);
			}
		}
	}

	/*
	public void ItemTypeConsumableFocus(){
		 * multiple item carry , but now is not use
		 * 
		for (int i = 0; i < itemNum; i++) {
			int itemID = globalData.Inventory [i];
			if (ItemsDataBase.ItemData [itemID].ItemType == BaseItemClass.ItemTypes.CONSUMABLE) {
				int isItemExist = itemSlotData.FindIndex (x => x == itemID);
				if (isItemExist == -1) {
					itemSlotData.Add (itemID);
					isItemExist = itemSlotData.FindIndex (x => x == itemID);
					allTextObject.itemSlotEachButton [isItemExist].gameObject.SetActive (true);
					allTextObject.itemSlotEachButton [isItemExist].GetComponent<DynamicButonScript> ().ItemName.text = ItemsDataBase.ItemData [itemID].Name;
					allTextObject.itemSlotEachButton [isItemExist].GetComponent<DynamicButonScript> ().Number++;

				} else {
					allTextObject.itemSlotEachButton [isItemExist].GetComponent<DynamicButonScript> ().Number++;
				}

				Debug.Log ("I have " + ItemsDataBase.ItemData [itemID].Name);
			}
		}
		Debug.Log ("Data Count:" + itemSlotData.Count);
	}
	*/

	public void EquipmentButtonFocus(DynamicButonScript obj){
		focusLocked = true;
		int itemID = obj.LinkedItemID;
		allTextObject.equipmentCurrentInfo.text = ItemsDataBase.ItemData[itemID].Description + "\n" + ItemsDataBase.ItemData[itemID].BonusDescription;
	}

	public void EquippedButtonFocus(DynamicButonScript obj){
		focusLocked = false;
		allTextObject.equipActorWeapon.text = ItemsDataBase.ItemData[globalData.ActiveActors [focusActorSlot].EquippedWeapon].Name;
		allTextObject.equipActorArmor.text = ItemsDataBase.ItemData[globalData.ActiveActors [focusActorSlot].EquippedArmor].Name;
		allTextObject.equipActorShoes.text = ItemsDataBase.ItemData[globalData.ActiveActors [focusActorSlot].EquippedShoes].Name;
		allTextObject.equipActorRing.text = ItemsDataBase.ItemData[globalData.ActiveActors [focusActorSlot].EquippedRing].Name;

		focusType = obj.equipmentType;
		RefreshCurrentActorCanEquipEquipment ();
		RefreshFocusEquippedInfo (obj);
	}

	public void SkillButtonFocus(DynamicButonScript obj){
		//問題點,先於panel To panel時將button設定為1，之後從Button Aim callBack呼叫此處，而此處的作法是先刷新技能資料Line 513 , 緊接著讀取按鈕1的資料。因為按鈕1已經被513清空了，因此發生null
		//2017.08.04 已解決
		RefreshFocusSkillInfo (obj);
	}

	public void MagicButtonFocus(DynamicButonScript obj){
		RefreshFocusMagicInfo (obj);
	}

	public void RefreshCurrentActorLearnedMagic(){
		for (int i = 0; i < GlobalData.MAX_MAGIC_NUM; i++) {
			if (i < globalData.ActiveActors [focusActorSlot].LearnMagic.Count) {
				int magicID = globalData.ActiveActors [focusActorSlot].LearnMagic [i];
				allTextObject.magicSlotEachButton [i].GetComponent<DynamicButonScript> ().SetButtonCanUse (true);
				allTextObject.magicSlotEachButton [i].GetComponent<DynamicButonScript> ().NameText.text = MagicDataBase.MagicData [magicID].Name;
				allTextObject.magicSlotEachButton [i].GetComponent<DynamicButonScript> ().SetMPText (MagicDataBase.MagicData [magicID].MpCost.ToString());
				allTextObject.magicSlotEachButton [i].GetComponent<DynamicButonScript> ().LinkedMagicID = magicID;
				allTextObject.magicSlotEachButton [i].GetComponent<DynamicButonScript> ().LinkedMagicSlot = i;
			} else {
				//inactive 可以使PanelToPanel系統中不Aim目標，所以不需修改魔法數不同時的Aim
				allTextObject.magicSlotEachButton [i].GetComponent<DynamicButonScript> ().SetButtonCanUse (false);

				/* 測試Aim用
				if(magicButtoms.GetComponent<PanelData> ().memoryAim != null)
					Debug.Log ("Name A:" + allTextObject.magicSlotEachButton [i].gameObject.name + " , name B:" + magicButtoms.GetComponent<PanelData> ().memoryAim.name);
				if (allTextObject.magicSlotEachButton [i].gameObject == magicButtoms.GetComponent<PanelData> ().memoryAim)
					Debug.Log ("<find exit button ID>:" + i);
				//magicButtoms.GetComponent<PanelData> ().memoryAim = magicButtoms.GetComponent<PanelData> ().firstAim;
				*/
			}
		}
	}


	public void RefreshCurrentActorLearnedSkill(){
		for (int i = 0; i < GlobalData.MAX_SKILL_NUM; i++) {
			if (i < globalData.ActiveActors [focusActorSlot].LearnSkill.Count) {
				int skillID = globalData.ActiveActors [focusActorSlot].LearnSkill [i];
				allTextObject.skillSlotEachButton [i].GetComponent<DynamicButonScript> ().SetButtonCanUse (true);
				allTextObject.skillSlotEachButton [i].GetComponent<DynamicButonScript> ().NameText.text = SkillDataBase.SkillData [skillID].Name;
				allTextObject.skillSlotEachButton [i].GetComponent<DynamicButonScript> ().SetSPText (SkillDataBase.SkillData [skillID].SpCost.ToString());
				allTextObject.skillSlotEachButton [i].GetComponent<DynamicButonScript> ().LinkedSkillID = skillID;
				allTextObject.skillSlotEachButton [i].GetComponent<DynamicButonScript> ().LinkedSkillSlot = i;
			} else {
				//inactive 可以使PanelToPanel系統中不Aim目標，所以不需修改魔法數不同時的Aim
				allTextObject.skillSlotEachButton [i].GetComponent<DynamicButonScript> ().SetButtonCanUse (false);
			}
		}
	}

	public void RefreshCurrentActorCanEquipEquipment(){

		// max item number is 16 (MAX_ITEM_CARRY_NUM)
		int itemID = 0;
		int slotActiveNum = 0;
		int itemNum = globalData.InventoryEquipment.Count;
		for (int i = 0; i < GlobalData.MAX_EQUIPMENT_NUM ; i++) {
			allTextObject.equipmentSlotEachButton [i].GetComponent<DynamicButonScript> ().SetButtonCanUse (false);

			if (i < itemNum) {
				itemID = globalData.InventoryEquipment [i];
				if ((ItemsDataBase.ItemData [itemID] as EquipmentClass).EquipmentType == focusType) {
					allTextObject.equipmentSlotEachButton [slotActiveNum].GetComponent<DynamicButonScript> ().SetButtonCanUse (true);
					allTextObject.equipmentSlotEachButton [slotActiveNum].GetComponent<DynamicButonScript> ().NameText.text = ItemsDataBase.ItemData [itemID].Name;
					allTextObject.equipmentSlotEachButton [slotActiveNum].GetComponent<DynamicButonScript> ().LinkedActorInventorySlot = i;
					allTextObject.equipmentSlotEachButton [slotActiveNum].GetComponent<DynamicButonScript> ().LinkedItemID = itemID;
					slotActiveNum++;
					//Debug.Log ("I have " + ItemsDataBase.ItemData [itemID].Name);
				}
			}
		}
	}

	///////////////
	//work function
	///////////////

	public void SetPanelCurrentActor(int actorSlot){
		focusActorSlot = actorSlot;
		RefreshCurrentActorInfo ();
	}

	public void RefreshCurrentActorInfo (){
		StandardActor tempActor = globalData.ActiveActors [focusActorSlot];

		// Magic Page
		allTextObject.magicUserIcon.sprite = tempActor.Icon;
		allTextObject.magicUserName.text = tempActor.ActorName;
		allTextObject.magicUserMp.text = tempActor.MP.ToString();

		//Equip Page
		allTextObject.equipActorIcon.sprite = tempActor.Icon;
		allTextObject.equipActorName.text = tempActor.ActorName;
		allTextObject.equipActorATK.text = tempActor.ATK.ToString();
		allTextObject.equipActorMATK.text = tempActor.MATK.ToString();
		allTextObject.equipActorDEF.text = tempActor.DEF.ToString();
		allTextObject.equipActorSPD.text = tempActor.SPD.ToString();
		allTextObject.equipActorWeapon.text = ItemsDataBase.ItemData [tempActor.EquippedWeapon].Name;
		allTextObject.equipActorArmor.text = ItemsDataBase.ItemData [tempActor.EquippedArmor].Name;
		allTextObject.equipActorShoes.text = ItemsDataBase.ItemData [tempActor.EquippedShoes].Name;
		allTextObject.equipActorRing.text = ItemsDataBase.ItemData [tempActor.EquippedRing].Name;

		//Skill Page
		allTextObject.skillActorIcon.sprite = tempActor.Icon;
		allTextObject.skillActorName.text = tempActor.ActorName;
		allTextObject.skillActorMSP.text = tempActor.MSP.ToString ();

		//Status Page
		allTextObject.infoActorIcon.sprite = tempActor.Icon;
		allTextObject.infoActorName.text = tempActor.ActorName;
		allTextObject.infoActorLv.text = tempActor.Level.ToString();
		allTextObject.infoActorHP.text = tempActor.HP.ToString();
		allTextObject.infoActorMP.text = tempActor.MP.ToString();
		allTextObject.infoActorSP.text = tempActor.SP.ToString();
		allTextObject.infoActorMHP.text = tempActor.MHP.ToString();
		allTextObject.infoActorMMP.text = tempActor.MMP.ToString();
		allTextObject.infoActorMSP.text = tempActor.MSP.ToString();
		allTextObject.infoActorSTR.text = tempActor.BaseStr.ToString();
		allTextObject.infoActorINT.text = tempActor.BaseInt.ToString();
		allTextObject.infoActorVIT.text = tempActor.BaseVit.ToString();
		allTextObject.infoActorAGI.text = tempActor.BaseAgi.ToString();
		allTextObject.infoActorATK.text = tempActor.ATK.ToString ();
		allTextObject.infoActorMATK.text = tempActor.MATK.ToString ();
		allTextObject.infoActorDEF.text = tempActor.DEF.ToString ();
		allTextObject.infoActorSPD.text = tempActor.SPD.ToString ();
		allTextObject.infoActorTotalEXP.text = tempActor.EXP.ToString();
		allTextObject.infoActorWeapon.text = ItemsDataBase.ItemData [tempActor.EquippedWeapon].Name;
		allTextObject.infoActorArmor.text = ItemsDataBase.ItemData [tempActor.EquippedArmor].Name;
		allTextObject.infoActorShoes.text = ItemsDataBase.ItemData [tempActor.EquippedShoes].Name;
		allTextObject.infoActorRing.text = ItemsDataBase.ItemData [tempActor.EquippedRing].Name;
		//allTextObject.infoActorNextEXP.text = (globalData.EXPTable [tempActor.Level + 1] - tempActor.EXP).ToString ();
		allTextObject.infoActorNextEXP.text = EXPTable.Normal.GetNextNeedExp(tempActor).ToString ();

		//Button connection , so need to deal with focus problem
		if (inPanelName == "InSkill") {
			RefreshCurrentActorLearnedSkill ();
			if (!eventSystem.currentSelectedGameObject.activeSelf) {
				StartCoroutine (highlightBtn (skillButtons.GetComponent<PanelData> ().firstAim));
				RefreshFocusSkillInfo (skillButtons.GetComponent<PanelData> ().firstAim.GetComponent<DynamicButonScript> ());
				//int skillID = skillButtons.GetComponent<PanelData> ().firstAim.GetComponent<DynamicButonScript> ().LinkedSkillID;
				//allTextObject.skillCurrentInfo.text = SkillDataBase.SkillData [skillID].Description + "\n" + SkillDataBase.SkillData [skillID].BonusDescription;;
				//Debug.Log ("SkillID:" + skillID + " , description : " + SkillDataBase.SkillData [skillID].Description);
			} else {
				RefreshFocusSkillInfo (eventSystem.currentSelectedGameObject.GetComponent<DynamicButonScript> ());
				//int skillID = eventSystem.currentSelectedGameObject.GetComponent<DynamicButonScript> ().LinkedSkillID;
				//allTextObject.skillCurrentInfo.text = SkillDataBase.SkillData [skillID].Description + "\n" + SkillDataBase.SkillData [skillID].BonusDescription;;
			}
		}

		if (inPanelName == "InMagic") {
			RefreshCurrentActorLearnedMagic ();
			if (!eventSystem.currentSelectedGameObject.activeSelf) {
				StartCoroutine (highlightBtn (magicButtoms.GetComponent<PanelData> ().firstAim));
				RefreshFocusMagicInfo (magicButtoms.GetComponent<PanelData> ().firstAim.GetComponent<DynamicButonScript> ());
			} else {
				RefreshFocusMagicInfo (eventSystem.currentSelectedGameObject.GetComponent<DynamicButonScript> ());
			}
		}

		if (inPanelName == "InEquip") {
			RefreshCurrentActorCanEquipEquipment ();
			RefreshFocusEquippedInfo (eventSystem.currentSelectedGameObject.GetComponent<DynamicButonScript> ());
		}
	}

	public void RefreshAllMenuInfo(){
		RefreshCurrentActorInfo ();
		RefreshSlotsInfo ();
	}

	public void RefreshSlotsInfo(){
		int activeActorNumber = globalData.ActiveActors.Count;
		//Debug.Log ("Start , total active slot " + activeActorNumber);

		for (int i = 0; i < totalSlot; i++) {
			//Debug.Log ("in loop" + i);

			if (i < activeActorNumber) {
				StandardActor tempActor = globalData.ActiveActors [i];

				allTextObject.statsActor [i].statsSlotPanel.alpha = 1;
				allTextObject.statsActor [i].statsSlotPanel.interactable = true;
				allTextObject.statsActor [i].magicTargetSlotPanel.alpha = 1;
				allTextObject.statsActor [i].magicTargetSlotPanel.interactable = true;

				allTextObject.statsActor [i].statsIcon.sprite = tempActor.Icon;
				allTextObject.statsActor [i].statsName.text = tempActor.ActorName;
				allTextObject.statsActor [i].statsLv.text = tempActor.Level.ToString();
				allTextObject.statsActor [i].statsHP.text = tempActor.HP.ToString();
				allTextObject.statsActor [i].statsMp.text = tempActor.MP.ToString();
				allTextObject.statsActor [i].statsSp.text = tempActor.SP.ToString();
				allTextObject.statsActor [i].statsMHP.text = tempActor.MHP.ToString();
				allTextObject.statsActor [i].statsMMp.text = tempActor.MMP.ToString();
				allTextObject.statsActor [i].statsMSp.text = tempActor.MSP.ToString();

				allTextObject.statsActor [i].magicTargetIcon.sprite = tempActor.Icon;
				allTextObject.statsActor [i].magicTargetName.text = tempActor.ActorName.ToString ();

			} else {
				allTextObject.statsActor [i].statsSlotPanel.alpha = 0;
				allTextObject.statsActor [i].statsSlotPanel.interactable = false;
				allTextObject.statsActor [i].magicTargetSlotPanel.alpha = 0;
				allTextObject.statsActor [i].magicTargetSlotPanel.interactable = false;
			}
		}


	}

	public void BackPanel(){
		if (buttonPanelHistort.Count > 0) {
			if (EVENT_PLAYER_PRESS_B != null)
				EVENT_PLAYER_PRESS_B (this,new PanelArgs(){PanelNow = currentButtonPanel,PanelGoTo = buttonPanelHistort [buttonPanelHistort.Count - 1]});
			
			if(currentButtonPanel.GetComponent<PanelData>() != null)
				currentButtonPanel.GetComponent<PanelData> ().memoryAim = eventSystem.currentSelectedGameObject;
			
			PanelData targetComponemt = buttonPanelHistort [buttonPanelHistort.Count - 1].GetComponent<PanelData> ();
			EnableButtoms (buttonPanelHistort [buttonPanelHistort.Count - 1]);
			StartCoroutine (highlightBtn (targetComponemt.memoryAim));

			DisableButtoms (currentButtonPanel);
			currentButtonPanel = buttonPanelHistort [buttonPanelHistort.Count - 1];

			if(targetComponemt.panelName != "") inPanelName = targetComponemt.panelName;
			buttonPanelHistort.RemoveAt (buttonPanelHistort.Count - 1);


		} else {
			MenuCancel ();
		}

	}

	public void FromPanelToPanel(GameObject ga,GameObject gb){
		if (!gb.GetComponent<PanelData> ().firstAim.activeSelf)
			return;
		if (ga.GetComponent<PanelData> () != null) {
			ga.GetComponent<PanelData> ().memoryAim = eventSystem.currentSelectedGameObject;
			//Debug.Log ("set panel:" + ga.name + " , aim to "+ga.GetComponent<PanelData> ().memoryAim.name);
		}
		buttonPanelHistort.Add (ga);
		currentButtonPanel = gb;

		EnableButtoms (gb);
		DisableButtoms (ga);

		if (gb.GetComponent<PanelData> () != null) {
			//如果Aim的Button並未啟用，則Aim 預設的Button
			if (gb.GetComponent<PanelData> ().memoryAim == null || !gb.GetComponent<PanelData> ().memoryAim.activeSelf) {
				StartCoroutine (highlightBtn (gb.GetComponent<PanelData> ().firstAim));
				Debug.Log ("in panel:" + gb.name + " , aim is null or inactive , so aim : " + gb.GetComponent<PanelData> ().firstAim.name);
			} else {
				StartCoroutine (highlightBtn (gb.GetComponent<PanelData> ().memoryAim));
				Debug.Log ("in panel:" + gb.name + " , aim : " + gb.GetComponent<PanelData> ().memoryAim.name);
			}
			if (gb.GetComponent<PanelData> ().panelName != "") {
				inPanelName = gb.GetComponent<PanelData> ().panelName;
			}
		}
	}

	public void EnableButtoms(GameObject go){
		if (go.GetComponent<CanvasGroup> () != null)
			//Debug.Log ("Will close interactable!");
			go.GetComponent<CanvasGroup>().interactable= true;
	}

	public void DisableButtoms(GameObject go){
		if(go.GetComponent<CanvasGroup>() != null)
			go.GetComponent<CanvasGroup>().interactable= false;
	}

	// unity bug , this works for buttom highlight
	IEnumerator highlightBtn(GameObject btn)
	{
		eventSystem.SetSelectedGameObject(btn);
		yield return null;
		eventSystem.SetSelectedGameObject(null);
		eventSystem.SetSelectedGameObject(btn);
	}

	IEnumerator MenuEffectTurn (bool active)
	{
		menuTurnLock = true;
		if (active) {
			mainMenu.SetActive (active);
			mainPanel.SetActive (active);
			do {
				yield return null;
			} while (anim.GetCurrentAnimatorStateInfo (0).IsName ("Base Layer.FadeIn"));
		} else {
			do {
				yield return null;
			} while (anim.GetCurrentAnimatorStateInfo (0).IsName ("Base Layer.FadeOut"));
			InactiveAllPanel ();

		}
		menuTurnLock = false;
	}

	public void InactiveAllPanel(){
		mainPanel.SetActive (false);
		magicPanel.SetActive (false);
		itemPanel.SetActive (false);
		equipPanel.SetActive (false);
		skillPanel.SetActive (false);
		statusPanel.SetActive (false);
		systemPanel.SetActive (false);

		commandButtoms.GetComponent<PanelData> ().memoryAim = null;
		actorButtoms.GetComponent<PanelData> ().memoryAim = null;
		magicButtoms.GetComponent<PanelData> ().memoryAim = null;
		magicTargetButtoms.GetComponent<PanelData> ().memoryAim = null;
		itemTypeButtons.GetComponent<PanelData> ().memoryAim = null;
		itemsButtons.GetComponent<PanelData> ().memoryAim = null;
		nowEquipButtons.GetComponent<PanelData> ().memoryAim = null;
		equipmentButtons.GetComponent<PanelData> ().memoryAim = null;
		skillButtons.GetComponent<PanelData> ().memoryAim = null;
		statusButtons.GetComponent<PanelData> ().memoryAim = null;

	}



	///////////////
	// setter & getter
	///////////////
	/// 
	public bool MenuTurnLock {
		get { return menuTurnLock;}
		set { menuTurnLock = value;}
	}

	/*
	IEnumerator DelayInactive()
	{
		yield return new WaitForSeconds (0.4f);
		statsMenu.SetActive (false);
	}

	IEnumerator DelayInactive2 (Animation animation)
	{
		do {
			yield return null;
		} while ( animation.isPlaying );
		statsMenu.SetActive (false);
	}

	IEnumerator DelayInactive3 (Animator anim)
	{
		do {
			yield return null;
		} while ( anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.FadeOut"));
		statsMenu.SetActive (false);
		menuTurnLock = false;
	}
	*/

}
