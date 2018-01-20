using System;
using System.Collections.Generic;
using UnityEngine;

namespace CCore.CubeWorlds.Worlds.WorldTiles
{
	public class WorldTile : MonoBehaviour
	{
		// TODO: Shove into own scriptable object
		[SerializeField] private WorldTileSurface walkablePlanePrefab;

		[Tooltip("The distance from the surface of the world tile")]
		[SerializeField] private float walkablePlaneDistance = 0.01f;

		private WorldTileCoordinates coordinates = new WorldTileCoordinates();

		private WorldTileState state = new WorldTileState();

		private SurfaceRotations surfaceRotations = new SurfaceRotations();

		private List<WorldTileSurface> surfaces = new List<WorldTileSurface>();

		public WorldTileCoordinates Coordinates { get { return coordinates; } }

		public WorldTileState State { get { return state; } }

		public List<WorldTileSurface> WorldTileSurfaces { get { return surfaces; } }

		public bool IsSurfaced { get { return surfaces.Count > 0; } }

		private void CreateWalkablePlanes()
		{
			// First destroy old walkable planes
			DestroyWalkablePlanes();

			for (int i = 0; i < surfaceRotations.SurfaceRotationList.Count; i++)
			{
				float worldTileHalfSize = GetComponent<Renderer>().bounds.size.x / 2f;

				float offset = worldTileHalfSize + walkablePlaneDistance;
				
				SurfaceRotation surfaceRotation = surfaceRotations.SurfaceRotationList[i];

				WorldTileSurface walkablePlane = Instantiate(GetWorldTileSurfacePrefab());

				walkablePlane.transform.parent = transform;

				walkablePlane.Setup(surfaceRotation, offset);

				surfaces.Add(walkablePlane);
			}
		}

		private void DestroyWalkablePlanes()
		{
			for (int i = 0; i < surfaces.Count; i++)
			{
				Destroy(surfaces[i].gameObject);
			}

			surfaces.Clear();
		}

		private WorldTileSurface GetWorldTileSurfacePrefab()
		{
			GameObject worldTileSurfacePrefab = Resources.Load("WorldTileSurface") as GameObject;

			WorldTileSurface worldTileSurface = worldTileSurfacePrefab.GetComponent<WorldTileSurface>();

			return worldTileSurface;
		}

		public void Setup(int x, int y, int z)
		{
			coordinates.x = x;
			coordinates.y = y;
			coordinates.z = z;

			state = WorldTileState.Alive;
		}

		public void UpdateWalkableArea(WorldTile[,,] worldTiles, int gridSize, bool debug = false)
		{
			// First try to find surrounding tiles
			WorldTile leftTile 		= null;
			WorldTile rightTile 	= null;
			WorldTile topTile 		= null;
			WorldTile bottomTile 	= null;
			WorldTile frontTile 	= null;
			WorldTile rearTile 		= null;
			
			if (coordinates.left >= 0)
			{
				leftTile = worldTiles[
					coordinates.left,
					coordinates.y,
					coordinates.z
				];
			}

			if (coordinates.right < gridSize)
			{
				rightTile = worldTiles[
					coordinates.right,
					coordinates.y,
					coordinates.z
				];
			}

			if (coordinates.bottom >= 0)
			{
				bottomTile = worldTiles[
					coordinates.x,
					coordinates.bottom,
					coordinates.z
				];
			}

			if (coordinates.top < gridSize)
			{
				topTile = worldTiles[
					coordinates.x,
					coordinates.top,
					coordinates.z
				];
			}

			if (coordinates.front >= 0)
			{
				frontTile = worldTiles[
					coordinates.x,
					coordinates.y,
					coordinates.front
				];
			}

			if (coordinates.rear < gridSize)
			{
				rearTile = worldTiles[
					coordinates.x,
					coordinates.y,
					coordinates.rear
				];
			}

			surfaceRotations.Reset();

			// If a specific surrounding tile wasn't found, it means 
			// there's walking space on this side of the tile
			if (leftTile == null)
			{
				surfaceRotations.Add(SurfaceRotation.Left);
			}

			if (rightTile == null)
			{
				surfaceRotations.Add(SurfaceRotation.Right);
			}

			if (bottomTile == null)
			{
				surfaceRotations.Add(SurfaceRotation.Bottom);
			}

			if (topTile == null)
			{
				surfaceRotations.Add(SurfaceRotation.Top);
			}

			if (frontTile == null)
			{
				surfaceRotations.Add(SurfaceRotation.Front);
			}

			if (rearTile == null)
			{
				surfaceRotations.Add(SurfaceRotation.Rear);
			}

			CreateWalkablePlanes();
		}

		public WorldTileSurface GetRandomWalkablePlane()
		{
			int randomIndex = UnityEngine.Random.Range(0, surfaces.Count);

			return surfaces[randomIndex];
		}
	}
}
