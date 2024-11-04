using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    static public bool CheckCollisionLayer(GameObject gameObject, LayerMask layer)
    {
        return ((1 << gameObject.layer) & layer.value) > 0;
    }
}
