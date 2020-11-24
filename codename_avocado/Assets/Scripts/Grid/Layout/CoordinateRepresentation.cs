using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoordinateRepresentation : MonoBehaviour
{
	public MeshRenderer m_Mesh;
	public Coordinate m_Coordinate;

	public Vector3 m_DefaultPosition;

	private GameObject m_mesh_object;


	public void Configure(Coordinate coordinate, GridTileBuilder builder)
	{
		m_Coordinate = coordinate;
		transform.position = new Vector3(m_Coordinate.GridPosition().x, -.5f, m_Coordinate.GridPosition().y);
		m_DefaultPosition = transform.position;
		m_Coordinate.Decorate(this);

		if (m_mesh_object)
        {
			Destroy(m_mesh_object);
			m_mesh_object = null;

		}

		m_mesh_object = builder.GetTile(coordinate);
        m_mesh_object.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		
		// position fix
		m_mesh_object.transform.parent = this.transform;
		m_mesh_object.transform.localPosition = Vector3.zero;
		m_mesh_object.transform.Rotate(new Vector3(0f, 0f, 90f));

		//Debug.Log(m_mesh_object.transform.position);
		//Debug.Log("Spawned Coordinate: " + m_Coordinate.GridPosition().x.ToString() + "," +  m_Coordinate.GridPosition().y.ToString());
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
