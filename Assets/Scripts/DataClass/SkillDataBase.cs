using UnityEngine;
using System.Collections;

public static class SkillDataBase {

	public static SkillClass[] SkillData;

	static SkillDataBase(){

		int id;

		SkillData = new SkillClass[1024];

		id = 0;
		SkillData [id] = new SkillClass ();
		SkillData [id].Name = "無";
		SkillData [id].Description = "Error";
		SkillData [id].AtkRate = 1.0;
		SkillData [id].SpCost = 0;
		SkillData [id].SkillTarget = SkillClass.SkillTargets.ENEMY;

		id = 1;
		SkillData [id] = new SkillClass ();
		SkillData [id].Name = "通常攻擊";
		SkillData [id].Description = "敵方的通常攻擊";
		SkillData [id].Icon = GameResource.Sprite.BTN_Normal_Attack;
		SkillData [id].AnimationName = "attack";
		SkillData [id].AtkRate = 1.0;
		SkillData [id].SpCost = 0;
		SkillData [id].TimeCost = 1;
		SkillData [id].SkillTarget = SkillClass.SkillTargets.ALLY;
		SkillData [id].Type = GemData.GemType.NULL;

		// Defend
		id = 10;
		SkillData [id] = new SkillClass ();
		SkillData [id].Name = "防禦";
		SkillData [id].Description = "防禦敵方的攻擊";
		SkillData [id].Icon = GameResource.Sprite.BTN_Normal_Attack;
		SkillData [id].AnimationName = "defend";
		SkillData [id].AtkRate = 0.0;
		SkillData [id].SpCost = 0;
		SkillData [id].TimeCost = 2;
		SkillData [id].SkillTarget = SkillClass.SkillTargets.ALLY;
		SkillData [id].Type = GemData.GemType.NULL;
		// Horus List
		id = 11;
		SkillData [id] = new SkillClass ();
		SkillData [id].Name = "半月斬";
		SkillData [id].Description = "荷魯斯的通常攻擊";
		SkillData [id].Icon = GameResource.Sprite.BTN_Normal_Attack;
		SkillData [id].AnimationName = "attack";
		SkillData [id].AtkRate = 1.0;
		SkillData [id].SpCost = 0;
		SkillData [id].TimeCost = 0;
		SkillData [id].SkillTarget = SkillClass.SkillTargets.ENEMY;
		SkillData [id].Type = GemData.GemType.FIRE;
		SkillData [id].Shape = new SkillNode ();
		SkillData [id].Size = new Vector2 (1, 1);
		SkillData [id].StartAt = new Vector2 (0, 0);

		id = 12;
		SkillData [id] = new SkillClass ();
		SkillData [id].Name = "炎擊破";
		SkillData [id].Description = "飛身的一擊，並給予敵方火屬性傷害";
		SkillData [id].Icon = GameResource.Sprite.BTN_Horus_Skill_1;
		SkillData [id].AnimationName = "skill_12";
		SkillData [id].BonusDamage = 20;
		SkillData [id].AtkRate = 1.5;
		SkillData [id].SpCost = 0;
		SkillData [id].TimeCost = 1;
		SkillData [id].SkillTarget = SkillClass.SkillTargets.ENEMY;
		SkillData [id].Type = GemData.GemType.FIRE;
		SkillData [id].Shape = new SkillNode ();
		SkillData [id].Shape.right = new SkillNode ();
		SkillData [id].Shape.right.right = new SkillNode ();
		SkillData [id].Size = new Vector2 (3, 1);
		SkillData [id].StartAt = new Vector2 (0, 0);



		id = 21;
		SkillData [id] = new SkillClass ();
		SkillData [id].Name = "上鉤擊";
		SkillData [id].Description = "奈芙蒂斯的通常攻擊";
		SkillData [id].Icon = GameResource.Sprite.BTN_Normal_Attack;
		SkillData [id].AnimationName = "attack";
		SkillData [id].AtkRate = 1.0;
		SkillData [id].SpCost = 0;
		SkillData [id].TimeCost = 0;
		SkillData [id].SkillTarget = SkillClass.SkillTargets.ENEMY;
		SkillData [id].Type = GemData.GemType.WATER;
		SkillData [id].Shape = new SkillNode ();
		SkillData [id].Size = new Vector2 (1, 1);
		SkillData [id].StartAt = new Vector2 (0, 0);

		id = 22;
		SkillData [id] = new SkillClass ();
		SkillData [id].Name = "冰霜破";
		SkillData [id].Description = "呼喚冰柱攻擊敵方";
		SkillData [id].Icon = GameResource.Sprite.BTN_Nephthys_Skill_1;
		SkillData [id].AnimationName = "skill_22";
		SkillData [id].MatkRate = 3.0;
		SkillData [id].BonusDamage = 5;
		SkillData [id].SpCost = 0;
		SkillData [id].TimeCost = 1;
		SkillData [id].SkillTarget = SkillClass.SkillTargets.ENEMY;
		SkillData [id].Type = GemData.GemType.WATER;
		SkillData [id].Shape = new SkillNode ();
		SkillData [id].Shape.down = new SkillNode ();
		SkillData [id].Shape.right = new SkillNode ();
		SkillData [id].Shape.right.down = new SkillNode ();
		SkillData [id].Size = new Vector2 (2, 2);
		SkillData [id].StartAt = new Vector2 (0, 0);

		id = 23;
		SkillData [id] = new SkillClass ();
		SkillData [id].Name = "生命的祝福";
		SkillData [id].Description = "恢復我方全體HP";
		SkillData [id].Icon = GameResource.Sprite.BTN_Nephthys_Magic_1;
		SkillData [id].AnimationName = "skill_23";
		SkillData [id].MatkRate = 2.0;
		SkillData [id].BonusDamage = 5;
		SkillData [id].SpCost = 0;
		SkillData [id].TimeCost = 1;
		SkillData [id].SkillTarget = SkillClass.SkillTargets.ALLY;
		SkillData [id].Type = GemData.GemType.WATER;
		SkillData [id].Shape = new SkillNode ();
		SkillData [id].Shape.right = new SkillNode ();
		SkillData [id].Shape.right.right = new SkillNode ();
		SkillData [id].Shape.right.right.right = new SkillNode ();
		SkillData [id].Size = new Vector2 (4, 1);
		SkillData [id].StartAt = new Vector2 (0, 0);


		id = 24;
		SkillData [id] = new SkillClass ();
		SkillData [id].Name = "閃雷鳴";
		SkillData [id].Description = "呼喚閃雷攻擊敵全體";
		SkillData [id].Icon = GameResource.Sprite.BTN_Nephthys_Skill_2;
		SkillData [id].AnimationName = "skill_24";
		SkillData [id].MatkRate = 3.0;
		SkillData [id].SpCost = 0;
		SkillData [id].TimeCost = 4;
		SkillData [id].SkillTarget = SkillClass.SkillTargets.ENEMY;
		SkillData [id].Type = GemData.GemType.WIND;
		SkillData [id].Shape = new SkillNode ();
		SkillData [id].Shape.down = new SkillNode ();
		SkillData [id].Shape.down.down = new SkillNode ();
		SkillData [id].Shape.down.down.down = new SkillNode ();
		SkillData [id].Size = new Vector2 (1, 4);
		SkillData [id].StartAt = new Vector2 (0, 0);
	}
}
