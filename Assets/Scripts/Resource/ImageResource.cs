using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageResource {

	public Sprite Black_Mask;

	public Sprite Icon_Default;

	public Sprite Icon_Horus;
	public Sprite Icon_Nephthys;


	public Sprite Photo_Horus_Stand;
	public Sprite Photo_Horus_Angry;
	public Sprite Photo_Horus_Cry;
	public Sprite Photo_Horus_Smile_1;
	public Sprite Photo_Horus_Smile_2;
	public Sprite Photo_Horus_Smile_3;

	public Sprite Photo_Nephthys_Stand;
	public Sprite Photo_Nephthys_Angry;
	public Sprite Photo_Nephthys_Cry;
	public Sprite Photo_Nephthys_Hate;
	public Sprite Photo_Nephthys_Smile_1;
	public Sprite Photo_Nephthys_Smile_2;
	public Sprite Photo_Nephthys_Smile_3;

	public Sprite Photo_Anubis_Stand;
	public Sprite Photo_Amit_Stand;

	public Sprite BattleIcon_Horus;
	public Sprite BattleIcon_Nephthys;
	public Sprite BattleIcon_Anubis;
	public Sprite BattleIcon_Enemy_4;
	public Sprite BattleIcon_Sater;
	public Sprite BattleIcon_SaterOld;
	public Sprite BattleIcon_DarkAmit;


	public Sprite GemSun;
	public Sprite GemDark;
	public Sprite GemFire;
	public Sprite GemWater;
	public Sprite GemWind;
	public Sprite GemNull;

	public Sprite BTN_Normal_Attack;
	public Sprite BTN_Horus_Skill_1;

	public Sprite BTN_Nephthys_Skill_1;
	public Sprite BTN_Nephthys_Skill_2;

	public Sprite BTN_Horus_Magic_1;
	public Sprite BTN_Nephthys_Magic_1;
	public Sprite BTN_Nephthys_Magic_2;

	public ImageResource(){

		Black_Mask = Resources.Load<Sprite> ("Sprite/BlackIcon");

		Icon_Default = Resources.Load<Sprite>("Sprite/DefaultIcon");

		Icon_Horus = Resources.Load<Sprite>("Sprite/Horus");
		Icon_Nephthys = Resources.Load<Sprite>("Sprite/Nephthys");


		Photo_Horus_Stand = Resources.Load<Sprite>("Sprite/Photo/Horus/Stand");
		Photo_Horus_Angry = Resources.Load<Sprite>("Sprite/Photo/Horus/Angry");
		Photo_Horus_Cry = Resources.Load<Sprite>("Sprite/Photo/Horus/Cry");
		Photo_Horus_Smile_1 = Resources.Load<Sprite>("Sprite/Photo/Horus/Smile_1");
		Photo_Horus_Smile_2 = Resources.Load<Sprite>("Sprite/Photo/Horus/Smile_2");
		Photo_Horus_Smile_3 = Resources.Load<Sprite>("Sprite/Photo/Horus/Smile_3");


		Photo_Nephthys_Stand = Resources.Load<Sprite>("Sprite/Photo/Nephthys/Stand");
		Photo_Nephthys_Angry = Resources.Load<Sprite>("Sprite/Photo/Nephthys/Angry");
		Photo_Nephthys_Cry = Resources.Load<Sprite>("Sprite/Photo/Nephthys/Cry");
		Photo_Nephthys_Hate = Resources.Load<Sprite>("Sprite/Photo/Nephthys/Hate");
		Photo_Nephthys_Smile_1 = Resources.Load<Sprite>("Sprite/Photo/Nephthys/Smile_1");
		Photo_Nephthys_Smile_2 = Resources.Load<Sprite>("Sprite/Photo/Nephthys/Smile_2");
		Photo_Nephthys_Smile_3 = Resources.Load<Sprite>("Sprite/Photo/Nephthys/Smile_3");

		Photo_Anubis_Stand =  Resources.Load<Sprite>("Sprite/Photo/Anubis/Stand");
		Photo_Amit_Stand =  Resources.Load<Sprite>("Sprite/Photo/Amit/Stand");



		BattleIcon_Horus = Resources.Load<Sprite>("Sprite/BattleIcon/HorusTargetIcon");
		BattleIcon_Nephthys = Resources.Load<Sprite>("Sprite/BattleIcon/NephthysTargetIcon");
		BattleIcon_Anubis = Resources.Load<Sprite>("Sprite/BattleIcon/AnubisTargetIcon");
		BattleIcon_Enemy_4 = Resources.Load<Sprite>("Sprite/BattleIcon/enemy4");
		BattleIcon_Sater = Resources.Load<Sprite>("Sprite/BattleIcon/Sater");
		BattleIcon_SaterOld = Resources.Load<Sprite>("Sprite/BattleIcon/SaterOld");
		BattleIcon_DarkAmit = Resources.Load<Sprite>("Sprite/BattleIcon/DarkAmit");



		GemSun = Resources.Load<Sprite>("Sprite/Gem/Sun");
		GemDark = Resources.Load<Sprite>("Sprite/Gem/Dark");
		GemFire = Resources.Load<Sprite>("Sprite/Gem/Fire");
		GemWater = Resources.Load<Sprite>("Sprite/Gem/Water");
		GemWind = Resources.Load<Sprite>("Sprite/Gem/Wind");
		GemNull = Resources.Load<Sprite>("Sprite/Gem/NULL");

		BTN_Horus_Skill_1 = Resources.Load<Sprite>("UI/Skills/HorusSlash");
		BTN_Normal_Attack = Resources.Load<Sprite>("UI/Skills/NormalAttack");

		BTN_Nephthys_Skill_1 = Resources.Load<Sprite>("UI/Skills/Neph_1");
		BTN_Nephthys_Skill_2 = Resources.Load<Sprite>("UI/Skills/NephLighting");


		BTN_Horus_Magic_1 = Resources.Load<Sprite>("UI/Magic/MagicHorus_1");

		BTN_Nephthys_Magic_1 = Resources.Load<Sprite>("UI/Magic/NephWater1");
		BTN_Nephthys_Magic_2 = Resources.Load<Sprite>("UI/Magic/NephWater2");
	}
}
