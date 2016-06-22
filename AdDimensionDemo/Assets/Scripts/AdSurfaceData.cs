using UnityEngine;
using System.Collections;
using System;

using UnityEngine;

[Serializable]
public class AdSurfaceData
{
	public int id;
	public int dimX;
	public int dimY;
	public string uriImage;
	public string uriAction;
	public string seName;

	public static AdSurfaceData CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<AdSurfaceData>(jsonString);
	}
}
