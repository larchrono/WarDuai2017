using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealBarTest : MonoBehaviour {

	public GameObject[] buttons;
	public GameObject[] target ;
	private List<GameObject> hps;

	public GameObject HPPrefabs;

	private GameObject refPoint;

	public GameObject shaderTestImage;

	void Start(){
		HPPrefabs = Resources.Load ("Prefabs/UI/Battle/BattleUITag") as GameObject;

		hps = new List<GameObject> ();
		foreach (GameObject obj in target) {
			foreach (Transform child in obj.transform) {
				if (child.name == "overhead ref") {
					refPoint = child.gameObject;
					GameObject hp = Instantiate (HPPrefabs);
					hp.GetComponent<SpriteReference> ().refLocation = refPoint;

					hp.transform.SetParent(GameObject.Find("LayerTag").transform,false);
					hps.Add (hp);
					break;
				}
			}
		}
	}


	// Update is called once per frame
	void Update () {
		
		foreach (GameObject obj in hps) {
			Vector2 pos = Camera.main.WorldToScreenPoint (obj.GetComponent<SpriteReference>().refLocation.transform.position);
			obj.GetComponent<RectTransform> ().position = pos;
		}
			
		//print(shaderTestImage.GetComponent<Image> ().material.GetFloat("_SparkInterval"));
	}
}
