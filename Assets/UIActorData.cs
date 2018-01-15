using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActorData : MonoBehaviour {

	public Image Icon;
	public Text Name;
	public Text Lv;
	public Text EXP;

	public Image EXP_Bar;

	public Text LevelUpTip;

	StandardActor _bindData;

	int UIUSE_level;
	float _exp_base;
	float _exp_end;
	float _exp_now;
	float _expbar_now;

	int _expget = 0;
	float _expget_remain = 0;

	float everytimeExpRun = 1f;

	public void SetupData(StandardActor data){
		_bindData = data;
		UIUSE_level = data.Level;
		if(Icon != null)
			Icon.sprite = data.Icon;
		if(Name != null)
			Name.text = data.ActorName;
		if(Lv != null)
			Lv.text = data.Level.ToString();
		if (EXP != null)
			EXP.text = data.EXP.ToString();
		if (EXP_Bar != null){
			_exp_base = (float)EXPTable.Normal.GetThisLevelBaseExp (UIUSE_level);
			_exp_end  = (float)EXPTable.Normal.GetToNextEXP (UIUSE_level);
			_exp_now  = (float)data.EXP;
			CalcuExpBarLocation ();
		}
	}

	void CalcuExpBarLocation(){
		if (EXP_Bar != null) {
			_expbar_now = (_exp_now - _exp_base) / (_exp_end - _exp_base);
			if (_expbar_now > 1.0f) {
				UIUSE_level++;
				Lv.text = "" + UIUSE_level;
				if (LevelUpTip != null)
					LevelUpTip.gameObject.SetActive (true);
				_exp_base = EXPTable.Normal.GetThisLevelBaseExp (UIUSE_level);
				_exp_end = EXPTable.Normal.GetToNextEXP (UIUSE_level);
				_expbar_now = (_exp_now - _exp_base) / (_exp_end - _exp_base);
			}
			EXP_Bar.rectTransform.localScale = new Vector3 (_expbar_now, 1, 1);
		}
	}

	public void AddExpAnim(int mount){
		_expget = mount;
		_expget_remain = (float)mount;
	}

	void Update () {
		if (_expget_remain > 0.01) {
			
			if (EXP_Bar != null) {
				
				float workAmount = _expget * everytimeExpRun * Time.deltaTime;
				_exp_now += workAmount;
				_expget_remain -= workAmount;

				CalcuExpBarLocation ();

				Debug.Log ("now exp :" + _exp_now);
			}
		}
	}
}
