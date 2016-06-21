using UnityEngine;
using System.Collections;
using System;

public class Startup : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start() {
		// Download the file from the URL. It will not be saved in the Cache
		using (WWW www = new WWW(this.Url)) {
			yield return www;
			if (www.error != null)
				throw new Exception("WWW download had an error:" + www.error);
			AssetBundle bundle = www.assetBundle;
			if (AssetName == "")
				Instantiate (bundle.mainAsset);
			else {

				String[] scenePaths = bundle.GetAllScenePaths ();
				foreach (string scenePath in scenePaths) {
					Debug.Log (scenePath);
				}
				Application.LoadLevel ("advert");

			}
			// Unload the AssetBundles compressed contents to conserve memory
			bundle.Unload(false);

		} // memory is freed from the web stream (www.Dispose() gets called implicitly)
	}

	// Update is called once per frame
	void Update () {
	
	} 


	public string Url;
	public string AssetName;

}
