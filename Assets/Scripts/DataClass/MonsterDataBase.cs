using UnityEngine;
using System.Collections;

public static class MonsterDataBase {

	public static StandardActor[] MonsterData;

	static MonsterDataBase(){

		int id;

		MonsterData = new StandardActor[1024];

		id = 1;
		MonsterData [id] = new StandardActor ();
		MonsterData [id].ActorName = "箱子";
		MonsterData [id].MHP = 100;
		MonsterData [id].SPD = 10;
		MonsterData [id].BattleIcon = GameResource.Sprite.BattleIcon_Anubis;
		MonsterData [id].BattleModel = Resources.Load ("Prefabs/EnemyModel/ID1") as GameObject;


		id = 2;
		MonsterData [id] = new StandardActor ();
		MonsterData [id].ActorName = "少年";
		MonsterData [id].Photo = GameResource.Sprite.Photo_Anubis_Stand;
		MonsterData [id].MHP = 100;
		MonsterData [id].SPD = 10;
		MonsterData [id].ATK = 18;
		MonsterData [id].EXP = 12;
		MonsterData [id].BattleIcon = GameResource.Sprite.BattleIcon_Anubis;
		MonsterData [id].BattleModel = Resources.Load ("Prefabs/EnemyModel/Anubis") as GameObject;
		//MonsterData [id].BattleIcon = GameResource.Sprite.BattleIcon_Sater;

		id = 3;
		MonsterData [id] = new StandardActor ();
		MonsterData [id].ActorName = "阿米特";
		MonsterData [id].Photo = GameResource.Sprite.Photo_Amit_Stand;
		MonsterData [id].SPD = 10;
		//MonsterData [id].BattleIcon = GameResource.Sprite.BattleIcon_Sater;

		id = 4;
		MonsterData [id] = new StandardActor ();
		MonsterData [id].ActorName = "蠍子";
		MonsterData [id].MHP = 40;
		MonsterData [id].SPD = 10;
		MonsterData [id].ATK = 18;
		MonsterData [id].EXP = 10;
		MonsterData [id].BattleIcon = GameResource.Sprite.BattleIcon_Enemy_4;
		MonsterData [id].BattleModel = Resources.Load ("Prefabs/EnemyModel/Mons2017") as GameObject;

		id = 5;
		MonsterData [id] = new StandardActor ();
		MonsterData [id].ActorName = "守衛獸";
		MonsterData [id].MHP = 500;
		MonsterData [id].SPD = 20;
		MonsterData [id].ATK = 32;
		MonsterData [id].EXP = 350;
		MonsterData [id].BattleIcon = GameResource.Sprite.BattleIcon_DarkAmit;
		MonsterData [id].BattleModel = Resources.Load ("Prefabs/EnemyModel/DarkAmit") as GameObject;

	}
}
