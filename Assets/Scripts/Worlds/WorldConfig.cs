using UnityEngine;
using System.Collections;
using CCore.CubeWorlds.Worlds.WorldTiles;

namespace CCore.CubeWorlds.Worlds
{
	[CreateAssetMenu(fileName = "WorldConfig", menuName = "Config/World", order = 1)]
	public class WorldConfig : ScriptableObject
	{
		[Header("World Identification")]
		[SerializeField] private int id;

		[SerializeField] private new string name;

		[Header("World Appearance")]
		[SerializeField] private int worldSize;

		[SerializeField] private WorldTile worldTile;

		[SerializeField] private float spaceBetweenCubes;

		public int ID { get { return id; } }

		public string Name { get { return name; } }

		public int WorldSize { get { return worldSize; } }

		public WorldTile WorldTilePrefab { get { return worldTile; } }

		public float SpaceBetweenCubes { get { return spaceBetweenCubes; } }
	}
}
