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

		private Vector3 relativeCameraDirection;

		private void Start()
		{
			player = GetComponent<Player>();

			rigidbody = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			UpdatePosition();
		}

        private void OnPlayerInput(object sender, PlayerInputArgs e)
        {
			if (e.inputState != InputState.Hold)
			{
				relativeCameraDirection = Vector3.zero;

				return;
			}

			Transform cameraTransform = player.CameraSlot.Camera.transform;

			if (e.playerInputType == PlayerInputType.Movement)
			{
				float horizontalInput = Input.GetAxis("Horizontal");

				float verticalInput = Input.GetAxis("Vertical");

				Vector3 horizontalDirection = cameraTransform.right * horizontalInput;

				Vector3 verticalDirection = cameraTransform.forward * verticalInput;

				relativeCameraDirection = horizontalDirection + verticalDirection;
			}
        }

		private void UpdatePosition()
		{
			Vector3 targetLocalVelocity = GetMovementVelocity();

			Vector3 currentLocalVelocity = transform.worldToLocalMatrix.MultiplyVector(rigidbody.velocity);

			// Keep jump/gravity intact
			targetLocalVelocity.y = currentLocalVelocity.y;

			rigidbody.velocity = transform.localToWorldMatrix.MultiplyVector(targetLocalVelocity);
		}

		private Vector3 GetMovementVelocity()
		{
			Vector3 localCameraDirection = transform.worldToLocalMatrix.MultiplyVector(relativeCameraDirection);

			localCameraDirection.y = 0f;

			Vector3 normalizedVector = Vector3.Normalize(localCameraDirection);

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
