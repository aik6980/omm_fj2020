using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		world.start_piece = GridPiece.GeneratePiece(grid, GridTileBuilder.TileType.start, new Square());
		world.start_piece.Place(Vector2.zero, Direction.North);

		world.end_piece = GridPiece.GeneratePiece(grid, GridTileBuilder.TileType.exit, new Square());
		world.end_piece.Place(new Vector2(0, distance), Direction.North);

		// Volcanoes:
		world.volcano_pieces = new List<PollutionPiece>();
		for (int i = 0; i < volcano_count; ++i)
		{
			world.volcano_pieces.Add(new PollutionPiece(grid, new Volcano(), RandomPollutant(), GridTileBuilder.TileType.toxic));
		}

		// Blocks:
		world.block_pieces = new List<PollutionPiece>();
		for (int i = 0; i < obstacle_count; ++i)
		{
			world.block_pieces.Add(new BlockingPiece(grid, new Shape(), RandomPollutant()));
		}

		return world;
	}

	public WorldGrid.WorldData BuildLevel(WorldGrid grid, int level_num)
    {
        var level = LevelReader.GetOrCreateInstance().GetLevelData(level_num);
		WorldGrid.WorldData world = new WorldGrid.WorldData();

		world.start_piece = PlaceNewPiece(new BuilderShape(Color.green), level.Start, Direction.North, GridTileBuilder.TileType.start);
		world.end_piece = PlaceNewPiece(new BuilderShape(Color.cyan), level.End, Direction.North, GridTileBuilder.TileType.exit);
		world.island_pieces = new List<GridPiece>(level.Solid.Select(coord => PlaceNewPiece(new BuilderShape(Color.white), coord, Direction.North, GridTileBuilder.TileType.grass)));
		world.volcano_pieces = new List<PollutionPiece>(level.Magma.Select(coord => new PollutionPiece(grid, new Volcano(), coord.ToVector2(), GridTileBuilder.TileType.toxic)));
		world.block_pieces = new List<PollutionPiece>(level.Block.Select(coord => new BlockingPiece(grid, new Shape(), coord.ToVector2())));

		return world;

		GridPiece PlaceNewPiece(Shape shape, LevelReader.LevelData.Coordinates coord, Direction dir, GridTileBuilder.TileType type)
        {
			var piece = GridPiece.GeneratePiece(grid, type, shape);
			piece.Place(coord.ToVector2(), dir);
			return piece;
		}
	}

}
