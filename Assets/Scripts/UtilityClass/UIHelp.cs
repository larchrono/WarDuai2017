using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHelp {

	public static void CreateUIInCanvas(GameObject prefab){
		GameObject temp = GameObject.Instantiate (prefab) as GameObject;
		temp.transform.SetParent(GameObject.Find("Canvas").transform,false);
	}

	public static void CreateUIInCanvas(GameObject prefab, Vector3 pos){
		GameObject temp = GameObject.Instantiate (prefab) as GameObject;
		temp.transform.position = pos;
		temp.transform.SetParent(GameObject.Find("Canvas").transform,false);
	}

	public static void CreateUIInChild(GameObject prefab,GameObject parent){
		GameObject temp = GameObject.Instantiate (prefab) as GameObject;
		temp.transform.SetParent(parent.transform,false);
	}

	public static void CreateUIInChild(GameObject prefab,GameObject parent, Vector3 pos){
		GameObject temp = GameObject.Instantiate (prefab) as GameObject;
		temp.transform.position = pos;
		temp.transform.SetParent(parent.transform,false);
	}


	public static void CreateDamageTextCanvas(GameObject prefab, Vector2 pos , int amount){
		GameObject temp = GameObject.Instantiate (prefab) as GameObject;
		temp.GetComponent<TextDamage>().text.GetComponent<Text> ().text += amount;
		temp.transform.SetParent(GameObject.Find("Canvas").transform,false);
		temp.GetComponent<RectTransform>().position = pos;

		float random_x = Random.Range (-0.03f,0.03f);
		float random_y = Random.Range (-0.03f,0.03f);
		temp.GetComponent<RectTransform> ().anchorMin = new Vector2 (0.5f + random_x , 0.5f + random_y);
		temp.GetComponent<RectTransform> ().anchorMax = new Vector2 (0.5f + random_x , 0.5f + random_y);
		GameObject.Destroy (temp,3.0f);
	}
}
