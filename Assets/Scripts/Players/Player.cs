using System;
using CCore.CubeWorlds.Cameras;
using UnityEngine;

namespace CCore.CubeWorlds.Players
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private new Renderer renderer;

		[SerializeField] private CameraSlot cameraSlot;

		private IPlayerEnabler[] playerEnablers;

		public Renderer Renderer { get { return renderer; } }

		public CameraSlot CameraSlot { get { return cameraSlot; } }

		public void Activate()
		{
			CameraController.Instance.SetCameraSlot(cameraSlot, transform);

			playerEnablers = GetComponentsInChildren<IPlayerEnabler>();

			for (int i = 0; i < playerEnablers.Length; i++)
			{
				playerEnablers[i].OnPlayerEnabled();
			}
		}

		public void Deactivate()
		{
			for (int i = 0; i < playerEnablers.Length; i++)
			{
				playerEnablers[i].OnPlayerDisabled();
			}
		}
	}
}
