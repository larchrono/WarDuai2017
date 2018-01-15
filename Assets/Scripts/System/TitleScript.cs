using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class TitleScript : MonoBehaviour {

	public GameObject pressStartButton;
	public GameObject mainPanel;
	public GameObject continueButton;
	public GameObject loadGamePanel;

	// Use this for initialization
	void Start () {
		if (loadGamePanel != null)
			loadGamePanel.SetActive (false);

		//Debug.Log(DateTime.Now.ToString ("yyyy") + "年" + DateTime.Now.ToString ("MM") + "月" + DateTime.Now.ToString ("dd") + "日");
		//Debug.Log(DateTime.Now.ToString ("HH:mm:ss"));

		//Debug.Log (TimeTool.ConvertFloatToTimeString(963909));
		//Debug.Log ("ItemNum:"+GlobalData.Instance.InventoryConsumable.Count);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("B")) {
			OnCancel ();
		}
		if (Input.GetMouseButtonDown (1)) {
			OnCancel ();
		}
	}

	public void BTN_NewGame(){
		GameCamera.FadeOut (1.0f,10f,delegate() {
			SceneManager.LoadSceneAsync ("PreMovie");
		});
	}

	public void BTN_LoadGame(){
		CheckSaveDataList ();
	}

	public void BTN_SaveGame(){
		SaveTool.SaveNowGlobalToFile (0);
	}

	public void OnCancel()
	{
		pressStartButton.SetActive (false);
		loadGamePanel.SetActive (false);
		mainPanel.SetActive (true);
		EventSystem.current.SetSelectedGameObject (continueButton);
	}

	public void CheckSaveDataList(){
		for (int i = 0; i < 5; i++) {
			SaveLoad.memorySlot = i;
			DataMemory memo = SaveLoad.M_LoadGame ();
			MemoryGameSlotAttach slot = loadGamePanel.GetComponent<LoadGamePanelAttach> ().memorySlot [i].GetComponent<MemoryGameSlotAttach> ();

			//Save is null when chapter is zero . when not setup , it will be null
			if (memo.chapter != 0)
				slot.SetupMemory (memo);
				
		}
	}

	public void LoadSlotGame(MemoryGameSlotAttach data){
		if (data.Memory != null) {
			GlobalData.Instance.LoadMemory (data.Memory);
			GameCamera.FadeOut (1.0f,10f,delegate() {
				SceneManager.LoadSceneAsync ("Map_World_1");
			});
		}
	}


}
