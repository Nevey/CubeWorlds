using System;
using UnityEngine;

namespace CCore.CubeWorlds.GameInput
{
	/// <summary>
	/// This class handles input from both keyboard and mouse
	/// </summary>
	public class KeyboardInput : MonoBehaviour
	{
		private KeyboardInputArgs keyboardInputArgs;

		private Array keyCodes;

		public event EventHandler<KeyboardInputArgs> KeyboardInputEvent;

		private void Start()
		{
			keyboardInputArgs = new KeyboardInputArgs();

			keyCodes = Enum.GetValues(typeof(KeyCode));
		}

		private void Update()
		{
			WatchForInput();
		}

		private void WatchForInput()
		{
			for (int i = 0; i < keyCodes.Length; i++)
			{
				KeyCode keyCode = (KeyCode)keyCodes.GetValue(i);

				if (Input.GetKeyDown(keyCode))
				{
					DispatchKeyboardInput(keyCode, InputState.Down);
				}
				else if (Input.GetKey(keyCode))
				{
					DispatchKeyboardInput(keyCode, InputState.Hold);
				}
				else if (Input.GetKeyUp(keyCode))
				{
					DispatchKeyboardInput(keyCode, InputState.Up);
				}
			}
		}

		private void DispatchKeyboardInput(KeyCode keyCode, InputState inputState)
		{
			Log("KeyboardInput.DispatchKeyboardInput -- KeyCode: " + keyCode + " InputState: " + inputState);

			if (KeyboardInputEvent != null)
			{
				keyboardInputArgs.keyCode = keyCode;

				keyboardInputArgs.inputState = inputState;

				KeyboardInputEvent(this, keyboardInputArgs);
			}
		}
	}
}
