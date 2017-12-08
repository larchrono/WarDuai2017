using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class EditorAssetCreator : MonoBehaviour {

	[AddComponentMenu("MyScripts/Create Asset")]

	[MenuItem("Custom Editor/Create My Asset")]
	static void CreateDataAsset(){
		//資料 Asset 路徑
		string holderAssetPath = "Assets/Resources/";

		if(!Directory.Exists(holderAssetPath)) Directory.CreateDirectory(holderAssetPath);

		//建立實體
		GamesetData holder = ScriptableObject.CreateInstance<GamesetData> ();

		//使用 holder 建立名為 dataHolder.asset 的資源
		AssetDatabase.CreateAsset(holder, holderAssetPath + "GamesetData.asset");
	}
}


public class GamesetData : ScriptableObject {

	public string[] strings;
	public int[] integers;
	public float[] floats;
	public bool[] booleans;
	public byte[] bytes;
}