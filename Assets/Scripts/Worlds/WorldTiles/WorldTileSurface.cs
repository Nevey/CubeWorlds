using System;
using UnityEngine;

namespace CCore.CubeWorlds.Worlds.WorldTiles
{
	public class WorldTileSurface : MonoBehaviour
	{
		private SurfaceRotation surfaceRotation;

		public SurfaceRotation SurfaceRotation { get { return surfaceRotation; } }

		private void Awake()
		{
			GetComponent<Renderer>().enabled = false;
		}

		public void Setup(SurfaceRotation walkableSide, float offset)
		{
			this.surfaceRotation = walkableSide;
			
			Vector3 position = transform.parent.position;

			Vector3 rotation = transform.parent.localRotation.eulerAngles;

			switch (walkableSide)
			{
				case SurfaceRotation.Left:
					position.x -= offset;
					rotation.y += 90f;
					break;

				case SurfaceRotation.Right:
					position.x += offset;
					rotation.y -= 90f;
					break;

				case SurfaceRotation.Bottom:
					position.y -= offset;
					rotation.x -= 90f;
					break;

				case SurfaceRotation.Top:
					position.y += offset;
					rotation.x += 90f;
					break;

				case SurfaceRotation.Front:
					position.z -= offset;
					break;

				case SurfaceRotation.Rear:
					position.z += offset;
					rotation.y += 180f;
					break;
			}

			transform.position = position;

			transform.localRotation = Quaternion.Euler(rotation);
		}
	}
}
