using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

public interface IAssetLoader
{
    // 스크립터블 오브젝트, 오브젝트 등 모든 유니티의 에셋을 다루는 것들은 유니티의 Object를 상속받음 
    // 다른 기본 타입이 못들어가게 막기 위함도 있음
    public T LoadAsset<T>(string path) where T : Object; 
    public UniTask<T> LoadAssetAsync<T>(string path) where T : Object;
    public void UnLoadAsset(Object asset);

}
