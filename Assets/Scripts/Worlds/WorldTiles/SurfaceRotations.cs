using System.Collections.Generic;

namespace CCore.CubeWorlds.Worlds.WorldTiles
{
    /// <summary>
    /// Holds the walkable sides of a world tile
    /// </summary>
    public class SurfaceRotations
    {
        private List<SurfaceRotation> surfaceRotations = new List<SurfaceRotation>();

        public List<SurfaceRotation> SurfaceRotationList { get { return surfaceRotations; } }

        public void Add(SurfaceRotation surfaceRotation)
        {
            if (surfaceRotations.Contains(surfaceRotation))
            {
                return;
            }

            surfaceRotations.Add(surfaceRotation);
        }

        public void Reset()
        {
            surfaceRotations.Clear();
        }
    }
}
