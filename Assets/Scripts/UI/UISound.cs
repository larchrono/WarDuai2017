using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class UISound : MonoBehaviour , ISelectHandler {
	static public bool isPlayed = false;
	static float miniPlayTime = 0.1f;

	public void OnSelect(BaseEventData eventData)
	{
		PlaySound ();
	}

	public void PlaySound(){
		if (WorldMenuController.current == null)
			return;
		if (WorldMenuController.current.noSelect == true)
			StartCoroutine (SetNoSelectSound ());
		else {
			if (!isPlayed) {
				isPlayed = true;
				SoundCollect.current.SNDSelect.Play ();
				StartCoroutine (RevokePlay ());
			} else {
				StartCoroutine (RevokePlay ());
			}
		}
	}

	IEnumerator SetNoSelectSound(){
		yield return new WaitForSecondsRealtime (0.2f);
		WorldMenuController.current.noSelect = false;
	}

	IEnumerator RevokePlay(){
		yield return new WaitForSecondsRealtime (miniPlayTime);
		isPlayed = false;
	}
}
