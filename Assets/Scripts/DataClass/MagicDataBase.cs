using UnityEngine;
using System.Collections;

public static class MagicDataBase {

	public static MagicClass[] MagicData;

	static MagicDataBase(){

		int id;

		MagicData = new MagicClass[1024];


		// Empty
		id = 0;
		MagicData [id] = new MagicClass ();
		MagicData [id].Name = "無";
		MagicData [id].Description = "";
		MagicData [id].MpCost = 0;


		// Horus List
		id = 11;
		MagicData [id] = new MagicClass ();
		MagicData [id].Name = "熱旋風";
		MagicData [id].Description = "將闇元素轉換為火元素";
		MagicData [id].Icon = GameResource.Sprite.BTN_Horus_Magic_1;
		MagicData [id].MpCost = 6;
		MagicData [id].Effect = MagicClass.MagicEffect.CONVERT;
		MagicData [id].FromType = GemData.GemType.DARK;
		MagicData [id].ToType = GemData.GemType.FIRE;

		id = 12;
		MagicData [id] = new MagicClass ();
		MagicData [id].Name = "烈火粉塵";
		MagicData [id].Description = "將元素表最上一列轉換為火元素";
		MagicData [id].Icon = GameResource.Sprite.BTN_Horus_Magic_1;
		MagicData [id].MpCost = 12;
		MagicData [id].Effect = MagicClass.MagicEffect.CONVERT_ROW;
		MagicData [id].RowNumber.Add (0);
		MagicData [id].ToType = GemData.GemType.FIRE;

		id = 13;
		MagicData [id] = new MagicClass ();
		MagicData [id].Name = "炎準旋風陣";
		MagicData [id].Description = "將所有元素轉化為火、光";
		MagicData [id].Icon = GameResource.Sprite.BTN_Horus_Magic_1;
		MagicData [id].MpCost = 12;
		MagicData [id].Effect = MagicClass.MagicEffect.CONVERT_ALL;
		MagicData [id].ToTypes.Add(GemData.GemType.FIRE);
		MagicData [id].ToTypes.Add(GemData.GemType.SUN);


		// Nephthys List
		id = 21;
		MagicData [id] = new MagicClass ();
		MagicData [id].Name = "極凍凝結";
		MagicData [id].Description = "將光元素轉換為水元素";
		MagicData [id].Icon = GameResource.Sprite.BTN_Nephthys_Magic_1;
		MagicData [id].MpCost = 6;
		MagicData [id].Effect = MagicClass.MagicEffect.CONVERT;
		MagicData [id].FromType = GemData.GemType.SUN;
		MagicData [id].ToType = GemData.GemType.WATER;

		id = 22;
		MagicData [id] = new MagicClass ();
		MagicData [id].Name = "冰霜破";
		MagicData [id].Description = "隨機生成6個水元素";
		MagicData [id].Icon = GameResource.Sprite.BTN_Nephthys_Magic_2;
		MagicData [id].MpCost = 6;
		MagicData [id].AddAmount = 6;
		MagicData [id].Effect = MagicClass.MagicEffect.ADD;
		MagicData [id].ToType = GemData.GemType.WATER;

		id = 23;
		MagicData [id] = new MagicClass ();
		MagicData [id].Name = "冰牙追月";
		MagicData [id].Description = "將前3列元素轉換為水元素";
		MagicData [id].Icon = GameResource.Sprite.BTN_Nephthys_Magic_2;
		MagicData [id].MpCost = 8;
		MagicData [id].Effect = MagicClass.MagicEffect.CONVERT_COLUMN;
		MagicData [id].ColumnNumber.Add (0);
		MagicData [id].ColumnNumber.Add (1);
		MagicData [id].ColumnNumber.Add (2);
		MagicData [id].ToType = GemData.GemType.WATER;

		id = 24;
		MagicData [id] = new MagicClass ();
		MagicData [id].Name = "冰河爆裂破";
		MagicData [id].Description = "將所有元素轉化為水";
		MagicData [id].Icon = GameResource.Sprite.BTN_Nephthys_Magic_2;
		MagicData [id].MpCost = 12;
		MagicData [id].Effect = MagicClass.MagicEffect.CONVERT_ALL;
		MagicData [id].ToTypes.Add(GemData.GemType.WATER);
	}
}