using UnityEngine;
using System.Collections;

public class BaseItemClass {

	private string name;
	private string description;
	private int itemID;
	private int rarity;
	private bool throwable;
	public enum ItemTypes {
		CONSUMABLE,
		EQUIPMENT,
		PRECIOUS
	}
	private ItemTypes itemType;

	//////
	/// getter && setter
	/// //

	public string Name{
		get {return name;}
		set {name = value;}
	}
	public string Description{
		get {return description;}
		set {description = value;}
	}
	public int ItemID{
		get {return itemID;}
		set {itemID = value;}
	}
	public int Rarity{
		get {return rarity;}
		set {rarity = value;}
	}
	public bool Throwable{
		get {return throwable;}
		set {throwable = value;}
	}
	public ItemTypes ItemType{
		get {return itemType;}
		set {itemType = value;}
	}
	public virtual string BonusDescription {
		get { return "Error"; }
	}
}
