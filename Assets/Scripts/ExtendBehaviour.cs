using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtendBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	protected void LoadSceneAsyncDelay(string name ,float delaySec){
		StartCoroutine (LoadSceneAsyncDelayWork (name,delaySec));
	}

	protected void LoadSceneAsyncDelay(int id ,float delaySec){
		StartCoroutine (LoadSceneAsyncDelayWork (id,delaySec));
	}
	IEnumerator LoadSceneAsyncDelayWork(string name ,float delaySec){
		yield return new WaitForSeconds (delaySec);
		SceneManager.LoadSceneAsync (name);
	}
	IEnumerator LoadSceneAsyncDelayWork(int id ,float delaySec){
		yield return new WaitForSeconds (delaySec);
		SceneManager.LoadSceneAsync (id);
	}

	protected GameObject InstantiateChild(GameObject original,GameObject parent){
		GameObject temp = Instantiate (original);
		temp.transform.SetParent (parent.transform,false);
		return temp;
	}

	protected GameObject InstantiateUI(GameObject original){
		GameObject temp = Instantiate (original);
		if(GameObject.Find("Canvas") != null)
			temp.transform.SetParent (GameObject.Find("Canvas").GetComponent<RectTransform>(),false);
		
		return temp;
	}
}
