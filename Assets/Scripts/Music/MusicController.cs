using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	public static MusicController current;

	AudioSource bgm;

	private float originVolume;
	private WorkTypes workType;

	int invokeTime = 0;

	private enum WorkTypes
	{
		STAY,
		FADE_OUT,
		FADE_INT,
	}

	void Awake(){
		current = this;
	}

	// Use this for initialization
	void Start () {
		bgm = GetComponent<AudioSource> ();
		if (bgm != null)
			originVolume = bgm.volume;

		if (GlobalData.Instance.GameInState == GlobalData.GameStates.IN_WORLD){
			bgm.time = GlobalData.Instance.worldBGMTime;
			bgm.volume = 0;
			InvokeRepeating ("FadeIn", 0.25f, 0.25f);
		}
		bgm.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (workType == WorkTypes.FADE_OUT && bgm != null) {
			bgm.volume = Mathf.Max(0, bgm.volume -  1f * Time.deltaTime);
			if (bgm.volume <= 0)
				workType = WorkTypes.STAY;
		}
	}

	public void FadeOut(){
		workType = WorkTypes.FADE_OUT;
	}

	void FadeIn(){
		bgm.volume = Mathf.Min (bgm.volume + 0.05f, originVolume);
		invokeTime++;
		if(invokeTime > 20)
			CancelInvoke ();
	}

	public AudioSource BGM {
		get { return bgm; }
	}
}
