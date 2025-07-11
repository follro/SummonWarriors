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
            Debug.LogError($"{typeof(ResourcesAssetLoader).Name}: '{path}'���� {typeof(T).Name}Ÿ���� ������ �ε��� �� �������ϴ�.");

        return loadedAsset;
    }

    public void UnLoadAsset(Object asset)
    {
        Debug.LogError($"{typeof(ResourcesAssetLoader).Name}: ������ UnLoadAsset �Լ��� �������� �ʽ��ϴ� ");
        //�����հ��� �����ǰ� ������ ó���ϱ� ����� ������ ����
    }
}
