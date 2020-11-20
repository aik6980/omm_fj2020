using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
	public GameObject m_CoordinatePrefab;
	public List<Coordinate> m_Coordinates = new List<Coordinate>();

	private void Start()
	{
		Populate();
	}

	private void Populate()
	{
		int obstacleCount = 0;
		while (obstacleCount < 10)
		{
			int randomX = Random.Range(-10, 10);
			int randomY = Random.Range(-10, 10);

			// placeholder populate logic...
			Coordinate coordinate = new BrokenCoordinate(this, new Vector2(randomX, randomY));
			AddCoordinate(coordinate);
			obstacleCount++;
		}
	}

	private void AddCoordinate(Coordinate c)
	{
		var newCoordinate = GameObject.Instantiate<GameObject>(m_CoordinatePrefab, transform);
		var rep = newCoordinate.GetComponent<CoordinateRepresentation>();
		rep.Configure(c);
	}

	public void CoordinateRegistration(Coordinate coordinate, bool registered)
	{
		if (registered)
			m_Coordinates.Add(coordinate);
		else
			m_Coordinates.Remove(coordinate);
	}

}
