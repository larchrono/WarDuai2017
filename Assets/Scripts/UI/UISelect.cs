using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class UISelect : MonoBehaviour , ISelectHandler , IDeselectHandler {

	private static GameObject arrowObj;

	private static GameObject nowArrow;

	public float location = 0.1f;


	void Awake(){
		if (arrowObj == null)
			arrowObj = Resources.Load ("Prefabs/UI/ImageArrow") as GameObject;
	}

	public void OnSelect(BaseEventData eventData)
	{
		if(nowArrow != null)
			Destroy (nowArrow);
		nowArrow = Instantiate (arrowObj, Vector3.zero, Quaternion.identity) as GameObject;
		nowArrow.transform.SetParent (this.GetComponent<RectTransform>(),false);
		nowArrow.GetComponent<RectTransform> ().anchorMin = new Vector2 (location,0.5f);
		nowArrow.GetComponent<RectTransform> ().anchorMax = new Vector2 (location,0.5f);

		if (GlobalData.Instance.GameInState == GlobalData.GameStates.IN_WORLD && GetComponent<UISound> () == null)
			gameObject.AddComponent<UISound> ().PlaySound();
	}

	public void OnDeselect(BaseEventData eventData)
	{
		if(nowArrow != null)
			Destroy (nowArrow);
	}

}
