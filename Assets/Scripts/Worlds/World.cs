using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCore;
using CCore.CubeWorlds.Worlds.WorldTiles;
using System;

namespace CCore.CubeWorlds.Worlds
{
	public enum WorldSideAxis
	{
		X,
		Y,
		Z
	}

	public class World : MonoBehaviour
	{
		[SerializeField] private bool debugWalkableSides;
		
		private WorldConfig config;

		/// <summary>
		/// This is the 3D grid of the world
		/// </summary>
		private WorldTile[,,] grid;

		/// <summary>
		/// This is the flattened grid of the world
		/// </summary>
		private List<WorldTile> flattenedGrid = new List<WorldTile>();

		private IWorldEnabler[] worldEnablers;

		public WorldConfig Config { get { return config; } }

		public WorldTile[,,] Grid { get { return grid; } }

		public List<WorldTile> FlattenedGrid { get { return flattenedGrid; } }

		private void Start()
		{
			config = Resources.Load(name) as WorldConfig;

			MapWorldTiles();

			UpdateWorldGrid();

			EnableWorld();
		}

		private void MapWorldTiles()
		{
			grid = new WorldTile[config.GridSize, config.GridSize, config.GridSize];

			for (int i = 0; i < transform.childCount; i++)
			{
				WorldTile worldTile = transform.GetChild(i).GetComponent<WorldTile>();

				if (worldTile == null)
				{
					continue;
				}

				// NOTE: Very dependant on world tile's name!!!
				// TODO: Find a safer way of doing this...
				string[] string1 = worldTile.name.Split('[');
				string[] string2 = string1[1].Split(']');
				string[] string3 = string2[0].Split(',');

				int x = Convert.ToInt16(string3[0]);
				int y = Convert.ToInt16(string3[1]);
				int z = Convert.ToInt16(string3[2]);

				worldTile.Setup(x, y, z);

				grid[x, y, z] = worldTile;

				flattenedGrid.Add(worldTile);
			}
		}

		private void UpdateWorldGrid()
		{
			for (int i = 0; i < flattenedGrid.Count; i++)
			{
				flattenedGrid[i].UpdateWalkableArea(grid, config.GridSize, debugWalkableSides);
			}
		}

		private void EnableWorld()
		{
			worldEnablers = GetComponentsInChildren<IWorldEnabler>();

			for (int i = 0; i < worldEnablers.Length; i++)
			{
				worldEnablers[i].OnWorldEnable();
			}
		}

		private void DisableWorld()
		{
			for (int i = 0; i < worldEnablers.Length; i++)
			{
				worldEnablers[i].OnWorldDisable();
			}
		}

		private List<WorldTile> GetSurfacedWorldTiles()
		{
			List<WorldTile> surfacedWorldTiles = new List<WorldTile>();

			for (int i = 0; i < flattenedGrid.Count; i++)
			{
				WorldTile worldTile = flattenedGrid[i];

				if (worldTile.IsSurfaced)
				{
					surfacedWorldTiles.Add(worldTile);
				}
			}

			return surfacedWorldTiles;
		}

		public WorldTile GetRandomWorldTile()
		{
			int randomIndex = UnityEngine.Random.Range(0, flattenedGrid.Count);

			return flattenedGrid[randomIndex];
		}

		public WorldTile GetRandomSurfacedWorldTile()
		{
			List<WorldTile> surfacedWorldTiles = GetSurfacedWorldTiles();

			int randomIndex = UnityEngine.Random.Range(0, surfacedWorldTiles.Count);

			return surfacedWorldTiles[randomIndex];
		}
	}
}
