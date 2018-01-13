using System;
using CCore.CubeWorlds.GameInput;
using CCore.CubeWorlds.Players;
using CCore.CubeWorlds.Worlds.WorldTiles;
using UnityEngine;

namespace CCore.CubeWorlds.Worlds
{
	[RequireComponent(typeof(World))]
	public class WorldPlayerSpawner : MonoBehaviour, IWorldEnabler
	{
		[SerializeField] private Player playerPrefab;

		private World world;

		private void Start()
		{
			world = GetComponent<World>();
		}

        private void OnPlayerInputEvent(object sender, PlayerInputArgs e)
        {
            if (e.inputState == InputState.Down
				&& e.playerInputType == PlayerInputType.SpawnCheat)
			{
				WorldTile worldTile = world.GetRandomSurfacedWorldTile();

				WorldTileSurface worldTileSurface = worldTile.GetRandomWalkablePlane();

				Player player = Instantiate(playerPrefab);

				// TODO: Set different parent, higher up in hierarchy
				player.transform.parent = worldTile.transform.parent;

				player.transform.position = worldTileSurface.transform.position;

				player.Activate();

				Log("Spawned player on coordinate [" 
					+ worldTile.Coordinates.x + ", " + worldTile.Coordinates.y + ", " + worldTile.Coordinates.z
					+ "] on the " + worldTileSurface.SurfaceRotation + " side.");
			}
        }

		private void EnableSpawnCheat()
		{
			if (Debug.isDebugBuild)
			{
				PlayerInput.PlayerInputEvent += OnPlayerInputEvent;
			}
		}

		private void DisableSpawnCheat()
		{
			if (Debug.isDebugBuild)
			{
				PlayerInput.PlayerInputEvent -= OnPlayerInputEvent;
			}
		}

		public void OnWorldEnable()
		{
			EnableSpawnCheat();
		}

		public void OnWorldDisable()
		{
			DisableSpawnCheat();
		}
    }
}
