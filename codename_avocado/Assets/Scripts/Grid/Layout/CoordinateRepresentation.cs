using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoordinateRepresentation : MonoBehaviour
{
	public Coordinate m_Coordinate;

	private GridTileBuilder.TileType m_previous_type;
	private GridTileBuilder.ToxicLevel m_previous_toxicity = GridTileBuilder.ToxicLevel.none;

	private GameObject m_base_tile_object = null;
	private GameObject[] m_adorning_objects = null;

	// Toxic decoration
	private GameObject m_vfx_object = null;

	public void Configure(WorldGrid worldGrid, Coordinate coordinate, GridTileBuilder builder)
	{
		bool type_changed() => m_previous_type != coordinate.Type;
		bool toxicity_changed() => m_previous_toxicity != worldGrid.GetToxicLevel(coordinate);

		m_Coordinate = coordinate;
		transform.position = new Vector3(m_Coordinate.GridPosition().x, -.5f, m_Coordinate.GridPosition().y);
		m_Coordinate.Decorate(this);

		// Destroy the old...
		if (type_changed() || toxicity_changed())
        {
			if (m_base_tile_object != null)
			{
				Destroy(m_base_tile_object);
				m_base_tile_object = null;
			}

			if (m_vfx_object != null)
			{
				Destroy(m_vfx_object);
				m_vfx_object = null;
			}
		}

		if (m_adorning_objects != null && type_changed())
		{
			for (int i = 0; i < m_adorning_objects.Length; ++i)
			{
				Destroy(m_adorning_objects[i]);
			}
			m_adorning_objects = null;
		}

		// Create the new...
		if (m_base_tile_object == null || type_changed() || toxicity_changed())
		{
			m_base_tile_object = builder.GetTile(worldGrid, coordinate);
			m_base_tile_object.GetComponentInChildren<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

			// position/rotation fix
			m_base_tile_object.transform.parent = this.transform;
			m_base_tile_object.transform.localPosition = Vector3.zero;

			m_base_tile_object.transform.Rotate(new Vector3(0f, 0f, 90f * Random.Range(0, 3)));

			// add VFX 
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

		if (m_adorning_objects == null || type_changed())
		{
			m_adorning_objects = builder.GetTileAdornments(coordinate);
			m_adorning_objects.ForEach(adornment =>
			{
				adornment.transform.parent = this.transform;
				adornment.transform.localPosition = Vector3.zero;
			});
		}

		m_previous_type = coordinate.Type;
		m_previous_toxicity = worldGrid.GetToxicLevel(coordinate);
	}
}
