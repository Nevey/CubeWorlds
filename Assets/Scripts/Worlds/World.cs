using CCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCore.CubeWorlds.Worlds.WorldTiles;

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

		private IWorldEnabler[] iWorldEnablers;

		public WorldConfig Config { get { return config; } }

		public WorldTile[,,] Grid { get { return grid; } }

		public List<WorldTile> FlattenedGrid { get { return flattenedGrid; } }

		private void Start()
		{
			config = Resources.Load("BIG PHAT TEST") as WorldConfig;

			Debug.Log(config.GridSize);

			// TODO: In stead of creating world, find and map all tiles, then update grid etc...
			// CreateWorldGrid();

			// UpdateWorldGrid();

			// EnableWorld();
		}

		// private void CreateWorldGrid()
		// {
		// 	grid = new WorldTile[
		// 		config.WorldSize,
		// 		config.WorldSize,
		// 		config.WorldSize
		// 	];

		// 	for (int x = 0; x < config.WorldSize; x++)
		// 	{
		// 		for (int y = 0; y < config.WorldSize; y++)
		// 		{
		// 			for (int z = 0; z < config.WorldSize; z++)
		// 			{
		// 				WorldTile worldTile = CreateCube(x, y, z);

		// 				grid[x, y, z] = worldTile;

		// 				flattenedGrid.Add(worldTile);
		// 			}
		// 		}
		// 	}
		// }

		// private WorldTile CreateCube(int x, int y, int z)
		// {
		// 	WorldTile worldTile = Instantiate(config.WorldTilePrefab);

		// 	worldTile.Setup(x, y, z);

		// 	float cubeSize = worldTile.GetComponent<Renderer>().bounds.size.x;

		// 	float halfGridSize = (cubeSize + config.SpaceBetweenCubes) * config.WorldSize / 2;

		// 	float halfCubeSize = (cubeSize + config.SpaceBetweenCubes) / 2;

		// 	Vector3 position = new Vector3(
		// 		(cubeSize + config.SpaceBetweenCubes) * x - halfGridSize + halfCubeSize,
		// 		(cubeSize + config.SpaceBetweenCubes) * y - halfGridSize + halfCubeSize,
		// 		(cubeSize + config.SpaceBetweenCubes) * z - halfGridSize + halfCubeSize
		// 	);
			
		// 	worldTile.transform.position = position;
			
		// 	worldTile.transform.parent = transform;
			
		// 	worldTile.name = config.Name + "'s tile[" + x + ", " + y + ", " + z + "]";

		// 	return worldTile;
		// }

		// private void UpdateWorldGrid()
		// {
		// 	for (int i = 0; i < flattenedGrid.Count; i++)
		// 	{
		// 		flattenedGrid[i].UpdateWalkableArea(grid, config, debugWalkableSides);
		// 	}
		// }

		private void EnableWorld()
		{
			iWorldEnablers = GetComponentsInChildren<IWorldEnabler>();

			for (int i = 0; i < iWorldEnablers.Length; i++)
			{
				iWorldEnablers[i].OnWorldEnable();
			}
		}

		private void DisableWorld()
		{
			for (int i = 0; i < iWorldEnablers.Length; i++)
			{
				iWorldEnablers[i].OnWorldDisable();
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
			int randomIndex = Random.Range(0, flattenedGrid.Count);

			return flattenedGrid[randomIndex];
		}

		public WorldTile GetRandomSurfacedWorldTile()
		{
			List<WorldTile> surfacedWorldTiles = GetSurfacedWorldTiles();

			int randomIndex = Random.Range(0, surfacedWorldTiles.Count);

			return surfacedWorldTiles[randomIndex];
		}
	}
}
