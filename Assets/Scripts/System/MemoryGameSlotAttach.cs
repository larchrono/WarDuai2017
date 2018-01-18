using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameSlotAttach : MonoBehaviour {

	string t_chapter = "章節 ";
	string t_level = "等級 ";
	//string t_locat = "";
	string t_playTime = "遊戲時間　";
	//string t_saveDate = "";
	//string t_saveTime = "";

	public DataMemory Memory;

	public Text NoData;
	public Text Chapter;
	public Text Level;
	public Text Locat;
	public Text PlayTime;
	public Text SaveDate;
	public Text SaveTime;

	public Image[] Slots;

	// Use this for initialization
	void Awake () {
		NoData.gameObject.SetActive (true);
		Chapter.gameObject.SetActive (false);
		Level.gameObject.SetActive (false);
		Locat.gameObject.SetActive (false);
		PlayTime.gameObject.SetActive (false);
		SaveDate.gameObject.SetActive (false);
		SaveTime.gameObject.SetActive (false);

		Slots[0].gameObject.SetActive (false);
		Slots[1].gameObject.SetActive (false);
		Slots[2].gameObject.SetActive (false);
		Slots[3].gameObject.SetActive (false);
		Debug.Log ("Slot Menu Set false");
	}

	public void SetupMemory(DataMemory memo){
		Memory = memo;
		int level = memo.actors [0].level;
		SetupData (memo.chapter,level,memo.locat,memo.playedTime,memo.savedTime);
		SetupActor (memo.actors.Count);
		Debug.Log ("Slot Menu Setup OK");
	}

	public void SetupData(int cht,int level,string locat,float playTime,DateTime savedTime){
		// close NoData Message
		NoData.gameObject.SetActive (false);

		//Setup datas
		Chapter.text = t_chapter + cht;
		Level.text = t_level + level;
		Locat.text = locat;
		PlayTime.text = t_playTime + TimeTool.ConvertFloatToTimeString(playTime);
		SaveDate.text = TimeTool.ConvertDateToChtDay (savedTime);
		SaveTime.text = TimeTool.ConvertDateToHHMMSS (savedTime);

		Chapter.gameObject.SetActive (true);
		Level.gameObject.SetActive (true);
		Locat.gameObject.SetActive (true);
		PlayTime.gameObject.SetActive (true);
		SaveDate.gameObject.SetActive (true);
		SaveTime.gameObject.SetActive (true);
	}

	public void SetupActor(int num){
		for (int i = 0; i < num; i++) {
			Slots [i].gameObject.SetActive (true);
			Slots [i].sprite = GlobalData.Instance.Actors[i].Icon;
		}
	}
}
