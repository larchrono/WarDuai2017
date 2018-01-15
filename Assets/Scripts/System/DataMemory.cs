using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMemory {

	public int chapter;
	public string locat;
	public DateTime savedTime;


	public float playedTime; 
	public int gold;

	public List<int> inventoryConsumable;
	public List<int> inventoryEquipment;
	public List<int> inventoryPrecious;

	public List<ActorData> actors = new List<ActorData> ();

	// Actor Position
	public Vector3 savePosition;
	public Vector2 cameraRotateAngle;

	// Scene Para
	public bool movie_0_active;
	public bool movie_1_active;
	public bool movie_2_active;
	public bool movie_3_active;
	public bool isDarkFireGateShow;

	//int [] gems;


	public class ActorData {
		//Stats
		public int level;
		public int baseHp;
		public int baseMp;
		public int baseSp;
		public int baseStr;
		public int baseInt;
		public int baseVit;
		public int baseAgi;
		public int exp;

		public int equippedWeapon;
		public int equippedArmor;
		public int equippedShoes;
		public int equippedRing;

		public List<int> learnSkill;
		public List<int> learnMagic;

		//now Stats
		public int hp;
		public int mp;
		public int sp;

	}
}
