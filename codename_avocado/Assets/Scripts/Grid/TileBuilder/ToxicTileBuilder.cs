using System.Linq;
using UnityEngine;

public class ToxicTileBuilder : ITileBuilder
{
    private DeepToxicTileSet configuration;

    public ToxicTileBuilder(DeepToxicTileSet configuration)
    {
        this.configuration = configuration;
    }

    //public GameObject CreateTileRepresentation(WorldGrid grid, Coordinate coord)
    //{
    //    var orthogonalNeighbours = grid.GetOrthogonalNeighbours(coord).ToArray();
    //    int numShallowNeighbours = orthogonalNeighbours.Count(c => c.IsPolluted() && grid.GetToxicLevel(c) == GridTileBuilder.ToxicLevel.shallow);
    //    int numDeepNeighbours = orthogonalNeighbours.Count(c => c.IsPolluted() && grid.GetToxicLevel(c) == GridTileBuilder.ToxicLevel.deep);

    //    var tile = (numShallowNeighbours, numDeepNeighbours) switch
    //    {
    //        // Zero neighbours (tile type 1)...
    //        (0, 0) => configuration.type1.GetRandomTile(),

    //        // One neighbour (tile type 2)...
    //        (1, 0) => configuration.type2.GetRandomTile(),  // Shallow tile adjacent
    //        (0, 1) => configuration.type2a.GetRandomTile(), // Deep tile adjacent

    //        // Two opposite neighbours (tile type 3)...
    //        (2, 0) when                                     // Shallow tile adjacent
    //            orthogonalNeighbours[0].IsPolluted() && orthogonalNeighbours[2].IsPolluted() ||
    //            orthogonalNeighbours[1].IsPolluted() && orthogonalNeighbours[3].IsPolluted() => configuration.type3.GetRandomTile(),

    //        (1, 1) when                                     // One shallow one deep adjacent
    //            orthogonalNeighbours[0].IsPolluted() && orthogonalNeighbours[2].IsPolluted() && grid.GetToxicLevel(orthogonalNeighbours[0]) == GridTileBuilder.ToxicLevel.deep ||
    //            orthogonalNeighbours[1].IsPolluted() && orthogonalNeighbours[3].IsPolluted() && grid.GetToxicLevel(orthogonalNeighbours[1]) == GridTileBuilder.ToxicLevel.deep => configuration.type3a.GetRandomTile(),

    //        (1, 1) when                                     // One shallow one deep adjacent
    //            orthogonalNeighbours[0].IsPolluted() && orthogonalNeighbours[2].IsPolluted() ||
    //            orthogonalNeighbours[1].IsPolluted() && orthogonalNeighbours[3].IsPolluted() => configuration.type3a.GetRandomTile().FlippedY(),

    //        (0, 2) when                                     // Deep tile adjacent
    //            orthogonalNeighbours[0].IsPolluted() && orthogonalNeighbours[2].IsPolluted() ||
    //            orthogonalNeighbours[1].IsPolluted() && orthogonalNeighbours[3].IsPolluted() => configuration.type3e.GetRandomTile(),

    //        // Two adjacent neighbours (tile type 4)...
    //        (2, 0) => configuration.type4.GetRandomTile(),  // Shallow tile adjacent
    //        (1, 1) when                                     // One shallow one deep adjacent
    //            grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).Index()]) == GridTileBuilder.ToxicLevel.deep &&
    //            orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).NextCWIndex()].IsPolluted() => configuration.type4a.GetRandomTile(),
    //        (1, 1) => configuration.type4b.GetRandomTile(), // One shallow one deep adjacent
    //        (0, 2) => configuration.type4e.GetRandomTile(), // Deep tile adjacent

