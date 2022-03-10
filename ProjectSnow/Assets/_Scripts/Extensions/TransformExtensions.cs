using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void SetZPosition(this Transform tr, float newZ)
    {
        tr.position = new Vector3(tr.position.x, tr.position.y, newZ);
    }
}
