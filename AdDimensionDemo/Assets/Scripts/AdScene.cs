using UnityEngine;
using System.Collections;

public class AdScene : MonoBehaviour {

	private float _totalRun= 1.0f;
	private float _lockTime = 10.0f;
	private float _runningTime = 0f;

	// Use this for initialization
	void Start () {
		Debug.Log ("STARTED");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("UPDATED");
		_runningTime += Time.deltaTime * 1;

		if( _runningTime >= _lockTime)
		{
			_runningTime = 0f;
			string originScene = PlayerPrefs.GetString ("originScene"); 
			Debug.Log ("!!!!" + originScene);
			PlayerPrefs.DeleteKey ("originScene");
			Application.LoadLevel (originScene);
		}
	
	}
}
