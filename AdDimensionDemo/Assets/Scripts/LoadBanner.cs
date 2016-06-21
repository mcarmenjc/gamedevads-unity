using UnityEngine;
using System.Collections;

// Get the latest webcam shot from outside "Friday's" in Times Square
public class LoadBanner : MonoBehaviour {
	public string url = "http://usrtk.org/wp-content/uploads/2015/12/coca-cola.jpg";

	IEnumerator Start() {
		// Start a download of the given URL
		WWW www = new WWW(url);

		// Wait for download to complete
		yield return www;

		// assign texture
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = www.texture;
	}
}