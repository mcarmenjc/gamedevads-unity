using UnityEditor;

public class CreateAssetBundles
{
	[MenuItem ("Assets/Build AssetBundles")]
	static void BuildAllAssetBundles ()
	{
		BuildPipeline.BuildAssetBundles ("Assets/Build/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneLinuxUniversal);
	}

	[MenuItem ("Build/BuildWebplayerStreamed")]
	public static void MyBuild(){
		string[] levels = new string[] {"Assets/Scenes/advert.unity"};
		BuildPipeline.BuildStreamedSceneAssetBundle( levels, "Assets/Build/advert.unity3d", BuildTarget.StandaloneLinuxUniversal); 
	}

}