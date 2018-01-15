using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class FirstMovie : MonoBehaviour {

	public VideoPlayer vp;

	// Use this for initialization
	void Start () {
		vp.loopPointReached += EndReached;
	}
	
	void EndReached(UnityEngine.Video.VideoPlayer vp)
	{
		SceneManager.LoadSceneAsync ("Map_World_1");
	}
}
