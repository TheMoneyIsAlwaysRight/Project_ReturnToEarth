using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildAssetBundles : MonoBehaviour
{
    [MenuItem("Bundles/Build AssetBundles Android")]
    static public void BuildAllAssetBundlesAndroid()
    {
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
    }

    [MenuItem("Bundles/Build AssetBundles Window")]
    static public void BuildAllAssetBundlesWindow()
    {
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }

    [MenuItem("Bundles/Clear Cache")]
    static public void ClearingCache()
    {
        Caching.ClearCache();
        print("cache clearing done");
    }
}
