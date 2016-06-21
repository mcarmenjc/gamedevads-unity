using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;

public class LoadBanner : MonoBehaviour {


	public string clickThrough = "";
	public string jsonUrl = "";

	void Start () {
		string url = jsonUrl;
		WWW www = new WWW(jsonUrl);
		StartCoroutine(WaitForJSON(www));
	}

	void Update() {
		//TODO: Implement 'Click on gaze'
		//Debug.Log(clickThrough);
	}

	IEnumerator WaitForJSON(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			AdSurfaceData adSurfaceData = AdSurfaceData.CreateFromJSON (www.data);
			Debug.Log (adSurfaceData.uriImage);
			WWW imageWWW = new WWW(adSurfaceData.uriImage);
			StartCoroutine(WaitForImage(imageWWW));
			this.clickThrough = adSurfaceData.uriAction;
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}

	IEnumerator WaitForImage(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null)
		{
			Renderer renderer = GetComponent<Renderer>();
			renderer.material.mainTexture = www.texture;

		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
}