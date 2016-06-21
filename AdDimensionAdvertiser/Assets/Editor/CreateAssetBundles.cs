using UnityEditor;

public class CreateAssetBundles
{
	[MenuItem ("Assets/Build AssetBundles")]
	static void BuildAllAssetBundles ()
	{
		BuildPipeline.BuildAssetBundles ("Assets/Scenes", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXUniversal);
	}
}