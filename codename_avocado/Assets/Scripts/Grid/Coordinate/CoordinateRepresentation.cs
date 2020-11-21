using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoordinateRepresentation : MonoBehaviour
{
	public MeshRenderer m_Mesh;
	public Coordinate m_Coordinate;

	public void Configure(Coordinate coordinate, bool previewOnly)
	{
		m_Coordinate = coordinate;
		transform.position = new Vector3(m_Coordinate.GridPosition().x, -.5f, m_Coordinate.GridPosition().y);
		m_Coordinate.Decorate(this);
		Debug.Log("Spawned Coordinate: " + m_Coordinate.GridPosition().x.ToString() + "," +  m_Coordinate.GridPosition().y.ToString());
	}
}
