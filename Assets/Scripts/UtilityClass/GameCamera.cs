using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCamera : MonoBehaviour {

	public delegate void ActionCallback();

	public enum EffectTypes {
		APPLY_CAMERA,
		SHOW_BLUR,
	}
	public EffectTypes effectType;

	//Apply Camera Location
	public static GameObject runningRuntime;
	public static bool IsAimCharactor = true;
	public Camera _targetCamera;
	public float _castTime;

	private float _timeElapse = 0;
	private Vector3 startLocation;
	private Quaternion startQuaternion;

	//Blur effect
	private bool grabBlur = false;
	public static GameObject nowBlurObject;

	public static void ApplyCameraObject(Camera camera,float time){

		if(runningRuntime != null) Destroy (runningRuntime);

		IsAimCharactor = false;

		GameCamera runtime = new GameObject ().AddComponent<GameCamera> ();
		runtime.effectType = EffectTypes.APPLY_CAMERA;
		runtime.name = "CameraApplyRuntime";
		runtime._targetCamera = camera;
		runtime._castTime = time;
		runningRuntime = runtime.gameObject;
	}

	public static void ShowBlurOnCanvas(){
		GameCamera runtime = new GameObject ().AddComponent<GameCamera> ();
		runtime.effectType = EffectTypes.SHOW_BLUR;
		runtime.grabBlur = true;
	}

	public static void FadeIn(float duration){
		BlackFade FadeWork = new GameObject("FadeWork").AddComponent<BlackFade>();
		FadeWork.StartFadeIn (duration);
	}

	public static void FadeOut(float duration,float delete_delay = 0){
		BlackFade FadeWork = new GameObject("FadeWork").AddComponent<BlackFade>();
		FadeWork.StartFadeOut (duration,delete_delay);
	}

	public static void FadeOut(float duration,float delete_delay,ActionCallback function){
		BlackFade FadeWork = new GameObject("FadeWork").AddComponent<BlackFade>();
		FadeWork.callback = function;
		FadeWork.StartFadeOut (duration,delete_delay);
	}

	public static void FadeOutIn(float duration){
		BlackFade FadeWork = new GameObject("FadeWork").AddComponent<BlackFade>();
		FadeWork.StartFadeOutIn (duration);
	}

	void Start(){
		
		if(effectType == EffectTypes.APPLY_CAMERA){
			startLocation = Camera.main.transform.position;
			startQuaternion = Camera.main.transform.rotation;

			Destroy (this.gameObject, _castTime + 0.5f);
		}

		if(effectType == EffectTypes.SHOW_BLUR){
			Camera cm = gameObject.AddComponent<Camera> ();
			cm.gameObject.transform.position = Camera.main.transform.position;
			cm.gameObject.transform.rotation = Camera.main.transform.rotation;
			cm.depth = -99;
			cm.clearFlags = CameraClearFlags.Depth;
		}
	}
		
	void LateUpdate(){
		_timeElapse += Time.deltaTime;

		if(effectType == EffectTypes.APPLY_CAMERA){
			//Unity can catch Infinity
			Camera.main.transform.position = Vector3.Lerp (startLocation, _targetCamera.transform.position, _timeElapse / _castTime);
			Camera.main.transform.rotation = Quaternion.Slerp (startQuaternion, _targetCamera.transform.rotation, _timeElapse / _castTime);
		}
	}

	void OnPostRender() {
		Debug.Log ("in post render");
		if(effectType == EffectTypes.SHOW_BLUR) {
			if (grabBlur) {
				Texture2D tex = new Texture2D(Screen.width, Screen.height);
				tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
				tex.Apply();

				GameObject blur = Instantiate (GameResource.Prefab.UIBlurScreenBackgound);
				blur.GetComponent<Image>().sprite = Sprite.Create(tex,new Rect(0,0,Screen.width,Screen.height),new Vector2(0,0));
				blur.transform.SetParent(GameObject.Find("Canvas").transform,false);

				if (nowBlurObject != null)
					Destroy (nowBlurObject);
				nowBlurObject = blur;
				Destroy (this.gameObject);
			}
		}
	}

	void OnDestroy(){
		IsAimCharactor = true;
	}
}
