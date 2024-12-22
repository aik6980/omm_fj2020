using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public static class EnumExtensions
{
    public static T Mask<T>(this T e, int mask) where T : Enum => (T)Enum.ToObject(typeof(T), Convert.ToInt32(e) & mask);

    public static int Index<T>(this T e) where T : Enum => Enum.GetValues(typeof(T)).Cast<T>().TakeWhile(o => !Equals(o, e)).Count();
}
