using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TileConfiguration
{
    public Tile[] tileset;
    public float[] allowable_rotations = { 0.0f, 90.0f, 180.0f, -90.0f };
    public bool can_be_flipped_ns = true;
    public bool can_be_flipped_ew = true;
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
        none = -1,
        healable_pool = 0,
        pool,
        big_spill,
        small_spill
    }

    public GameObject GetTile(WorldGrid worldGrid, Coordinate coord)
    {
        switch(coord.Type)
        {
            case TileType.toxic:
                switch (worldGrid.GetToxicLevel(coord))
                {
                    case ToxicLevel.big_spill: return GetRandomFromSet(toxic_big_spill_tile);
                    case ToxicLevel.small_spill: return ConfigureTile(worldGrid, coord);//GetRandomFromSet(toxic_small_spill_tile);
                    default: Debug.Assert(false); return null;
                }

            case TileType.toxic_pool:
                switch (worldGrid.GetToxicLevel(coord))
                {
                    case ToxicLevel.pool: return GetRandomFromSet(toxic_big_spill_tile);
                    case ToxicLevel.healable_pool: return ConfigureTile(worldGrid, coord);//GetRandomFromSet(toxic_small_spill_tile);
                    default: Debug.Assert(false); return null;
                }

            case TileType.grass:    return GetRandomFromSet(grass_tile);
            case TileType.start:    return GetRandomFromSet(start_tile);
            case TileType.exit:     return GetRandomFromSet(exit_tile);
            case TileType.obstacle: return GetRandomFromSet(obstacle_tile);
            case TileType.floor: 
            default:                return GetRandomFromSet(floor_tile);
        }
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
        //Direction[] directions = { Direction.North, Direction.East, Direction.South, Direction.West };
        Coordinate[] adjacentTiles = {
            worldGrid.GetAdjacentCoordinate(coord.m_Position, Direction.North),
            worldGrid.GetAdjacentCoordinate(coord.m_Position, Direction.East),
            worldGrid.GetAdjacentCoordinate(coord.m_Position, Direction.South),
            worldGrid.GetAdjacentCoordinate(coord.m_Position, Direction.West)
        };

        int numToxicNeighbours = 0;
        int firstToxicNeighbourIndex = 5;
        int lastToxicNeighbourIndex = -1;
        for (int i = 0; i < 4; ++i)
        {
            if (IsPolluted(adjacentTiles[i]))
            {
                ++numToxicNeighbours;
                firstToxicNeighbourIndex = System.Math.Min(firstToxicNeighbourIndex, i);
                lastToxicNeighbourIndex = System.Math.Max(lastToxicNeighbourIndex, i);
            }
        }

        switch (numToxicNeighbours)
        {
            case 0: return GetRandomisedTile(toxic_no_join_tile, 0);
            case 1: return GetRandomisedTile(toxic_join_north_tile, firstToxicNeighbourIndex);
            case 2:
                {
                    // Opposite sides or adjacent sides?
                    if (IsPolluted(adjacentTiles[0]) && IsPolluted(adjacentTiles[2]) ||
                        IsPolluted(adjacentTiles[1]) && IsPolluted(adjacentTiles[3]))
                    {
                        return GetRandomisedTile(toxic_join_north_south_tile, firstToxicNeighbourIndex);
                    }
                    else
                    {
                        return GetRandomisedTile(toxic_join_north_east_tile, lastToxicNeighbourIndex - firstToxicNeighbourIndex == 1 ? firstToxicNeighbourIndex : lastToxicNeighbourIndex);
                    }
                }
            case 3:
                if (IsPolluted(adjacentTiles[0]) && IsPolluted(adjacentTiles[1]) && IsPolluted(adjacentTiles[2]) ||
                    IsPolluted(adjacentTiles[1]) && IsPolluted(adjacentTiles[2]) && IsPolluted(adjacentTiles[3]))
                {
                    // N-E-S or E-S-W
                    return GetRandomisedTile(toxic_join_north_east_south_tile, firstToxicNeighbourIndex);
                }
                else if (IsPolluted(adjacentTiles[2]))
                {
                    // S-W-N
                    return GetRandomisedTile(toxic_join_north_east_south_tile, 2);
                }
                else
                {
                    // W-N-E
                    return GetRandomisedTile(toxic_join_north_east_south_tile, 3);
                }
        }

        return null;

        bool IsPolluted(Coordinate c)
        {
            return c?.IsPolluted ?? false;
        }
    }

    private GameObject GetRandomisedTile(TileConfiguration configuration, int alignToDirection)
    {
        var tile = GetRandomFromTileSet(configuration.tileset);
        //tile.transform.Rotate(0.0f, 0.0f, configuration.allowable_rotations[Random.Range(0, configuration.allowable_rotations.Length)]);
        tile.transform.Rotate(0.0f, 0.0f, 180.0f + 90.0f * alignToDirection);

        switch (alignToDirection)
        {
            case 0:
                tile.AddComponent<North>();
                break;

            case 1:
                tile.AddComponent<East>();
                break;

            case 2:
                tile.AddComponent<South>();
                break;

            case 3:
                tile.AddComponent<West>();
                break;
        }
        return tile;
    }

    private GameObject GetRandomFromTileSet(Tile[] tileset)
    {
        float rangeSum = tileset.Sum(t => t.weight);
        float value = Random.Range(0.0f, rangeSum);
        var selectedTile = tileset.SkipWhile(t => (value -= t.weight) > 0.0f).First();
        return Instantiate(selectedTile.tile);
    }
}
