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
            switch (e.keyCode)
			{
				case KeyCode.LeftArrow:
					DispatchPlayerInput(PlayerInputType.MoveLeft, e.inputState);
					break;

				case KeyCode.RightArrow:
					DispatchPlayerInput(PlayerInputType.MoveRight, e.inputState);
					break;

				case KeyCode.UpArrow:
					DispatchPlayerInput(PlayerInputType.MoveForward, e.inputState);
					break;

				case KeyCode.DownArrow:
					DispatchPlayerInput(PlayerInputType.MoveBackward, e.inputState);
					break;
				
				case KeyCode.T:
					DispatchPlayerInput(PlayerInputType.DebugSpawn, e.inputState);
					break;
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
