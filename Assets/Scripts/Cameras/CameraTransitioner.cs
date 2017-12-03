using System;
using UnityEngine;

namespace CCore.CubeWorlds.Cameras
{
	/// <summary>
	/// Transitions the camera between camera slots
	/// </summary>
	[RequireComponent(typeof(Camera))]
	public class CameraTransitioner : MonoBehaviour
	{
		private new Camera camera;

		private CameraSlot cameraSlot;

		public static CameraTransitioner Instance;

		private void Awake()
		{
			Instance = this;

			camera = GetComponent<Camera>();
		}

		private void Update()
		{
			FollowCameraSlot();
		}

		private void FollowCameraSlot()
		{
			if (!cameraSlot)
			{
				return;
			}

			transform.position = cameraSlot.transform.position;

			transform.rotation = cameraSlot.transform.rotation;
		}

		public void DoTransitionInstant(CameraSlot cameraSlot)
		{
			camera.fieldOfView = cameraSlot.FieldOfFiew;

			transform.position = cameraSlot.transform.position;

			transform.rotation = cameraSlot.transform.rotation;

			this.cameraSlot = cameraSlot;
		}
	}
}
