    using Cysharp.Threading.Tasks;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


public class AssetLoadManager : Singleton<AssetLoadManager>
{
    public enum AssetLoadMode
    {
        Resources,
        AssetBundle,
        Addressables,
        External,
        End
    }

    private IAssetLoader[] assetLoader;
    
    private void Awake()
    {
        PreserveSingleton();

        Initialize();
    }

    private void Initialize()
    {
        assetLoader = new IAssetLoader[(int)AssetLoadMode.End];
        for (int i = 0; i < (int)AssetLoadMode.End; i++)
        {
            //임시로 리솔시스 에셋로더를 넣어놓는다
            assetLoader[i] = new ResourcesAssetLoader();
        }
    }

    public T LoadAsset<T>(AssetLoadMode assetLoadMode, string path) where T : Object
    {
        return assetLoader[(int)assetLoadMode].LoadAsset<T>(path);
    }

    public async UniTask<T> LoadAssetAsync<T>(AssetLoadMode assetLoadMode, string path) where T : Object
    {
        if (assetLoader[(int)assetLoadMode] == null)
            return null;
        return await assetLoader[(int)assetLoadMode].LoadAssetAsync<T>(path);    
    }
}