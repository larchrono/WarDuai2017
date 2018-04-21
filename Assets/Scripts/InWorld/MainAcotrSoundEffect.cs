using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAcotrSoundEffect : MonoBehaviour {

	public GameObject _footDust;

	Rigidbody _rigibody;
	WorldActorAct _actScript;
	AudioSource footstep;

	bool once = false;
	public float o_volume;
	public float footTimeReset = 0.35f;


	// Use this for initialization
	void Start () {
		_rigibody = GetComponent<Rigidbody> ();
		_actScript = GetComponent<WorldActorAct> ();
		footstep = SoundCollect.current.SNDWalkSand;
		o_volume = footstep.volume;
	}
	
	// Update is called once per frame
	void Update () {
		if (_actScript.isGround && _rigibody.velocity.magnitude > 2f && once == false) {
			once = true;
			footstep.volume = Random.Range (o_volume - 0.1f, o_volume + 0.1f);
			footstep.pitch = Random.Range (0.8f, 1.1f);
			footstep.Play ();
			Invoke ("ResetFootStep", footTimeReset);
			Destroy (Instantiate (_footDust, transform.position,Quaternion.identity), 3f);
		}
	}

	void ResetFootStep(){
		once = false;
	}
}
