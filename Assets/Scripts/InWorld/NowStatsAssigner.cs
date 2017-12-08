using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowStatsAssigner : MonoBehaviour {

	public GameObject mainActor;
	private bool movie_0_active;
	public GameObject movie_0;
	public GameObject movie_1;
	public GameObject movie_3;

	public GameObject darkFireGate;

	public GameObject anubisModel;

	// Use this for initialization
	void Awake () {
		
	}

	IEnumerator Start(){

		Debug.Log ("Global Data Gem count: " + GlobalData.Instance.InPanelGems.Count);
		Debug.Log ("Global Data Id: " + GlobalData.Instance.GetInstanceID());

		foreach(StandardActor act in GlobalData.Instance.ActiveActors){
			if (act.HP <= 0)
				act.HP = 1;
		}

		darkFireGate.SetActive (GlobalData.Instance.isDarkFireGateShow);

		movie_0_active = GlobalData.Instance.movie_0_active;
		movie_0.SetActive (movie_0_active);
		movie_1.SetActive (GlobalData.Instance.movie_1_active);
		movie_3.SetActive (GlobalData.Instance.movie_3_active);
		mainActor.transform.position = GlobalData.Instance.PositionBeforeBattle;
		GameCamera.IsAimCharactor = true;

		if (GlobalData.Instance.movie_1_active == true) {
			anubisModel.GetComponent<Animator> ().Play ("death");


		}

		GlobalData.Instance.Invincible = true;
		yield return new WaitForSeconds (3.0f);
		GlobalData.Instance.Invincible = false;



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
