using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BattleMainCtl : MonoBehaviour {

	/*
	private GlobalData globalData;

	BattleActor[] bac;
	BattleActionTimeSystem actionTime;

	// Use this for initialization
	void Start () {
		globalData = GlobalData.Instance;



		int charactorNum = 4;
		int enemyNum = 4;
		int allActorNum = charactorNum + enemyNum;
		bac = new BattleActor[allActorNum];

		// Create User actor
		for (int i = 0; i < charactorNum; i++) {
			bac [i] = new BattleActor (i,BattleActor.ActorType.USER,1);

		}

		//Create enemy actor
		for (int i = charactorNum; i < allActorNum; i++) {
			bac [i] = new BattleActor (i,BattleActor.ActorType.NPC,1);
			// data , speed
		}

		//Create ActionTimeSystem
		// include UI
		actionTime = new BattleActionTimeSystem(bac);




	}
	
	// Update is called once per frame
	void Update () {

		actionTime.Pass ();

		if (actionTime.HasActorEvent ()) {

			if (actionTime.ActionType == BattleActionTimeSystem.ActiveActionType.Condition) {
				// if is user condition
				if (actionTime.ActorActionCondition ().actorType == BattleActor.ActorType.USER) {

					// pass time and wait for action
					actionTime.FreezePass ();

					//move camera to actor


					//show menu


					//if submit , HasActorEvent = false , and actime can pass;


				}

				// if is npc condition

				if (actionTime.ActorActionCondition ().actorType == BattleActor.ActorType.NPC) {

					// npc not need freeze

					//read the action of npc
					//when condition run , change actor speed

				}
			}

			if (actionTime.ActionType == BattleActionTimeSystem.ActiveActionType.Action) {

				// run actor submit action


			}
	
		}

		bool win = true;
		for (int i = 0; i < bac.Length; i++) {
			if (bac [i].Alive && bac[i].actorType == BattleActor.ActorType.NPC)
				win = false;
		}
		if (win) {

			//run win camera


			//show win Window;


			//exit battle scence

		}


		bool failed = true;
		for (int i = 0; i < bac.Length; i++) {
			if (bac [i].Alive && bac[i].actorType == BattleActor.ActorType.USER)
				failed = false;
		}
		if (failed) {

			//run win camera


			//show win Window;


			//exit battle scence

		}



		if (Input.GetButtonDown ("Cancel")) {
			//SceneManager.UnloadScene ("BattleDesert");
	//		globalData.InMapData.IsMonsterAlive[globalData.BattleMonsterId] = false;
			SceneManager.LoadSceneAsync (0);
			//SceneManager.
			//SceneManager.
		}

	}


	void CharactorStop(){

	}

	void CharactorMoveTo(){

	}

	void CreateText(){

	}

	void CreateIcon(){

	}

	void MoveIcon(){

	}

	void CreateSlider(){

	}
	*/
}
