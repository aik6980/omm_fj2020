using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape
{
	public virtual Color GetColor()
	{
		return Color.clear;
	}

	public virtual List<Vector2Int> Coordinates()
	{
		List<Vector2Int> coords = new List<Vector2Int>();
		coords.Add(new Vector2Int(0, 0));
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


	public static Vector2Int Faceto(Vector2Int src, Direction dir)
    {
		switch(dir)
        {
			case Direction.North: return src;
			case Direction.East: return new Vector2Int(src.y, -src.x);
			case Direction.West: return new Vector2Int(-src.y, src.x);
			case Direction.South: return -src;
		}

		return src;
    }

    public virtual List<GridTileBuilder.ToxicLevel> Toxicity()
    {
		return new List<GridTileBuilder.ToxicLevel>() { GridTileBuilder.ToxicLevel.none };
    }
}

public class Volcano : Shape
{
	public override Color GetColor()
	{
		return Color.red;
	}

	public override List<Vector2Int> Coordinates()
	{
		var coords = base.Coordinates();
		coords.Add(new Vector2Int(0, 1));
		coords.Add(new Vector2Int(1, 1));
		coords.Add(new Vector2Int(1, 0));
		coords.Add(new Vector2Int(1, -1));
		coords.Add(new Vector2Int(0, -1));
		coords.Add(new Vector2Int(-1, -1));
		coords.Add(new Vector2Int(-1, 0));
		coords.Add(new Vector2Int(0, -1));
		coords.Add(new Vector2Int(-1, 1));
		return coords;
	}

    public override List<GridTileBuilder.ToxicLevel> Toxicity()
    {
        var toxic = base.Toxicity();
		toxic[0] = GridTileBuilder.ToxicLevel.pool;
		toxic.Add(GridTileBuilder.ToxicLevel.small_spill);
		toxic.Add(GridTileBuilder.ToxicLevel.small_spill);
		toxic.Add(GridTileBuilder.ToxicLevel.small_spill);
		toxic.Add(GridTileBuilder.ToxicLevel.small_spill);
		toxic.Add(GridTileBuilder.ToxicLevel.small_spill);
		toxic.Add(GridTileBuilder.ToxicLevel.small_spill);
		toxic.Add(GridTileBuilder.ToxicLevel.small_spill);
		toxic.Add(GridTileBuilder.ToxicLevel.small_spill);
		toxic.Add(GridTileBuilder.ToxicLevel.small_spill);
		return toxic;
    }
}

public class Square : Shape
{
	public override Color GetColor()
	{
		return Color.green;
	}

	public override List<Vector2Int> Coordinates()
	{
		var coords = base.Coordinates();
		coords.Add(new Vector2Int(0, 1));
		coords.Add(new Vector2Int(1, 1));
		coords.Add(new Vector2Int(1, 0));
		return coords;
	}
}

public class Line : Shape
{
	public override Color GetColor()
	{
		return Color.blue;
	}

	public override List<Vector2Int> Coordinates()
	{
		var coords = base.Coordinates();
		coords.Add(new Vector2Int(0, 1));
		coords.Add(new Vector2Int(0, 2));
		coords.Add(new Vector2Int(0, 3));
		return coords;
	}
}

public class Corner : Shape
{
	public override Color GetColor()
	{
		return Color.magenta;
	}

	public override List<Vector2Int> Coordinates()
	{
		var coords = base.Coordinates();
		coords.Add(new Vector2Int(0, 1));
		coords.Add(new Vector2Int(0, 2));
		coords.Add(new Vector2Int(1, 2));
		coords.Add(new Vector2Int(2, 2));
		return coords;
	}
}

public class UnfoldedShape : Shape
{
    private Unfold unfoldScript;

    public UnfoldedShape(Unfold unfoldScript)
    {
        this.unfoldScript = unfoldScript;
    }

    public override Color GetColor()
    {
        return Color.green;
    }

    public override List<Vector2Int> Coordinates()
    {
        return unfoldScript.GetUnfoldedNet();
    }
}