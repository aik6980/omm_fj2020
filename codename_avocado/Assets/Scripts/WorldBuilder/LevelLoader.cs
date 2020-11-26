﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelLoader
{
    WorldGrid.WorldData LoadLevel(WorldGrid grid);
}

public class LevelLoader : MonoBehaviour, ILevelLoader
{
    public int Level = 1;

    public WorldGrid.WorldData LoadLevel(WorldGrid grid)
    {
        LaunchGameScript ls = LaunchGameScript.singleton;
        if (ls)
        {
            this.Level = ls.levelToLoad++;
        }

        return WorldBuilder.GetOrCreateInstance().BuildLevel(grid, this.Level++);
    }
}
