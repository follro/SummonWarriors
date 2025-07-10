using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssetLoadManager : Singleton<AssetLoadManager>
{
    [SerializeField]
    private IAssetLoader assetLoader;

    private void Awake()
    {
        PreserveSingleton();

        if(assetLoader != null) 
            assetLoader = new ResourcesAssetLoader();
    }

    private T LoadAsset<T>(string path) where T : Object
    {
        return assetLoader.LoadAsset<T>(path);
    }

    private T LoadAssetAsync<T>(string path) where T : Object
    {
        return assetLoader.LoadAssetAsync<T>(path);    
    }
}
