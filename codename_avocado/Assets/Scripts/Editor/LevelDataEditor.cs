using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TileData))]
public class LevelDataEditor : PropertyDrawer
{
    private const int cellSize = 32;
    private const int cellMargin = 0;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var dimensionsProp = property.FindPropertyRelative("Dimensions");
        int height = dimensionsProp.vector2IntValue.y;

        return EditorGUIUtility.singleLineHeight + (cellSize + cellMargin) * height + EditorGUIUtility.singleLineHeight * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string propertyLabelText = label.text;
        var dimensionsProp = property.FindPropertyRelative("Dimensions");
        var dataProp = property.FindPropertyRelative("tiles");

        var dimensions = dimensionsProp.vector2IntValue;

        // Ensure data list has correct size
        SerializedObject so = property.serializedObject;

        EditorGUI.BeginProperty(position, label, property);

        Rect d = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        EditorGUI.BeginChangeCheck();
        dimensionsProp.vector2IntValue = EditorGUI.Vector2IntField(d, "Level Size", dimensions);

        if (EditorGUI.EndChangeCheck())
        {
            dimensionsProp.serializedObject.ApplyModifiedProperties();
        }

        var gridObj = fieldInfo.GetValue(so.targetObject) as TileData;
        gridObj?.EnsureSize();

        Rect r = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(r, propertyLabelText);
        r.y += EditorGUIUtility.singleLineHeight + 4;

        for (int y = 0; y < dimensions.y; y++)
        {
            r.x = position.x;
            for (int x = 0; x < dimensions.x; x++)
            {
                int index = y * dimensions.x + x;
                var tileProp = dataProp.GetArrayElementAtIndex(index);
                var icon = new GUIContent(GetIcon((GridTileBuilder.TileType)tileProp.enumValueIndex), $"{(GridTileBuilder.TileType)tileProp.enumValueIndex}");

                if (GUI.Button(new Rect(r.x, r.y, cellSize, cellSize), icon, GUIStyle.none))
                {
                    var next = (tileProp.enumValueIndex + 1) % System.Enum.GetValues(typeof(GridTileBuilder.TileType)).Length;
                    dataProp.GetArrayElementAtIndex(index).enumValueIndex = next;
                    dataProp.serializedObject.ApplyModifiedProperties();
                }


                r.x += cellSize + cellMargin;
            }

            r.y += cellSize + cellMargin;
        }

        EditorGUI.EndProperty();
    }

    private Texture2D GetIcon(GridTileBuilder.TileType tileType)
    {
        string[] icons = new string[7]
        {
            "Assets/Scripts/Editor/ico_grass_tile.png",         // grass,
            "Assets/Scripts/Editor/ico_shallow_toxic_tile.png", // toxic,
            "Assets/Scripts/Editor/ico_entrance_tile.png",      // start,
            "Assets/Scripts/Editor/ico_exit_tile.png",          // exit,
            "Assets/Scripts/Editor/ico_obstacle_tile.png",      // obstacle,
            "Assets/Scripts/Editor/ico_empty_tile.png",         // floor,
            "Assets/Scripts/Editor/ico_source_toxic_tile.png"   // toxic_pool
        };

        return AssetDatabase.LoadAssetAtPath<Texture2D>(icons[(int)tileType]);
    }
}
