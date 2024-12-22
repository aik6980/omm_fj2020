using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DisplayWithIconAttribute))]
public class DisplayNameAndIconCustomDecoratorDrawer : DecoratorDrawer
{
    public override void OnGUI(Rect position)
    {
        Rect propertyRect = position;

        // Retrieve the custom attribute
        var displayAttribute = (DisplayWithIconAttribute)attribute;

        if (!string.IsNullOrEmpty(displayAttribute.IconPath))
        {
            // Load the icon from the specified path
            var icon = AssetDatabase.LoadAssetAtPath<Texture>(displayAttribute.IconPath);

            // Set up the icon size and position
            float iconSize = position.height; // Square icon, height matches property height
            var iconRect = new Rect(position.x, position.y, iconSize, iconSize);

            // Set up the property field position
            propertyRect = new Rect(position.x + iconSize + 5, position.y, position.width - iconSize - 5, position.height);

            // Draw the icon if it exists
            if (icon != null)
            {
                GUI.DrawTexture(iconRect, icon, ScaleMode.ScaleToFit);
            }
        }

        GUI.Label(propertyRect, displayAttribute.DisplayName);
    }
}
