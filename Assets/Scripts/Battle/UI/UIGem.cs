using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGem : MonoBehaviour {

	public Image img;
	public GemData.GemType type;


	public int moveColumn = 0;
	public bool haveToRemove = false;



	public RectTransform imageRect;

	public float width;
	public float height;

	public enum GemsState { Create, Remove , Stay};
	public GemsState state;

	private float gemMoveSpeed = 3f;

	//when to Start remove , in our case , remove is immediate
	private float removeTime = 0f;
	private float timer = 0;
	private float removeAlpha = 4f;


	void Update()
	{
		
		switch (state)
		{
		//Create or Change case 都會進入 MoveAni 函式
		case GemsState.Create:
			MoveAni();
			break;
		case GemsState.Remove:
			RemoveAni();
			break;
		}
	}

	public void RemoveAni()
	{
		timer += Time.deltaTime;
		if (timer > removeTime)
		{
			if (img.GetComponent<CanvasRenderer>().GetAlpha() > 0)
			{
				img.GetComponent<CanvasRenderer> ().SetAlpha (img.GetComponent<CanvasRenderer>().GetAlpha() - removeAlpha * Time.deltaTime);
			}
			else
			{
				state = GemsState.Create;
				timer = 0;
			}

		}

	}
	public void SetAniPos(int count, GemsState state)
	{
		img.GetComponent<RectTransform> ().anchorMin = new Vector2 (0 + count, 0);
		img.GetComponent<RectTransform> ().anchorMax = new Vector2 (1 + count, 1);
		this.state = state;
	}

	public void MoveAni()
	{
		img.GetComponent<RectTransform> ().anchorMin = Vector2.MoveTowards(img.GetComponent<RectTransform> ().anchorMin, Vector2.zero, gemMoveSpeed * Time.deltaTime);
		img.GetComponent<RectTransform> ().anchorMax = Vector2.MoveTowards(img.GetComponent<RectTransform> ().anchorMax, Vector2.one, gemMoveSpeed * Time.deltaTime);

		if (img.GetComponent<RectTransform> ().anchorMin == Vector2.zero)
			state = GemsState.Stay;
	}
	void Start()
	{

		//imageRect.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
		state = GemsState.Stay;
	}


}
