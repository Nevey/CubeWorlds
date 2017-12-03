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
            CameraTransitioner.Instance.DoTransitionInstant(cameraSlot);
        }

        public void OnWorldDisable()
        {
            
        }
    }
}
