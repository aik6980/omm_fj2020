using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TileConfiguration
{
    [System.Flags]
    public enum RotationDirections
    {
        None = 0,

        North = 1 << 0,
        East = 1 << 1,
        South = 1 << 2,
        West = 1 << 3,

        All = North | East | South | West
    }

    [System.Flags]
    public enum FlipDirections
    {
        None,

        NorthSouth = 1 << 0,
        EastWest = 1 << 1,

        All = NorthSouth | EastWest
    }
}

[System.Serializable]
public class ShallowToxicTileSet
{
    [DisplayWithIcon("1) Zero Neighbours", "Assets\\tile_icon_1.png")]
    public TileSet type1;

    [Header("Shallow Tile Adjacencies...")]
    [DisplayWithIcon("2) One Neighbour", "Assets\\tile_icon_2.png")]
    public TileSet type2;

    [DisplayWithIcon("3) Two Neighbours (Opposite)", "Assets\\tile_icon_3.png")]
    public TileSet type3;

    [DisplayWithIcon("4) Two Neighbours (Adjacent)", "Assets\\tile_icon_4.png")]
    public TileSet type4;

    [DisplayWithIcon("5) Three Neighbours", "Assets\\tile_icon_5.png")]
    public TileSet type5;

    [Header("One Deep Tile Adjacencies...")]
    [DisplayWithIcon("2a) One Neighbour", "Assets\\tile_icon_2a.png")]
    public TileSet type2a;

    [DisplayWithIcon("3a) Two Neighbours (Opposite)", "Assets\\tile_icon_3a.png")]
    public TileSet type3a;

    [DisplayWithIcon("4a) Two Neighbours (Adjacent)", "Assets\\tile_icon_4a.png")]
    public TileSet type4a;

    [DisplayWithIcon("4b) Two Neighbours (Adjacent)", "Assets\\tile_icon_4b.png")]
    public TileSet type4b;

    [DisplayWithIcon("4c) Two Neighbours (Adjacent)", "Assets\\tile_icon_4c.png")]
    public TileSet type4c;

    [DisplayWithIcon("4d) Two Neighbours (Adjacent)", "Assets\\tile_icon_4d.png")]
    public TileSet type4d;

    [DisplayWithIcon("5a) Three Neighbours", "Assets\\tile_icon_5a.png")]
    public TileSet type5a;

    [DisplayWithIcon("5b) Three Neighbours", "Assets\\tile_icon_5b.png")]
    public TileSet type5b;

    [DisplayWithIcon("5c) Three Neighbours", "Assets\\tile_icon_5c.png")]
    public TileSet type5c;

    [DisplayWithIcon("5d) Three Neighbours", "Assets\\tile_icon_5d.png")]
    public TileSet type5d;

    [Header("Two Deep Tile Adjacencies...")]
    [DisplayWithIcon("3e) Two Neighbours (Opposite)", "Assets\\tile_icon_3e.png")]
    public TileSet type3e;

    [DisplayWithIcon("4e) Two Neighbours (Adjacent)", "Assets\\tile_icon_4e.png")]
    public TileSet type4e;

    [DisplayWithIcon("5e) Three Neighbours", "Assets\\tile_icon_5e.png")]
    public TileSet type5e;

    [DisplayWithIcon("5f) Three Neighbours", "Assets\\tile_icon_5f.png")]
    public TileSet type5f;

    [Header("Three Deep Tile Adjacencies...")]
    [DisplayWithIcon("5g) Three Neighbours", "Assets\\tile_icon_5g.png")]
    public TileSet type5g;
}

[System.Serializable]
public class TileSet
{
    public Tile[] tiles;

    [EnumButtons]
    public TileConfiguration.RotationDirections allowableRotations;

    [EnumButtons]
    public TileConfiguration.FlipDirections allowableFlips;
}

[System.Serializable]
public class AdornmentSet
{
    public Tile[] adornments;