    //        // Three adjacent neighbours (tile type 5)...
    //        (3, 0) => configuration.type5.GetRandomTile(),  // Shallow tile adjacent
    //        (2, 1) when                                     // Two shallow one deep adjacent
    //            grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).Index()]) == GridTileBuilder.ToxicLevel.deep => configuration.type5a.GetRandomTile(),
    //        (2, 1) when                                     // Two shallow one deep adjacent
    //            grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).NextCWIndex()]) == GridTileBuilder.ToxicLevel.deep => configuration.type5b.GetRandomTile(),
    //        (2, 1) when                                     // Two shallow one deep adjacent
    //           grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).Index()]) == GridTileBuilder.ToxicLevel.shallow &&
    //           grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).NextCWIndex()]) == GridTileBuilder.ToxicLevel.shallow => configuration.type5a.GetRandomTile().FlippedY(),
    //        (2, 1) => configuration.type5a.GetRandomTile().FlippedY(),    // Two shallow one deep adjacent
    //        (1, 2) when                                     // One shallow two deep adjacent
    //            grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).Index()]) == GridTileBuilder.ToxicLevel.deep &&
    //            grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).NextCWIndex()]) == GridTileBuilder.ToxicLevel.deep => configuration.type5e.GetRandomTile(),
    //        (1, 2) when                                     // One shallow two deep adjacent
    //            grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).Index()]) == GridTileBuilder.ToxicLevel.deep &&
    //            grid.GetToxicLevel(orthogonalNeighbours[GetAlignmentDirection(orthogonalNeighbours).NextCWIndex()]) != GridTileBuilder.ToxicLevel.deep => configuration.type5f.GetRandomTile(),
    //        (1, 2) => configuration.type5e.GetRandomTile().FlippedY(),    // One shallow two deep adjacent
    //        (0, 3) => configuration.type5g.GetRandomTile(),
    //        _ => null
    //    };

    //    if (tile != null)
    //    {
    //        var alignmentDirection = GetAlignmentDirection(orthogonalNeighbours);
    //        tile.transform.Rotate(0.0f, 0.0f, alignmentDirection.Heading());
    //    }

    //    return tile;
    //}

