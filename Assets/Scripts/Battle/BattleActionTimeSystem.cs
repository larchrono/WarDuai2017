using System;

public class BattleActionTimeSystem
{
	private int nowTime;
	private int actionTime;
	private int conditionTime;
	private bool hasEvent;
	private BattleActor nowActor;

	private bool canPass;

	private BattleActor[] bac;

	public enum ActiveActionType {
		Condition,
		Action
	}
	public ActiveActionType ActionType;


	public BattleActionTimeSystem (BattleActor[] src)
	{
		hasEvent = false;
		canPass = true;
		nowTime = 0;
		conditionTime = 180;
		actionTime = 200;


		bac = src;
		//Create Slider Icon
		for (int i = 0; i < bac.Length; i++) {
			//Create Actor Icon


		}
	}

	public void Pass(){
		if (canPass) {
			nowTime++;

			for (int i = 0; i < bac.Length; i++) {
				//time line ++
				bac[i].TimePoint += bac[i].TimeSpeed;

				if (bac [i].TimePoint > conditionTime) {
					hasEvent = true;
					ActionType = ActiveActionType.Condition;
					nowActor = bac [i];
					break;
				}

				if (bac [i].TimePoint > actionTime) {
					hasEvent = true;
					ActionType = ActiveActionType.Action;
					nowActor = bac [i];
					break;
				}
			}


		}
	}

	public void FreezePass(){
		canPass = false;
	}

	public void ActivePass(){
		canPass = true;
	}

	public BattleActor ActorActionAction(){


		return nowActor;
	}

	public BattleActor ActorActionCondition(){


		return nowActor;
	}

	public bool HasActorEvent(){

		ActionType = ActiveActionType.Condition;

		return hasEvent;
	}

}

