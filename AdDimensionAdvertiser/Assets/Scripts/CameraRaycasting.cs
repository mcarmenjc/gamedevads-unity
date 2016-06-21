using UnityEngine;
using System.Collections;

public class CameraRaycasting : MonoBehaviour {

	private Transform _camera;
	private GameObject _lastSelectedObject;
	private float _mainSpeed = 10.0f; //regular speed
	private float _shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
	private float _maxShift = 1000.0f; //Maximum speed when holdin gshift
	private float _camSens = 0.25f; //How sensitive it with mouse
	private Vector3 _lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
	private float _totalRun= 1.0f;

	private void Start()
	{
		_camera = GetComponent<Transform> ();
		_lastSelectedObject = null;
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
		if (Input.GetKey(KeyCode.Space)){ //If player wants to move on X and Z axis only
			transform.Translate(p);
			newPosition.x = transform.position.x;
			newPosition.z = transform.position.z;
			transform.position = newPosition;
		}
		else{
			transform.Translate(p);
		}
	}
		
	private void EyeRaycast()
	{
		RaycastHit hit;
		Ray ray = new Ray(_camera.position, _camera.forward);
		if (Physics.Raycast (ray, out hit)) {
			_lastSelectedObject = hit.transform.gameObject;
			_lastSelectedObject.GetComponent<Renderer> ().material.color = Color.red;
		} else {
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
}
