    using Cysharp.Threading.Tasks;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class AssetLoadManager : Singleton<AssetLoadManager>
    {
        private IAssetLoader assetLoader = null;   

        private void Awake()
        {
            PreserveSingleton();

            if(assetLoader == null) 
                assetLoader = new ResourcesAssetLoader();
        }

        public T LoadAsset<T>(string path) where T : Object
        {
            return assetLoader.LoadAsset<T>(path);
        }

        public async UniTask<T> LoadAssetAsync<T>(string path) where T : Object
        {
            if (assetLoader == null)
                return null;

            return await assetLoader.LoadAssetAsync<T>(path);    
        }
    }
