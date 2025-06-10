using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileData
{
    [SerializeField]
    public GridTileBuilder.TileType[] tiles;

    [SerializeField]
    public Vector2Int Dimensions;

    public void EnsureSize()
    {
        if (tiles == null || Dimensions.y * Dimensions.x != tiles.Length)
        {
            tiles = new GridTileBuilder.TileType[Dimensions.y * Dimensions.x];
            Array.Fill(tiles, GridTileBuilder.TileType.floor);
        }
    }

    [SerializeField]
    public GridTileBuilder.TileType Test;

    public GridTileBuilder.TileType this[int row, int column]
    {
        get { return tiles[column + row * Dimensions.x]; }
        set { tiles[column + row * Dimensions.x] = value; }
    }

    public IEnumerable<GridTileBuilder.TileType> Row(int row)
    {
        for (int c = 0; c < Dimensions.x; c++)
        {
            yield return tiles[c + row * Dimensions.x];
        }
    }

    public IEnumerable<GridTileBuilder.TileType> Column(int column)
    {
        for (int r = 0; r < Dimensions.y; r++)
        {
            yield return tiles[column + r * Dimensions.x];
        }
    }

    public IEnumerable<(int row, int col, GridTileBuilder.TileType value)> GetAll()
    {
        for (int r = 0; r < Dimensions.y; r++)
        {
            for (int c = 0; c < Dimensions.x; c++)
            {
                yield return (r, c, tiles[c + r * Dimensions.x]);
            }
        }
    }
}


[CreateAssetMenu(fileName = "LevelDataObject", menuName = "Scriptable Objects/LevelDataObject")]
public class LevelDataObject : ScriptableObject
{
    [SerializeField]
    public string Name;

    [SerializeField]
    public TileData Tiles;

    // Environment
    public string SkyName;
    public string EnvironmentName;
    public float EnvironmentOffsetX;
    public float EnvironmentOffsetY;
    public float EnvironmentOffsetZ;

    // Toxic
    public int MaxSpreadDistance;
    public float ToxicSpreadTime;
    public float ToxicSpreadTimeVariation;
}
