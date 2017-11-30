using CCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCore.CubeWorlds.Worlds
{
	public enum WorldSideAxis
	{
		X,
		Y,
		Z
	}

	public class World : Monobehaviour
	{
		[SerializeField] private WorldConfig worldConfig;

		/// <summary>
		/// This is the 3D grid of the world
		/// </summary>
		private GameObject[,,] worldGrid;

		private void Awake()
		{
			CreateWorld();
		}

		private void CreateWorld()
		{
			CreateWorldGrid();

			CreateWorldWalkableGrid();
		}

		private void CreateWorldGrid()
		{
			worldGrid = new GameObject[
				worldConfig.WorldSize,
				worldConfig.WorldSize,
				worldConfig.WorldSize
			];

			for (int x = 0; x < worldConfig.WorldSize; x++)
			{
				for (int y = 0; y < worldConfig.WorldSize; y++)
				{
					for (int z = 0; z < worldConfig.WorldSize; z++)
					{
						worldGrid[x, y, z] = CreateCube(x, y, z);
					}
				}
			}
		}

		private GameObject CreateCube(int x, int y, int z)
		{
			GameObject cube = Instantiate(worldConfig.CubePrefab);

			float cubeSize = cube.GetComponent<Renderer>().bounds.size.x;

			float halfGridSize = (cubeSize + worldConfig.SpaceBetweenCubes) * worldConfig.WorldSize / 2;

			float halfCubeSize = (cubeSize + worldConfig.SpaceBetweenCubes) / 2;

			Vector3 position = new Vector3(
				(cubeSize + worldConfig.SpaceBetweenCubes) * x - halfGridSize + halfCubeSize,
				(cubeSize + worldConfig.SpaceBetweenCubes) * y - halfGridSize + halfCubeSize,
				(cubeSize + worldConfig.SpaceBetweenCubes) * z - halfGridSize + halfCubeSize
			);
			
			cube.transform.position = position;
			
			cube.transform.parent = transform;
			
			cube.name = "Cube [" + x + ", " + y + ", " + z + "]";

			return cube;
		}

		private void CreateWorldWalkableGrid()
		{
			// TODO: Put a 2D grid on the face of each side of the cube

			// For every cube, look around cube in all 6 directions in the cube grid

			for (int x = 0; x < worldConfig.WorldSize; x++)
			{
				for (int y = 0; y < worldConfig.WorldSize; y++)
				{
					for (int z = 0; z < worldConfig.WorldSize; z++)
					{
						GameObject cube = worldGrid[x, y, z];

						if (x == 0 || x == worldConfig.WorldSize - 1)
						{
							CreateTile(cube, WorldSideAxis.X, x);
						}

						if (y == 0 || y == worldConfig.WorldSize - 1)
						{
							CreateTile(cube, WorldSideAxis.Y, y);
						}

						if (z == 0 || z == worldConfig.WorldSize - 1)
						{
							CreateTile(cube, WorldSideAxis.Z, z);
						}
					}
				}
			}
		}

		private GameObject CreateTile(GameObject cube, WorldSideAxis axis, int axisValue)
		{
			// TODO: Apply an offset, based on start or end of the grid's side

			GameObject tile = Instantiate(
				worldConfig.TilePrefab,
				cube.transform.position,
				cube.transform.rotation,
				cube.transform
			);

			tile.GetComponent<Renderer>().material.color = Color.red;

			float cubeSize = cube.GetComponent<Renderer>().bounds.size.x;

			float offset = worldConfig.TileDistanceFromWorld + (cubeSize / 2f);

			Vector3 position = tile.transform.localPosition;

			Vector3 rotation = tile.transform.localRotation.eulerAngles;

			switch (axis)
			{
				case WorldSideAxis.X:

					if (axisValue == 0)
					{
						position.x -= offset;

						rotation.z += 90f;
					}
					else if (axisValue == worldConfig.WorldSize - 1)
					{
						position.x += offset;

						rotation.z -= 90f;
					}

				break;

				case WorldSideAxis.Y:

					if (axisValue == 0)
					{
						position.y -= offset;

						rotation.x += 180f;
					}
					else if (axisValue == worldConfig.WorldSize - 1)
					{
						position.y += offset;
					}

				break;

				case WorldSideAxis.Z:

					if (axisValue == 0)
					{
						position.z -= offset;

						rotation.x -= 90f;
					}
					else if (axisValue == worldConfig.WorldSize - 1)
					{
						position.z += offset;

						rotation.x += 90f;
					}

				break;
			}

			tile.transform.localPosition = position;

			tile.transform.localRotation = Quaternion.Euler(rotation);

			return tile;
		}
	}
}
