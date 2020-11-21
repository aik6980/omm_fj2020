using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape
{
	public virtual Color GetColor()
	{
		return Color.white;
	}

	public virtual List<Vector2> Coordinates()
	{
		List<Vector2> coords = new List<Vector2>();
		coords.Add(new Vector2(0, 0));
		return coords;
	}

	public static Shape RandomShape()
	{
		int randomShape = Random.Range(0, 2);
		switch (randomShape)
		{
			case 0:
				return new Line();
			case 1:
				return new Corner();
		}

		return null;
	}

}

public class Square : Shape
{
	public override Color GetColor()
	{
		return Color.yellow;
	}

	public override List<Vector2> Coordinates()
	{
		var coords = base.Coordinates();
		coords.Add(new Vector2(0, 1));
		coords.Add(new Vector2(1, 1));
		coords.Add(new Vector2(1, 0));
		return coords;
	}
}

public class Line : Shape
{
	public override Color GetColor()
	{
		return Color.blue;
	}

	public override List<Vector2> Coordinates()
	{
		var coords = base.Coordinates();
		coords.Add(new Vector2(0, 1));
		coords.Add(new Vector2(0, 2));
		coords.Add(new Vector2(0, 3));
		return coords;
	}
}

public class Corner : Shape
{
	public override Color GetColor()
	{
		return Color.green;
	}

	public override List<Vector2> Coordinates()
	{
		var coords = base.Coordinates();
		coords.Add(new Vector2(0, 1));
		coords.Add(new Vector2(0, 2));
		coords.Add(new Vector2(1, 2));
		coords.Add(new Vector2(2, 2));
		return coords;
	}
}