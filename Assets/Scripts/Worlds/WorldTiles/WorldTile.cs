using System;
using System.Collections.Generic;
using UnityEngine;

namespace CCore.CubeWorlds.Worlds.WorldTiles
{
	public class WorldTile : MonoBehaviour
	{
		// TODO: Shove into own scriptable object
		[SerializeField] private WalkablePlane walkablePlanePrefab;

		[Tooltip("The distance from the surface of the world tile")]
		[SerializeField] private float walkablePlaneDistance = 0.01f;

		private WorldTileCoordinates coordinates = new WorldTileCoordinates();

		private WorldTileState state = new WorldTileState();

		private WorldTileWalkableSides walkableSides = new WorldTileWalkableSides();

		private List<WalkablePlane> walkablePlanes = new List<WalkablePlane>();

		public WorldTileCoordinates Coordinates { get { return coordinates; } }

		public WorldTileState State { get { return state; } }

		public List<WalkablePlane> WalkablePlanes { get { return walkablePlanes; } }

		public bool IsSurfaced { get { return walkablePlanes.Count > 0; } }

		private void CreateWalkablePlanes()
		{
			// First destroy old walkable planes
			DestroyWalkablePlanes();

			for (int i = 0; i < walkableSides.WalkableSidesList.Count; i++)
			{
				float worldTileHalfSize = GetComponent<Renderer>().bounds.size.x / 2f;

				float offset = worldTileHalfSize + walkablePlaneDistance;
				
				WorldTileWalkableSide walkableSide = walkableSides.WalkableSidesList[i];

				WalkablePlane walkablePlane = Instantiate(walkablePlanePrefab);

				walkablePlane.transform.parent = transform;

				walkablePlane.Setup(walkableSide, offset);

				walkablePlanes.Add(walkablePlane);
			}
		}

		private void DestroyWalkablePlanes()
		{
			for (int i = 0; i < walkablePlanes.Count; i++)
			{
				Destroy(walkablePlanes[i].gameObject);
			}

			walkablePlanes.Clear();
		}

		public void Setup(int x, int y, int z)
		{
			coordinates.x = x;
			coordinates.y = y;
			coordinates.z = z;

			state = WorldTileState.Alive;
		}

		public void UpdateWalkableArea(WorldTile[,,] worldTiles, WorldConfig worldConfig, bool debug = false)
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

			if (coordinates.right < worldConfig.WorldSize)
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

			if (coordinates.top < worldConfig.WorldSize)
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

			if (coordinates.rear < worldConfig.WorldSize)
			{
				rearTile = worldTiles[
					coordinates.x,
					coordinates.y,
					coordinates.rear
				];
			}

			walkableSides.Reset();

			// If a specific surrounding tile wasn't found, it means 
			// there's walking space on this side of the tile
			if (leftTile == null)
			{
				walkableSides.Add(WorldTileWalkableSide.Left);
			}

			if (rightTile == null)
			{
				walkableSides.Add(WorldTileWalkableSide.Right);
			}

			if (bottomTile == null)
			{
				walkableSides.Add(WorldTileWalkableSide.Bottom);
			}

			if (topTile == null)
			{
				walkableSides.Add(WorldTileWalkableSide.Top);
			}

			if (frontTile == null)
			{
				walkableSides.Add(WorldTileWalkableSide.Front);
			}

			if (rearTile == null)
			{
				walkableSides.Add(WorldTileWalkableSide.Rear);
			}

			CreateWalkablePlanes();
		}

		public WalkablePlane GetRandomWalkablePlane()
		{
			int randomIndex = UnityEngine.Random.Range(0, walkablePlanes.Count);

			return walkablePlanes[randomIndex];
		}
	}
}
