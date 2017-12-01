using System;
using UnityEngine;

namespace CCore.CubeWorlds.Worlds.WorldTiles
{
	public class WorldTile : MonoBehaviour
	{
		private WorldTileCoordinates coordinates = new WorldTileCoordinates();

		private WorldTileState state = new WorldTileState();

		private WorldTileWalkableSides walkableSides = new WorldTileWalkableSides();

		public WorldTileCoordinates Coordinates { get { return coordinates; } }

		public WorldTileState State { get { return state; } }

		public void Setup(int x, int y, int z)
		{
			coordinates.x = x;
			coordinates.y = y;
			coordinates.z = z;

			state = WorldTileState.Alive;
		}

		public void UpdateWalkableSides(WorldTile[,,] worldTiles, WorldConfig worldConfig)
		{
			// First try to find surrounding tiles
			WorldTile leftTile 		= null;
			WorldTile rightTile 	= null;
			WorldTile topTile 		= null;
			WorldTile bottomTile 	= null;
			WorldTile frontTile 	= null;
			WorldTile rearTile 		= null;
			
			if (coordinates.left > 0)
			{
				leftTile = worldTiles[
					coordinates.left,
					coordinates.y,
					coordinates.z
				];
			}

			if (coordinates.right < worldConfig.WorldSize - 1)
			{
				rightTile = worldTiles[
					coordinates.right,
					coordinates.y,
					coordinates.z
				];
			}

			if (coordinates.bottom > 0)
			{
				bottomTile = worldTiles[
					coordinates.x,
					coordinates.bottom,
					coordinates.z
				];
			}

			if (coordinates.top < worldConfig.WorldSize - 1)
			{
				topTile = worldTiles[
					coordinates.x,
					coordinates.top,
					coordinates.z
				];
			}

			if (coordinates.front > 0)
			{
				frontTile = worldTiles[
					coordinates.x,
					coordinates.y,
					coordinates.front
				];
			}

			if (coordinates.rear < worldConfig.WorldSize - 1)
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
		}
	}
}
