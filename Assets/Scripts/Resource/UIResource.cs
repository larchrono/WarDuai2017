using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIResource {

	public Sprite SelectArrow;

	public UIResource(){

		SelectArrow = Resources.Load<Sprite>("UI/UIArrow");


	}
}
