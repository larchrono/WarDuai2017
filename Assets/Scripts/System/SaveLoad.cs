using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;

public class SaveLoad : MonoBehaviour {

	public static DataMemory nowWorkingMemory;
	public static int memorySlot;
	static ISaveGameSerializer encoder = new SaveGameJsonSerializer ();

	public static void M_SaveGame(){
		string identifier = "slot" + memorySlot.ToString () + ".txt";

		SaveGame.Save<DataMemory> ( identifier, nowWorkingMemory, encoder );
	}

	public static DataMemory M_LoadGame(){
		string identifier = "slot" + memorySlot.ToString () + ".txt";

		nowWorkingMemory = SaveGame.Load<DataMemory> (identifier,new DataMemory (),encoder );

		return nowWorkingMemory;
	}
}
