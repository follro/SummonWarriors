using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private const int ExpansionDivisor = 2;
    private const int MinimumExpansionAmount = 10;

    private T prefab;
    private Queue<T> pool;
    private Transform poolParent; 
    public int TotalPoolCapacity { get; private set; }

    public ObjectPool(T poolingTargetPrefab, Transform factory, int poolInitialSize)
    {
        if (poolingTargetPrefab == null)
            throw new System.ArgumentNullException(nameof(poolingTargetPrefab), $"{typeof(ObjectPool<T>).Name}: 프리팹은 null일 수 없습니다");
        if (factory == null)
            Debug.LogError($"{typeof(ObjectPool<T>).Name}: 초기 오브젝트가 생성될 Pool의 부모 facotry를 상속 받는 Transform이 null 입니다");

        pool = new Queue<T>();
        prefab = poolingTargetPrefab;
        TotalPoolCapacity = 0;
        poolParent = new GameObject($"{typeof(T).Name}Pool").transform;
        
        if(factory != null)
            poolParent.SetParent(factory);
        if(poolInitialSize < 0)
            poolInitialSize = 0;

        if (!CreateObject(poolInitialSize))
            Debug.LogError($"{typeof(ObjectPool<T>).Name}: 초기 오브젝트 ({poolInitialSize}개) 생성에 실패했습니다. 풀이 비정상적으로 초기화될 수 있습니다.");
    }

    private bool CreateObject(int count)
    {
        if (count <= 0)
        {
            Debug.LogWarning($"{typeof(ObjectPool<T>).Name}: 생성할 오브젝트 개수가 0이하여서 아무것도 생성하지 않습니다. (Count: {count})");
            return false;
        }
        for (int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(prefab, poolParent);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }

        TotalPoolCapacity += count;
        return true;
    }

    public T GetFromPool()
    {
        T obj = null;
        if (pool.TryDequeue(out obj))
        {
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            int expansionAmount = Mathf.Max(TotalPoolCapacity / ExpansionDivisor, MinimumExpansionAmount);
            Debug.LogWarning($"{typeof(ObjectPool<T>).Name}: 풀({prefab.name})이 비었습니다. 현재 총 용량: {TotalPoolCapacity}. {expansionAmount}개의 새 인스턴스를 추가로 생성합니다.");

            if (!CreateObject(expansionAmount))
            {
                Debug.LogError($"{typeof(ObjectPool<T>).Name}: 오브젝트 ({expansionAmount}개) 확장 생성에 실패했습니다. 오브젝트를 가져올 수 없습니다.");
                return null;
            }
            if (pool.TryDequeue(out obj))
                return obj;
        }
        return null;
    }

    public void ReturnToPool(T obj)
    {
        if (obj == null)
        {
            Debug.LogWarning($"{typeof(ObjectPool<T>).Name}: null 오브젝트를 풀에 반환하려 했습니다.");
            return;
        }

        obj.gameObject.SetActive(false);
        obj.transform.SetParent(poolParent);
        pool.Enqueue(obj);
        
    }

}
