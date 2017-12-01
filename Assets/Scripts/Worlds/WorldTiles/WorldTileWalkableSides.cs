using System.Collections.Generic;

namespace CCore.CubeWorlds.Worlds.WorldTiles
{
    public class WorldTileWalkableSides
    {
        private List<WorldTileWalkableSide> walkableSides = new List<WorldTileWalkableSide>();

        public List<WorldTileWalkableSide> WalkableSides { get { return walkableSides; } }

        public void Add(WorldTileWalkableSide walkableSide)
        {
            if (walkableSides.Contains(walkableSide))
            {
                return;
            }

            walkableSides.Add(walkableSide);
        }

        public void Reset()
        {
            walkableSides.Clear();
        }
    }
}
