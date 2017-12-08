using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreator : MonoBehaviour {

	public GameObject mainActor;

	public GameObject worldEnemy;

	public GameObject monsGroups;
	public float createTime;

	private float timer;

	private float maxCreateDistance = 20;
	private float minCreateDistance = 5;

	// Use this for initialization
	void Start () {
		timer = 0;

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > createTime && GlobalData.Instance.GameInState == GlobalData.GameStates.IN_WORLD) {
			timer = 0;
			float range = Random.Range (minCreateDistance,maxCreateDistance);
			Quaternion rotationEuler = Quaternion.Euler (0,Random.Range(0,360),0);
			Vector3 pos = rotationEuler * new Vector3 (range,1.5f,0) + mainActor.transform.position;
			GameObject obj = Instantiate (worldEnemy);
			obj.transform.position = pos;
			obj.transform.SetParent (monsGroups.transform,false);
			obj.transform.Rotate (0,Random.Range(0,360),0);
			obj.GetComponent<WorldMonsterAct> ().monsterNum = Random.Range (1, 2);
		}
	}
}
