using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class BattleUI : MonoBehaviour {

	public static EventHandler<ActionUnitArgs> EVENT_SHOW_RESULT_CALL;

	public static event EventHandler<PressEventArgs> KEYEVENT_PRESS_A;
	public static event EventHandler<PressEventArgs> KEYEVENT_PRESS_B;

	public delegate void EventButtonCallBack(ButtonCallBack data);

	public static EventButtonCallBack EVENT_ON_BUTTON_PRESS_LEFT;
	public static EventButtonCallBack EVENT_ON_BUTTON_PRESS_RIGHT;
	public static EventButtonCallBack EVENT_PLAYER_BUTTON_SUBMIT;
	public static EventButtonCallBack EVENT_PLAYER_BUTTON_AIM;
	public static EventButtonCallBack EVENT_PLAYER_BUTTON_EXITAIM;
	public static EventButtonCallBack EVENT_PLAYER_SKILLBUTTON_CONFORM;
	public static EventButtonCallBack EVENT_PLAYER_MAGICBUTTON_CONFORM;

	public delegate void EventPanelCallBack(PanelArgs panelArgs);

	public static EventPanelCallBack EVENT_UI_PLAYER_PANEL_ENTER;
	public static EventPanelCallBack EVENT_UI_PLAYER_PANEL_BACK;
	public static EventPanelCallBack EVENT_UI_PLAYER_PANEL_LEAVE;

	public static BattleUI current;

	public GameObject ManuButtonAttack;
	public GameObject ManuButtonMagic;
	public GameObject ManuButtonDefend;
	public GameObject ManuButtonRun;

	public GameObject PanelStatus;
	public GameObject PanelGem;
	public GameObject PanelOrder;
	public GameObject PanelSkill;
	public GameObject PanelSkillList;
	public GameObject PanelSkillCostGem;
	public GameObject PanelMagic;
	public GameObject PanelMagicList;
	public GameObject PanelMagicCostMp;

	public GameObject PanelUnitTag;
	public GameObject PanelInfo;
	public Text PanelInfoText;
	private GameObject panelResult;

	public GameObject PanelGameOver;

	private List<GameObject> UIGems;
	private List<GameObject> UISkillCostGems;

	private BattleActor nowWorkUnit;
	private GameObject nowWorkPanel;
	private List<List<GemData>> nowCanSkillUseShapes;
	private List<GemData> nowSkillUseShape;
	private List<GemData> nowMagicUseShape;
	public GameObject nowTargetEffect;
	public ButtonCallBack nowAimSkillButton;



	void Awake() {
		//Clear static callback , or it will cause null reference
		EVENT_SHOW_RESULT_CALL = null;
		KEYEVENT_PRESS_A = null;
		KEYEVENT_PRESS_B = null;
		EVENT_ON_BUTTON_PRESS_LEFT = null;
		EVENT_ON_BUTTON_PRESS_LEFT = null;
		EVENT_PLAYER_BUTTON_SUBMIT = null;
		EVENT_PLAYER_BUTTON_AIM = null;
		EVENT_PLAYER_BUTTON_EXITAIM = null;
		EVENT_PLAYER_SKILLBUTTON_CONFORM = null;
		EVENT_PLAYER_MAGICBUTTON_CONFORM = null;
		EVENT_UI_PLAYER_PANEL_ENTER = null;
		EVENT_UI_PLAYER_PANEL_BACK = null;
		EVENT_UI_PLAYER_PANEL_LEAVE = null;

		current = this;
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("my name is " + PanelOrder.name);
		Debug.Log ("Global Data Gem count: " + GlobalData.Instance.InPanelGems.Count);
		Debug.Log ("Global Data Id: " + GlobalData.Instance.GetInstanceID());

		EVENT_PLAYER_BUTTON_SUBMIT += OnSkillOrderSelect;
		EVENT_PLAYER_BUTTON_SUBMIT += OnMagicOrderSelect;
		EVENT_PLAYER_BUTTON_SUBMIT += OnDefendOrderSelect;
		EVENT_PLAYER_BUTTON_AIM += OnPanelOrdersAim;
		EVENT_PLAYER_BUTTON_EXITAIM += OnPanelOrdersExitAim;
		EVENT_PLAYER_BUTTON_SUBMIT += OnSkillDecide;
		EVENT_PLAYER_BUTTON_SUBMIT += OnMagicDecide;
		EVENT_PLAYER_BUTTON_SUBMIT += OnButtonSubmitSound;
		EVENT_PLAYER_BUTTON_AIM += OnSkillAim;
		EVENT_PLAYER_BUTTON_AIM += OnMagicAim;
		EVENT_PLAYER_BUTTON_AIM += OnEnemyAim;
		EVENT_PLAYER_BUTTON_AIM += OnButtonAimSound;
		EVENT_PLAYER_BUTTON_EXITAIM += OnEvemyExitAim;
		EVENT_PLAYER_BUTTON_SUBMIT += OnEvemyDecide;
		EVENT_PLAYER_BUTTON_SUBMIT += OnRestartButtonPress;
		EVENT_UI_PLAYER_PANEL_ENTER += OnSkillPanelEnter;
		EVENT_UI_PLAYER_PANEL_ENTER += OnMagicPanelEnter;

		EVENT_UI_PLAYER_PANEL_LEAVE += OnSkillPanelLeave;

		KEYEVENT_PRESS_A += OnPressSubmit;
		KEYEVENT_PRESS_A += OnResultCancel;
		KEYEVENT_PRESS_B += OnPressBack;

		EVENT_SHOW_RESULT_CALL += OnResultPlay;
		BattleMain.EVENT_BATTLE_LOSE += OnBattleLose;

		BattleMain.EVENT_UNIT_ENTER_READYPOINT += OnUnitReady;

		PanelOrder.SetActive (false);
		PanelSkill.SetActive (false);
		PanelMagic.SetActive (false);
		PanelInfo.SetActive (false);

		UIGems = GemController.current.CreateImageGems (PanelGem);
		for (int i = 0; i < GlobalData.Instance.ActiveActors.Count; i++) {
			CreateStatusSlotInfoIntoPanel (PanelStatus, i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("A")){
			if (KEYEVENT_PRESS_A != null) {
				KEYEVENT_PRESS_A (this,new PressEventArgs(){});
			}
		}
		if (Input.GetButtonDown("B")){
			if (KEYEVENT_PRESS_B != null) {
				KEYEVENT_PRESS_B (this,new PressEventArgs(){PanelBeforeWork = nowWorkPanel});
			}
		}
		if (Input.GetMouseButtonDown(1)){
			if (KEYEVENT_PRESS_B != null) {
				KEYEVENT_PRESS_B (this,new PressEventArgs(){PanelBeforeWork = nowWorkPanel});
			}
		}
		if (Input.GetButtonDown("Horizontal")){
			if (Input.GetAxisRaw ("Horizontal") == -1) {
				OnSkillElementSelectChange (-1);
				//Debug.Log ("i press Horizen Left");
			}
			if (Input.GetAxisRaw ("Horizontal") == 1) {
				OnSkillElementSelectChange (1);
				//Debug.Log ("i press Horizen Right");
			}
		}
		/*
		if (Input.GetButtonDown("Vertical")){
			if (Input.GetAxisRaw ("Vertical") == -1) {
				Debug.Log ("i press Vertical Down");
			}
			if (Input.GetAxisRaw ("Vertical") == 1) {
				Debug.Log ("i press Vertical Up");
			}
		}
		*/
	}


	void OnSkillOrderSelect(ButtonCallBack data){
		if (data.ButtonWork != ButtonCallBack.ButtonWorks.MENU_SKILL)
			return;
		
		GoToPanel (PanelSkill);
		print ("1");
	}

	void OnMagicOrderSelect(ButtonCallBack data){
		if (data.ButtonWork != ButtonCallBack.ButtonWorks.MENU_MAGIC)
			return;

		GoToPanel (PanelMagic);
		print ("2");
	}

	void OnDefendOrderSelect(ButtonCallBack data){
		if (data.ButtonWork != ButtonCallBack.ButtonWorks.MENU_DEFEND)
			return;
		

		nowWorkUnit.actorAction = BattleActor.ActorAction.Defend;
		nowWorkUnit.actionSkill = 10;

		nowWorkPanel = null;
		PanelOrder.GetComponent<PanelReference> ().Hide ();

		nowSkillUseShape = GemController.current.Firstcolumn();
		GemController.current.UseSkillShape (nowSkillUseShape,UIGems);

		if (BattleMain.EVENT_PLATER_UNIT_SUBMIT_ORDER != null)
			BattleMain.EVENT_PLATER_UNIT_SUBMIT_ORDER (this,new ActionUnitArgs(){triggerUnit = nowWorkUnit});
		if (BattleMain.EVENT_UNIT_ENTER_PREPARE != null)
			BattleMain.EVENT_UNIT_ENTER_PREPARE (this,new ActionUnitArgs(){triggerUnit = nowWorkUnit});
		
		print ("3");
	}

	void OnRunOrderSelect(ButtonCallBack data){

		print ("4");
	}

	void OnPanelOrdersAim(ButtonCallBack data){
		if (data.ButtonWork != ButtonCallBack.ButtonWorks.MENU_SKILL && 
			data.ButtonWork != ButtonCallBack.ButtonWorks.MENU_MAGIC && 
			data.ButtonWork != ButtonCallBack.ButtonWorks.MENU_DEFEND && 
			data.ButtonWork != ButtonCallBack.ButtonWorks.MENU_RUN )
			return;
		data.AnimImage.GetComponent<Animator> ().Play ("OrderButtonGo");

		//解除珠子的Cost預覽狀態
		foreach(GameObject obj in UIGems){
			//color32 參數的範圍是 0~255
			obj.GetComponent<UIGem>().img.color = new Color32(255, 255, 255, 255);
			obj.GetComponent<UIGem>().img.material = null;
		}
		//Hide Info
		PanelInfo.SetActive(false);
	}

	void OnPanelOrdersExitAim(ButtonCallBack data){
		if (data.ButtonWork != ButtonCallBack.ButtonWorks.MENU_SKILL && 
			data.ButtonWork != ButtonCallBack.ButtonWorks.MENU_MAGIC && 
			data.ButtonWork != ButtonCallBack.ButtonWorks.MENU_DEFEND && 
			data.ButtonWork != ButtonCallBack.ButtonWorks.MENU_RUN )
			return;
		data.AnimImage.GetComponent<Animator> ().Play ("OrderButtonBack");
	}

	void OnSkillPanelEnter(PanelArgs args){

		if (args.PanelNow != PanelSkill)
			return;

		PanelInfo.SetActive(true);
		//更新該單位所有的資訊, 先移除後新增
		//更新技能資訊
		foreach (Transform child in PanelSkillList.transform) {

			GameObject.Destroy(child.gameObject);
		}
		int skillNum = nowWorkUnit.Data.LearnSkill.Count;
		GameObject[] slots = new GameObject[skillNum];
		for (int i = 0; i < skillNum; i++) {
			slots[i] = (GameObject)Instantiate (GameResource.Prefab.BattleSkillSlot, Vector3.zero, Quaternion.identity);
			slots[i].transform.SetParent (PanelSkillList.GetComponent<RectTransform> (), false);
			slots[i].GetComponent<ButtonCallBack> ().SetSkillID (nowWorkUnit.Data.LearnSkill[i],i);
			SkillClass skill = SkillDataBase.SkillData [nowWorkUnit.Data.LearnSkill [i]];
			List<List<GemData>> searchResult = GemController.current.FindShapeInGemPanel (skill.Shape, skill.Type);
			if (searchResult.Count <= 0) {
				slots [i].GetComponent<ButtonCallBack> ().UIText.GetComponent<Text> ().color = new Color32 (150, 150, 150, 255);
			}
		}
		PanelSkillList.GetComponent<PanelReference> ().ButtonAim = slots [PanelSkillList.GetComponent<PanelReference> ().ButtonAimDynamicId];
	}

	void OnMagicPanelEnter(PanelArgs args){

		if (args.PanelNow != PanelMagic)
			return;

		PanelInfo.SetActive(true);
		//更新該單位所有的資訊, 先移除後新增
		//更新Magic資訊
		foreach (Transform child in PanelMagicList.transform) {

			GameObject.Destroy(child.gameObject);
		}
		foreach (Transform child in PanelMagicCostMp.transform) {

			GameObject.Destroy(child.gameObject);
		}

		int magicNum = nowWorkUnit.Data.LearnMagic.Count;
		GameObject[] slots = new GameObject[magicNum];
		for (int i = 0; i < magicNum; i++) {
			slots[i] = (GameObject)Instantiate (GameResource.Prefab.BattleMagicSlot, Vector3.zero, Quaternion.identity);
			slots[i].transform.SetParent (PanelMagicList.GetComponent<RectTransform> (), false);
			slots[i].GetComponent<ButtonCallBack> ().SetMagicID (nowWorkUnit.Data.LearnMagic[i],i);

			List<GemData> searchResult = GemController.current.FindMagicEffectInGemPanel(nowWorkUnit.Data.LearnMagic[i]);
			if (searchResult.Count <= 0 || !nowWorkUnit.Data.CanCostMp(MagicDataBase.MagicData[nowWorkUnit.Data.LearnMagic[i]].MpCost)) {
				slots [i].GetComponent<ButtonCallBack> ().UIText.GetComponent<Text> ().color = new Color32 (120, 120, 120, 255);
			}

			GameObject mpcost = Instantiate (GameResource.Prefab.UITextMPCost);
			mpcost.GetComponent<Text> ().text = "" + MagicDataBase.MagicData [nowWorkUnit.Data.LearnMagic [i]].MpCost;
			mpcost.transform.SetParent (PanelMagicCostMp.transform, false);
		}
		PanelMagicList.GetComponent<PanelReference> ().ButtonAim = slots [PanelMagicList.GetComponent<PanelReference> ().ButtonAimDynamicId];
		//PanelMagicList.GetComponent<PanelReference> ().PanelWorkPanel.GetComponent<PanelReference>().ButtonAim = slots [0];
	}

	void OnSkillAim(ButtonCallBack data){
		if (data.ButtonType != ButtonCallBack.ButtonTypes.SKILL_SLOT)
			return;
		
		nowAimSkillButton = data;
		SkillClass skill = SkillDataBase.SkillData [data.SkillID];
		PanelInfoText.text = skill.Description + "\n" + skill.BonusDescription;

		//動態地顯示需要耗費的元素
		if (UISkillCostGems != null) {
			foreach (GameObject temp in UISkillCostGems) {
				Destroy (temp);
			}
			UISkillCostGems.Clear ();
		}
		UISkillCostGems = GemController.current.CreateImageGemByNode (PanelSkillCostGem,skill.Shape, skill.StartAt, skill.Type);

		//先把所有元素變暗
		foreach(GameObject obj in UIGems){
			//color32 參數的範圍是 0~255
			obj.GetComponent<UIGem>().img.color = new Color32(75, 75, 75, 255);
			obj.GetComponent<UIGem>().img.material = null;
		}

		//點亮可用的元素 
		nowCanSkillUseShapes = GemController.current.FindShapeInGemPanel (skill.Shape, skill.Type);
		if (nowCanSkillUseShapes.Count > 0) {
			nowSkillUseShape = nowCanSkillUseShapes [0];
			foreach (GemData gem in nowSkillUseShape) {
				UIGems [GlobalData.Instance.InPanelGems.IndexOf (gem)].GetComponent<UIGem> ().img.color = new Color32 (255, 255, 255, 255);
				UIGems [GlobalData.Instance.InPanelGems.IndexOf (gem)].GetComponent<UIGem> ().img.material = GameResource.Mat.UISpark;
			}
			PanelGem.GetComponent<UIShift> ().OpenLR ();
		} else {
			//如果找不到元素的話，也要設定目前的使用形狀為空，不然會影響出招判斷
			if (nowSkillUseShape != null)
				nowSkillUseShape.Clear ();
			else
				nowSkillUseShape = new List<GemData> ();
			PanelGem.GetComponent<UIShift> ().CloseLR ();
		}
	}

	void OnSkillPanelLeave(PanelArgs args){
		if (args.PanelBeforeWork != PanelSkill)
			return;
		PanelGem.GetComponent<UIShift> ().CloseLR ();
	}

	void OnMagicAim(ButtonCallBack data){
		if (data.ButtonType != ButtonCallBack.ButtonTypes.MAGIC_SLOT)
			return;

		MagicClass magic = MagicDataBase.MagicData [data.MagicID];
		PanelInfoText.text = magic.Description;

		//先把所有元素變暗
		foreach(GameObject obj in UIGems){
			//color32 參數的範圍是 0~255
			obj.GetComponent<UIGem>().img.color = new Color32(75, 75, 75, 255);
			obj.GetComponent<UIGem>().img.material = null;
		}

		//點亮可用的元素
		nowMagicUseShape = GemController.current.FindMagicEffectInGemPanel(data.MagicID);
		foreach (GemData gem in nowMagicUseShape) {
			UIGems[GlobalData.Instance.InPanelGems.IndexOf(gem)].GetComponent<UIGem>().img.color = new Color32(255, 255, 255, 255);
			UIGems[GlobalData.Instance.InPanelGems.IndexOf(gem)].GetComponent<UIGem>().img.material = GameResource.Mat.UISpark;
		}

	}

	void OnSkillElementSelectChange (int dir){
		if (nowWorkPanel != PanelSkill)
			return;
		
		//Debug.Log ("I press " + dir);
		ButtonCallBack data = nowAimSkillButton;
		if (data == null)
			return;

		int canUseCount = nowCanSkillUseShapes.Count;
		int id = nowCanSkillUseShapes.IndexOf (nowSkillUseShape);

		if (canUseCount == 0)
			return;
		
		if (dir == -1) {
			if (id == 0)
				id = canUseCount - 1;
			else
				id = id - 1;
		}

		if (dir == 1) {
			if (id == (canUseCount -1))
				id = 0;
			else
				id = id + 1;
		}

		//先把所有元素變暗
		foreach(GameObject obj in UIGems){
			//color32 參數的範圍是 0~255
			obj.GetComponent<UIGem>().img.color = new Color32(75, 75, 75, 255);
			obj.GetComponent<UIGem>().img.material = null;
		}

		//點亮可用的元素
		nowSkillUseShape = nowCanSkillUseShapes [id];
		foreach (GemData gem in nowSkillUseShape) {
			UIGems [GlobalData.Instance.InPanelGems.IndexOf (gem)].GetComponent<UIGem> ().img.color = new Color32 (255, 255, 255, 255);
			UIGems [GlobalData.Instance.InPanelGems.IndexOf (gem)].GetComponent<UIGem> ().img.material = GameResource.Mat.UISpark;
		}

	}
		
	void OnSkillDecide(ButtonCallBack data){
		if (data.ButtonWork != ButtonCallBack.ButtonWorks.SKILL_DECIDE)
			return;
		
		if (nowSkillUseShape.Count > 0) {
			if (EVENT_PLAYER_SKILLBUTTON_CONFORM != null)
				EVENT_PLAYER_SKILLBUTTON_CONFORM (data);
			
			PanelUnitTag.GetComponent<PanelReference> ().PanelBack = nowWorkPanel;
			PanelSkillList.GetComponent<PanelReference> ().ButtonAimDynamicId = data.ButtonDynamicID;
			PanelInfo.SetActive (false);

			GameObject tempUnitTag = null;
			foreach (BattleActor unit in BattleMain.Units) {
				if (unit.actorType == BattleActor.ActorType.NPC && unit.IsAlive ()) {
					tempUnitTag = unit.ButtonTag;
					tempUnitTag.GetComponent<Button> ().interactable = true;
					if (tempUnitTag.GetComponent<ButtonCallBack> ().ButtonDynamicID == PanelUnitTag.GetComponent <PanelReference> ().ButtonAimDynamicId)
						PanelUnitTag.GetComponent <PanelReference> ().ButtonAim = tempUnitTag;

					print ("open" + unit.Model.name);
				}
				else 
					print ("not open" + unit.Model.name);
			}

			if(PanelUnitTag.GetComponent <PanelReference> ().ButtonAim == null && tempUnitTag != null)
				PanelUnitTag.GetComponent <PanelReference> ().ButtonAim = tempUnitTag;
			
			nowWorkUnit.actionSkill = data.SkillID;
			GoToPanel (PanelUnitTag);
		}
		//print ("use id " + id);
	}

	void OnMagicDecide(ButtonCallBack data){
		if (data.ButtonWork != ButtonCallBack.ButtonWorks.MAGIC_DECIDE)
			return;

		if (nowMagicUseShape.Count > 0 && nowWorkUnit.Data.CanCostMp(MagicDataBase.MagicData[data.MagicID].MpCost)) {

			//Magic Effect here
			GemController.current.UseMagicToGem (nowWorkUnit.Data, data.MagicID, UIGems);
			PanelMagicList.GetComponent<PanelReference> ().ButtonAimDynamicId = data.ButtonDynamicID;


			//START -- Update Skill effect Info here
			//OnMagicAim(data);
			foreach (Transform child in PanelMagicList.transform) 
				GameObject.Destroy(child.gameObject);
			foreach (Transform child in PanelMagicCostMp.transform)
				GameObject.Destroy(child.gameObject);

			int magicNum = nowWorkUnit.Data.LearnMagic.Count;
			GameObject[] slots = new GameObject[magicNum];
			for (int i = 0; i < magicNum; i++) {
				slots[i] = (GameObject)Instantiate (GameResource.Prefab.BattleMagicSlot, Vector3.zero, Quaternion.identity);
				slots[i].transform.SetParent (PanelMagicList.GetComponent<RectTransform> (), false);
				slots[i].GetComponent<ButtonCallBack> ().SetMagicID (nowWorkUnit.Data.LearnMagic[i],i);

				List<GemData> searchResult = GemController.current.FindMagicEffectInGemPanel(nowWorkUnit.Data.LearnMagic[i]);
				if (searchResult.Count <= 0 || !nowWorkUnit.Data.CanCostMp(MagicDataBase.MagicData[nowWorkUnit.Data.LearnMagic[i]].MpCost)) {
					slots [i].GetComponent<ButtonCallBack> ().UIText.GetComponent<Text> ().color = new Color32 (120, 120, 120, 255);
				}

				GameObject mpcost = Instantiate (GameResource.Prefab.UITextMPCost);
				mpcost.GetComponent<Text> ().text = "" + MagicDataBase.MagicData [nowWorkUnit.Data.LearnMagic [i]].MpCost;
				mpcost.transform.SetParent (PanelMagicCostMp.transform, false);
			}
			EventSystem.current.SetSelectedGameObject (slots [data.ButtonDynamicID]);
			//END -- Update Skill effect Info here

			//work magic assign
			nowWorkUnit.actionMagic = data.MagicID;
			
			if (BattleMain.EVENT_ANYUNIT_STATUS_UPDATE != null)
				BattleMain.EVENT_ANYUNIT_STATUS_UPDATE (this,new ActionUnitArgs(){ triggerUnit = nowWorkUnit });
			
			if (EVENT_PLAYER_MAGICBUTTON_CONFORM != null)
				EVENT_PLAYER_MAGICBUTTON_CONFORM (data);
			
		}
	}

	void OnEnemyAim(ButtonCallBack data){
		if (data.ButtonType != ButtonCallBack.ButtonTypes.UNIT_TAG)
			return;
		if (nowTargetEffect != null)
			Destroy (nowTargetEffect);
		//GameObject selection = Instantiate (GameResource.Prefab.EffectUnitSelecttion, data.GetComponent<SpriteReference> ().refLocation.transform.parent.transform, false);
		GameObject selection = Instantiate (GameResource.Prefab.EffectUnitSelecttion);
		selection.transform.position = BattleMain.Units[data.UnitID].Model.transform.position;
		nowTargetEffect = selection;
		PanelUnitTag.GetComponent<PanelReference> ().ButtonAimDynamicId = data.ButtonDynamicID;
	}

	void OnEvemyExitAim(ButtonCallBack data){
		if (data.ButtonType != ButtonCallBack.ButtonTypes.UNIT_TAG)
			return;
		if (nowTargetEffect != null)
			Destroy (nowTargetEffect);
	}

	void OnEvemyDecide(ButtonCallBack data){
		if (data.ButtonWork != ButtonCallBack.ButtonWorks.TARGET_DECIDE)
			return;

		PanelUnitTag.GetComponent <PanelReference> ().ButtonAim = data.gameObject;
		foreach (BattleActor unit in BattleMain.Units) {
			if (unit.actorType == BattleActor.ActorType.NPC && unit.IsAlive ()) {
				unit.ButtonTag.GetComponent<Button> ().interactable = false;
			}
		}

		nowWorkUnit.actionTarget = data.UnitID;

		foreach (GameObject obj in UIGems) {
			obj.GetComponent<UIGem> ().img.material = null;
			obj.GetComponent<UIGem> ().img.color = Color.white;
		}
		GemController.current.UseSkillShape (nowSkillUseShape,UIGems);

		if (BattleMain.EVENT_PLATER_UNIT_SUBMIT_ORDER != null)
			BattleMain.EVENT_PLATER_UNIT_SUBMIT_ORDER (this,new ActionUnitArgs(){triggerUnit = nowWorkUnit});
		if (BattleMain.EVENT_UNIT_ENTER_PREPARE != null)
			BattleMain.EVENT_UNIT_ENTER_PREPARE (this,new ActionUnitArgs(){triggerUnit = nowWorkUnit});

		nowWorkPanel = null;
	}

	private bool _resultCancelSubmit = false;
	void OnResultCancel(object sender,PressEventArgs args){
		//ExitBattle 
		if (nowWorkPanel == panelResult && nowWorkPanel != null) {
			if (!_resultCancelSubmit) {
				_resultCancelSubmit = true;
				if (BattleMain.EVENT_EXIT_BATTLE != null)
					BattleMain.EVENT_EXIT_BATTLE (this, new ActionUnitArgs (){ });
			}
		}
	}

	void OnPressSubmit(object sender,PressEventArgs args){
		if (EventSystem.current.currentSelectedGameObject == null) 
			return;	
		//EventSystem.current.currentSelectedGameObject.GetComponent<Button> ().onClick.Invoke ();
	}
	void OnPressBack(object sender,PressEventArgs args){
		if (nowWorkPanel == null)
			return;
		//避免最後了還一直退
		if (nowWorkPanel != nowWorkPanel.GetComponent<PanelReference> ().PanelBack) {

			if (EVENT_UI_PLAYER_PANEL_BACK != null)
				EVENT_UI_PLAYER_PANEL_BACK (new PanelArgs(){PanelBeforeWork = nowWorkPanel , PanelGoTo = nowWorkPanel.GetComponent<PanelReference> ().PanelBack});
			
			GoToPanel (nowWorkPanel.GetComponent<PanelReference> ().PanelBack);

		}
	}

	void OnRestartButtonPress(ButtonCallBack data){
		if (data.ButtonWork != ButtonCallBack.ButtonWorks.GAME_RESTART)
			return;

		GameCamera.FadeOut (1.0f,20f);
		MusicController.current.FadeOut();

		StartCoroutine (RestartToWorld());

		Debug.Log ("GameRestart!!");
	}

	IEnumerator RestartToWorld(){
		yield return new WaitForSeconds (1.0f);
		Destroy (GlobalData.Instance.gameObject);
		SceneManager.LoadSceneAsync ("Title");
	}






	void OnBattleLose(object sender,ActionUnitArgs args){
		StartCoroutine (OnBattleLoseWork ());
	}
	IEnumerator OnBattleLoseWork(){
		yield return new WaitForSeconds (4.0f);
		GameCamera.ShowBlurOnCanvas ();
		yield return new WaitForSeconds (0.1f);
		int id = GameCamera.nowBlurObject.transform.GetSiblingIndex ();
		PanelGameOver.transform.SetSiblingIndex (id + 1);
		PanelGameOver.SetActive (true);
		EventSystem.current.SetSelectedGameObject (PanelGameOver.GetComponent<PanelReference>().ButtonAim);
		GameCamera.nowBlurObject.GetComponent<Image> ().color = new Color32 (125,125,125,150);
	}


	void OnResultPlay(object sender,ActionUnitArgs args){
		panelResult = Instantiate (GameResource.Prefab.UIPanelResult);
		panelResult.transform.SetParent (GameObject.Find("Canvas").GetComponent<RectTransform>(),false);
		panelResult.GetComponent<PanelResult> ().expGet.text = "" + BattleMain.current.expGet;
		panelResult.GetComponent<PanelResult> ().goldGet.text = "" + 15;
		nowWorkPanel = panelResult;

		/*
		foreach(StandardActor act in GlobalData.Instance.ActiveActors){
			if (act.HP > 0) {
				act.AddExp (BattleMain.current.expGet);
			}
		}
		*/

		for (int i = 0; i < GlobalData.Instance.ActiveActors.Count; i++) {
			if (GlobalData.Instance.ActiveActors [i].HP > 0) {
				GlobalData.Instance.ActiveActors [i].AddExp (BattleMain.current.expGet);
				panelResult.GetComponent<PanelResult> ().actorSlots [i].GetComponent<UIActorData> ().AddExpAnim (BattleMain.current.expGet);
			}
		}
	}

	void OnUnitReady(object sender,ActionUnitArgs args){
		if (args.triggerUnit.actorType == BattleActor.ActorType.USER) {
			nowWorkUnit = args.triggerUnit;
			nowWorkPanel = PanelOrder;

			PanelSkillList.GetComponent<PanelReference> ().ButtonAimDynamicId = 0;
			PanelMagicList.GetComponent<PanelReference> ().ButtonAimDynamicId = 0;

			PanelOrder.GetComponent<PanelReference> ().Show ();
			PanelOrder.GetComponent<Animator> ().Play ("PanelOrderEnter");

			EventSystem.current.SetSelectedGameObject(ManuButtonAttack);
		}
	}

	public void GoToPanel(GameObject goal){
		//save last panel Aim
		nowWorkPanel.GetComponent<PanelReference> ().PanelWorkPanel.GetComponent<PanelReference> ().ButtonAim = EventSystem.current.currentSelectedGameObject;

		//Fade to next panel
		goal.GetComponent<PanelReference> ().Show ();

		if (EVENT_UI_PLAYER_PANEL_ENTER != null)
			EVENT_UI_PLAYER_PANEL_ENTER (new PanelArgs(){PanelNow = goal});

		//set now panel Aim
		if (goal.GetComponent<PanelReference> ().PanelWorkPanel.GetComponent<PanelReference> ().ButtonAim != null) {
			EventSystem.current.SetSelectedGameObject (goal.GetComponent<PanelReference> ().PanelWorkPanel.GetComponent<PanelReference> ().ButtonAim);
		} else {
			print ("panel error , can't find aim button");
		}

		//Hide before panel
		nowWorkPanel.GetComponent<PanelReference> ().Hide ();

		if (EVENT_UI_PLAYER_PANEL_LEAVE != null)
			EVENT_UI_PLAYER_PANEL_LEAVE (new PanelArgs(){PanelBeforeWork = nowWorkPanel,PanelGoTo = goal});

		//define now panel
		nowWorkPanel = goal;

	}

	private void CreateStatusSlotInfoIntoPanel(GameObject panel,int slot){
		List<GameObject> statusSlots = new List<GameObject> ();
		GameObject temp = (GameObject)Instantiate (GameResource.Prefab.UIPlayerStatusSlot, Vector3.zero, Quaternion.identity);
		temp.transform.SetParent (panel.GetComponent<RectTransform> (), false);
		temp.GetComponent<UIStatusBar> ().Init (GlobalData.Instance.ActiveActors[slot]);
		statusSlots.Add (temp);
	}

	public string GetNowPanelName(){
		if(nowWorkPanel != null)
			return nowWorkPanel.name;
		return "Null";
	}


	public void OnButtonAimSound(ButtonCallBack data){
		AudioSource snd = SoundCollect.current.SNDSelect;
		SoundCollect.current.Play (snd);
		//Debug.Log ("play select");
	}

	public void OnButtonSubmitSound(ButtonCallBack data){
		AudioSource snd = SoundCollect.current.SNDSubmit;
		SoundCollect.current.Play (snd);
		//Debug.Log ("play submit");
	}

	/*
	public void FromPanelToPanel(GameObject ga,GameObject gb){
		if (!gb.GetComponent<PanelData> ().firstAim.activeSelf)
			return;
		if (ga.GetComponent<PanelData> () != null) {
			ga.GetComponent<PanelData> ().memoryAim = eventSystem.currentSelectedGameObject;
			//Debug.Log ("set panel:" + ga.name + " , aim to "+ga.GetComponent<PanelData> ().memoryAim.name);
		}
		buttonPanelHistort.Add (ga);
		currentButtonPanel = gb;

		DisableButtoms (ga);
		EnableButtoms (gb);

		if (gb.GetComponent<PanelData> () != null) {
			if(gb.GetComponent<PanelData> ().memoryAim == null || !gb.GetComponent<PanelData> ().memoryAim.activeSelf)
				StartCoroutine (highlightBtn (gb.GetComponent<PanelData> ().firstAim));
			else 
				StartCoroutine (highlightBtn (gb.GetComponent<PanelData> ().memoryAim));
			if (gb.GetComponent<PanelData> ().panelName != "") {
				inPanelName = gb.GetComponent<PanelData> ().panelName;
			}
		}

		if(gb.GetComponent<PanelData> ().memoryAim != null)
			Debug.Log ("in panel:" + gb.name + " , aim : " + gb.GetComponent<PanelData> ().memoryAim.name);
		else
			Debug.Log ("in panel:" + gb.name + " , aim : null");
		
	}
	*/
}
