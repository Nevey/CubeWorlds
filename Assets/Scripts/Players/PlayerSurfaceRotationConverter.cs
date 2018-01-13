using CCore.CubeWorlds.Worlds.WorldTiles;
using UnityEngine;

namespace CCore.CubeWorlds.Players
{
    public class PlayerSurfaceRotationConverter
    {
        /// <summary>
        /// The given value is based on local rotation.
        /// </summary>
        /// <param name="surfaceRotation"></param>
        /// <returns></returns>
        public static Vector3 SurfaceRotationToWorldRotation(SurfaceRotation surfaceRotation)
		{
            // Start rotation, as if standing on top of a world
			Vector3 rotation = Vector3.zero;

			switch (surfaceRotation)
			{
				case SurfaceRotation.Left:
					rotation.z += 90f;
					break;
				
				case SurfaceRotation.Right:
					rotation.z -= 90f;
					break;
				
				case SurfaceRotation.Bottom:
					rotation.z += 180f;
					break;
				
				case SurfaceRotation.Top:
					break;
				
				case SurfaceRotation.Front:
					rotation.x -= 90f;
					break;
				
				case SurfaceRotation.Rear:
					rotation.x += 90f;
					break;
				
			}

			return rotation;
		}
    }
}