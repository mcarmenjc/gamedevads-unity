using UnityEngine;
using System.Collections;

public class MoveTowardsTarget : MonoBehaviour {

    private float _totalRun= 1.0f;
    private float _lockTime = 10.0f;
    private float _runningTime = 0f;

	Vector3 defaultPosition = new Vector3(-3.0f, 1.40f, 35f);
	string[] slogans = new string[5] {"Hackathon", "24 hours", "Axel Springer", "HACKDAY!", "Sign up now!"};
	int i = 0;

	void Start () {
		GetComponent<TextMesh>().text = slogans[i++];
	}

	void Update () {
		if (transform.position.z > -10) {
			transform.Translate(Vector3.back * Time.deltaTime * 35);
		} else {
			transform.position = defaultPosition;
			GetComponent<TextMesh>().text = slogans[i++];
		}

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
