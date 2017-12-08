using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelResult : ExtendBehaviour {

	public Text expGet;
	public Text goldGet;
	public GameObject panelSlots;
	public GameObject[] actorSlots;

	public GameObject actorSlotPrefabs;

	public GameObject levelUpPanel;
	public GameObject textLevelUp;

	float nowSlotPanelX = 1.1f;

	// Use this for initialization
	void Awake () {
		if (panelSlots != null) {
			panelSlots.GetComponent<RectTransform> ().anchorMin = new Vector2 (nowSlotPanelX, 0f);

			int totalActor = GlobalData.Instance.ActiveActors.Count;
			actorSlots = new GameObject[totalActor];
			for (int i = 0; i < totalActor; i++) {
				actorSlots [i] = InstantiateChild (actorSlotPrefabs, panelSlots);
				actorSlots [i].GetComponent<UIActorData> ().SetupData (GlobalData.Instance.ActiveActors [i]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (nowSlotPanelX > 0.1f) {
			nowSlotPanelX = Mathf.Lerp (0.1f,nowSlotPanelX,0.8f);
			panelSlots.GetComponent<RectTransform> ().anchorMin = new Vector2 (nowSlotPanelX, 0);
		}
	}

	public void PanelAddExpAnim(int slot,int mount){
		actorSlots [slot].GetComponent<UIActorData> ().AddExpAnim (mount);

	}

	public void LevelUp(string name){
		/*
		GameObject obj = Instantiate (textLevelUp);
		obj.transform.SetParent (levelUpPanel.transform,false);
		obj.GetComponent<Text> ().text = name + obj.GetComponent<Text> ().text;
		*/
	}
}
