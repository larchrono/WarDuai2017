using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatusBar : MonoBehaviour {
	
	private const float damagedSpeed = 0.8f;

	public GameObject ObjectName;
	public GameObject ObjectHPValue;
	public GameObject ObjectHPDamaged;
	public GameObject ObjectHPText;

	public GameObject ObjectMPValue;
	public GameObject ObjectMPText;

	private float nowHP;
	private float damagedHP;

	private float nowMP;

	private StandardActor bindActor;

	void Start(){
		BattleMain.EVENT_ANYUNIT_STATUS_UPDATE += UpdateInfomation;
	}

	public void Init(StandardActor actor){
		bindActor = actor;
		ObjectName.GetComponent<Text> ().text = actor.ActorName;
		ObjectHPText.GetComponent<Text> ().text = "" + actor.HP;
		ObjectMPText.GetComponent<Text> ().text = "" + actor.MP;
		nowHP = (float)actor.HP / actor.MHP;
		nowMP = (float)actor.MP / actor.MMP;
		damagedHP = nowHP;
		ObjectHPDamaged.GetComponent<Image> ().fillAmount = damagedHP;
		SetHPPercent (nowHP);
		SetMPPercent (nowMP);
	}

	public void SetHPPercent(float val){
		val = Mathf.Clamp (val, 0, 1);
		ObjectHPValue.GetComponent<Image> ().fillAmount = val;
		nowHP = val;
		if (damagedHP < nowHP)
			damagedHP = nowHP;
	}

	public void SetMPPercent(float val){
		val = Mathf.Clamp (val, 0, 1);
		ObjectMPValue.GetComponent<Image> ().fillAmount = val;
		nowMP = val;
	}

	public void UpdateInfomation(object sender,ActionUnitArgs atrgs){
		nowHP = bindActor.HP / (float)bindActor.MHP ;
		nowMP = bindActor.MP / (float)bindActor.MMP ;
		ObjectHPText.GetComponent<Text> ().text = "" + bindActor.HP;
		ObjectMPText.GetComponent<Text> ().text = "" + bindActor.MP;
		SetHPPercent (nowHP);
		SetMPPercent (nowMP);
	}

	// Update is called once per frame
	void Update () {
		if (nowHP < damagedHP) {
			damagedHP -= damagedSpeed * Time.deltaTime;
			ObjectHPDamaged.GetComponent<Image> ().fillAmount = damagedHP;
		}
	}
}
