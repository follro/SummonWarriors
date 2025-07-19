using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory<T> : MonoBehaviour where T : IProduct
{
    public abstract T GetProduct(Vector3 pos, int id = 0);

    public abstract void ReleaseProduct(T product, int id = 0);

}
