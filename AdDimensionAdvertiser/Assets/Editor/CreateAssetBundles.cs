using UnityEditor;

public class CreateAssetBundles
{
	[MenuItem ("Assets/Build AssetBundles")]
	static void BuildAllAssetBundles ()
	{
		BuildPipeline.BuildAssetBundles ("Assets/Build/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneLinuxUniversal);
	}
}