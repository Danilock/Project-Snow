using UnityEngine;

public static class RectTransformExtensions
{
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }
 
    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }
 
    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }
 
    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }

    public static float GetWidth(this RectTransform rt)
    {
        return rt.sizeDelta.x;
    }

    public static void Reset(this RectTransform rt)
    {
        SetLeft(rt, 0f);
        SetRight(rt, 0f);
        SetBottom(rt, 0f);
        SetTop(rt, 0f);
    }

    public static void SetZPosition(this RectTransform rt, float newZ)
    {
        rt.position = new Vector3(rt.position.x, rt.position.y, newZ);
    }
}
