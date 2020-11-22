using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelLoader : MonoBehaviour, ILevelLoader
{
    public int m_Distance = 20;

    public WorldGrid.WorldData LoadLevel(WorldGrid grid)
    {
        return WorldBuilder.GetOrCreateInstance().BuildDefaultTest(grid, m_Distance, 1, 3, 3);
    }
}
