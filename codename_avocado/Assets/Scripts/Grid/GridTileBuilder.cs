using System.Collections;
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
                    case ToxicLevel.small_spill: return GetRandomFromSet(toxic_small_spill_tile);
                    default: Debug.Assert(false); return null;
                }

            case TileType.toxic_pool:
                switch (worldGrid.GetToxicLevel(coord))
                {
                    case ToxicLevel.pool:
                        return GetRandomFromSet(toxic_big_spill_tile);
                        //{
                        //    var tile = GetRandomFromSet(toxic_big_spill_tile);
                        //    var adorner = GetRandomFromSet(toxic_adornments);
                        //
                        //    var holder = new GameObject("tile_holder");
                        //    holder.transform.Rotate(Vector3.right * 90.0f);
                        //    tile.transform.parent = holder.transform;
                        //    adorner.transform.parent = holder.transform;
                        //    return holder;
                        //    //return GetRandomTile(toxic_src_full_tile);
                        //}
                    case ToxicLevel.healable_pool:
                        return GetRandomFromSet(toxic_small_spill_tile);
                        //{
                        //    var tile = GetRandomFromSet(toxic_small_spill_tile);
                        //    var adorner = GetRandomFromSet(toxic_adornments);
                        //
                        //    var holder = new GameObject("tile_holder");
                        //    holder.transform.Rotate(Vector3.right * 90.0f);
                        //    tile.transform.parent = holder.transform;
                        //    adorner.transform.parent = holder.transform;
                        //    return holder;
                        //    //return GetRandomTile(toxic_src_empty_tile);
                        //}
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
}
