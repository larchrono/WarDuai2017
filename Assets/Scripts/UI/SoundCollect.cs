using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCollect : MonoBehaviour {

	public static SoundCollect current;

	public AudioSource[] sounds;

	public AudioSource SNDSelect;
	public AudioSource SNDSubmit;
	public AudioSource SNDEncounting;
	public AudioSource SNDHit;

	private bool playingLock = false;

	void Awake(){
		current = this;

		sounds = GetComponents<AudioSource> ();
		SNDSelect = sounds [0];
		SNDSubmit = sounds [1];
		SNDEncounting = sounds [2];
		SNDHit = sounds [3];
	}

	public void Play(AudioSource src){
		if (!playingLock) {
			playingLock = true;
			src.Play ();
			StartCoroutine (ReleaseLock());
		}
	}

	IEnumerator ReleaseLock(){
		yield return new WaitForSeconds (0.1f);
		playingLock = false;
	}
}
