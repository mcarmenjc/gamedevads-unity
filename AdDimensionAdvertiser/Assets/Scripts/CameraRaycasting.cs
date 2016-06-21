using UnityEngine;
using System.Collections;

public class CameraRaycasting : MonoBehaviour {

	private Transform _camera;

	private void Start()
	{
		_camera = GetComponent<Transform> ();
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
		Debug.DrawRay(_camera.position, _camera.forward * 100, Color.blue, 1);
		RaycastHit hit;
		// Create a ray from the transform position along the transform's z-axis
		Ray ray = new Ray(_camera.position, _camera.forward);
		if (Physics.Raycast (ray, out hit)) 
		{
			Debug.Log ("HIT 3D");
			GameObject selectedObject = hit.transform.gameObject;
		}
	}
}
