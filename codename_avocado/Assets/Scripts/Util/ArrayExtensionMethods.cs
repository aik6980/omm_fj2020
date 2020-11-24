using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtensionMethods
{
    public static void ForEach<T>(this T[,] array, System.Action<T> fn)
    {
        for (int y = 0; y < array.GetLength(1); ++y)
        {
            for (int x = 0; x < array.GetLength(0); ++x)
            {
                fn(array[x, y]);
            }
        }
    }

    public static void ForEach<T>(this T[,] array, System.Action<int, int, T> fn)
    {
        for (int y = 0; y < array.GetLength(1); ++y)
        {
            for (int x = 0; x < array.GetLength(0); ++x)
            {
                fn(x, y, array[x, y]);
            }
        }
    }
}
