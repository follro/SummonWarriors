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
            throw new System.ArgumentNullException(nameof(poolingTargetPrefab), $"{typeof(ObjectPool<T>).Name}: �������� null�� �� �����ϴ�");
        if (factory == null)
            Debug.LogError($"{typeof(ObjectPool<T>).Name}: �ʱ� ������Ʈ�� ������ Pool�� �θ� facotry�� ��� �޴� Transform�� null �Դϴ�");

        pool = new Queue<T>();
        prefab = poolingTargetPrefab;
        TotalPoolCapacity = 0;
        poolParent = new GameObject($"{typeof(T).Name}Pool").transform;
        
        if(factory != null)
            poolParent.SetParent(factory);
        if(poolInitialSize < 0)
            poolInitialSize = 0;

        if (!CreateObject(poolInitialSize))
            Debug.LogError($"{typeof(ObjectPool<T>).Name}: �ʱ� ������Ʈ ({poolInitialSize}��) ������ �����߽��ϴ�. Ǯ�� ������������ �ʱ�ȭ�� �� �ֽ��ϴ�.");
    }

    private bool CreateObject(int count)
    {
        if (count <= 0)
        {
            Debug.LogWarning($"{typeof(ObjectPool<T>).Name}: ������ ������Ʈ ������ 0���Ͽ��� �ƹ��͵� �������� �ʽ��ϴ�. (Count: {count})");
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
            Debug.LogWarning($"{typeof(ObjectPool<T>).Name}: Ǯ({prefab.name})�� ������ϴ�. ���� �� �뷮: {TotalPoolCapacity}. {expansionAmount}���� �� �ν��Ͻ��� �߰��� �����մϴ�.");

            if (!CreateObject(expansionAmount))
            {
                Debug.LogError($"{typeof(ObjectPool<T>).Name}: ������Ʈ ({expansionAmount}��) Ȯ�� ������ �����߽��ϴ�. ������Ʈ�� ������ �� �����ϴ�.");
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
            Debug.LogWarning($"{typeof(ObjectPool<T>).Name}: null ������Ʈ�� Ǯ�� ��ȯ�Ϸ� �߽��ϴ�.");
            return;
        }

        obj.gameObject.SetActive(false);
        obj.transform.SetParent(poolParent);
        pool.Enqueue(obj);
        
    }

}
