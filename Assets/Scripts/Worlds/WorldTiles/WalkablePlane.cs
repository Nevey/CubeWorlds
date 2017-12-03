using System;
using UnityEngine;

namespace CCore.CubeWorlds.Worlds.WorldTiles
{
	public class WalkablePlane : MonoBehaviour
	{
		private WorldTileWalkableSide walkableSide;

		public WorldTileWalkableSide WalkableSide { get { return walkableSide; } }

		private void Awake()
		{
			GetComponent<Renderer>().enabled = false;
		}

		public void Setup(WorldTileWalkableSide walkableSide, float offset)
		{
			this.walkableSide = walkableSide;
			
			Vector3 position = transform.parent.position;

			Vector3 rotation = transform.parent.localRotation.eulerAngles;

			switch (walkableSide)
			{
				case WorldTileWalkableSide.Left:
					position.x -= offset;
					rotation.y += 90f;
					break;

				case WorldTileWalkableSide.Right:
					position.x += offset;
					rotation.y -= 90f;
					break;

				case WorldTileWalkableSide.Bottom:
					position.y -= offset;
					rotation.x -= 90f;
					break;

				case WorldTileWalkableSide.Top:
					position.y += offset;
					rotation.x += 90f;
					break;

				case WorldTileWalkableSide.Front:
					position.z -= offset;
					break;

				case WorldTileWalkableSide.Rear:
					position.z += offset;
					rotation.y += 180f;
					break;
			}

			transform.position = position;

			transform.localRotation = Quaternion.Euler(rotation);
		}
	}
}
