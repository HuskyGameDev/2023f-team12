using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static bool TryGetComponent<T>(GameObject obj, out T result)
    {
        result = obj.GetComponent<T>();
        return result is not null;
    }
    public static bool HasComponent<T>(GameObject obj)
    {
        return obj.GetComponent<T>() is not null;
    }

    public static GameObject GetChildOf(GameObject parent, string name)
    {
        return parent.transform.Find(name).gameObject;
    }

    public static int LayerMask(int layer)
    {
        return 1 << layer;
    }
}
