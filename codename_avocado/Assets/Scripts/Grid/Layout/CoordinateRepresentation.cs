using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoordinateRepresentation : MonoBehaviour
{
	public MeshRenderer m_Mesh;
	public Coordinate m_Coordinate;

	public Vector3 m_DefaultPosition;

	private GridTileBuilder.TileType m_previous_type;
	private GridTileBuilder.ToxicLevel m_previous_toxicity;
	private GameObject m_mesh_object = null;

	// Toxic decoration
	private GameObject m_vfx_object = null;

	public void Configure(Coordinate coordinate, GridTileBuilder builder)
	{
		bool is_changed()
		{
			return
				m_previous_type != coordinate.Type ||
				m_previous_toxicity != coordinate.ToxicLevel;
		};

		m_Coordinate = coordinate;
		transform.position = new Vector3(m_Coordinate.GridPosition().x, -.5f, m_Coordinate.GridPosition().y);
		m_DefaultPosition = transform.position;
		m_Coordinate.Decorate(this);

		if (m_mesh_object != null && is_changed())
        {
			Destroy(m_mesh_object);
			m_mesh_object = null;

			if(m_vfx_object != null)
            {
				Destroy(m_vfx_object);
				m_vfx_object = null;
			}
		}

		if (is_changed())
		{
			m_mesh_object = builder.GetTile(coordinate);
			m_mesh_object.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

			// position/rotation fix
			m_mesh_object.transform.parent = this.transform;
			m_mesh_object.transform.localPosition = Vector3.zero;

			m_mesh_object.transform.Rotate(new Vector3(0f, 0f, 90f * Random.Range(0, 3)));

			// add VFX 
			if(coordinate.Type == GridTileBuilder.TileType.toxic)
            {
				m_vfx_object = builder.InstantiateToxicVFX();
				m_vfx_object.transform.parent = this.transform;
				m_vfx_object.transform.localPosition = Vector3.zero;
			}

			m_previous_type = coordinate.Type;
			m_previous_toxicity = coordinate.ToxicLevel;

			//Debug.Log(m_mesh_object.transform.position);
			//Debug.Log("Spawned Coordinate: " + m_Coordinate.GridPosition().x.ToString() + "," +  m_Coordinate.GridPosition().y.ToString());
		}
	}

	public void SetColor(Color color)
	{
		m_Mesh.material.color = color;
		// change alpha or something....
	}

	public void Offset(float amount)
	{
		Vector3 offsetPosition = new Vector3(m_DefaultPosition.x, m_DefaultPosition.y + amount, m_DefaultPosition.z);
		transform.position = offsetPosition;
	}
}
