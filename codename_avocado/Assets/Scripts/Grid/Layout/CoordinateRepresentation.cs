using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoordinateRepresentation : MonoBehaviour
{
    private class CoordinatePreviousState
    {
        public GridTileBuilder.TileType type;
        public int num_toxic_neighbours;
        public GridTileBuilder.ToxicLevel toxic_level
        {
            get => num_toxic_neighbours switch
            {
                >= 4 => GridTileBuilder.ToxicLevel.deep,
                >= 1 => GridTileBuilder.ToxicLevel.shallow,
                _ => GridTileBuilder.ToxicLevel.none
            };
        }

        public CoordinatePreviousState(GridTileBuilder.TileType type, int num_toxic_neighbours)
        {
            this.type = type;
            this.num_toxic_neighbours = num_toxic_neighbours;
        }
    }


    public Coordinate coordinate;

    private WorldGrid worldGrid;
    private GridTileBuilder builder;
    private Dictionary<Vector2Int, CoordinatePreviousState> previousStates = new();

    private GameObject m_base_tile_object = null;
    private GameObject[] m_adorning_objects = null;

    // Toxic decoration
    private GameObject m_vfx_object = null;

    private void OnDestroy()
    {
        DestroyPart(ref m_base_tile_object);
        DestroyPart(ref m_vfx_object);
        DestroyParts(ref m_adorning_objects);
    }

    public void Configure(WorldGrid worldGrid, Coordinate coordinate, GridTileBuilder builder)
    {
        this.worldGrid = worldGrid;
        this.builder = builder;
        this.coordinate = coordinate;

        previousStates.Add(coordinate.m_Position, new CoordinatePreviousState(coordinate.Type, num_toxic_neighbours(worldGrid, coordinate)));
        foreach (var neighbour in worldGrid.GetOrthogonalNeighbours(coordinate).Concat(worldGrid.GetDiagonalNeighbours(coordinate)).Where(c => c != null))
        {
            previousStates.Add(neighbour.m_Position, new CoordinatePreviousState(neighbour.Type, num_toxic_neighbours(worldGrid, neighbour)));
        }

        // Create the tile and such...
        name = $"Tile({coordinate.m_Position.x}, {coordinate.m_Position.y})";
        this.transform.position = new Vector3(coordinate.GridPosition().x, -.5f, coordinate.GridPosition().y);
        CreateTileRepresentation(coordinate);
        CreateTileAdornments(coordinate);

        #region old code
        //bool type_changed() => m_previous_type != coordinate.Type;
        //bool toxicity_changed() => m_num_toxic_neighbours != worldGrid.GetOrthogonalNeighbours(coordinate).Count(c => c?.IsPolluted ?? false) /*+ worldGrid.GetDiagonalNeighbours(coordinate).Count(c => c.IsPolluted)*/;
        //
        //m_Coordinate = coordinate;
        //transform.position = new Vector3(m_Coordinate.GridPosition().x, -.5f, m_Coordinate.GridPosition().y);
        //m_Coordinate.Decorate(this);
        //
        //// Destroy the old...
        //if (type_changed() || toxicity_changed())
        //{
        //	if (m_base_tile_object != null)
        //	{
        //		Destroy(m_base_tile_object);
        //		m_base_tile_object = null;
        //	}
        //
        //	if (m_vfx_object != null)
        //	{
        //		Destroy(m_vfx_object);
        //		m_vfx_object = null;
        //	}
        //}
        //
        //if (m_adorning_objects != null && type_changed())
        //{
        //	for (int i = 0; i < m_adorning_objects.Length; ++i)
        //	{
        //		Destroy(m_adorning_objects[i]);
        //	}
        //	m_adorning_objects = null;
        //}
        //
        //// Create the new...
        //if (m_base_tile_object == null || type_changed() || toxicity_changed())
        //{
        //	m_base_tile_object = builder.GetTile(worldGrid, coordinate);
        //	m_base_tile_object.GetComponentInChildren<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        //
        //	// position/rotation fix
        //	m_base_tile_object.transform.parent = this.transform;
        //	m_base_tile_object.transform.localPosition = Vector3.zero;
        //
        //	//m_base_tile_object.transform.Rotate(new Vector3(0f, 0f, 90f /** Random.Range(0, 3)*/));
        //
        //	// add VFX 
        //	if (coordinate.Type == GridTileBuilder.TileType.toxic ||
        //		coordinate.Type == GridTileBuilder.TileType.toxic_pool)
        //    {
        //		m_vfx_object = builder.InstantiateToxicVFX();
        //		m_vfx_object.transform.parent = this.transform;
        //		m_vfx_object.transform.localPosition = Vector3.zero;
        //	}
        //
        //	if (coordinate.Type == GridTileBuilder.TileType.grass)
        //	{
        //		var vfx = builder.InstantiateGrassVFX();
        //		vfx.transform.parent = this.transform;
        //		vfx.transform.localPosition = Vector3.zero;
        //	}
        //}
        //
        //if (m_adorning_objects == null || type_changed())
        //{
        //	m_adorning_objects = builder.GetTileAdornments(coordinate);
        //	m_adorning_objects.ForEach(adornment =>
        //	{
        //		adornment.transform.parent = this.transform;
        //		adornment.transform.localPosition = Vector3.zero;
        //	});
        //}
        //
        //m_previous_type = coordinate.Type;
        ////m_previous_toxicity = worldGrid.GetToxicLevel(coordinate);
        //m_num_toxic_neighbours = worldGrid.GetOrthogonalNeighbours(coordinate).Count(c => c?.IsPolluted ?? false);
        #endregion
    }

    [Button("Force Update Representation")]
    private void ForceUpdateRepresentation()
    {
        DestroyPart(ref m_base_tile_object);
        DestroyPart(ref m_vfx_object);
        DestroyParts(ref m_adorning_objects);

        CreateTileRepresentation(coordinate);
        CreateTileAdornments(coordinate);
        UpdateNeighbourStateCache();
    }

    public bool UpdateRepresentation()
    {
        if (coordinate.Type != previousStates[coordinate.m_Position].type)
        {
            ForceUpdateRepresentation();
            return true;
        }
        else if (coordinate.Type.IsPolluted())
        {
            foreach (var neighbour in worldGrid.GetOrthogonalNeighbours(coordinate).Concat(worldGrid.GetDiagonalNeighbours(coordinate)).Where(c => c != null))
            {
                if (HasToxicStateChanged(neighbour) ||
                    (neighbour.IsPolluted() && worldGrid.GetToxicLevel(neighbour) != previousStates[neighbour.m_Position].toxic_level))
                {
                    DestroyPart(ref m_base_tile_object);
                    DestroyPart(ref m_vfx_object);

                    CreateTileRepresentation(coordinate);
                    UpdateNeighbourStateCache();
                    return true;
                }
            }
        }

        return false;
    }

    private void UpdateNeighbourStateCache()
    {
        previousStates[coordinate.m_Position].type = coordinate.Type;
        previousStates[coordinate.m_Position].num_toxic_neighbours = num_toxic_neighbours(worldGrid, coordinate);
        foreach (var neighbour in worldGrid.GetOrthogonalNeighbours(coordinate).Concat(worldGrid.GetDiagonalNeighbours(coordinate)).Where(c => c != null))
        {
            previousStates[neighbour.m_Position].type = neighbour.Type;
            previousStates[neighbour.m_Position].num_toxic_neighbours = num_toxic_neighbours(worldGrid, neighbour);
        }
    }

    private void CreateTileRepresentation(Coordinate coordinate)
    {
        // Base tile...
        m_base_tile_object = builder.GetTile(worldGrid, coordinate);
        m_base_tile_object.transform.parent = this.transform;
        m_base_tile_object.transform.localPosition = Vector3.zero;
        m_base_tile_object.GetComponentInChildren<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        // VFX...
        if (coordinate.Type == GridTileBuilder.TileType.toxic ||
            coordinate.Type == GridTileBuilder.TileType.toxic_pool)
        {
            m_vfx_object = builder.InstantiateToxicVFX();
            m_vfx_object.transform.parent = this.transform;
            m_vfx_object.transform.localPosition = Vector3.zero;
        }

        if (coordinate.Type == GridTileBuilder.TileType.grass)
        {
            var vfx = builder.InstantiateGrassVFX();
            vfx.transform.parent = this.transform;
            vfx.transform.localPosition = Vector3.zero;
        }
    }

    private void CreateTileAdornments(Coordinate coordinate)
    {
        // Adornments...
        m_adorning_objects = builder.GetTileAdornments(coordinate);
        m_adorning_objects.ForEach(adornment =>
        {
            adornment.transform.parent = this.transform;
            adornment.transform.localPosition = Vector3.zero;
        });
    }

    private int num_toxic_neighbours(WorldGrid worldGrid, Coordinate coordinate) => worldGrid.GetOrthogonalNeighbours(coordinate).Count(c => c.IsPolluted());


    private bool HasToxicStateChanged(Coordinate coord) =>
        coord.IsPolluted() && !previousStates[coord.m_Position].type.IsPolluted() ||
        !coord.IsPolluted() && previousStates[coord.m_Position].type.IsPolluted();

    private void DestroyParts(ref GameObject[] objs)
    {
        if (objs != null)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                Destroy(objs[i]);
            }

            objs = null;
        }
    }

    private void DestroyPart(ref GameObject obj)
    {
        if (obj != null)
        {
            Destroy(obj);
            obj = null;
        }
    }
}
