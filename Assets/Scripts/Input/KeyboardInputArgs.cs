using System;
using UnityEngine;

namespace CCore.CubeWorlds.GameInput
{
    public class KeyboardInputArgs : EventArgs
    {
        /// <summary>
        /// KeyCode can either be a keyboard key or a mouse key 
        /// </summary>
        /// <returns>KeyCode</returns>
        public KeyCode keyCode { get; set; }

        /// <summary>
        /// Enum to define whether keyboard input was "Down", "Hold" or "Up"
        /// </summary>
        /// <returns>InputState</returns>
        public InputState inputState { get; set; }
    }
}
