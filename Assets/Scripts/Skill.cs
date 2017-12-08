using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill {

	public static int GetSkillPower(BattleActor caster ,int skill_id){
		SkillClass skill = SkillDataBase.SkillData [skill_id];
		int damage =  (int)(caster.Data.ATK * skill.AtkRate + caster.Data.MATK * skill.MatkRate + skill.BonusDamage);
		return damage;
	}
}
