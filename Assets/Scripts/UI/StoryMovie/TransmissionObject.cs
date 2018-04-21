using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransmissionObject : MonoBehaviour {

	public Text _name;
	public Text _context;
	public Image _namePanel;
	public Image _unitImage;
	public Image _continueArrow;

	private bool _isWait;
	private float _dur;

	BasicActorClass _unit;

	private Transmission.ActionCallback callback;

	// Use this for initialization
	void Start () {
	
	}

	public void Setup(Sprite icon, string name,string context, int location, bool isWait, double dur, Transmission.ActionCallback function){
		_name.text = name;
		_context.text = context;
		_isWait = isWait;
		_dur = (float)dur;
		callback = function;

		if (location == 1) {
			_unitImage.rectTransform.anchorMin = new Vector2 (0.5f, 0.2f);
			_unitImage.rectTransform.anchorMax = new Vector2 (0.9f, 0.9f);
			_namePanel.rectTransform.anchorMin = new Vector2 (0.7f, 0.3f);
			_namePanel.rectTransform.anchorMax = new Vector2 (0.9f, 0.36f);
			_unitImage.rectTransform.localScale = new Vector3 (-1, 1, 1);
		}
		if (isWait) {
			_continueArrow.gameObject.SetActive (false);
		}

		if (icon != null) {
			_unitImage.sprite = icon;
		}
	}

	public void Setup(BasicActorClass unit, string name,string context, string mood,int location, bool isWait, double dur, Transmission.ActionCallback function){
		_unit = unit;
		_name.text = name;
		_context.text = context;
		_isWait = isWait;
		_dur = (float)dur;
		callback = function;

		if (location == 1) {
			_unitImage.rectTransform.anchorMin = new Vector2 (0.5f, 0.2f);
			_unitImage.rectTransform.anchorMax = new Vector2 (0.9f, 0.9f);
			_namePanel.rectTransform.anchorMin = new Vector2 (0.7f, 0.3f);
			_namePanel.rectTransform.anchorMax = new Vector2 (0.9f, 0.36f);
			_unitImage.rectTransform.localScale = new Vector3 (-1, 1, 1);
		}
		if (isWait) {
			_continueArrow.gameObject.SetActive (false);
		}

		if (_unit != null) {
			if (_unit.Photos.ContainsKey (mood)) {
				_unitImage.sprite = _unit.Photos [mood];
			} else
				_unitImage.sprite = _unit.Photo;
		}
	}

	void Update() {

		if (!_isWait) {
			if (Input.GetButtonDown ("A")) {
				Finish ();
			}
			if (Input.GetMouseButtonDown (0)) {
				Finish ();
			}
		}
	}

	IEnumerator AutoTransmit(float t){
		yield return new WaitForSeconds (t);
		Finish ();
	}

	void OnEnable()
	{
		if(_dur != 0)
			StartCoroutine (AutoTransmit(_dur));
	}

	void Finish(){
		Transmission.TransmitQueueRun ();
		Destroy (this.gameObject);
		if (callback != null) {
			callback ();
		}
	}
}
