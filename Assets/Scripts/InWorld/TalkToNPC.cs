using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToNPC : MonoBehaviour {

	Transform _closestNpc = null;

	float _closetDistance = 2.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GlobalData.Instance.GameInState != GlobalData.GameStates.IN_WORLD || Time.timeScale == 0)
			return;
		
		_closestNpc = NearNpc (transform.position, _closetDistance);
		if (_closestNpc != null) {
			_closestNpc.GetComponent<NPCBehaviour> ().ActiveArrow ();
		}

		if (Input.GetButtonDown ("A") && _closestNpc != null && GlobalData.Instance.GameInState == GlobalData.GameStates.IN_WORLD) {

			_closestNpc.GetComponent<NPCBehaviour> ().FaceToObject (gameObject);
			_closestNpc.GetComponent<NPCBehaviour> ().Talk ();

			Debug.Log ("Talk to " + _closestNpc.name);
		}
	}

	Transform NearNpc(Vector3 center, float radius) {
		Transform bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;

		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		for (int i = 0; i < hitColliders.Length; i++) {
			if (hitColliders [i].tag != "Npc")
				continue;
			Vector3 directionToTarget = hitColliders[i].transform.position - transform.position;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if(dSqrToTarget < closestDistanceSqr)
			{
				closestDistanceSqr = dSqrToTarget;
				bestTarget = hitColliders[i].transform;
			}
		}
		return bestTarget;
	}
}
