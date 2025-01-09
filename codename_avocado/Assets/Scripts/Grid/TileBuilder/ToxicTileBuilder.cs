using System.Linq;
using UnityEngine;

public class ToxicTileBuilder : ITileBuilder
{
    private ShallowToxicTileSet configuration;

    public ToxicTileBuilder(ShallowToxicTileSet configuration)
    {
        this.configuration = configuration;
    }

    public GameObject CreateTileRepresentation(WorldGrid grid, Coordinate coord)
    {
        var orthogonalNeighbours = grid.GetOrthogonalNeighbours(coord).ToArray();
        int numShallowNeighbours = orthogonalNeighbours.Count(c => c.IsPolluted && grid.GetToxicLevel(c) == GridTileBuilder.ToxicLevel.shallow);
        int numDeepNeighbours = orthogonalNeighbours.Count(c => c.IsPolluted && grid.GetToxicLevel(c) == GridTileBuilder.ToxicLevel.deep);

        var tile = (numShallowNeighbours, numDeepNeighbours) switch
        {
            // Zero neighbours (tile type 1)...
            (0, 0) => configuration.type1.GetRandomTile(),

            // One neighbour (tile type 2)...
            (1, 0) => configuration.type2.GetRandomTile(),  // Shallow tile adjacent
            (0, 1) => configuration.type2a.GetRandomTile(), // Deep tile adjacent

            // Two opposite neighbours (tile type 3)...
            (2, 0) when                                     // Shallow tile adjacent
                orthogonalNeighbours[0].IsPolluted && orthogonalNeighbours[2].IsPolluted ||
                orthogonalNeighbours[1].IsPolluted && orthogonalNeighbours[3].IsPolluted => configuration.type3.GetRandomTile(),

            (1, 1) when                                     // One shallow one deep adjacent
                orthogonalNeighbours[0].IsPolluted && orthogonalNeighbours[2].IsPolluted && grid.GetToxicLevel(orthogonalNeighbours[0]) == GridTileBuilder.ToxicLevel.deep ||
                orthogonalNeighbours[1].IsPolluted && orthogonalNeighbours[3].IsPolluted && grid.GetToxicLevel(orthogonalNeighbours[1]) == GridTileBuilder.ToxicLevel.deep => configuration.type3a.GetRandomTile(),

            (1, 1) when                                     // One shallow one deep adjacent
                orthogonalNeighbours[0].IsPolluted && orthogonalNeighbours[2].IsPolluted ||
                orthogonalNeighbours[1].IsPolluted && orthogonalNeighbours[3].IsPolluted => configuration.type3a.GetRandomTile().FlippedY(),

            (0, 2) when                                     // Deep tile adjacent
                orthogonalNeighbours[0].IsPolluted && orthogonalNeighbours[2].IsPolluted ||
                orthogonalNeighbours[1].IsPolluted && orthogonalNeighbours[3].IsPolluted => configuration.type3e.GetRandomTile(),

            // Two adjacent neighbours (tile type 4)...
            (2, 0) => configuration.type4.GetRandomTile(),  // Shallow tile adjacent
            (1, 1) when                                     // One shallow one deep adjacent
                grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).Index()]) == GridTileBuilder.ToxicLevel.deep &&
                orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).NextCWIndex()].IsPolluted => configuration.type4a.GetRandomTile(),
            (1, 1) => configuration.type4b.GetRandomTile(), // One shallow one deep adjacent
            (0, 2) => configuration.type4e.GetRandomTile(), // Deep tile adjacent

            // Three adjacent neighbours (tile type 5)...
            (3, 0) => configuration.type5.GetRandomTile(),  // Shallow tile adjacent
            (2, 1) when                                     // Two shallow one deep adjacent
                grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).Index()]) == GridTileBuilder.ToxicLevel.deep => configuration.type5a.GetRandomTile(),
            (2, 1) when                                     // Two shallow one deep adjacent
                grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).NextCWIndex()]) == GridTileBuilder.ToxicLevel.deep => configuration.type5b.GetRandomTile(),
            (2, 1) when                                     // Two shallow one deep adjacent
               grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).Index()]) == GridTileBuilder.ToxicLevel.shallow &&
               grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).NextCWIndex()]) == GridTileBuilder.ToxicLevel.shallow => configuration.type5a.GetRandomTile().FlippedY(),
            (2, 1) => configuration.type5a.GetRandomTile().FlippedY(),    // Two shallow one deep adjacent
            (1, 2) when                                     // One shallow two deep adjacent
                grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).Index()]) == GridTileBuilder.ToxicLevel.deep &&
                grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).NextCWIndex()]) == GridTileBuilder.ToxicLevel.deep => configuration.type5c.GetRandomTile(),
            (1, 2) when                                     // One shallow two deep adjacent
                grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).NextCWIndex()]) == GridTileBuilder.ToxicLevel.deep &&
                grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).NextCWIndex()]) != GridTileBuilder.ToxicLevel.deep => configuration.type5f.GetRandomTile(),
            (1, 2) => configuration.type5c.GetRandomTile().FlippedY(),    // One shallow two deep adjacent
            (0, 3) => configuration.type5g.GetRandomTile(),
            _ => null
        };

        if (tile != null)
        {
            var alignmentDirection = GetAlignmentDirection(orthogonalNeighbours);
            tile.transform.Rotate(0.0f, 0.0f, alignmentDirection.Heading());
        }

        return tile;
    }

    public GameObject[] CreateTileAdornments(WorldGrid grid, Coordinate coord)
    {
        return new GameObject[0];
    }

    public GameObject UpdateTileRepresentation(WorldGrid grid, Coordinate coord, ref GameObject currentTile)
    {
        throw new System.NotImplementedException();
    }

    //private bool IsNextNeighbourPolluted(Coordinate[] neighbours, int i) => neighbours[(i + 1) % 4].IsPolluted;

    private Direction GetAlignmentDirection(Coordinate[] orthogonalNeighbours)
    {
        if (orthogonalNeighbours.Count(c => c.IsPolluted) == 2
            && orthogonalNeighbours[0].IsPolluted && orthogonalNeighbours[2].IsPolluted)
        {
            // North-south two polluted tiles are handled wrong by the below, causing them to flip in the
            // North-South direction. So this special handling gets us out of jail by checking for the one
            // awkward case.
            return Direction.North;
        }

        var coordIndexTuple = orthogonalNeighbours
            .Select((coord, index) => (coord, index))
            .SkipWhile(tuple => tuple.coord.IsPolluted)
            .FirstOrDefault(tuple => tuple.coord.IsPolluted);

        return (Direction)(coordIndexTuple == default ? 0 : coordIndexTuple.index);
    }
}
