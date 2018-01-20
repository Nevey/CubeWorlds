using System.Collections.Generic;
using CCore.CubeWorlds.Worlds.WorldTiles;
using UnityEditor;
using UnityEngine;

namespace CCore.CubeWorlds.Worlds.Editor
{
    [ExecuteInEditMode]
    public class WorldBuilder : MonoBehaviour
    {
		private int gridSize;

		public int GridSize { get { return gridSize; } }

        public void CreateWorldGrid(int gridSize, WorldTile tilePrefab, float spaceBetweenTiles)
		{
			this.gridSize = gridSize;

			for (int x = 0; x < gridSize; x++)
			{
				for (int y = 0; y < gridSize; y++)
				{
					for (int z = 0; z < gridSize; z++)
					{
						WorldTile worldTile = CreateCube(
							gridSize,
							tilePrefab,
							spaceBetweenTiles,
							x, y, z);
					}
				}
			}
		}

		private WorldTile CreateCube(
			int gridSize,
			WorldTile tilePrefab,
			float spaceBetweenTiles,
			int x, int y, int z)
		{
			WorldTile worldTile;

			if (tilePrefab != null)
			{
				worldTile = Instantiate(tilePrefab);
			}
			else
			{
				worldTile = GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<WorldTile>();
			}

			float cubeSize = worldTile.GetComponent<Renderer>().bounds.size.x;

			float halfGridSize = (cubeSize + spaceBetweenTiles) * gridSize / 2;

			float halfCubeSize = (cubeSize + spaceBetweenTiles) / 2;

			Vector3 position = new Vector3(
				(cubeSize + spaceBetweenTiles) * x - halfGridSize + halfCubeSize,
				(cubeSize + spaceBetweenTiles) * y - halfGridSize + halfCubeSize,
				(cubeSize + spaceBetweenTiles) * z - halfGridSize + halfCubeSize
			);
			
			worldTile.transform.position = position;
			
			worldTile.transform.parent = transform;
			
			worldTile.name = gameObject.name + "'s tile[" + x + ", " + y + ", " + z + "]";

			return worldTile;
		}
    }
}