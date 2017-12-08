using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GemController : MonoBehaviour {

	//目前處理的系統
	public static GemController current;
	//GEM的圖片資料
	private Sprite[] gemSprite;

	//目前的盤面暫存
	private List<GemData> inPanelGems ;

	private static int y_Max = 5;
	private static int x_Max = 6;

	public List<List<GemData>> group_of_linkedOrbGroup = new List<List<GemData>>();

	private int debugID = 0;

	//最大盤面8*5 , 底8 高5
	public const int GemPanel_Max_X = 8;
	public const int GemPanel_Max_Y = 5;
	private const float everyAnchor_X = (float)1 / GemPanel_Max_X;
	private const float everyAnchor_Y = (float)1 / GemPanel_Max_Y;

	//技能最大占用盤面5*5 , 底5 高5
	public const int GemCost_Max_X = 5;
	public const int GemCost_Max_Y = 5;
	private const float costEveryAnchor_X = (float)1 / GemCost_Max_X;
	private const float costEveryAnchor_Y = (float)1 / GemCost_Max_Y;


	void Awake()
	{
		current = this;

		gemSprite = new Sprite[6];
		gemSprite [0] = GameResource.Sprite.GemFire;
		gemSprite [1] = GameResource.Sprite.GemWind;
		gemSprite [2] = GameResource.Sprite.GemWater;
		gemSprite [3] = GameResource.Sprite.GemSun;
		gemSprite [4] = GameResource.Sprite.GemDark;
		gemSprite [5] = GameResource.Sprite.GemNull;

	}

	void Start(){
		inPanelGems = GlobalData.Instance.InPanelGems;
	}

	//建立單科的Gem UI
	public GameObject CreateImageGemSingle(GemData gem){
		GameObject ins = Instantiate (GameResource.Prefab.UIGemInstance);

		ins.GetComponent<RectTransform>().anchorMin = new Vector2 (everyAnchor_X * gem.x, 1f - everyAnchor_Y - everyAnchor_Y * gem.y);
		ins.GetComponent<RectTransform>().anchorMax = new Vector2 (everyAnchor_X * gem.x + everyAnchor_X, 1f - everyAnchor_Y * gem.y);
		ins.GetComponent<UIGem>().img.sprite = gemSprite[(int)gem.type];
		ins.name = "UIGem" + gem.y + gem.x + "_id:" + debugID;

		debugID++;
		return ins;
	}

	//從GlobalData裡目前擁有的Gem表，建立多顆的Gem UI，並附著於參數上
	public List<GameObject> CreateImageGems(GameObject parent){
		debugID = 0;
		List<GameObject> output = new List<GameObject> ();
		foreach (GemData obj in inPanelGems) {
			GameObject UIGem = CreateImageGemSingle (obj);
			UIGem.GetComponent<RectTransform> ().SetParent (parent.GetComponent<RectTransform>(),false);
			// [ left - bottom ]
			//ins.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
			// [ right - top ]
			//ins.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

			output.Add (UIGem);
		}
		return output;
	}

	public bool ShapeSearchLoop(List<GemData> savedList, GemData gem, SkillNode node, GemData.GemType type){
		if (gem.type == type) {
			savedList.Add (gem);

			if (node.up != null) {
				if (gem.GemUp != null) {
					if (!ShapeSearchLoop (savedList, gem.GemUp, node.up, type))
						return false;
				}
				else
					return false;
			}

			if (node.left != null) {
				if (gem.GemLeft != null) {
					if (!ShapeSearchLoop (savedList, gem.GemLeft, node.left, type))
						return false;
				}
				else
					return false;
			}

			if (node.right != null) {
				if (gem.GemRight != null) {
					if (!ShapeSearchLoop (savedList, gem.GemRight, node.right, type))
						return false;
				}
				else
					return false;
			}
			if (node.down != null) {
				if (gem.GemDown != null) {
					if (!ShapeSearchLoop (savedList, gem.GemDown, node.down, type))
						return false;
				}
				else
					return false;
			}
			return true;
		}
		return false;
	}

	public List<List<GemData>> FindShapeInGemPanel(SkillNode Shape,GemData.GemType type){
		List<List<GemData>> mutipleResultList = new List<List<GemData>> ();
		List<GemData> resultList = new List<GemData> ();
		foreach (GemData gem in inPanelGems) {
			if (ShapeSearchLoop (resultList, gem, Shape, type)){
				mutipleResultList.Add (resultList);
				resultList = new List<GemData> ();
				//return resultList;
			}
			else
				resultList.Clear ();
		}
		//最後return的一定是空的
		return mutipleResultList;
	}

	public void ShapeExpandLoopAndCreateUI(List<GameObject> resultList, SkillNode node,int x, int y , GemData.GemType type){
		//由於我們在搜索Node時，可能會超出邊界，因此要定義好技能從哪開始搜索，藉此讓圖形不會超出邊界
		GameObject temp = Instantiate (GameResource.Prefab.UIGemInstance);
		temp.GetComponent<RectTransform>().anchorMin = new Vector2 (costEveryAnchor_X * x, 1f - costEveryAnchor_Y - costEveryAnchor_Y * y);
		temp.GetComponent<RectTransform>().anchorMax = new Vector2 (costEveryAnchor_X * x + costEveryAnchor_X, 1f - costEveryAnchor_Y * y);
		temp.GetComponent<UIGem>().img.sprite = gemSprite[(int)type];
		resultList.Add (temp);

		if (node.up != null) {
			ShapeExpandLoopAndCreateUI (resultList, node.up, x, y - 1, type);
		}
		if (node.left != null) {
			ShapeExpandLoopAndCreateUI (resultList, node.left, x - 1, y, type);
		}
		if (node.right != null) {
			ShapeExpandLoopAndCreateUI (resultList, node.right, x + 1, y, type);
		}
		if (node.down != null) {
			ShapeExpandLoopAndCreateUI (resultList, node.down, x, y + 1, type);
		}
	}
	//用於創造技能施展時耗費的Gem UI , 並將UI貼到帶入參數的面板上
	public List<GameObject> CreateImageGemByNode(GameObject parent,SkillNode Shape,Vector2 startAt,GemData.GemType type){
		List<GameObject> resultList = new List<GameObject> ();
		ShapeExpandLoopAndCreateUI (resultList, Shape, (int)startAt.x, (int)startAt.y, type);
		foreach (GameObject temp in resultList) {
			temp.GetComponent<RectTransform> ().SetParent (parent.GetComponent<RectTransform>(),false);
		}
		return resultList;
	}



	public bool UseMagicToGem(StandardActor user,int magicID,List<GameObject> gemImagePanel){
		bool success = false;

		if (MagicDataBase.MagicData [magicID].Effect == MagicClass.MagicEffect.CONVERT) {
			foreach (GemData gem in inPanelGems) {
				if (gem.type == MagicDataBase.MagicData [magicID].FromType) {
					success = true;
					gem.type = MagicDataBase.MagicData [magicID].ToType;
					if (gemImagePanel != null) {
						gemImagePanel [gem.inPanelPosID].GetComponent<UIGem>().img.sprite = gemSprite[(int)gem.type];

						CreateGemUseEffect (gemImagePanel, gem, MagicDataBase.MagicData [magicID].FromType);
					}
				}
			}
		}

		if (MagicDataBase.MagicData [magicID].Effect == MagicClass.MagicEffect.ADD) {
			List<GemData> goalGem = new List<GemData>();
			foreach(GemData gem in inPanelGems){
				if (gem.type != MagicDataBase.MagicData [magicID].ToType) {
					goalGem.Add (gem);
				}
			}
			if (goalGem.Count >= MagicDataBase.MagicData [magicID].AddAmount){
				success = true;
				for (int i = 0; i < MagicDataBase.MagicData [magicID].AddAmount; i++) {
					GemData gem = goalGem[Random.Range( 0, goalGem.Count )];
					GemData.GemType beforeType = gem.type;
					gem.type = MagicDataBase.MagicData [magicID].ToType;
					goalGem.Remove (gem);

					if (gemImagePanel != null) {
						gemImagePanel [gem.inPanelPosID].GetComponent<UIGem>().img.sprite = gemSprite[(int)gem.type];

						CreateGemUseEffect (gemImagePanel, gem, beforeType);
					}
				}

			}
		}


		if (MagicDataBase.MagicData [magicID].Effect == MagicClass.MagicEffect.CONVERT_ROW) {
			success = true;
			foreach (int whichRow in MagicDataBase.MagicData [magicID].RowNumber) {
				for (int x = 0; x < x_Max; x++) {
					GemData gem = inPanelGems [x + x_Max * whichRow];
					GemData.GemType beforeType = gem.type;
					gem.type = MagicDataBase.MagicData [magicID].ToType;

					if (gemImagePanel != null) {
						gemImagePanel [gem.inPanelPosID].GetComponent<UIGem> ().img.sprite = gemSprite [(int)gem.type];

						CreateGemUseEffect (gemImagePanel, gem, beforeType);
					}
				}
			}
		}

		if (MagicDataBase.MagicData [magicID].Effect == MagicClass.MagicEffect.CONVERT_COLUMN) {
			success = true;
			foreach (int whichColumn in MagicDataBase.MagicData [magicID].ColumnNumber) {
				for (int y = 0; y < y_Max; y++) {
					GemData gem = inPanelGems [whichColumn + x_Max * y];
					GemData.GemType beforeType = gem.type;
					gem.type = MagicDataBase.MagicData [magicID].ToType;

					if (gemImagePanel != null) {
						gemImagePanel [gem.inPanelPosID].GetComponent<UIGem> ().img.sprite = gemSprite [(int)gem.type];

						CreateGemUseEffect (gemImagePanel, gem, beforeType);
					}
				}
			}
		}

		if (MagicDataBase.MagicData [magicID].Effect == MagicClass.MagicEffect.CONVERT_ALL) {
			success = true;
			foreach (GemData gem in inPanelGems) {
				GemData.GemType beforeType = gem.type;
				gem.type = MagicDataBase.MagicData [magicID].ToTypes[Random.Range(0,MagicDataBase.MagicData [magicID].ToTypes.Count)];

				if (gemImagePanel != null) {
					gemImagePanel [gem.inPanelPosID].GetComponent<UIGem>().img.sprite = gemSprite[(int)gem.type];

					CreateGemUseEffect (gemImagePanel, gem, beforeType);
				}
			}
		}


		if (success) {
			user.ConsumMP (MagicDataBase.MagicData [magicID].MpCost);
			return true;
		}
		return false;
	}
	public List<GemData> FindMagicEffectInGemPanel(int magicID){
		List<GemData> resultList = new List<GemData> ();
		if (MagicDataBase.MagicData [magicID].Effect == MagicClass.MagicEffect.CONVERT) {
			foreach (GemData gem in inPanelGems) {
				if (gem.type == MagicDataBase.MagicData [magicID].FromType) {
					resultList.Add (gem);
				}
			}
		}

		if (MagicDataBase.MagicData [magicID].Effect == MagicClass.MagicEffect.ADD) {
			List<GemData> goalGem = new List<GemData> ();
			foreach (GemData gem in inPanelGems) {
				if (gem.type != MagicDataBase.MagicData [magicID].ToType) {
					goalGem.Add (gem);
				}
			}
			if (goalGem.Count >= MagicDataBase.MagicData [magicID].AddAmount) {
				resultList.AddRange (goalGem);
			}
		}

		if (MagicDataBase.MagicData [magicID].Effect == MagicClass.MagicEffect.CONVERT_ROW) {
			foreach(int whichRow in MagicDataBase.MagicData [magicID].RowNumber){
				for (int x = 0; x < x_Max; x++) {
					resultList.Add (inPanelGems [x + x_Max * whichRow]);
				}
			}
		}

		if (MagicDataBase.MagicData [magicID].Effect == MagicClass.MagicEffect.CONVERT_COLUMN) {
			foreach(int whichRow in MagicDataBase.MagicData [magicID].ColumnNumber){
				for (int y = 0; y < y_Max; y++) {
					resultList.Add (inPanelGems [whichRow + x_Max * y]);
				}
			}
		}

		if (MagicDataBase.MagicData [magicID].Effect == MagicClass.MagicEffect.CONVERT_ALL) {
			resultList.AddRange (inPanelGems);
		}

		return resultList;
	}

	//建立一張全新的Gem表
	public static List<GemData> InitGem(int table_x , int table_y)
	{
		List<GemData> newGems = new List<GemData>();
		x_Max = Mathf.Min(GemPanel_Max_X, table_x);
		y_Max = Mathf.Min(GemPanel_Max_Y, table_y);
		for (int y = 0; y < y_Max; y++)
		{
			for (int x = 0; x < x_Max; x++)
			{
				//orb pos scale
				/*
				GameObject gemObj = Instantiate(gemImagePrefabs) as GameObject;
				RectTransform gemRect = gemObj.GetComponent<RectTransform>();
				gemRect.SetParent(BGRect);
				gemRect.localScale = Vector2.one;
				gemRect.anchoredPosition = new Vector2(x * addPos.x, y * addPos.y);
				*/

				//orb type number init
				GemData gem = new GemData(x,y);
				gem.inPanelPosID = gem.y * x_Max + gem.x;
				newGems.Add(gem);
				//inPanelPosID 即為 gem 在 List 裡的陣列id
				//Debug.Log ("d:"+gem.inPanelPosID + " " + inPanelGems.IndexOf(gem));
				//RANGE NULL 是不包含NULL的
				int typeNum = Random.Range(0, (int)GemData.GemType.NULL);
				gem.type = (GemData.GemType)typeNum;

			}
		}
		//連接珠子的上下左右
		//link orb ( " + " -> right / top / left / bottom , direction)
		for (int index = 0; index < newGems.Count; index++)
		{
			//上
			if (newGems [index].y != y_Max - 1) {
				newGems [index].linkOrbs.Add (newGems [index + x_Max]);
				newGems [index].SetGemNextTo (GemData.GemNext.UP,newGems [index + x_Max]);
			}
			//下
			if (newGems [index].y != 0) {
				newGems [index].linkOrbs.Add (newGems [index - x_Max]);
				newGems [index].SetGemNextTo (GemData.GemNext.DOWN,newGems [index - x_Max]);
			}
			//左
			if (newGems [index].x != 0) {
				newGems [index].linkOrbs.Add (newGems [index - 1]);
				newGems [index].SetGemNextTo (GemData.GemNext.LEFT,newGems [index - 1]);
			}
			//右

			if (newGems [index].x != x_Max - 1) {
				newGems [index].linkOrbs.Add (newGems [index + 1]);
				newGems [index].SetGemNextTo (GemData.GemNext.RUGHT,newGems [index + 1]);
			}
		}
		return newGems;
	}

	public List<GemData> Firstcolumn(){
		List<GemData> temp = new List<GemData> ();
		for (int y = 0; y < y_Max; y++) {
			temp.Add (inPanelGems [y * x_Max]);
			//Debug.Log ("get gem id : " + (y * x_Max) + ", y=" + y);
		}
		return temp;
	}

	public void UseSkillShape(List<GemData> shape, List<GameObject> UIGems) {

		//Clear Skill Used Gem
		foreach (GemData gem in shape) {
			GameObject temp = Instantiate (GameResource.Prefab.UIGemUsedIns);
			temp.transform.SetParent(UIGems[InPanelGems.IndexOf(gem)].transform,false);
			temp.GetComponent<Image>().sprite = gemSprite[(int)gem.type];
			Destroy (temp, 4.0f);

			gem.type = GemData.GemType.NULL;
			UIGems[InPanelGems.IndexOf(gem)].GetComponent<UIGem>().state = UIGem.GemsState.Remove;
		}

		//Find NULL Gems Right space
		foreach(GemData gem in inPanelGems){
			if (gem.type == GemData.GemType.NULL) {

				//計算包含自己在內往右，有幾個空格，方便等下從那些空格移入
				int count = 0;
				for(int x = gem.x ; x < x_Max; x++){
					if(inPanelGems [x_Max * gem.y + x].type == GemData.GemType.NULL){
						count++;
					}
				}
				//gem.moveColumn = count;

			}
		}

		//Assign right type to now ,set right type to null
		for (int x = 0; x < x_Max; x++) {
			for (int y = 0; y < y_Max; y++) {
				GemData gem = inPanelGems[x_Max * y + x];

				int count = 0;
				bool find = false;
				if (gem.type == GemData.GemType.NULL) {
					for (int r = x + 1; r < x_Max; r++) {
						GemData rightGem = inPanelGems [x_Max * y + r];
						count++;
						if (rightGem.type != GemData.GemType.NULL) {
							gem.type = rightGem.type;
							rightGem.type = GemData.GemType.NULL;
							UIGems[InPanelGems.IndexOf(gem)].GetComponent<UIGem>().moveColumn = count ;
							UIGems [inPanelGems.IndexOf (gem)].GetComponent<UIGem> ().SetAniPos (count, UIGem.GemsState.Create);
							find = true;
							break;
						}
					}
					//最右邊也是空的話，從右邊外補進來
					if (!find) {
						UIGems [InPanelGems.IndexOf (gem)].GetComponent<UIGem> ().moveColumn = count + 1;
						UIGems [inPanelGems.IndexOf (gem)].GetComponent<UIGem> ().SetAniPos (count + 1, UIGem.GemsState.Create);
					}
				}

			}
		}

		//Assign data to NULL Gems 
		foreach (GemData gem in inPanelGems) {
			if (gem.type == GemData.GemType.NULL) {
				int typeNum = Random.Range(0, (int)GemData.GemType.NULL);
				gem.type = (GemData.GemType)typeNum;
			}
			UIGems[inPanelGems.IndexOf(gem)].GetComponent<UIGem>().img.sprite = gemSprite[(int)gem.type];
		}

		foreach (GameObject UIGem in UIGems) {


		}
	}

	public List<GemData> InPanelGems {
		get { return inPanelGems; }
		set { inPanelGems = value; }
	}

	public void CreateGemUseEffect(List<GameObject> gemImagePanel,GemData pos, GemData.GemType type){
		GameObject temp = Instantiate (GameResource.Prefab.UIGemUsedIns);
		temp.transform.SetParent(gemImagePanel[InPanelGems.IndexOf(pos)].transform,false);
		temp.GetComponent<Image>().sprite = gemSprite[(int)type];
		//Destroy (temp, 4.0f);
	}
}
