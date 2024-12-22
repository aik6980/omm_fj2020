using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 Scale(this Vector3 vec, float x, float y, float z)
    {
        return new Vector3(vec.x * x, vec.y * y, vec.z * z);
    }
}
