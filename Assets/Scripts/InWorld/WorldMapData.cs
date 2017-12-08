using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WorldMapData : MonoBehaviour {
	
	public static WorldMapData current;

	private GlobalData globalData;
	private Scene inScene;

	// GameObject reference data
	public GameObject mainCamera;
	public GameObject mainActorObj;
	public GameObject[] monsterObj;



	// Player location class Data
	private Vector3 mainActorPoint;




	//Monster class Data
	private bool [] isMonsterAlive;




	//Items Location


	void Awake(){
		current = this;
	}


	// When Map Loading , Set all Unit position , and item picks
	void Start () {
		globalData = GlobalData.Instance;

		// initialize reffence variable
		inScene = SceneManager.GetActiveScene ();
		isMonsterAlive = new bool[monsterObj.Length];
		for (int i = 0; i < monsterObj.Length; i++) {
			isMonsterAlive [i] = true;
		}

		if (globalData.InMapName == inScene.name) {
			
			//update Map information
			SetPlayerPosition ();
			SetMonsterPosition ();

			//change reference to self
			globalData.InMapData = this;

			//current refference update
			mainActorObj.transform.position = mainActorPoint;
			for (int i = 0; i < monsterObj.Length; i++) {
				Debug.Log ("id:"+i + ", is " + isMonsterAlive[i]);
				if (isMonsterAlive[i] == false) {
					monsterObj [i].SetActive (false);
				}
			}
			Debug.Log ("Set Finished");

		} else {
			globalData.InMapName = inScene.name;
			globalData.InMapData = this;
		}

	}

	void SetPlayerPosition(){
		mainActorPoint = globalData.InMapData.MainActorPoint;

	}

	void SetMonsterPosition(){
		for (int i = 0; i < monsterObj.Length; i++) {
			if (globalData.InMapData.IsMonsterAlive[i] == false) {
				isMonsterAlive[i] = false;
			}
		}
	}


	void Update(){
		//if (GameCamera.IsAimCharactor) {
			mainActorPoint = mainActorObj.transform.position;
		//}
	}


	// setter & getter

	public Vector3 MainActorPoint{
		get { return mainActorPoint;}
	}

	public bool [] IsMonsterAlive {
		get { return isMonsterAlive;}
	}
}
