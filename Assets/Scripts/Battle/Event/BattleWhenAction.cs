using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleWhenAction : MonoBehaviour {

	public GameObject testMsg;

	// Use this for initialization
	void Start () {
		BattleMain.EVENT_UNIT_ENTER_ACTION += OnOrderAttack;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnOrderAttack(object sender,ActionUnitArgs args){
		StartCoroutine (AttackMsg (args));
		args.triggerUnit.TimeSpeed = args.triggerUnit.Data.SPD / 5.0f;
	}

	IEnumerator AttackMsg(ActionUnitArgs args){
		testMsg.GetComponent<Text>().text = "> " + args.triggerUnit.Data.ActorName + "的攻擊!!";
		testMsg.SetActive (true);
		yield return new WaitForSeconds (1.5f);
		testMsg.SetActive (false);
	}
}
