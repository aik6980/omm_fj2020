﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public enum TileType
    {
        grass,
        toxic,
        start,
        exit,
        obstacle,
        floor
    }

    public enum ToxicLevel
    {
        none = -1,
        healable_pool = 0,
        pool,
        big_spill,
        small_spill
    }

    public GameObject GetTile(Coordinate coord)
    {
        GameObject GetRandomTile(GameObject[] gameObjects)
        {
            if(gameObjects.Length == 0)
            {
                Debug.Assert(false);
                return new GameObject();
            }

            return Instantiate(gameObjects[Random.Range(0, gameObjects.Length)]);
        }

        switch(coord.Type)
        {
            case TileType.toxic:
                switch (coord.ToxicLevel)
                {
                    case ToxicLevel.healable_pool:  return GetRandomTile(toxic_src_empty_tile);
                    case ToxicLevel.pool:           return GetRandomTile(toxic_src_full_tile);
                    case ToxicLevel.big_spill:      return GetRandomTile(toxic_big_spill_tile);
                    case ToxicLevel.small_spill:    return GetRandomTile(toxic_small_spill_tile);

                    case ToxicLevel.none:
                    default:                        return null;
                }

            case TileType.grass:    return GetRandomTile(grass_tile);
            case TileType.start:    return GetRandomTile(start_tile);
            case TileType.exit:     return GetRandomTile(exit_tile);
            case TileType.obstacle: return GetRandomTile(obstacle_tile);
            case TileType.floor: 
            default:                return GetRandomTile(floor_tile);
        }
    }

    public CoordinateRepresentation InstantiateTile(Coordinate coord)
    {
        var instance = GameObject.Instantiate(m_coordRepresentativePrefab);
        var representation = instance.GetComponent<CoordinateRepresentation>();
        representation.Configure(coord, this);
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
}
