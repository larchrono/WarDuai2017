using UnityEngine;
using System.Collections;

public class PrefabsResource {

	public GameObject BattleIcon;
	public GameObject BattleSkillSlot;
	public GameObject BattleMagicSlot;
	public GameObject BlackScreen;

	public GameObject EffectDeath;

	public GameObject EffectUnitSelecttion;
	public GameObject EffectAttackHit;
	public GameObject EffectSkill_11;
	public GameObject EffectSkill_11_hit;

	public GameObject EffectSkill_22_hit;
	public GameObject EffectSkill_23_hit;
	public GameObject EffectSkill_24_hit;

	public GameObject UIBlurScreenBackgound;
	public GameObject UIPanelResult;

	public GameObject UIGemInstance;
	public GameObject UIGemUsedIns;

	public GameObject UIBattleUnitTag;
	public GameObject UIPlayerStatusSlot;
	public GameObject UITextDamaged;
	public GameObject UITextMPCost;

	public GameObject UIBattleRunLineIcon;

	public GameObject UIMessageSaveOK;

	public GameObject ModelBattleHorus;
	public GameObject ModelBattleNephthys;



	public PrefabsResource(){

		BattleIcon = Resources.Load ("Prefabs/UI/BattleIcon") as GameObject;
		BattleSkillSlot = Resources.Load ("Prefabs/UI/Battle/ButtonSkillSlot") as GameObject;
		BattleMagicSlot = Resources.Load ("Prefabs/UI/Battle/ButtonMagicSlot") as GameObject;

		BlackScreen = Resources.Load ("Prefabs/UI/BlackScreen") as GameObject;

		EffectDeath = Resources.Load ("Prefabs/Effect/GuardEffect02") as GameObject;

		EffectUnitSelecttion = Resources.Load ("Prefabs/Effect/Selection") as GameObject;

		EffectAttackHit = Resources.Load ("Prefabs/Effect/HitEffect01") as GameObject;

		EffectSkill_11 = Resources.Load ("Prefabs/Effect/Skill_11") as GameObject;
		EffectSkill_11_hit = Resources.Load ("Prefabs/Effect/GroundHit02") as GameObject;

		EffectSkill_22_hit = Resources.Load ("Prefabs/Effect/46_RFX_Straight_IceWall1") as GameObject;
		EffectSkill_23_hit = Resources.Load ("Prefabs/Effect/15_RFX_Magic_Buff2") as GameObject;
		EffectSkill_24_hit = Resources.Load ("Prefabs/Effect/EpicZeus") as GameObject;

		UIBlurScreenBackgound = Resources.Load ("Prefabs/UI/BlurBackground") as GameObject;

		UIPanelResult = Resources.Load ("Prefabs/UI/Battle/PanelResult") as GameObject;

		UIGemInstance = Resources.Load ("Prefabs/UI/GemIns") as GameObject;
		UIGemUsedIns = Resources.Load ("Prefabs/UI/GemInsUsed") as GameObject;

		UIBattleUnitTag = Resources.Load ("Prefabs/UI/Battle/BattleUITag") as GameObject;
		UIPlayerStatusSlot = Resources.Load ("Prefabs/UI/Battle/PanelPlayerSlot") as GameObject;
		UITextDamaged = Resources.Load ("Prefabs/UI/Battle/TextDamage") as GameObject;
		UITextMPCost = Resources.Load ("Prefabs/UI/Battle/TextMPCostValue") as GameObject;

		UIBattleRunLineIcon = Resources.Load ("Prefabs/UI/Battle/RunLineIcon") as GameObject;

		UIMessageSaveOK = Resources.Load ("Prefabs/UI/MessageSaveOK") as GameObject;


		ModelBattleHorus = Resources.Load ("Prefabs/PlayerModel/Horus") as GameObject;
		ModelBattleNephthys = Resources.Load ("Prefabs/PlayerModel/Nephthys") as GameObject;
	}
}
