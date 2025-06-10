using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelLoader
{
    int CurrentLevel { get; }
    LevelDataObject LoadLevel(WorldGrid grid, int levelToLoad);
}

public class LevelLoader : MonoBehaviour, ILevelLoader
{
    public int Level = 1;
    public LevelOrder LevelOrder;

    public int CurrentLevel { get => Level; }

    public LevelDataObject LoadLevel(WorldGrid grid, int levelToLoad)
    {
        Level = levelToLoad;
        PlayerPrefs.SetInt("LevelAt", System.Math.Max(PlayerPrefs.GetInt("LevelAt", 1), this.Level));
        return Level <= LevelOrder.Levels.Length ? LevelOrder.Levels[Level-1] : null;
    }
}
