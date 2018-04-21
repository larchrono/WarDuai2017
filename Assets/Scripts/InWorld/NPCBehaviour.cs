using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour {

	public string npcName;
	[TextArea(3,10)]
	public string message;
	public Sprite sprite;



	Quaternion _targetRotation ;
	float rSpeed = 5f;

	GameObject npcArrow;
	Vector3 _arrowPosition;
	float _arrowAlpha = 0;


	public void ActiveArrow(){
		_arrowAlpha = 1;
	}

	public void FaceToObject(GameObject target){
		
		Vector3 move_direction = target.transform.position - transform.position;
		if(move_direction != Vector3.zero) _targetRotation = Quaternion.LookRotation (move_direction);
	}

	public void Talk(){
		GlobalData.Instance.GameInState = GlobalData.GameStates.IN_TALK;
		Transmission.FromUnit (sprite, npcName, message, 1, false, 0, delegate() {
			GlobalData.Instance.GameInState = GlobalData.GameStates.IN_WORLD;
		});
	}

	// Use this for initialization
	void Start () {
		npcArrow = Instantiate (GameResource.Prefab.UINpcArrow,GameObject.Find("Canvas").transform);
		npcArrow.transform.SetSiblingIndex (0);

		foreach (Transform child in gameObject.transform) {
			if (child.name != "overhead ref")
				continue;
			_arrowPosition = child.position;

			break;
		}

		_targetRotation = transform.rotation;
		npcArrow.GetComponent<CanvasGroup> ().alpha = 0;
	}

	// Update is called once per frame
	void Update () {
		
		//Face work
		if (Quaternion.Angle (transform.rotation, _targetRotation) > 0.1) {
			transform.rotation = Quaternion.Slerp (transform.rotation, _targetRotation, rSpeed * Time.deltaTime);
		}

		//Arrow Work
		if(_arrowAlpha > 0)
			_arrowAlpha -= Time.deltaTime * 2 ;
		npcArrow.GetComponent<CanvasGroup> ().alpha = _arrowAlpha;
		Vector2 pos = Camera.main.WorldToScreenPoint (_arrowPosition);
		npcArrow.GetComponent<RectTransform> ().position = pos;

	}
		

	bool NearPlayer(Vector3 center, float radius) {
		bool isPlayerNear = false;
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		for (int i = 0; i < hitColliders.Length; i++) {
			if (hitColliders [i].tag != "Player")
				continue;
			isPlayerNear = true;

		}
		return isPlayerNear;
	}

}
