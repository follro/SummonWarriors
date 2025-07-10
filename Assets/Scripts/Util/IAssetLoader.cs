using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

public interface IAssetLoader
{
    // ��ũ���ͺ� ������Ʈ, ������Ʈ �� ��� ����Ƽ�� ������ �ٷ�� �͵��� ����Ƽ�� Object�� ��ӹ��� 
    // �ٸ� �⺻ Ÿ���� ������ ���� ���Ե� ����
    public T LoadAsset<T>(string path) where T : Object; 
    public UniTask<T> LoadAssetAsync<T>(string path) where T : Object;
    public void UnLoadAsset(Object asset);

}