    public GameObject CreateTileRepresentation(WorldGrid grid, Coordinate coord)
    {
        var orthogonalNeighbours = grid.GetOrthogonalNeighbours(coord).ToArray();
        var diagonalNeighbours = grid.GetDiagonalNeighbours(coord).ToArray();

        int numOrthogonalNeighbours = orthogonalNeighbours.Count(c => c.IsDeepPolluted());
        int numDiagonalNeighbours = diagonalNeighbours.Count(c => c.IsDeepPolluted());
        var alignmentDirection = GetAlignmentDirection(orthogonalNeighbours);

        var tile = (numOrthogonalNeighbours, numDiagonalNeighbours) switch
        {
            // Zero neighbours (tile type 1)...
            (0, _) => configuration.type1.GetRandomTile(),

            // One neighbour (tile type 2)...
            (1, _) => configuration.type2.GetRandomTile(),

            // Two opposite neighbours (tile type 3)...
            (2, _) when
                orthogonalNeighbours[0].IsDeepPolluted() && orthogonalNeighbours[2].IsDeepPolluted() ||
                orthogonalNeighbours[1].IsDeepPolluted() && orthogonalNeighbours[3].IsDeepPolluted() => configuration.type3.GetRandomTile(),

            // Two adjacent neighbours (tile type 4)...
            (2, >= 1) when
                diagonalNeighbours[alignmentDirection.Index()].IsDeepPolluted() => configuration.type4a.GetRandomTile(),

            (2, _) => configuration.type4.GetRandomTile(),

            // Three adjacent neighbours (tile type 5)...
            (3, >= 2) when
                diagonalNeighbours[alignmentDirection.Index()].IsDeepPolluted() &&
                diagonalNeighbours[alignmentDirection.NextCWIndex()].IsDeepPolluted() => configuration.type5b.GetRandomTile(),

            //(3, >= 2) when
            //    diagonalNeighbours[alignmentDirection.NextACWIndex()].IsDeepPolluted() &&
            //    diagonalNeighbours[alignmentDirection.NextACW().NextACWIndex()].IsDeepPolluted() => configuration.type5b.GetRandomTile().FlippedX(),

            (3, >= 1 and < 4) when
                diagonalNeighbours[alignmentDirection.Index()].IsDeepPolluted() => configuration.type5a.GetRandomTile().FlippedY(),

            (3, >= 1 and < 4) when
                diagonalNeighbours[alignmentDirection.NextCWIndex()].IsDeepPolluted() => configuration.type5a.GetRandomTile(),

            (3, _) => configuration.type5.GetRandomTile(),

            // Four adjacent neighbours (tile type 6)...
            (4, 4) => configuration.type6e.GetRandomTile(),

            (4, 3) when
                !diagonalNeighbours[alignmentDirection.Index()].IsDeepPolluted() => configuration.type6d.GetRandomTile(),

            (4, 3) when
                !diagonalNeighbours[alignmentDirection.NextCWIndex()].IsDeepPolluted() => configuration.type6d.GetRandomTile().FlippedY(),

            (4, 3) when
                !diagonalNeighbours[alignmentDirection.NextACWIndex()].IsDeepPolluted() => configuration.type6d.GetRandomTile().FlippedX(),

            (4, 3) => configuration.type6d.GetRandomTile().FlippedX().FlippedY(),

            (4, 2) when
                !diagonalNeighbours[alignmentDirection.NextACWIndex()].IsDeepPolluted() &&
                !diagonalNeighbours[alignmentDirection.NextCWIndex()].IsDeepPolluted() => configuration.type6c.GetRandomTile(),

            (4, 2) when
                diagonalNeighbours[alignmentDirection.NextACWIndex()].IsDeepPolluted() &&
                diagonalNeighbours[alignmentDirection.NextCWIndex()].IsDeepPolluted() => configuration.type6c.GetRandomTile().FlippedX(),

            (4, 2) when
                diagonalNeighbours[alignmentDirection.Index()].IsDeepPolluted() &&
                diagonalNeighbours[alignmentDirection.NextCWIndex()].IsDeepPolluted() => configuration.type6b.GetRandomTile(),

            (4, 2) when
                !diagonalNeighbours[alignmentDirection.Index()].IsDeepPolluted() &&
                !diagonalNeighbours[alignmentDirection.NextCWIndex()].IsDeepPolluted() => configuration.type6b.GetRandomTile().FlippedX(),

            (4, 2) when
                diagonalNeighbours[alignmentDirection.Index()].IsDeepPolluted() &&
                diagonalNeighbours[alignmentDirection.NextACWIndex()].IsDeepPolluted() => configuration.type6bx.GetRandomTile(),

            (4, 2) when
                !diagonalNeighbours[alignmentDirection.NextACWIndex()].IsDeepPolluted() &&
                !diagonalNeighbours[alignmentDirection.Index()].IsDeepPolluted() => configuration.type6bx.GetRandomTile().FlippedY(),

            (4, 1) when
                diagonalNeighbours[alignmentDirection.Index()].IsDeepPolluted() => configuration.type6a.GetRandomTile(),

            (4, 1) when
                diagonalNeighbours[alignmentDirection.NextACWIndex()].IsDeepPolluted() => configuration.type6a.GetRandomTile().FlippedX(),

            (4, 1) when
                diagonalNeighbours[alignmentDirection.NextCWIndex()].IsDeepPolluted() => configuration.type6a.GetRandomTile().FlippedY(),

            (4, 1) => configuration.type6a.GetRandomTile().FlippedX().FlippedY(),

            (4, _) => configuration.type6.GetRandomTile(),

            // Catch all (should never get hit)...
            _ => null
        };

        if (tile != null)
        {
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

    //private bool IsNextNeighbourPolluted(Coordinate[] neighbours, int i) => neighbours[(i + 1) % 4].IsPolluted();

    private Direction GetAlignmentDirection(Coordinate[] orthogonalNeighbours)
    {
        if (orthogonalNeighbours.Count(c => c.IsDeepPolluted()) == 2
            && (orthogonalNeighbours[0].IsDeepPolluted()) && (orthogonalNeighbours[2].IsDeepPolluted()))
        {
            // North-south two polluted tiles are handled wrong by the below, causing them to flip in the
            // North-South direction. So this special handling gets us out of jail by checking for the one
            // awkward case.
            return Direction.North;
        }

        var coordIndexTuple = orthogonalNeighbours
            .Select((coord, index) => (coord, index))
            .SkipWhile(tuple => tuple.coord.IsDeepPolluted())
            .FirstOrDefault(tuple => tuple.coord.IsDeepPolluted());

        return (Direction)(coordIndexTuple == default ? 0 : coordIndexTuple.index);
    }
}
