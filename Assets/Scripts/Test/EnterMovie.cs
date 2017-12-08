using UnityEngine;
using System.Collections;

public class EnterMovie : MonoBehaviour {

	Animator animator;
	// Use this for initialization
	IEnumerator Start () {
		animator = GetComponent<Animator>();
		animator.Play ("standing_up");
		yield return new WaitForSeconds (0.1f);
		animator.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			animator.enabled = true;
		}
	}
}
