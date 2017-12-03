using System;
using CCore.CubeWorlds.Cameras;
using UnityEngine;

namespace CCore.CubeWorlds.Players
{
	public class Player : MonoBehaviour
	{
		[SerializeField] private new Renderer renderer;

		[SerializeField] private CameraSlot cameraSlot;

		public Renderer Renderer { get { return renderer; } }

		public void Activate()
		{
			// Vector3 rotation = transform.eulerAngles;

			// transform.eulerAngles = Vector3.zero;

			CameraController.Instance.SetCameraSlot(cameraSlot, transform);

			// transform.eulerAngles = rotation;
		}
	}
}
