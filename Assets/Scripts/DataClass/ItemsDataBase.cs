using UnityEngine;
using System.Collections;

public static class ItemsDataBase {

	public static BaseItemClass[] ItemData;

	static ItemsDataBase(){

		int id;

		ItemData = new BaseItemClass[1024];

		// Empty equipment
		id = 0;
		ItemData [id] = new EquipmentClass ();
		ItemData [id].Name = "空";
		ItemData [id].Description = "";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 1;
		ItemData [id].Throwable = false;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.EQUIPMENT;
		((EquipmentClass)ItemData [id]).EquipmentType = EquipmentClass.EquipmentTypes.EMPTY;

		//consumable
		id = 1;
		ItemData [id] = new ConsumableClass ();
		ItemData [id].Name = "水";
		ItemData [id].Description = "可飲用的水";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 1;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.CONSUMABLE;
		((ConsumableClass)ItemData [id]).HealHP = 30;

		id = 2;
		ItemData [id] = new ConsumableClass ();
		ItemData [id].Name = "甜甜圈";
		ItemData [id].Description = "豪吃";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 1;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.CONSUMABLE;
		((ConsumableClass)ItemData [id]).HealHP = 30;


		// equipment
		id = 100;
		ItemData [id] = new EquipmentClass ();
		ItemData [id].Name = "銀槍";
		ItemData [id].Description = "王子平時練習的配帶武器";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 3;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.EQUIPMENT;
		((EquipmentClass)ItemData [id]).EquipmentType = EquipmentClass.EquipmentTypes.WEAPON;
		((EquipmentClass)ItemData [id]).ATK_UP = 6;

		id = 101;
		ItemData [id] = new EquipmentClass ();
		ItemData [id].Name = "銀杖";
		ItemData [id].Description = "祭司們配帶的杖";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 3;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.EQUIPMENT;
		((EquipmentClass)ItemData [id]).EquipmentType = EquipmentClass.EquipmentTypes.WEAPON;
		((EquipmentClass)ItemData [id]).ATK_UP = 3;
		((EquipmentClass)ItemData [id]).MATK_UP = 4;

		id = 198;
		ItemData [id] = new EquipmentClass ();
		ItemData [id].Name = "聖靈杖";
		ItemData [id].Description = "集結了先靈意志的法杖";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 6;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.EQUIPMENT;
		((EquipmentClass)ItemData [id]).EquipmentType = EquipmentClass.EquipmentTypes.WEAPON;
		((EquipmentClass)ItemData [id]).ATK_UP = 45;
		((EquipmentClass)ItemData [id]).MATK_UP = 80;
		((EquipmentClass)ItemData [id]).MATK_Raise = 0.5;

		id = 199;
		ItemData [id] = new EquipmentClass ();
		ItemData [id].Name = "翼神矛";
		ItemData [id].Description = "由太陽神幻化而成的武器";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 6;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.EQUIPMENT;
		((EquipmentClass)ItemData [id]).EquipmentType = EquipmentClass.EquipmentTypes.WEAPON;
		((EquipmentClass)ItemData [id]).ATK_UP = 134;
		((EquipmentClass)ItemData [id]).ATK_Rate = 0.5;
		((EquipmentClass)ItemData [id]).ATK_Raise = 0.25;
		((EquipmentClass)ItemData [id]).HP_Raise = 0.5;


		id = 200;
		ItemData [id] = new EquipmentClass ();
		ItemData [id].Name = "王子私服";
		ItemData [id].Description = "王子平時穿著的衣服";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 3;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.EQUIPMENT;
		((EquipmentClass)ItemData [id]).EquipmentType = EquipmentClass.EquipmentTypes.ARMOR;
		((EquipmentClass)ItemData [id]).DEF_UP = 5;

		id = 201;
		ItemData [id] = new EquipmentClass ();
		ItemData [id].Name = "祭司私服";
		ItemData [id].Description = "祭司平時穿著的衣服";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 3;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.EQUIPMENT;
		((EquipmentClass)ItemData [id]).EquipmentType = EquipmentClass.EquipmentTypes.ARMOR;
		((EquipmentClass)ItemData [id]).DEF_UP = 5;

		id = 299;
		ItemData [id] = new EquipmentClass ();
		ItemData [id].Name = "炎準神甲";
		ItemData [id].Description = "父王生前為荷魯斯準備的衣甲";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 6;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.EQUIPMENT;
		((EquipmentClass)ItemData [id]).EquipmentType = EquipmentClass.EquipmentTypes.ARMOR;
		((EquipmentClass)ItemData [id]).HP_UP = 100;
		((EquipmentClass)ItemData [id]).DEF_UP = 35;

		id = 300;
		ItemData [id] = new EquipmentClass ();
		ItemData [id].Name = "草鞋";
		ItemData [id].Description = "輕便的鞋子";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 3;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.EQUIPMENT;
		((EquipmentClass)ItemData [id]).EquipmentType = EquipmentClass.EquipmentTypes.SHOES;
		((EquipmentClass)ItemData [id]).SPD_UP = 5;

		id = 301;
		ItemData [id] = new EquipmentClass ();
		ItemData [id].Name = "戰靴";
		ItemData [id].Description = "防禦偏高，但缺少靈巧的靴子";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 3;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.EQUIPMENT;
		((EquipmentClass)ItemData [id]).EquipmentType = EquipmentClass.EquipmentTypes.SHOES;
		((EquipmentClass)ItemData [id]).DEF_UP = 10;
		((EquipmentClass)ItemData [id]).SPD_UP = -5;

		id = 401;
		ItemData [id] = new EquipmentClass ();
		ItemData [id].Name = "星月魔鍊";
		ItemData [id].Description = "星光與月光交融後的魔石鍊";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 6;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.EQUIPMENT;
		((EquipmentClass)ItemData [id]).EquipmentType = EquipmentClass.EquipmentTypes.RING;
		((EquipmentClass)ItemData [id]).Mp_Costdown_Rate = 1.0;

		id = 402;
		ItemData [id] = new EquipmentClass ();
		ItemData [id].Name = "流星護符";
		ItemData [id].Description = "蘊含著飛越一切的能力";
		ItemData [id].ItemID = id;
		ItemData [id].Rarity = 6;
		ItemData [id].Throwable = true;
		ItemData [id].ItemType = BaseItemClass.ItemTypes.EQUIPMENT;
		((EquipmentClass)ItemData [id]).EquipmentType = EquipmentClass.EquipmentTypes.RING;
		((EquipmentClass)ItemData [id]).SPD_UP = 80;
		((EquipmentClass)ItemData [id]).SPD_Raise = 1;

	}
		
}
