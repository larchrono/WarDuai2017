using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRunlineReference : MonoBehaviour {

	public GameObject Icon;

	public void SetIconImageByActor(BattleActor obj){
		Icon.GetComponent<Image> ().sprite = obj.Data.BattleIcon;
	}
}
