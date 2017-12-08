using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orders : MonoBehaviour {


	public GameObject triggerUnit;
	public GameObject targetUnit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown ("a")) {
			print ("attack");
			//triggerUnit.GetComponent<BattleActorUpdate> ().IssueTargetOrder ("attack",targetUnit);
		}

		if (Input.GetKeyDown ("r")) {
			//triggerUnit.GetComponent<BattleActorUpdate> ().SetUnitPositionLoc (new Vector3(0,0,3));
			//triggerUnit.GetComponent<BattleActorUpdate> ().IssueImmediateOrder ("stop");
		}
	}

}
