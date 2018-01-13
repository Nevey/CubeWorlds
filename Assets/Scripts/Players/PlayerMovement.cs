using System;
using CCore.CubeWorlds.Cameras;
using CCore.CubeWorlds.GameInput;
using UnityEngine;

namespace CCore.CubeWorlds.Players
{
    [RequireComponent(typeof(Player))]
    public class PlayerMovement : MonoBehaviour, IPlayerEnabler
    {
		private Player player;

		private new Rigidbody rigidbody;

		private Vector3 cameraDirection;

		private void Start()
		{
			player = GetComponent<Player>();

			rigidbody = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			UpdatePosition();
		}

        private void OnPlayerInput(object sender, PlayerInputArgs e)
        {
			if (e.inputState != InputState.Hold)
			{
				cameraDirection = Vector3.zero;

				return;
			}

			Transform cameraTransform = player.CameraSlot.Camera.transform;

            switch (e.playerInputType)
			{
				case PlayerInputType.MoveLeft:
					cameraDirection = -cameraTransform.right;
					break;
				
				case PlayerInputType.MoveRight:
					cameraDirection = cameraTransform.right;
					break;
				
				case PlayerInputType.MoveBackward:
					cameraDirection = -cameraTransform.forward;
					break;
				
				case PlayerInputType.MoveForward:
					cameraDirection = cameraTransform.forward;
					break;
			}
        }

		private void UpdatePosition()
		{
			Vector3 force = GetMovementForce();

			rigidbody.AddRelativeForce(force, ForceMode.Force);
		}

		private Vector3 GetMovementForce()
		{
			Vector3 localCameraDirection = transform.worldToLocalMatrix.MultiplyVector(cameraDirection);

			localCameraDirection.y = 0f;

			Vector3 normalizedVector = Vector3.Normalize(localCameraDirection);

			// TODO: Add strength multiplier

			return normalizedVector;
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
