using UnityEngine;
using System.Collections;

public class CameraRaycasting : MonoBehaviour {

	private Transform _camera;
	private GameObject _lastSelectedObject;

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
		float xAxisValue = Input.GetAxis("Horizontal");
		float zAxisValue = Input.GetAxis("Vertical");
		if(Camera.current != null)
		{
			Camera.current.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));
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
}
