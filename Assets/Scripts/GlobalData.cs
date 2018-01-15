using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Saved all Player Infos
public class GlobalData : MonoBehaviour {
	// Basic Global use
	public static GlobalData Instance;

	//Const parameter
	public const int MAX_SKILL_NUM = 8;
	public const int MAX_MAGIC_NUM = 8;
	public const int MAX_EQUIPMENT_NUM = 16;
	public const int MAX_ITEM_CARRY_NUM = 16;


	//For Game Status
	public bool m_isNew = true;
	public int chapter;
	public float playTime = 0;
	public string locat;

	// For Actor data
	private List<StandardActor> actors;
	private List<StandardActor> activeActors;

	// For Gem System
	private int GemMaxX = 8;
	private int GemMaxY = 5;
	private List<GemData> _inPanelGems;

	// For Party data
	public int goldCarry = 0;

	//For Item System
	private List<int> _inventoryConsumable;
	private List<int> _inventoryEquipment;
	private List<int> _inventoryPrecious;


	//For Movie data , set in GUI
	public bool movie_0_active;
	public bool movie_1_active;
	public bool movie_2_active;
	public bool movie_3_active;


	//Envioument object
	public bool isDarkFireGateShow;




	// For Scene Control
	public enum GameStates
	{
		IN_WORLD,
		IN_BATTLE,
		IN_MOVIE
	}
	private GameStates gameStats;

	// For world Data
	private string inMapName;
	private WorldMapData inMapData;
	private bool invincible = false;




	// For items picks data



	/// <summary>
	/// Need To Store to global 's  data section
	/// </summary>
	private int battleMonsterId = 6;
	private int battleMonsterNumber = 1;
	private int battleMonsterType = 2;
	private Vector3 positionBeforeBattle = new Vector3(500,100,500);
	public Vector2 WorldCameraRotateAngle;

	void Update () {
		playTime += Time.unscaledDeltaTime;
	}

	void Awake() {
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy (gameObject);
		}

		GameSet ();

	}
		
	private void GameSet(){
		if (m_isNew) {
			FirstGameSet ();
		} else {
			// load Game

		}
	}

	private void FirstGameSet(){
		// Setup System
		chapter = 1;
		playTime = 0;
		locat = "杜埃城外";

		// Setup Gems
		_inPanelGems = new List<GemData>();
		if (_inPanelGems.Count == 0) {
			_inPanelGems = GemController.InitGem (GemMaxX,GemMaxY);
		}

		//Setup Actor
		if (actors == null) {
			//Total Actor
			actors = new List<StandardActor>();
			actors.Add (new StandardActor (1));
			actors.Add (new StandardActor (2));
			//actors [0] = new StandardActor (1);
			//actors [1] = new StandardActor (2);

			//Active Actor
			activeActors = new List<StandardActor> ();
			activeActors.Add (actors [0]);
			activeActors.Add (actors [1]);
		}

		//Setup Items
		_inventoryConsumable = new List<int> ();
		_inventoryEquipment = new List<int> ();
		_inventoryPrecious = new List<int> ();

		_inventoryConsumable.Add (1);
		_inventoryConsumable.Add (1);
		_inventoryConsumable.Add (1);
		_inventoryConsumable.Add (2);
		_inventoryConsumable.Add (2);

		_inventoryEquipment.Add (198);
		_inventoryEquipment.Add (199);
		_inventoryEquipment.Add (299);
		_inventoryEquipment.Add (301);
		_inventoryEquipment.Add (401);
		_inventoryEquipment.Add (402);

	}


	// Setter & Getter
	public List<StandardActor> Actors {
		get { return actors;}
		set { actors = value;}
	}

	public List<StandardActor> ActiveActors {
		get { return activeActors;}
		set { activeActors = value;}
	}

	public string InMapName {
		get { return inMapName;}
		set { inMapName = value;}
	}

	public WorldMapData InMapData {
		get { return inMapData;}
		set { inMapData = value;}
	}

	public int BattleMonsterId {
		get { return battleMonsterId;}
		set { battleMonsterId = value;}
	}
	public int BattleMonsterNumber {
		get { return battleMonsterNumber;}
		set { battleMonsterNumber = value;}
	}
	public int BattleMonsterType {
		get { return battleMonsterType;}
		set { battleMonsterType = value;}
	}
	public List<GemData> InPanelGems {
		get { return _inPanelGems; }
		set { _inPanelGems = value; }
	}
	public List<int> InventoryConsumable {
		get { return _inventoryConsumable;}
		set { _inventoryConsumable = value;}
	}
	public List<int> InventoryEquipment {
		get { return _inventoryEquipment;}
		set { _inventoryEquipment = value;}
	}
	public List<int> InventoryPrecious {
		get { return _inventoryPrecious;}
		set { _inventoryPrecious = value;}
	}

	public GameStates GameInState {
		get {  return gameStats; }
		set { gameStats = value; }
	}
	public Vector3 PositionBeforeBattle {
		get {  return positionBeforeBattle; }
		set { positionBeforeBattle = value; }
	} 
	public bool Invincible {
		get { return invincible;}
		set { invincible = value;}
	}

	public void LoadMemory(DataMemory memo){

		chapter = memo.chapter;
		locat = memo.locat;
		playTime = memo.playedTime;
		goldCarry = memo.gold;

		InventoryConsumable = memo.inventoryConsumable;
		InventoryEquipment = memo.inventoryEquipment;
		InventoryPrecious = memo.inventoryPrecious;

		ActorSetup.SetupFromMemory (memo);

		PositionBeforeBattle = memo.savePosition;
		WorldCameraRotateAngle = memo.cameraRotateAngle;
	}
}
