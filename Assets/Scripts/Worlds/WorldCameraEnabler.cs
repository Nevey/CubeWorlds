using System;
using CCore.CubeWorlds.Cameras;
using UnityEngine;

namespace CCore.CubeWorlds.Worlds
{
    public class WorldCameraEnabler : MonoBehaviour, IWorldEnabler
    {
		[SerializeField] private CameraSlot cameraSlot;

        public void OnWorldEnable()
        {
            CameraController.Instance.SetCameraSlot(cameraSlot, transform);
        }

        public void OnWorldDisable()
        {
            
        }
    }
}
