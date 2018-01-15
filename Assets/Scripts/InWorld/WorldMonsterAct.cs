using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WorldMonsterAct : ExtendBehaviour {
	// Basic Global use
	private GlobalData globalData;
	private WorldMapData mapData;

	public int monsterType;
	public int monsterNum;

	// Use this for initialization
	IEnumerator Start () {
		globalData = GlobalData.Instance;
		mapData = WorldMapData.current;
		yield return new WaitForSeconds (2.0f);

		Destroy (GetComponent<Rigidbody>());
		GetComponent<CapsuleCollider> ().isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name != "MainActor")
			return;
		if (GlobalData.Instance.Invincible == false) {
			GlobalData.Instance.Invincible = true;

			//int myId = System.Array.IndexOf (mapData.monsterObj, gameObject);
			int myId = 0;
			Debug.Log ("Battle Monster Id :" + myId);

			GlobalData.Instance.BattleMonsterId = myId;
			GlobalData.Instance.BattleMonsterNumber = monsterNum;
			GlobalData.Instance.BattleMonsterType = monsterType;
			GlobalData.Instance.PositionBeforeBattle = other.gameObject.transform.position;

			mapData.mainCamera.GetComponent<WorldCameraAct> ().CreateBattleBlur ();
			GameCamera.FadeOut (0.5f, 20f);
			MusicController.current.FadeOut();
			SoundCollect.current.Play (SoundCollect.current.SNDEncounting);

			if (monsterType == 5)
				LoadSceneAsyncDelay ("BattleBoss", 1.0f);
			else
				LoadSceneAsyncDelay ("BattleSence", 1.0f);
		}
	}
}
