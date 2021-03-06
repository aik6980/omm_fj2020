﻿using System;
using System.Collections.Generic;
using UnityEngine;


public class LevelReader : MonoSingleton<LevelReader>
{
	public class LevelData
	{
		public struct Coordinates
		{
			public int x;
			public int y;

			public Vector2Int ToVector2Int()
            {
				return new Vector2Int(x, y);
            }
		}

        public int levelNumber;
		public Coordinates Start;
		public Coordinates End;
		public List<Coordinates> Solid = new List<Coordinates> { };
		public List<Coordinates> Magma = new List<Coordinates> { };
		public List<Coordinates> Block = new List<Coordinates> { };

		public Vector2Int Dimension;
		public LevelConfigData Config;
	}

	public LevelData GetLevelData(int level_num)
	{
		///
		/// 0	Unassigned (empty)
		/// 1	Starting tile
		/// 2	Ending tile
		/// 3	Magma source
		/// 4	Blocking tile (rock/obstacle)
		/// 5	Pre-gen island/ground
		///
		var level = new LevelData();

        level.levelNumber = level_num;

		var level_config_json_file	= Resources.Load<TextAsset>(string.Format("Levels/Lv{0}.cfg", level_num));

		if (level_config_json_file != null)
		{
			var level_config = JsonUtility.FromJson<LevelConfigData>(level_config_json_file.text);
			level.Config = level_config;
		}
		else
		{
			level.Config = new LevelConfigData(){ EnvironmentName = "Env_Rocky", MaxSpreadDistance = 0, ToxicSpreadTime = 15f, ToxicSpreadTimeVariation = 5f };
		}

		var level_file			= Resources.Load<TextAsset>(string.Format("Levels/Lv{0}", level_num));
		if (level_file == null)
        {
			return null;
        }

		var line = level_file.text.Split('\n');

		var width = 0;
		var height = line.Length;

		for (int y = 0; y < line.Length; ++y)
		{
			var part = line[y].Split(',', '\t');
			width = Mathf.Max(width, part.Length);
			for (int x = 0; x < part.Length; ++x)
			{
				switch (part[x])
				{
					case "1":
						level.Start = new LevelData.Coordinates { x = x, y = line.Length - y - 1 };
						break;

					case "2":
						level.End = new LevelData.Coordinates { x = x, y = line.Length - y - 1 };
						break;

					case "3":
						level.Magma.Add(new LevelData.Coordinates { x = x, y = line.Length - y - 1 });
						break;

					case "4":
						level.Block.Add(new LevelData.Coordinates { x = x, y = line.Length - y - 1 });
						break;

					case "5":
						level.Solid.Add(new LevelData.Coordinates { x = x, y = line.Length - y - 1 });
						break;
				}
			}

		}

		level.Dimension = new Vector2Int(width, height);

		return level;
	}
}
