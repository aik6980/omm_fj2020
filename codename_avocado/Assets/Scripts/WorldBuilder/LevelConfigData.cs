﻿using System;

[Serializable]
public class LevelConfigData
{
    // Environment
    public string EnvironmentName;
    public float EnvironmentOffsetX;
    public float EnvironmentOffsetY;
    public float EnvironmentOffsetZ;

    // Toxic
    public int MaxSpreadDistance;
    public float ToxicSpreadTime;
    public float ToxicSpreadTimeVariation;
}
