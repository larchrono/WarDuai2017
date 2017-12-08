using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetTagData : MonoBehaviour {

	private const float damagedSpeed = 0.8f;

	public GameObject ObjectName;
	public GameObject ObjectIcon;
	public GameObject ObjectHPValue;
	public GameObject ObjectHPDamaged;

	private float nowHP;
	private float damagedHP;

	private StandardActor bindActor = null;

	private bool isBreak = false;
	private float breakSpeed = 2.0f;

	void Start(){
		BattleMain.EVENT_ANYUNIT_STATUS_UPDATE += UpdateInfomation;
	}

	public void Init(StandardActor actor){
		bindActor = actor;
		ObjectName.GetComponent<Text> ().text = actor.ActorName;
		ObjectIcon.GetComponent<Image> ().sprite = actor.BattleIcon;
	}

	public void SetHPPercent(float val){
		val = Mathf.Clamp (val, 0, 1);
		ObjectHPValue.GetComponent<Image> ().fillAmount = val;
		nowHP = val;
		if (damagedHP < nowHP)
			damagedHP = nowHP;
	}

	// Use this for initialization
	void Awake () {
		nowHP = 1.0f;
		damagedHP = 1.0f;
	}

	public void UpdateInfomation(object sender,ActionUnitArgs atrgs){
		nowHP = bindActor.HP / (float)bindActor.MHP ;
		SetHPPercent (nowHP);
	}

	public void Break(){
		isBreak = true;
		BattleMain.EVENT_ANYUNIT_STATUS_UPDATE -= UpdateInfomation;

		Destroy (gameObject, 2);
		print ("Tag Break");
	}
	
	// Update is called once per frame
	void Update () {
		if (damagedHP > nowHP) {
			damagedHP -= damagedSpeed * Time.deltaTime;
			ObjectHPDamaged.GetComponent<Image> ().fillAmount = damagedHP;
		}
		if (isBreak) {
			foreach (CanvasRenderer rd in gameObject.GetComponentsInChildren<CanvasRenderer>()) {
				rd.SetAlpha(Mathf.Clamp(rd.GetAlpha() - breakSpeed * Time.deltaTime, 0, 1));
			}
		}
	}
}
