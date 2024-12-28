using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ITileBuilder
{
    /// <summary>
    /// Creates a new tile representation for the body coordinate. (i.e. just the ground cube bit, not
    /// any of the adornments.)
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="coord"></param>
    /// <returns></returns>
    GameObject CreateTileRepresentation(WorldGrid grid, Coordinate coord);

    /// <summary>
    /// Creates 
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="coord"></param>
    /// <returns></returns>
    GameObject[] CreateTileAdornments(WorldGrid grid, Coordinate coord);

    /// <summary>
    /// Takes an existing tile representation and modifies it, keeping unchanged adornments etc.
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="coord"></param>
    /// <param name="currentTile"></param>
    /// <returns></returns>
    GameObject UpdateTileRepresentation(WorldGrid grid, Coordinate coord, ref GameObject currentTile);
}

public static class TileConfigurationExtensions
{
    public static GameObject GetRandomTile(this TileSet tileSet)
    {
        if (tileSet.tiles.Length == 0)
            return null;

        float rangeSum = tileSet.tiles.Sum(t => t.weight);
        float value = Random.Range(0.0f, rangeSum);
        var selectedTile = tileSet.tiles.SkipWhile(t => (value -= t.weight) > 0.0f).First();
        var tile = GameObject.Instantiate(selectedTile.tile);
        var flipMask = Random.Range(0, (int)TileConfiguration.FlipDirections.All);

        tile.transform.localScale = tile.transform.localScale.Scale(
            (tileSet.allowableFlips.Mask(flipMask).HasFlag(TileConfiguration.FlipDirections.EastWest)) ? -1.0f : 1.0f,
            (tileSet.allowableFlips.Mask(flipMask).HasFlag(TileConfiguration.FlipDirections.NorthSouth)) ? -1.0f : 1.0f,
            1.0f);
        var randomRotation = tileSet.allowableRotations.GetRandomDirection().Heading();
        tile.transform.Rotate(0.0f, 0.0f, 180.0f + randomRotation);
        return tile;
    }

    public static Direction GetRandomDirection(this TileConfiguration.RotationDirections directions)
    {
        if (directions == 0)
        {
            return Direction.North;
        }

        var possibleDirections = DirectionFromMask(directions).ToArray();
        return possibleDirections[Random.Range(0, possibleDirections.Length - 1)];

        IEnumerable<Direction> DirectionFromMask(TileConfiguration.RotationDirections directions)
        {
            var mask = 1;
            for (int i = 0; i < 4; ++i)
            {
                if ((mask & (int)directions) != 0)
                {
                    yield return (Direction)i;
                }

                mask <<= 1;
            }
        }
    }

    //public static GameObject GetRandomTile(this TileConfiguration tileConfiguration, Direction alignToDirection)
    //{
    //    float rangeSum = tileConfiguration.tileset.Sum(t => t.weight);
    //    float value = Random.Range(0.0f, rangeSum);
    //    var selectedTile = tileConfiguration.tileset.SkipWhile(t => (value -= t.weight) > 0.0f).First();
    //
    //    var tile = GameObject.Instantiate(selectedTile.tile);
    //    if (tileConfiguration.can_be_flipped_ns && Random.Range(0, 1) > 0)
    //    {
    //        var flippedScale = tile.transform.localScale;
    //        flippedScale.x *= -1.0f;
    //        tile.transform.localScale = flippedScale;
    //    }
    //
    //    if (tileConfiguration.can_be_flipped_ew && Random.Range(0, 1) > 0)
    //    {
    //        var flippedScale = tile.transform.localScale;
    //        flippedScale.y *= -1.0f;
    //        tile.transform.localScale = flippedScale;
    //    }
    //
    //    tile.transform.Rotate(0.0f, 0.0f, 180.0f + heading[(int)alignToDirection] + tileConfiguration.allowable_rotations[Random.Range(0, tileConfiguration.allowable_rotations.Length)]);
    //
    //    return tile;
    //}
}

public class TileFactory
{
    private readonly TileFactoryConfiguration factoryConfig;
    private readonly GameObject tilePrefab;

