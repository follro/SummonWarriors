using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> 
{
    private T prefab;
    private Queue<T> pool;
    private GameObject poolParent; 

    ObjectPool(T poolingTargetPrefab, int poolInitialSize)
    {
        pool = new Queue<T>();
        prefab = poolingTargetPrefab;
        //poolParent = new GameObject()

    }

}
