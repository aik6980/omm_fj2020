using UnityEngine;

[CreateAssetMenu(fileName = "TileFactoryConfiguration", menuName = "ScriptableObjects/Tile Factory Configuration")]
public class TileFactoryConfiguration : ScriptableObject
{
    public TileSet startTiles;
    public TileSet exitTiles;
    public TileSet floorTiles;
    public TileSet grassTiles;
    public TileSet obstacleTiles;
    public DeepToxicTileSet deepToxicTiles;
    public TileSet shallowToxicTiles;

    public TileSet toxicSourceAdornments;
}
