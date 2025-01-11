using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class ButtonAttribute : Attribute
{
    public string Label { get; }

    public ButtonAttribute(string label = null)
    {
        Label = label;
    }
}
