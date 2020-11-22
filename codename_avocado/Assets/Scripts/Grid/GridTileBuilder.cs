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
    private GameObject GetTile(TileType tileType)
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

        switch(tileType)
        {
            case TileType.grass: return GetRandomTile(grass_tile);
            case TileType.toxic: return GetRandomTile(toxic_tile);
            case TileType.start: return GetRandomTile(start_tile);
            case TileType.exit: return GetRandomTile(exit_tile);
            case TileType.obstacle: return GetRandomTile(obstacle_tile);
            case TileType.floor: 
            default: return GetRandomTile(floor_tile);
        }
    }

    public GameObject InstantiateTile(TileType tileType)
    {
        var instance = GameObject.Instantiate(m_coordRepresentativePrefab);
         
        var new_tile = GetTile(tileType);
        new_tile.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        new_tile.transform.parent = instance.transform;

        return instance;
    }
}
