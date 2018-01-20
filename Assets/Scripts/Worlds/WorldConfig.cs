using UnityEngine;
using System.Collections;
using CCore.CubeWorlds.Worlds.WorldTiles;
using System.Collections.Generic;

namespace CCore.CubeWorlds.Worlds
{
	public class WorldConfig : ScriptableObject
	{
		[SerializeField] private int gridSize;

		public int GridSize { get { return gridSize; } }

		public void SetData(int gridSize)
		{
			this.gridSize = gridSize;
		}
	}
}
