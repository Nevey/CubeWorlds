namespace CCore.CubeWorlds.Worlds
{
    public class WorldTileCoordinates
	{
		public int x { get; set; }
		public int y { get; set; }
		public int z { get; set; }

		public int left 	{ get { return x - 1; } }
		public int right 	{ get { return x + 1; } }
		public int bottom 	{ get { return y - 1; } }
		public int top		{ get { return y + 1; } }
		public int front 	{ get { return z - 1; } }
		public int rear 	{ get { return z + 1; } }
	}
}