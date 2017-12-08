using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagicClass {

	private string name;
	private string description;
	public Sprite Icon {get;set;}
	private int mpCost;

	public enum MagicEffect { 
		CONVERT,
		ADD,
		HEAL,
		CONVERT_ROW,
		CONVERT_ALL,
		CONVERT_COLUMN,
	}
	private MagicEffect effect;

	private GemData.GemType fromType;
	private GemData.GemType toType;
	private List<GemData.GemType> toTypes = new List<GemData.GemType>();

	private int addAmount;
	private List<int> rowNumber = new List<int>();
	private List<int> columnNumber = new List<int>();

	private int healAmount;


	public string Name{
		get {return name;}
		set {name = value;}
	}
	public string Description{
		get {return description;}
		set {description = value;}
	}
	public int MpCost{
		get {return mpCost;}
		set {mpCost = value;}
	}
	public MagicEffect Effect{
		get { return effect; }
		set { effect = value; }
	}
	public GemData.GemType FromType{
		get { return fromType; }
		set { fromType = value; }
	}
	public GemData.GemType ToType {
		get { return toType; }
		set { toType = value; }
	}
	public List<GemData.GemType> ToTypes {
		get { return toTypes; }
		set { toTypes = value; }
	}
	public int AddAmount{
		get { return addAmount; }
		set { addAmount = value; }
	}
	public List<int> RowNumber{
		get { return rowNumber; }
		set { rowNumber = value; }
	}
	public List<int> ColumnNumber{
		get { return columnNumber; }
		set { columnNumber = value; }
	}
	public int HealAmount{
		get { return healAmount; }
		set { healAmount = value; }
	}

}
