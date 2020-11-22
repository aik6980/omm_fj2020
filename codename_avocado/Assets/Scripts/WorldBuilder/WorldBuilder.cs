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

	public WorldGrid.WorldData BuildDefaultTest(WorldGrid grid, int distance, int volcano_count, int obstacle_range, int obstacle_count)
    {
		Vector2 RandomPollutant()
		{
			int randomX = Random.Range(0, obstacle_range);
			int randomY = Random.Range(4, distance - 4);
			var position = new Vector2(randomX, randomY);
			return position;
		}

		WorldGrid.WorldData world = new WorldGrid.WorldData();
		world.start_piece = GridPiece.GeneratePiece(grid, new Square());
		world.start_piece.Place(Vector2.zero, Direction.North);

		world.end_piece = GridPiece.GeneratePiece(grid, new Square());
		world.end_piece.Place(new Vector2(0, distance), Direction.North);

		// Volcanoes:
		world.volcano_pieces = new List<PollutionPiece>();
		for (int i = 0; i < volcano_count; ++i)
		{
			world.volcano_pieces.Add(new PollutionPiece(grid, new Volcano(), RandomPollutant()));
		}

		// Blocks:
		world.block_pieces = new List<PollutionPiece>();
		for (int i = 0; i < obstacle_count; ++i)
		{
			world.block_pieces.Add(new BlockingPiece(grid, new Shape(), RandomPollutant()));
		}

		return world;
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
