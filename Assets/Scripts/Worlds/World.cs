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
		[SerializeField] private WorldConfig config;

		[SerializeField] private bool debugWalkableSides;

		/// <summary>
		/// This is the 3D grid of the world
		/// </summary>
		private WorldTile[,,] grid;

		/// <summary>
		/// This is the flattened grid of the world
		/// </summary>
		private List<WorldTile> flattenedGrid = new List<WorldTile>();

		public WorldConfig Config { get { return config; } }

		public WorldTile[,,] Grid { get { return grid; } }

		private void Start()
		{
			CreateWorld();
		}

		private void CreateWorld()
		{
			CreateWorldGrid();

			UpdateWorldGrid();
		}

		private void CreateWorldGrid()
		{
			grid = new WorldTile[
				config.WorldSize,
				config.WorldSize,
				config.WorldSize
			];

			for (int x = 0; x < config.WorldSize; x++)
			{
				for (int y = 0; y < config.WorldSize; y++)
				{
					for (int z = 0; z < config.WorldSize; z++)
					{
						WorldTile worldTile = CreateCube(x, y, z);

						grid[x, y, z] = worldTile;

						flattenedGrid.Add(worldTile);
					}
				}
			}
		}

		private WorldTile CreateCube(int x, int y, int z)
		{
			WorldTile worldTile = Instantiate(config.WorldTilePrefab);

			worldTile.Setup(x, y, z);

			float cubeSize = worldTile.GetComponent<Renderer>().bounds.size.x;

			float halfGridSize = (cubeSize + config.SpaceBetweenCubes) * config.WorldSize / 2;

			float halfCubeSize = (cubeSize + config.SpaceBetweenCubes) / 2;

			Vector3 position = new Vector3(
				(cubeSize + config.SpaceBetweenCubes) * x - halfGridSize + halfCubeSize,
				(cubeSize + config.SpaceBetweenCubes) * y - halfGridSize + halfCubeSize,
				(cubeSize + config.SpaceBetweenCubes) * z - halfGridSize + halfCubeSize
			);
			
			worldTile.transform.position = position;
			
			worldTile.transform.parent = transform;
			
			worldTile.name = config.Name + "'s tile[" + x + ", " + y + ", " + z + "]";

			return worldTile;
		}

		private void UpdateWorldGrid()
		{
			for (int i = 0; i < flattenedGrid.Count; i++)
			{
				flattenedGrid[i].UpdateWalkableSides(grid, config, debugWalkableSides);
			}
		}
	}
}
