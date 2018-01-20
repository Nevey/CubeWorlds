using UnityEngine;
using System.Collections;
using CCore.CubeWorlds.Worlds.WorldTiles;

namespace CCore.CubeWorlds.Worlds
{
	public class WorldConfig : ScriptableObject
	{
		private int gridSize;

		private WorldTile[,,] grid;

		public int GridSize { get { return gridSize; } }

		public WorldTile[,,] Grid { get { return grid; } }

		public void SetData(int gridSize, WorldTile[,,] grid)
		{
			this.gridSize = gridSize;

			this.grid = grid;
		}
	}
}
