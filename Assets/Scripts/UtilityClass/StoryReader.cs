using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Telling {

	public string name;
	public string talk;
	public string status;
	public string locate;
	public string time;

}

public class StoryReader : MonoBehaviour {

	TextAsset _srcText;



	//public static  

	// Use this for initialization
	void Start () {
		Telling[] tells = new Telling[30];

		_srcText = (TextAsset)Resources.Load ("testText");
		string[] fLines = _srcText.text.Split ('\n');

		int i = 0;
		foreach (string line in fLines)
		{
			/* initial structure */
			tells[i] = new Telling ();
			tells[i].name = "";
			tells[i].talk = "";
			tells[i].status = "";
			tells[i].locate = "";
			tells[i].time = "";

			/* split dialog to structure */
			insert (tells[i], line);

			/* print to check */
			Debug.Log(tells[i].name + " say: " + tells[i].talk );
			Debug.Log(tells[i].status+ " " + tells[i].locate + " " + tells[i].time);

			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	void insert(Telling tells, string line){

		string[] Array;
		Array = line.Split (';');

		tells.name = Array[0];
		tells.talk = Array[1];
		tells.status = Array[2];
		tells.locate = Array[3];
		tells.time = Array[4];
	}

}
