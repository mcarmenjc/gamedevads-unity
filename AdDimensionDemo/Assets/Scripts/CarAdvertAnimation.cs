using UnityEngine;
using System.Collections;

public class CarAdvertAnimation : MonoBehaviour {
	
	void Update () {
		transform.RotateAround (new Vector3 (0, 0, 0), new Vector3 (0, 1, 0), 30f * Time.deltaTime);
	}
}