    private readonly SimpleTileBuilder startBuilder;
    private readonly SimpleTileBuilder exitBuilder;
    private readonly SimpleTileBuilder floorBuilder;
    private readonly SimpleTileBuilder grassBuilder;
    private readonly SimpleTileBuilder obstacleBuilder;
    private readonly ToxicTileBuilder toxicBuilder;
    private readonly SimpleTileBuilder deepToxicBuilder;
    private readonly SimpleTileBuilder toxicSourceBuilder;

    public TileFactory(TileFactoryConfiguration factoryConfig, GameObject tilePrefab)
    {
        this.factoryConfig = factoryConfig;
        this.tilePrefab = tilePrefab;

        startBuilder = new SimpleTileBuilder(factoryConfig.startTiles);
        exitBuilder = new SimpleTileBuilder(factoryConfig.exitTiles);
        floorBuilder = new SimpleTileBuilder(factoryConfig.floorTiles);
        grassBuilder = new SimpleTileBuilder(factoryConfig.grassTiles);
        obstacleBuilder = new SimpleTileBuilder(factoryConfig.obstacleTiles);
        toxicBuilder = new ToxicTileBuilder(factoryConfig.shallowToxicTiles);
        deepToxicBuilder = new SimpleTileBuilder(factoryConfig.deepToxicTiles);
        toxicSourceBuilder = new SimpleTileBuilder(factoryConfig.toxicSourceAdornments);
    }

    public GameObject CreateTileRepresentation(WorldGrid grid, Coordinate coord)
    {
        //// Create a prefab holder for the tile bits...
        //var instance = GameObject.Instantiate(tilePrefab);
        //instance.name = $"Tile({coord.m_Position.x}, {coord.m_Position.y})";

        // Create an appropriate base tile model (aligned appropriately)...
        var tile = coord.Type switch
        {
            GridTileBuilder.TileType.start => startBuilder.CreateTileRepresentation(grid, coord),
            GridTileBuilder.TileType.exit => exitBuilder.CreateTileRepresentation(grid, coord),
            GridTileBuilder.TileType.floor => floorBuilder.CreateTileRepresentation(grid, coord),
            GridTileBuilder.TileType.obstacle => obstacleBuilder.CreateTileRepresentation(grid, coord),
            GridTileBuilder.TileType.grass => grassBuilder.CreateTileRepresentation(grid, coord),
            GridTileBuilder.TileType.toxic or GridTileBuilder.TileType.toxic_pool when grid.GetToxicLevel(coord) == GridTileBuilder.ToxicLevel.deep => deepToxicBuilder.CreateTileRepresentation(grid, coord),
            GridTileBuilder.TileType.toxic or GridTileBuilder.TileType.toxic_pool when grid.GetToxicLevel(coord) == GridTileBuilder.ToxicLevel.shallow => toxicBuilder.CreateTileRepresentation(grid, coord),
            _ => null
        };

        if (tile is null)
            return null;

        return tile;
        //tile.transform.parent = instance.transform;

        //// Create adornments to the tile as appropriate...
        //var adornments = coord.Type switch
        //{
        //    GridTileBuilder.TileType.start => startBuilder.CreateTileAdornments(grid, coord),
        //    GridTileBuilder.TileType.exit => exitBuilder.CreateTileAdornments(grid, coord),
        //    GridTileBuilder.TileType.floor => floorBuilder.CreateTileAdornments(grid, coord),
        //    GridTileBuilder.TileType.obstacle => obstacleBuilder.CreateTileAdornments(grid, coord),
        //    GridTileBuilder.TileType.grass => grassBuilder.CreateTileAdornments(grid, coord),
        //    GridTileBuilder.TileType.toxic => toxicBuilder.CreateTileAdornments(grid, coord),
        //    GridTileBuilder.TileType.toxic_pool => toxicBuilder.CreateTileAdornments(grid, coord),
        //    _ => null
        //};
        //
        //for (int i = 0; i < adornments.Length; ++i)
        //{
        //    adornments[i].transform.parent = instance.transform;
        //}
        //
        //return instance;
    }
}
