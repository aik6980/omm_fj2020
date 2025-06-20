using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LevelDataImportEditor : EditorWindow
{
    [MenuItem("Tools/Level Data Importer")]
    static void Init() => GetWindow<LevelDataImportEditor>("Import Level Data");

    string folderPath = "Resources/Levels";

    void OnGUI()
    {
        folderPath = EditorGUILayout.TextField("Folder Path", folderPath);

        if (GUILayout.Button("Import Files"))
        {
            var levelsPath = Path.Combine(Application.dataPath, folderPath);
            foreach (var file in Directory.GetFiles(levelsPath, "*.txt"))
            {
                if (file.EndsWith(".cfg.txt"))
                {
                    continue;
                }

                string levelTileDataPath = Path.Combine("Assets", Path.GetRelativePath(Application.dataPath, file));
                string levelMetaDataPath = levelTileDataPath.Insert(levelTileDataPath.Length - ".txt".Length, ".cfg");

                var levelConfigJsonFile = AssetDatabase.LoadAssetAtPath<TextAsset>(levelMetaDataPath);
                var levelConfig = JsonUtility.FromJson<LevelConfigData>(levelConfigJsonFile.text);

                var nameParts = levelConfig.Name.Split(':');
                string assetPath = $"Assets/{folderPath}/Level_{nameParts[1].Trim().Replace(' ', '_')}.asset";

                var levelData = AssetDatabase.LoadAssetAtPath<LevelDataObject>(assetPath);
                if (levelData == null)
                {
                    levelData = CreateInstance<LevelDataObject>();
                    AssetDatabase.CreateAsset(levelData, assetPath);
                }

                levelData.Name = levelConfig.Name;

                // Environment...
                levelData.SkyName = levelConfig.SkyName;
                levelData.EnvironmentName = levelConfig.EnvironmentName;
                levelData.EnvironmentOffsetX = levelConfig.EnvironmentOffsetX;
                levelData.EnvironmentOffsetY = levelConfig.EnvironmentOffsetY;
                levelData.EnvironmentOffsetZ = levelConfig.EnvironmentOffsetZ;

                // Toxic spread...
                levelData.MaxSpreadDistance = levelConfig.MaxSpreadDistance;
                levelData.ToxicSpreadTime = levelConfig.ToxicSpreadTime;
                levelData.ToxicSpreadTimeVariation = levelConfig.ToxicSpreadTimeVariation;

                var levelTileDataContents = AssetDatabase.LoadAssetAtPath<TextAsset>(levelTileDataPath);

                var rows = levelTileDataContents.text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int rowIndex = 0; rowIndex < rows.Length; ++rowIndex)
                {
                    var columns = rows[rowIndex].Split(new char[]{ ' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                    if (rowIndex == 0)
                    {
                        levelData.Tiles = new TileData();
                        levelData.Tiles.Dimensions = new Vector2Int(columns.Length, rows.Length);
                        levelData.Tiles.EnsureSize();
                    }
                    Debug.Assert(columns.Length == levelData.Tiles.Dimensions.x);

                    for (int colIndex = 0; colIndex < columns.Length; ++colIndex)
                    {
                        var tile = GetTileTypeFromText(columns[colIndex]);
                        levelData.Tiles[rowIndex, colIndex] = tile;
                    }
                }
                EditorUtility.SetDirty(levelData);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private GridTileBuilder.TileType GetTileTypeFromText(string text)
    {
        switch (text)
        {
            default:
            case "0": return GridTileBuilder.TileType.floor;
            case "1": return GridTileBuilder.TileType.start;
            case "2": return GridTileBuilder.TileType.exit;
            case "3": return GridTileBuilder.TileType.toxic_pool;
            case "4": return GridTileBuilder.TileType.obstacle;
            case "5": return GridTileBuilder.TileType.grass;
            case "6": return GridTileBuilder.TileType.toxic;
        }
    }
}
