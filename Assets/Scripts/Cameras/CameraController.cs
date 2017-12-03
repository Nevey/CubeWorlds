using System;
using UnityEngine;

namespace CCore.CubeWorlds.Cameras
{
	/// <summary>
	/// Transitions the camera between camera slots
	/// </summary>
	[RequireComponent(typeof(Camera))]
	[RequireComponent(typeof(CameraLookAt))]
	public class CameraController : MonoBehaviour
	{
		private new Camera camera;

		private CameraLookAt cameraLookAt;

		public static CameraController Instance;

		private void Awake()
		{
			Instance = this;

			camera = GetComponent<Camera>();

			cameraLookAt = GetComponent<CameraLookAt>();
		}

		public void SetCameraSlot(CameraSlot cameraSlot, Transform lookAtTarget)
		{
			cameraSlot.AddCamera(camera);

			cameraLookAt.SetTarget(lookAtTarget);
		}
	}
}
