using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTool {

	public static void SaveNowGlobalToFile(int slot){
		DataMemory memo = new DataMemory ();
		SaveLoad.nowWorkingMemory = memo;

		memo.chapter = GlobalData.Instance.chapter;
		memo.locat = GlobalData.Instance.locat;

		memo.playedTime = GlobalData.Instance.playTime;
		memo.gold = GlobalData.Instance.goldCarry;

		memo.inventoryConsumable = GlobalData.Instance.InventoryConsumable;
		memo.inventoryEquipment = GlobalData.Instance.InventoryEquipment;
		memo.inventoryPrecious = GlobalData.Instance.InventoryPrecious;

		memo.savedTime = DateTime.Now;

		//All actors info will be saved to memo
		ActorSetup.SaveToMemory ();

		memo.savePosition = GlobalData.Instance.PositionBeforeBattle;
		memo.cameraRotateAngle = GlobalData.Instance.WorldCameraRotateAngle;

		memo.movie_0_active = GlobalData.Instance.movie_0_active;
		memo.movie_1_active = GlobalData.Instance.movie_1_active;
		memo.movie_2_active = GlobalData.Instance.movie_2_active;
		memo.movie_3_active = GlobalData.Instance.movie_3_active;
		memo.isDarkFireGateShow = GlobalData.Instance.isDarkFireGateShow;

		SaveLoad.memorySlot = slot;
		SaveLoad.M_SaveGame ();
	}
}