    [EnumButtons]
    public TileConfiguration.RotationDirections allowableRotations;

    [EnumButtons]
    public TileConfiguration.FlipDirections allowableFlips;

    [Range(0, 10)]
    public int minAdornments = 0;

    [Range(0, 10)]
    public int maxAdornments = 0;
}

[System.Serializable]
public class Tile
{
    public GameObject tile;

    [Range(0.0f, 100.0f)]
    public float weight = 1.0f;
}

public class GridTileBuilder : MonoBehaviour
{
    public GameObject m_coordRepresentativePrefab;
    public TileFactoryConfiguration factoryConfiguration;

    public GameObject[] grass_tile;

    public GameObject[] toxic_src_empty_tile;
    public GameObject[] toxic_src_full_tile;
    public GameObject[] toxic_big_spill_tile;
    public GameObject[] toxic_small_spill_tile;

    public GameObject[] start_tile;
    public GameObject[] exit_tile;
    public GameObject[] obstacle_tile;
    public GameObject[] floor_tile;

    // toxic
    public GameObject[] toxic_vfx;
    public GameObject[] grass_vfx;


    public TileConfiguration toxic_no_join_tile;
    public TileConfiguration toxic_join_north_tile;
    public TileConfiguration toxic_join_north_south_tile;
    public TileConfiguration toxic_join_north_east_tile;
    public TileConfiguration toxic_join_north_east_south_tile;

    public GameObject[] toxic_adornments;

    public enum TileType
    {
        grass,
        toxic,
        start,
        exit,
        obstacle,
        floor,
        toxic_pool
    }

    public enum ToxicLevel
    {
        none,
        shallow,
        deep
    }

    public GameObject GetTile(WorldGrid worldGrid, Coordinate coord)
    {
        TileFactory factory = new TileFactory(factoryConfiguration, m_coordRepresentativePrefab);
        return factory.CreateTileRepresentation(worldGrid, coord);
    }

    public GameObject[] GetTileAdornments(Coordinate coord)
    {
        switch(coord.Type)
        {
            case TileType.toxic_pool: return GetRandomAdornmentsFromSet(toxic_adornments, 1);
            case TileType.grass:
                return null;// up to 2?

            case TileType.toxic:
            case TileType.start:
            case TileType.exit:
            case TileType.obstacle:
            case TileType.floor:
            default:
                return null;
        }

        GameObject[] GetRandomAdornmentsFromSet(GameObject[] gameObjects, int num_adornments)
        {
            var adornments = new GameObject[num_adornments];
            for (int i = 0; i < adornments.Length; ++i)
            {
                adornments[i] = GetRandomFromSet(toxic_adornments);
            }
            return adornments;
        }
    }

    public CoordinateRepresentation InstantiateTile(WorldGrid worldGrid, Coordinate coord)
    {
        var instance = Instantiate(m_coordRepresentativePrefab);
        var representation = instance.GetComponent<CoordinateRepresentation>();
        representation.Configure(worldGrid, coord, this);
        return representation;
    }

    public GameObject InstantiateToxicVFX()
    {
        return Instantiate(toxic_vfx[0]);
        
    }

    public GameObject InstantiateGrassVFX()
    {
        return Instantiate(grass_vfx[0]);
    }

    private GameObject GetRandomFromSet(GameObject[] gameObjects)
    {
        if (gameObjects.Length == 0)
        {
            Debug.Assert(false);
            return new GameObject();
        }
        return Instantiate(gameObjects[Random.Range(0, gameObjects.Length)]);
    }

    public GameObject ConfigureTile(WorldGrid worldGrid, Coordinate coord)
    {
        switch (coord.Type)
        {
            case TileType.toxic:
            case TileType.toxic_pool:
                return ConfigureToxicTile(worldGrid, coord);
        }

        return null;
    }

