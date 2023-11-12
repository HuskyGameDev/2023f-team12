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

    // https://discussions.unity.com/t/rotate-a-vector-around-a-certain-point/81225/3
    /*public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }*/
}
