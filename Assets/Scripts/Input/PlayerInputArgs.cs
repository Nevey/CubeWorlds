using System;

namespace CCore.CubeWorlds.GameInput
{
    public class PlayerInputArgs : EventArgs
    {
        public PlayerInputType playerInputType { get; set; }

        /// <summary>
        /// Enum to define whether player input was "Down", "Hold" or "Up"
        /// </summary>
        /// <returns>InputState</returns>
        public InputState inputState { get; set; }
    }
}