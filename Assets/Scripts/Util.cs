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
}
