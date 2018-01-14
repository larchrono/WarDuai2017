using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Debug.Log ("ItemNum:"+GlobalData.Instance.InventoryConsumable.Count);
	}

	public void ChangeIC(){
		GlobalData.Instance.InventoryConsumable.Add(56);
	}

	public void BTN_LoadGame(){
		SaveLoad.memorySlot = 0;
		DataMemory memo = SaveLoad.M_LoadGame ();

		GlobalData.Instance.goldCarry = memo.gold;
		GlobalData.Instance.totalTime = memo.playedTime;
		GlobalData.Instance.InventoryConsumable = memo.inventoryConsumable;
	}

	public void BTN_SaveGame(){
		DataMemory memo = new DataMemory ();
		SaveLoad.nowWorkingMemory = memo;

		memo.gold = GlobalData.Instance.goldCarry;
		memo.playedTime = GlobalData.Instance.totalTime;
		memo.inventoryConsumable = GlobalData.Instance.InventoryConsumable;

		SaveLoad.memorySlot = 0;
		SaveLoad.M_SaveGame ();
	}
}
