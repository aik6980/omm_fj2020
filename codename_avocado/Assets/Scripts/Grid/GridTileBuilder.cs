using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridTileBuilder : MonoBehaviour
{
    public GameObject m_coordRepresentativePrefab;

    public GameObject[] grass_tile;

    public GameObject[] toxic_tile;

    public GameObject[] start_tile;
    public GameObject[] exit_tile;
    public GameObject[] obstacle_tile;
    public GameObject[] floor_tile;

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
            case TileType.grass: return GetRandomTile(grass_tile);
            case TileType.toxic: return Instantiate(toxic_tile[(int)coord.ToxicLevel]);
            case TileType.start: return GetRandomTile(start_tile);
            case TileType.exit: return GetRandomTile(exit_tile);
            case TileType.obstacle: return GetRandomTile(obstacle_tile);
            case TileType.floor: 
            default: return GetRandomTile(floor_tile);
        }
    }

    public CoordinateRepresentation InstantiateTile(Coordinate coord)
    {
        var instance = GameObject.Instantiate(m_coordRepresentativePrefab);
        var representation = instance.GetComponent<CoordinateRepresentation>();
        representation.Configure(coord, this);
        return representation;
    }
}
