using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesAssetLoader : IAssetLoader
{
    public T LoadAsset<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public async UniTask<T> LoadAssetAsync<T>(string path) where T : Object    
    {
        ResourceRequest request = Resources.LoadAsync<T>(path);
        await request.ToUniTask();
        T loadedAsset = request.asset as T;

        if (loadedAsset == null)
            Debug.LogError($"{typeof(ResourcesAssetLoader).Name}: '{path}'에서 {typeof(T).Name}타입의 에셋을 로드할 수 없었습니다.");

        return loadedAsset;
    }

    public void UnLoadAsset(Object asset)
    {
        Debug.LogError($"{typeof(ResourcesAssetLoader).Name}: 적절한 UnLoadAsset 함수가 존재하지 않습니다 ");
        //프리팹같이 참조되고 있으면 처리하기 어려움 에러도 나고
    }
}
