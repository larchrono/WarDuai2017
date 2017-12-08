using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GemData {

	public enum GemNext { UP, DOWN, LEFT, RUGHT};
	public enum GemType { FIRE, WIND, WATER, SUN, DARK, NULL};
	public GemType type;

	private Sprite[] sprite;
	public Image alphaColor;

	public int y;
	public int x;
	public int inPanelPosID;

	//週遭的珠子們
	public List<GemData> linkOrbs = new List<GemData>();
	public GemData GemUp { get; private set; }
	public GemData GemDown { get; private set; }
	public GemData GemLeft { get; private set; }
	public GemData GemRight { get; private set; }

	//移入動畫使用
	public int moveColumn;
	public bool haveToRemove = false;


	public bool isLinked = false;

	public float removeTime = 0.25f;

	//private float timer = 0;
	//private float removeAlpha = 0.25f;


	public GemData (int _x,int _y){
		this.x = _x;
		this.y = _y;
		type = GemData.GemType.NULL;
		//isLinked
	}

	public void SetGemNextTo(GemNext dir, GemData gem){
		if (dir == GemNext.UP)
			GemUp = gem;
		if (dir == GemNext.DOWN)
			GemDown = gem;
		if (dir == GemNext.LEFT)
			GemLeft = gem;
		if (dir == GemNext.RUGHT)
			GemRight = gem;
	}

}
