using UnityEngine;
using System.Collections;

public class CarAdvertAnimation : MonoBehaviour {
    private float _totalRun= 1.0f;
    private float _lockTime = 10.0f;
    private float _runningTime = 0f;	

	void Update () {
		transform.RotateAround (new Vector3 (0, 0, 0), new Vector3 (0, 1, 0), 30f * Time.deltaTime);

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
