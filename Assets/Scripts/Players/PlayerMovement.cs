using System;
using CCore.CubeWorlds.GameInput;
using UnityEngine;

namespace CCore.CubeWorlds.Players
{
    [RequireComponent(typeof(Player))]
    public class PlayerMovement : MonoBehaviour, IPlayerEnabler
    {
		[SerializeField] private float movementSpeed;

		private Player player;

		private void Start()
		{
			player = GetComponent<Player>();
		}

        private void OnPlayerInput(object sender, PlayerInputArgs e)
        {
			if (e.inputState != InputState.Hold)
			{
				return;
			}

            switch (e.playerInputType)
			{
				case PlayerInputType.MoveLeft:
					MoveCharacter(-transform.right, player.CameraSlot.Camera.transform);
					break;
				
				case PlayerInputType.MoveRight:
					MoveCharacter(transform.right, player.CameraSlot.Camera.transform);
					break;
				
				case PlayerInputType.MoveBackward:
					MoveCharacter(-transform.forward, transform);
					break;
				
				case PlayerInputType.MoveForward:
					MoveCharacter(transform.forward, transform);
					break;
			}
        }

		private void MoveCharacter(Vector3 direction, Transform relativeTo)
		{
			transform.Translate(direction * movementSpeed, Space.World);
		}

        public void OnPlayerEnabled()
        {
            PlayerInput.PlayerInputEvent += OnPlayerInput;
        }

        public void OnPlayerDisabled()
        {
            PlayerInput.PlayerInputEvent -= OnPlayerInput;
        }
    }
}
