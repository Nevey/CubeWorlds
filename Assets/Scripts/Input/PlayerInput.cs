using System;
using UnityEngine;

namespace CCore.CubeWorlds.GameInput
{
	[RequireComponent(typeof(KeyboardInput))]
	public class PlayerInput : Monobehaviour
	{
		private KeyboardInput keyboardInput;

		private PlayerInputArgs playerInputArgs;

		public event EventHandler<PlayerInputArgs> PlayerInputEvent;

		private void Start()
		{
			keyboardInput = GetComponent<KeyboardInput>();

			keyboardInput.KeyboardInputEvent += OnKeyboardInput;

			playerInputArgs = new PlayerInputArgs();
		}

        private void OnKeyboardInput(object sender, KeyboardInputArgs e)
        {
            switch (e.keyCode)
			{
				case KeyCode.LeftArrow:
				
					DispatchPlayerInput(PlayerInputType.Left, e.inputState);

				break;

				case KeyCode.RightArrow:

					DispatchPlayerInput(PlayerInputType.Right, e.inputState);

				break;

				case KeyCode.UpArrow:

					DispatchPlayerInput(PlayerInputType.Up, e.inputState);

				break;

				case KeyCode.DownArrow:

					DispatchPlayerInput(PlayerInputType.Down, e.inputState);

				break;
			}
        }

		private void DispatchPlayerInput(PlayerInputType playerInputType, InputState inputState)
		{
			Log("KeyboardInput.DispatchPlayerInput -- PlayerInputType: " + playerInputType + " InputState: " + inputState);

			if (PlayerInputEvent != null)
			{
				playerInputArgs.playerInputType = playerInputType;

				playerInputArgs.inputState = inputState;

				PlayerInputEvent(this, playerInputArgs);
			}
		}
    }
}
