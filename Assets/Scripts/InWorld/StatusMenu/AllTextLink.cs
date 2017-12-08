using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AllTextLink : MonoBehaviour {

	[Header("Magic")]
	public Button[] magicSlotEachButton;
	public Text magicCurrentInfo;

	[Header("Items")]
	public Button[] itemSlotEachButton;
	public Text itemCurrentInfo;

	[Header("Equipment")]
	public Text equipmentCurrentInfo;
	public Button[] equipmentSlotEachButton;

	[Header("Skill")]
	public Text skillCurrentInfo;
	public Button[] skillSlotEachButton;

	[Header("Current Actor")]
	public Image magicUserIcon;
	public Text magicUserName;
	public Text magicUserMp;
	public Text[] magicsName;
	public Text[] magicsCost;
	public Image equipActorIcon;
	public Text equipActorName;
	public Text equipActorATK;
	public Text equipActorDEF;
	public Text equipActorSPD;
	public Text equipActorMATK;
	public Text equipActorWeapon;
	public Text equipActorArmor;
	public Text equipActorShoes;
	public Text equipActorRing;
	public Image skillActorIcon;
	public Text skillActorName;
	public Text skillActorMSP;
	public Image infoActorIcon;
	public Text infoActorName;
	public Text infoActorLv;
	public Text infoActorHP;
	public Text infoActorMP;
	public Text infoActorSP;
	public Text infoActorMHP;
	public Text infoActorMMP;
	public Text infoActorMSP;
	public Text infoActorSTR;
	public Text infoActorINT;
	public Text infoActorVIT;
	public Text infoActorAGI;
	public Text infoActorATK;
	public Text infoActorMATK;
	public Text infoActorDEF;
	public Text infoActorSPD;
	public Text infoActorNextEXP;
	public Text infoActorTotalEXP;
	public Text infoActorWeapon;
	public Text infoActorArmor;
	public Text infoActorShoes;
	public Text infoActorRing;

	[Header("Actor Slot")]
	public StatsSlotTextLink[] statsActor;

}
