using UnityEngine;
using System.Collections;
using System;

public class CameraRaycasting : MonoBehaviour {

	public string Url;
	public string AssetName = "advert";

	private Transform _camera;
	private GameObject _lastSelectedObject;
	private float _mainSpeed = 2.0f; //regular speed
	private float _shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
	private float _maxShift = 1000.0f; //Maximum speed when holdin gshift
	private float _camSens = 0.25f; //How sensitive it with mouse
	private Vector3 _lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
	private float _totalRun= 1.0f;
	private float _lockTime = 2.0f;
	private float _impressionTime = 1.0f;
	private float _runningTime = 0f;

	private void Start()
	{
		_camera = GetComponent<Transform> ();
		Debug.Log ("LOBBY");
		Debug.Log (_camera.position);
		_lastSelectedObject = null;
		_runningTime = 0f;
	}

	private void Update()
	{
		PollControls();
		EyeRaycast();
	}

	private void PollControls()
	{
		_lastMouse = Input.mousePosition - _lastMouse ;
		_lastMouse = new Vector3(-_lastMouse.y * _camSens, _lastMouse.x * _camSens, 0 );
		_lastMouse = new Vector3(transform.eulerAngles.x + _lastMouse.x , transform.eulerAngles.y + _lastMouse.y, 0);
		transform.eulerAngles = _lastMouse;
		_lastMouse =  Input.mousePosition;
		//Mouse  camera angle done.  

		//Keyboard commands
		float f = 0.0f;
		Vector3 p = GetBaseInput();
		if (Input.GetKey (KeyCode.LeftShift)){
			_totalRun += Time.deltaTime;
			p  = p * _totalRun * _shiftAdd;
			p.x = Mathf.Clamp(p.x, -_maxShift, _maxShift);
			p.y = Mathf.Clamp(p.y, -_maxShift, _maxShift);
			p.z = Mathf.Clamp(p.z, -_maxShift, _maxShift);
		}
		else{
			_totalRun = Mathf.Clamp(_totalRun * 0.5f, 1f, 1000f);
			p = p * _mainSpeed;
		}

		p = p * Time.deltaTime;
		Vector3 newPosition = transform.position;
		transform.Translate(p);
		newPosition.x = transform.position.x;
		newPosition.z = transform.position.z;
		transform.position = newPosition;

	}
		
	private void EyeRaycast()
	{
		RaycastHit hit;
		Ray ray = new Ray(_camera.position, _camera.forward);
		if (Physics.Raycast (ray, out hit)) {
			_lastSelectedObject = hit.transform.gameObject;
			_runningTime += Time.deltaTime * 1;

			LoadBanner loadBanner = _lastSelectedObject.GetComponent<LoadBanner> ();
			if (_lastSelectedObject.tag.Contains ("AdSurface")) {
				loadBanner.RegisterImpression ();
			}

			if (_runningTime >= _impressionTime) {
				
			}
			if (_runningTime >= _lockTime) {
				_runningTime = 0f;
				_lastSelectedObject.GetComponent<Renderer> ().material.color = Color.red;
				Debug.Log (Application.loadedLevelName);
				try { //WTF
					PlayerPrefs.SetString ("originScene", Application.loadedLevelName);
					Debug.Log ("!!!!!!: " + PlayerPrefs.GetString ("originScene"));
				} catch (PlayerPrefsException e) {
					Debug.Log (e.Message);
				}
				if (_lastSelectedObject.tag.Contains ("AdSurface")) {
					Url = loadBanner.clickThrough;
					loadBanner.RegisterClickThrough ();
				}
				StartCoroutine (LoadAssetBundles ());
			} else {
				_lastSelectedObject.GetComponent<Renderer> ().material.color = Color.white;
			}
		} else {
			_runningTime = 0f;
			if (_lastSelectedObject != null) {
				_lastSelectedObject.GetComponent<Renderer> ().material.color = Color.white;
			}
		}
	}

	private Vector3 GetBaseInput() { //returns the basic values, if it's 0 than it's not active.
		Vector3 p_Velocity = new Vector3();
		if (Input.GetKey (KeyCode.W)){
			p_Velocity += new Vector3(0, 0 , 1);
		}
		if (Input.GetKey (KeyCode.S)){
			p_Velocity += new Vector3(0, 0, -1);
		}
		if (Input.GetKey (KeyCode.A)){
			p_Velocity += new Vector3(-1, 0, 0);
		}
		if (Input.GetKey (KeyCode.D)){
			p_Velocity += new Vector3(1, 0, 0);
		}
		return p_Velocity;
	}

	private IEnumerator LoadAssetBundles() {
		Debug.Log ("LoadAssetBundles triggered");
		Debug.Log (AssetName);
		Debug.Log (this.Url);
		// Download the file from the URL. It will not be saved in the Cache
		using (WWW www = new WWW(this.Url)) {
			yield return www;
			if (www.error != null)
				throw new Exception("WWW download had an error:" + www.error);
			AssetBundle bundle = www.assetBundle;
			if (AssetName == "")
				Instantiate (bundle.mainAsset);
			else {

				string[] scenePaths = bundle.GetAllScenePaths ();
				foreach (string scenePath in scenePaths) {
					Debug.Log (scenePath);
				}
				UnityEngine.SceneManagement.SceneManager.LoadScene("advert");
			}
			// Unload the AssetBundles compressed contents to conserve memory
			//WTF causing camera problem
			//bundle.Unload(false);

		} // memory is freed from the web stream (www.Dispose() gets called implicitly)
	}
}
