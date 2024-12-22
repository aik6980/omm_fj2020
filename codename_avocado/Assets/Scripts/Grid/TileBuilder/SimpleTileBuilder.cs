using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleTileBuilder : ITileBuilder
{
    private TileSet configuration;

    public SimpleTileBuilder(TileSet configuration)
    {
        this.configuration = configuration;
    }

    public GameObject CreateTileRepresentation(WorldGrid grid, Coordinate coord) => configuration.GetRandomTile();

    public GameObject[] CreateTileAdornments(WorldGrid grid, Coordinate coord)
    {
        return new GameObject[0];
        //int numAdornments = Random.Range(configuration.minAdornments, configuration.maxAdornments);
        //return GetNextAdornment(numAdornments).ToArray();

        //IEnumerable<GameObject> GetNextAdornment(int numAdornments)
        //{
        //    for (int i = 0; i < numAdornments; ++i)
        //    {
        //        var adornment = configuration.adornmentSet.GetRandomTile();
        //        if (adornment == null)
        //        {
        //            continue;
        //        }

        //        adornment.transform.Rotate(0.0f, 0.0f, 90.0f * Random.Range(0, 1));
        //        yield return adornment;
        //    }
        //}
    }

    public GameObject UpdateTileRepresentation(WorldGrid grid, Coordinate coord, ref GameObject currentTile)
    {
        throw new System.NotImplementedException();
    }
}
