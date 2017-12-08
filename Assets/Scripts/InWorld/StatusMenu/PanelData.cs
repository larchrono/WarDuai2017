using UnityEngine;
using System.Collections;

public class PanelData : MonoBehaviour {

	public GameObject firstAim;

	private GameObject memoryAimVal;

	public string panelName;


	public GameObject memoryAim {
		set{ memoryAimVal = value; }
		get{ return memoryAimVal; }
	}
}
