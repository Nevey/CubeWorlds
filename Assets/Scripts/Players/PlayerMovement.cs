using System;
using CCore.CubeWorlds.Cameras;
using CCore.CubeWorlds.GameInput;
using UnityEngine;

namespace CCore.CubeWorlds.Players
{
    [RequireComponent(typeof(Player))]
	[RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IPlayerEnabler
    {
		[SerializeField] private float movementStrength = 1f;

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

			if (e.playerInputType == PlayerInputType.Movement)
			{
				float horizontalInput = Input.GetAxis("Horizontal");

				float verticalInput = Input.GetAxis("Vertical");

				Vector3 horizontalDirection = cameraTransform.right * horizontalInput;

				Vector3 verticalDirection = cameraTransform.forward * verticalInput;

				cameraDirection = horizontalDirection + verticalDirection;
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

			// Vector3 normalizedVector = Vector3.Normalize(cameraDirection);

			return normalizedVector *= movementStrength;
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
