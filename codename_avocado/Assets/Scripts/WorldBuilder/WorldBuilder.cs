using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WorldBuilder : MonoSingleton<WorldBuilder>
{
	private class BuilderShape : Shape
	{
		private Color col;

		public BuilderShape(Color col)
        {
			this.col = col;
        }

        public override Color GetColor()
        {
            return this.col;
        }
    }

	public LevelReader.LevelData BuildLevel(WorldGrid grid, int level_num)
    {
		return LevelReader.GetOrCreateInstance().GetLevelData(level_num);
	}

}
