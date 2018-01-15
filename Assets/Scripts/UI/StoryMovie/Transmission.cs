using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Transmission : MonoBehaviour {

	public delegate void ActionCallback();

	public static GameObject transmisionObject;

	static public Queue<GameObject> QueueTransmit;
	static public GameObject ShowingTransmit;

	public static void CheckSetup(){
		if (transmisionObject == null) {
			transmisionObject = Resources.Load ("Prefabs/UI/TransmisionObject") as GameObject;
		}
		if (QueueTransmit == null) {
			QueueTransmit = new Queue<GameObject> ();
		}
	}
	
	public static void FromUnit(BasicActorClass fromUnit,string name,string message,string mood,int location, bool isWait , double duration , ActionCallback function = null){

		CheckSetup ();
		GameObject mainCanvas = GameObject.Find ("Canvas");

		GameObject clone = (GameObject)Instantiate(transmisionObject, Vector3.zero, Quaternion.identity);
		clone.GetComponent<TransmissionObject> ().Setup (fromUnit, name, message, mood, location, isWait, duration, function);
		clone.transform.SetParent (mainCanvas.GetComponent<RectTransform> (), false);
		clone.SetActive (false);

		QueueTransmit.Enqueue (clone);

		if (ShowingTransmit == null) {
			ShowingTransmit = QueueTransmit.Dequeue ();
			ShowingTransmit.SetActive (true);
		}
	}

	public static void TransmitQueueRun(){
		if (QueueTransmit.Count > 0) {
			ShowingTransmit = QueueTransmit.Dequeue ();
			ShowingTransmit.SetActive (true);
		}
	}
}
