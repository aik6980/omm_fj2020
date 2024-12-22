using UnityEngine;

public static class GameObjectExtensions
{
    public static GameObject FlippedX(this GameObject o)
    {
        o.transform.localScale = o.transform.localScale.Scale(-1.0f, 1.0f, 1.0f);
        return o;
    }
    public static GameObject FlippedY(this GameObject o)
    {
        o.transform.localScale = o.transform.localScale.Scale(1.0f, -1.0f, 1.0f);
        return o;
    }
}
