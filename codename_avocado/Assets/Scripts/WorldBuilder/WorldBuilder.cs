using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldBuilder : MonoSingleton<WorldBuilder>
{
	private class BuilderShape : Shape
	{
		private Color col;

		public BuilderShape(Color col)
        {
			this.col = col;
        }

        public override Color GetColor()
        {
            return this.col;
        }
    }

	public void BuildLevel(WorldGrid grid, int level_num)
    {
        var level = LevelReader.GetOrCreateInstance().GetLevelData(level_num);

		CreateRepresentation(new List<LevelReader.LevelData.Coordinates> { level.Start }, Color.green);
		CreateRepresentation(new List<LevelReader.LevelData.Coordinates> { level.End }, Color.yellow);
		CreateRepresentation(level.Solid, Color.white);
		CreateRepresentation(level.Block, Color.black);
		CreateRepresentation(level.Magma, Color.red);

		void CreateRepresentation(List<LevelReader.LevelData.Coordinates> coords, Color col)
		{
			coords.ForEach((LevelReader.LevelData.Coordinates coordinate) =>
			{
				var shape = new GridPiece(grid, new BuilderShape(col));
				shape.Place(new Vector2(coordinate.x, coordinate.y), Direction.North);
			});
		}
	}

}
