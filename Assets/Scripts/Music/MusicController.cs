using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	public static MusicController current;

	AudioSource bgm;

	private float originVolume;
	private WorkTypes workType;

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
}
