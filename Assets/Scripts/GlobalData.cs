using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Saved all Player Infos
public class GlobalData : MonoBehaviour {
	// Basic Global use
	public static GlobalData Instance;

	//For Item System
	public const int MAX_ITEM_CARRY_NUM = 16;
	private List<int> _inventoryConsumable;
	private List<int> _inventoryEquipment;
	private List<int> _inventoryPrecious;

	//For Party Data
	private List<GemData> inPanelGems = new List<GemData>();
	private int GemMaxX = 8;
	private int GemMaxY = 5;

	public int goldCarry = 0;
	public float totalTime = 0;


	// For Actor data
	public const int MAX_SKILL_NUM = 8;
	public const int MAX_MAGIC_NUM = 8;
	public const int MAX_EQUIPMENT_NUM = 16;
	private List<int> _expTable;
	private StandardActor [] actors;
	private List<StandardActor> activeActors;


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
	private int battleMonsterType = 4;
	private Vector3 positionBeforeBattle = new Vector3(500,100,500);





	/// <summary>
	/// /////////
	/// </summary>




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

		if (inPanelGems.Count == 0) {
			inPanelGems = GemController.InitGem (GemMaxX,GemMaxY);
		}

		// If actor is null , initialize
		if (actors == null) {
			actors = new StandardActor[12];
			actors [0] = new StandardActor (1);
			actors [1] = new StandardActor (2);

			activeActors = new List<StandardActor> ();
			_inventoryConsumable = new List<int> ();
			_inventoryEquipment = new List<int> ();
			_inventoryPrecious = new List<int> ();
			_expTable = new List<int> ();
			activeActors.Add (actors [0]);
			activeActors.Add (actors [1]);

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

			//Create exp table
			double exp_basic = 15;
			double exp_rate = 1.6;
			double exp_rate_rate = 0.98;
			double temp_final = 0;

			_expTable.Add (0); // 0等
			_expTable.Add (0); // 1等

			for (int i = 2; i < 100; i++)
			{
				temp_final += exp_basic;
				_expTable.Add(System.Convert.ToInt32(temp_final));
				if (i < 50) {
					exp_basic = exp_basic * exp_rate;
					exp_rate = (exp_rate * exp_rate_rate) < 1.1 ? 1.1 : exp_rate * exp_rate_rate;
				}
			}

		} else {

		}
	}

	public int GetNextNeedExp(StandardActor actor){
		return _expTable [actor.Level + 1] - actor.EXP;
	}

	public int GetToNextEXP(StandardActor actor){
		return _expTable [actor.Level + 1];
	}

	public int GetThisLevelBaseExp(StandardActor actor){
		return _expTable [actor.Level];
	}

	public int GetToNextEXP(int lv){
		return _expTable [lv + 1];
	}

	public int GetThisLevelBaseExp(int lv){
		return _expTable [lv];
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		totalTime += Time.unscaledDeltaTime;
	}



	// Setter & Getter
	public StandardActor[] Actors {
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
		get { return inPanelGems; }
		set { inPanelGems = value; }
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
	public List<int> EXPTable {
		get { return _expTable;}
		set { _expTable = value;}
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
}
