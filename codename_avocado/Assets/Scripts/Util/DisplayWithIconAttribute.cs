using UnityEngine;

public class DisplayWithIconAttribute : PropertyAttribute
{
    public string DisplayName { get; }
    public string IconPath { get; }

    public DisplayWithIconAttribute(string displayName, string iconPath = null)
    {
        DisplayName = displayName;
        IconPath = iconPath;
    }
}
