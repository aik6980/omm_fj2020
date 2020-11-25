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
		//world.start_piece = GridPiece.GeneratePiece(grid, GridTileBuilder.TileType.start, new Square());
		//world.start_piece.Place(Vector2.zero, Direction.North);
		//
		//world.end_piece = GridPiece.GeneratePiece(grid, GridTileBuilder.TileType.exit, new Square());
		//world.end_piece.Place(new Vector2(0, distance), Direction.North);

		//// Volcanoes:
		//world.volcano_pieces = new List<PollutionPiece>();
		//for (int i = 0; i < volcano_count; ++i)
		//{
		//	world.volcano_pieces.Add(new PollutionPiece(grid, new Volcano(), RandomPollutant(), GridTileBuilder.TileType.toxic));
		//}
		//
		//// Blocks:
		//world.block_pieces = new List<PollutionPiece>();
		//for (int i = 0; i < obstacle_count; ++i)
		//{
		//	world.block_pieces.Add(new BlockingPiece(grid, new Shape(), RandomPollutant()));
		//}

		return world;
	}

	public WorldGrid.WorldData BuildLevel(WorldGrid grid, int level_num)
    {
        var level = LevelReader.GetOrCreateInstance().GetLevelData(level_num);
		WorldGrid.WorldData world = new WorldGrid.WorldData();

		grid.m_Polluter.m_PollutionExpansionTime = level.Config.ToxicSpreadTime;
		grid.m_Polluter.m_PollutionExpansionTimeVariation = level.Config.ToxicSpreadTimeVariation;

		grid.InitialiseGrid(level.Dimension, level.Config.EnvironmentName, new Vector3(level.Config.EnvironmentOffsetX, level.Config.EnvironmentOffsetY, level.Config.EnvironmentOffsetZ));

		world.floor_pieces = new List<GridPiece>();
		for (int y = 0; y < level.Dimension.y; ++y)
        {
			for (int x = 0; x < level.Dimension.x; ++x)
            {
				var piece = GridPiece.GeneratePiece(grid, new Vector2Int(x, y), GridTileBuilder.TileType.floor, new BuilderShape(Color.white));
				piece.Place(new Vector2(x, y), Direction.North);
				world.floor_pieces.Add(piece);
            }
        }

		//world.start_piece = new GridPiece(grid, new BuilderShape(Color.green), level.Start.ToVector2Int(), GridTileBuilder.TileType.start);
		//world.end_piece = new GridPiece(grid, new BuilderShape(Color.cyan), level.End.ToVector2Int(), GridTileBuilder.TileType.exit);
		//world.island_pieces = new List<GridPiece>(level.Solid.Select(coord => new GridPiece(grid, new BuilderShape(Color.white), coord.ToVector2Int(), GridTileBuilder.TileType.grass)));
		//world.volcano_pieces = new List<PollutionPiece>(level.Magma.Select(coord => new PollutionPiece(grid, new Volcano(), coord.ToVector2Int(), GridTileBuilder.TileType.toxic)));
		//world.block_pieces = new List<PollutionPiece>(level.Block.Select(coord => new BlockingPiece(grid, new Shape(), coord.ToVector2Int())));
		//
		//world.start_piece.Place(level.Start.ToVector2Int(), Direction.North);
		//world.end_piece.Place(level.End.ToVector2Int(), Direction.North);
		//
		//return world;
		world.start_piece = PlaceNewPiece(new BuilderShape(Color.green), level.Start, Direction.North, GridTileBuilder.TileType.start);
		world.end_piece = PlaceNewPiece(new BuilderShape(Color.cyan), level.End, Direction.North, GridTileBuilder.TileType.exit);
		world.island_pieces = new List<GridPiece>(level.Solid.Select(coord => PlaceNewPiece(new BuilderShape(Color.white), coord, Direction.North, GridTileBuilder.TileType.grass)));
		world.volcano_pieces = new List<PollutionPiece>(level.Magma.Select(coord => PlacePollution(new ToxicPiece(grid, new Volcano(), coord.ToVector2Int(), level.Config.MaxSpreadDistance), coord)));
		world.block_pieces = new List<PollutionPiece>(level.Block.Select(coord => PlacePollution(new BlockingPiece(grid, new Shape(), coord.ToVector2Int()), coord)));

		return world;

		GridPiece PlaceNewPiece(Shape shape, LevelReader.LevelData.Coordinates coord, Direction dir, GridTileBuilder.TileType type)
		{
			var piece = GridPiece.GeneratePiece(grid, coord.ToVector2Int(), type, shape);
			piece.Place(coord.ToVector2Int(), dir);
			return piece;
		}

		PollutionPiece PlacePollution(PollutionPiece piece, LevelReader.LevelData.Coordinates coord)
        {
			piece.Place(coord.ToVector2Int(), Direction.North);
			return piece;
        }
	}

}
