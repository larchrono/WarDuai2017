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
	public AudioSource SNDWalkSand;
	public AudioSource SNDBattleReady;
	public AudioSource SNDMenuClose;
	public AudioSource SNDMenuOpen;
	public AudioSource SNDStartGame;
	public AudioSource SNDExpGet;
	public AudioSource SNDLevelUp;

	private bool playingLock = false;

	void Awake(){
		current = this;

		sounds = GetComponents<AudioSource> ();
		SNDSelect = sounds [0];
		SNDSubmit = sounds [1];
		SNDEncounting = sounds [2];
		SNDHit = sounds [3];
		SNDWalkSand = sounds [4];
		SNDBattleReady = sounds [5];
		SNDMenuClose = sounds [6];
		SNDMenuOpen = sounds [7];
		SNDStartGame = sounds [8];
		SNDExpGet = sounds [9];
		SNDLevelUp = sounds [10];
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

	public void Play(int num){
		if(num < sounds.Length)
			sounds [num].Play ();
	}
}
