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

		SaveLoad.memorySlot = slot;
		SaveLoad.M_SaveGame ();
	}
}
