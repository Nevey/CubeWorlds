using System;
using UnityEngine;

namespace CCore.CubeWorlds.GameInput
{
	[RequireComponent(typeof(KeyboardInput))]
	public class PlayerInput : MonoBehaviour
	{
		private KeyboardInput keyboardInput;

		private PlayerInputArgs playerInputArgs;

		public static event EventHandler<PlayerInputArgs> PlayerInputEvent;

		private void Start()
		{
			keyboardInput = GetComponent<KeyboardInput>();

			keyboardInput.KeyboardInputEvent += OnKeyboardInput;

			playerInputArgs = new PlayerInputArgs();
		}

        private void OnKeyboardInput(object sender, KeyboardInputArgs e)
        {				
			if (e.keyCode == KeyCode.LeftArrow
				|| e.keyCode == KeyCode.RightArrow
				|| e.keyCode == KeyCode.UpArrow
				|| e.keyCode == KeyCode.DownArrow)
			{
				DispatchPlayerInput(PlayerInputType.Movement, e.inputState);
			}

			if (e.keyCode == KeyCode.Space)
			{
				DispatchPlayerInput(PlayerInputType.Jump, e.inputState);
			}

			if (e.keyCode == KeyCode.T)
			{
				DispatchPlayerInput(PlayerInputType.SpawnCheat, e.inputState);
			}
        }

		private void DispatchPlayerInput(PlayerInputType playerInputType, InputState inputState)
		{
			Log("KeyboardInput.DispatchPlayerInput -- " + playerInputType + " : " + inputState);

			if (PlayerInputEvent != null)
			{
				playerInputArgs.playerInputType = playerInputType;

				playerInputArgs.inputState = inputState;

				PlayerInputEvent(this, playerInputArgs);
			}
		}
    }
}
