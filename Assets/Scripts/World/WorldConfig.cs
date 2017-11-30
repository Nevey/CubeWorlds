using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "WorldConfig", menuName = "Config/World", order = 1)]
public class WorldConfig : ScriptableObject
{
	[Tooltip("World Identification")]
	[SerializeField] private int id;

	[SerializeField] private new string name;

	[Tooltip("World Appearance")]
	[SerializeField] private int worldSize;

	[SerializeField] private GameObject cubePrefab;

	[SerializeField] private float spaceBetweenCubes;

	[Tooltip("Walkable Area")]
	[SerializeField] private GameObject tilePrefab;

	[SerializeField] private float tileDistanceFromWorld;

	public int ID { get { return id; } }

	public string Name { get { return name; } }

	public int WorldSize { get { return worldSize; } }

	public GameObject CubePrefab { get { return cubePrefab; } }

	public float SpaceBetweenCubes { get { return spaceBetweenCubes; } }

	public GameObject TilePrefab { get { return tilePrefab; } }

	public float TileDistanceFromWorld { get { return tileDistanceFromWorld; } }
}