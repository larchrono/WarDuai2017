using UnityEngine;
using System.Collections;

public static class GameResource {

	public static ImageResource Sprite;
	public static UIResource UI;

	public static PrefabsResource Prefab;

	public static MaterialResource Mat;

	//public static 

	static GameResource(){

		Sprite = new ImageResource ();
		UI = new UIResource ();
		Prefab = new PrefabsResource ();

		Mat = new MaterialResource ();
	}
}
