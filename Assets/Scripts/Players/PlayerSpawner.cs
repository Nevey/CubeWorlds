using System;
using CCore.CubeWorlds.GameInput;
using CCore.CubeWorlds.Players;
using CCore.CubeWorlds.Worlds.WorldTiles;
using UnityEngine;

namespace CCore.CubeWorlds.Worlds
{
	[RequireComponent(typeof(World))]
	public class PlayerSpawner : MonoBehaviour, IWorldEnabler
	{
		[SerializeField] private Player playerPrefab;

		private World world;

		private void Start()
		{
			world = GetComponent<World>();
		}

        private void OnPlayerInputEvent(object sender, PlayerInputArgs e)
        {
            if (e.playerInputType == PlayerInputType.DebugSpawn
				&& e.inputState == InputState.Down)
			{
				WorldTile worldTile = world.GetRandomSurfacedWorldTile();

				WalkablePlane walkablePlane = worldTile.GetRandomWalkablePlane();

				Player player = Instantiate(playerPrefab);

				player.transform.parent = worldTile.transform.parent;

				player.transform.position = walkablePlane.transform.position;

				player.transform.localRotation = Quaternion.Euler(
					WalkableSideToRotation(walkablePlane.WalkableSide)
				);

				player.Activate();

				Log("Spawned player on coordinate [" 
					+ worldTile.Coordinates.x + ", " + worldTile.Coordinates.y + ", " + worldTile.Coordinates.z
					+ "] on the " + walkablePlane.WalkableSide + " side.");
			}
        }

		private Vector3 WalkableSideToRotation(WorldTileWalkableSide walkableSide)
		{
			Vector3 rotation = Vector3.zero;

			switch (walkableSide)
			{
				case WorldTileWalkableSide.Left:
					rotation.z += 90f;
					break;
				
				case WorldTileWalkableSide.Right:
					rotation.z -= 90f;
					break;
				
				case WorldTileWalkableSide.Bottom:
					rotation.z += 180f;
					break;
				
				case WorldTileWalkableSide.Top:
					break;
				
				case WorldTileWalkableSide.Front:
					rotation.x -= 90f;
					break;
				
				case WorldTileWalkableSide.Rear:
					rotation.x += 90f;
					break;
				
			}

			return rotation;
		}

		private void EnableSpawnCheat()
		{
			if (Debug.isDebugBuild)
			{
				PlayerInput.PlayerInputEvent += OnPlayerInputEvent;
			}
		}

		public void OnWorldEnable()
		{
			EnableSpawnCheat();
		}

		public void OnWorldDisable()
		{

		}
    }
}