    private GameObject ConfigureToxicTile(WorldGrid worldGrid, Coordinate coord)
    {
        ////Direction[] directions = { Direction.North, Direction.East, Direction.South, Direction.West };
        //Coordinate[] adjacentTiles = {
        //    worldGrid.GetAdjacentCoordinate(coord.m_Position, Direction.North),
        //    worldGrid.GetAdjacentCoordinate(coord.m_Position, Direction.East),
        //    worldGrid.GetAdjacentCoordinate(coord.m_Position, Direction.South),
        //    worldGrid.GetAdjacentCoordinate(coord.m_Position, Direction.West)
        //};
        //
        //int numToxicNeighbours = 0;
        //int firstToxicNeighbourIndex = 5;
        //int lastToxicNeighbourIndex = -1;
        //for (int i = 0; i < 4; ++i)
        //{
        //    if (IsPolluted(adjacentTiles[i]))
        //    {
        //        ++numToxicNeighbours;
        //        firstToxicNeighbourIndex = System.Math.Min(firstToxicNeighbourIndex, i);
        //        lastToxicNeighbourIndex = System.Math.Max(lastToxicNeighbourIndex, i);
        //    }
        //}
        //
        //switch (numToxicNeighbours)
        //{
        //    case 0: return GetRandomisedTile(toxic_no_join_tile, 0);
        //    case 1: return GetRandomisedTile(toxic_join_north_tile, firstToxicNeighbourIndex);
        //    case 2:
        //        {
        //            // Opposite sides or adjacent sides?
        //            if (IsPolluted(adjacentTiles[0]) && IsPolluted(adjacentTiles[2]) ||
        //                IsPolluted(adjacentTiles[1]) && IsPolluted(adjacentTiles[3]))
        //            {
        //                return GetRandomisedTile(toxic_join_north_south_tile, firstToxicNeighbourIndex);
        //            }
        //            else
        //            {
        //                return GetRandomisedTile(toxic_join_north_east_tile, lastToxicNeighbourIndex - firstToxicNeighbourIndex == 1 ? firstToxicNeighbourIndex : lastToxicNeighbourIndex);
        //            }
        //        }
        //    case 3:
        //        if (IsPolluted(adjacentTiles[0]) && IsPolluted(adjacentTiles[1]) && IsPolluted(adjacentTiles[2]) ||
        //            IsPolluted(adjacentTiles[1]) && IsPolluted(adjacentTiles[2]) && IsPolluted(adjacentTiles[3]))
        //        {
        //            // N-E-S or E-S-W
        //            return GetRandomisedTile(toxic_join_north_east_south_tile, firstToxicNeighbourIndex);
        //        }
        //        else if (IsPolluted(adjacentTiles[2]))
        //        {
        //            // S-W-N
        //            return GetRandomisedTile(toxic_join_north_east_south_tile, 2);
        //        }
        //        else
        //        {
        //            // W-N-E
        //            return GetRandomisedTile(toxic_join_north_east_south_tile, 3);
        //        }
        //}

        return null;

        //bool IsPolluted(Coordinate c)
        //{
        //    return c?.IsPolluted ?? false;
        //}
    }

    //private GameObject GetRandomisedTile(TileSet configuration, int alignToDirection)
    //{
    //    var tile = configuration.tiles.GetRandomTile(configuration.allowableFlips, configuration.allowableRotations);
    //    //tile.transform.Rotate(0.0f, 0.0f, configuration.allowable_rotations[Random.Range(0, configuration.allowable_rotations.Length)]);
    //    tile.transform.Rotate(0.0f, 0.0f, 180.0f + 90.0f * alignToDirection);
    //
    //    switch (alignToDirection)
    //    {
    //        case 0:
    //            tile.AddComponent<North>();
    //            break;
    //
    //        case 1:
    //            tile.AddComponent<East>();
    //            break;
    //
    //        case 2:
    //            tile.AddComponent<South>();
    //            break;
    //
    //        case 3:
    //            tile.AddComponent<West>();
    //            break;
    //    }
    //    return tile;
    //}
}
