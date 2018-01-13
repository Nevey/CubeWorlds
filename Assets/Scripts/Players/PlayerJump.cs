using System;
using CCore.CubeWorlds.GameInput;
using UnityEngine;

namespace CCore.CubeWorlds.Players
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerJump : MonoBehaviour, IPlayerEnabler
    {
        [SerializeField] private float jumpStrength = 1f;

        private bool isJumping = false;

        private new Rigidbody rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnPlayerInput(object sender, PlayerInputArgs e)
        {
            if (e.inputState == InputState.Down)
            {
                if (e.playerInputType == PlayerInputType.Jump)
                {
                    Jump();
                }
            }
        }

        private void Jump()
        {
            float jumpInput = Input.GetAxis("Jump");

            float jumpForce = jumpInput * jumpStrength;

            rigidbody.AddRelativeForce(0f, jumpStrength, 0f, ForceMode.Impulse);

            Log("Jump Force: " + jumpStrength);

            isJumping = true;
        }

        public void OnPlayerDisabled()
        {
            PlayerInput.PlayerInputEvent -= OnPlayerInput;
        }

        public void OnPlayerEnabled()
        {
            PlayerInput.PlayerInputEvent += OnPlayerInput;
        }
    }
}