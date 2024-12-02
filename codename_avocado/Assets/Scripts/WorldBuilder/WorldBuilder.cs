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

	//public /*WorldGrid.WorldData*/LevelReader.LevelData BuildDefaultTest(WorldGrid grid, int distance, int volcano_count, int obstacle_range, int obstacle_count)
 //   {
	//	Vector2 RandomPollutant()
	//	{
	//		int randomX = Random.Range(0, obstacle_range);
	//		int randomY = Random.Range(4, distance - 4);
	//		var position = new Vector2(randomX, randomY);
	//		return position;
	//	}

	//	//WorldGrid.WorldData world = new WorldGrid.WorldData();
	//	////world.start_piece = GridPiece.GeneratePiece(grid, GridTileBuilder.TileType.start, new Square());
	//	////world.start_piece.Place(Vector2.zero, Direction.North);
	//	////
	//	////world.end_piece = GridPiece.GeneratePiece(grid, GridTileBuilder.TileType.exit, new Square());
	//	////world.end_piece.Place(new Vector2(0, distance), Direction.North);

	//	////// Volcanoes:
	//	////world.volcano_pieces = new List<PollutionPiece>();
	//	////for (int i = 0; i < volcano_count; ++i)
	//	////{
	//	////	world.volcano_pieces.Add(new PollutionPiece(grid, new Volcano(), RandomPollutant(), GridTileBuilder.TileType.toxic));
	//	////}
	//	////
	//	////// Blocks:
	//	////world.block_pieces = new List<PollutionPiece>();
	//	////for (int i = 0; i < obstacle_count; ++i)
	//	////{
	//	////	world.block_pieces.Add(new BlockingPiece(grid, new Shape(), RandomPollutant()));
	//	////}
	//	LevelReader.LevelData world = new LevelReader.LevelData();

	//	return world;
	//}

	public /*WorldGrid.WorldData*/LevelReader.LevelData BuildLevel(WorldGrid grid, int level_num)
    {
		//var level = LevelReader.GetOrCreateInstance().GetLevelData(level_num);
		//if (level == null)
		//	return null;
		//
		//WorldGrid.WorldData world = new WorldGrid.WorldData();
		//
		//grid.m_Polluter.m_PollutionExpansionTime = level.Config.ToxicSpreadTime;
		//grid.m_Polluter.m_PollutionExpansionTimeVariation = level.Config.ToxicSpreadTimeVariation;
		//grid.m_Levelname.text = level.Config.Name;
		//
		//grid.InitialiseGrid(level.levelNumber, level.Dimension, level.Config.EnvironmentName, new Vector3(level.Config.EnvironmentOffsetX, level.Config.EnvironmentOffsetY, level.Config.EnvironmentOffsetZ), level.Config.SkyName);
		//
		//world.floor_pieces = new List<Vector2Int>();
		//for (int y = 0; y < level.Dimension.y; ++y)
		//{
		//	for (int x = 0; x < level.Dimension.x; ++x)
		//    {
		//		//var piece = GridPiece.GeneratePiece(grid, new Vector2Int(x, y), GridTileBuilder.TileType.floor, new BuilderShape(Color.white));
		//		//piece.Place(new Vector2(x, y), Direction.North);
		//		//world.floor_pieces.Add(piece);
		//		world.floor_pieces.Add(new Vector2Int(x, y));
		//    }
		//}
		//
		////world.start_piece = PlaceNewPiece(new BuilderShape(Color.green), level.Start, Direction.North, GridTileBuilder.TileType.start);
		////world.end_piece = PlaceNewPiece(new BuilderShape(Color.cyan), level.End, Direction.North, GridTileBuilder.TileType.exit);
		////world.island_pieces = new List<GridPiece>(level.Solid.Select(coord => PlaceNewPiece(new BuilderShape(Color.white), coord, Direction.North, GridTileBuilder.TileType.grass)));
		////world.toxic_pool_pieces = new List<PollutionPiece>(level.Magma.Select(coord => PlacePollution(new ToxicPiece(grid, new /*Volcano*/Shape(), coord.ToVector2Int(), level.Config.MaxSpreadDistance), coord)));
		////world.obstacle_pieces = new List<PollutionPiece>(level.Block.Select(coord => PlacePollution(new BlockingPiece(grid, new Shape(), coord.ToVector2Int()), coord)));
		//world.start_piece = level.Start.ToVector2Int();
		//world.end_piece = level.End.ToVector2Int();
		//world.island_pieces = level.Solid.Select(c => c.ToVector2Int()).ToList();
		//world.toxic_pool_pieces = level.Magma.Select(c => c.ToVector2Int()).ToList();
		//world.obstacle_pieces = level.Block.Select(c => c.ToVector2Int()).ToList();
		//
		//return world;
		//
		////GridPiece PlaceNewPiece(Shape shape, LevelReader.LevelData.Coordinates coord, Direction dir, GridTileBuilder.TileType type)
		////{
		////	var piece = GridPiece.GeneratePiece(grid, coord.ToVector2Int(), type, shape);
		////	piece.Place(coord.ToVector2Int(), dir);
		////	return piece;
		////}
		////
		////PollutionPiece PlacePollution(PollutionPiece piece, LevelReader.LevelData.Coordinates coord)
		////{
		////	piece.Place(coord.ToVector2Int(), Direction.North);
		////	return piece;
		////}
		return LevelReader.GetOrCreateInstance().GetLevelData(level_num);
	}

}
