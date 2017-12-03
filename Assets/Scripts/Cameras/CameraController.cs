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

		private CameraSlot cameraSlot;

		public static CameraController Instance;

		private void Awake()
		{
			Instance = this;

			camera = GetComponent<Camera>();

			cameraLookAt = GetComponent<CameraLookAt>();
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

			// Vector3 test = transform.eulerAngles;

			// test.z = cameraSlot.transform.eulerAngles.z;

			// transform.eulerAngles = test;
		}

		public void SetCameraSlot(CameraSlot cameraSlot, Transform lookAtTarget)
		{
			this.cameraSlot = cameraSlot;

			camera.fieldOfView = cameraSlot.FieldOfFiew;

			transform.parent = cameraSlot.transform;

			transform.position = cameraSlot.transform.position;

			// transform.localRotation = cameraSlot.transform.localRotation;

			cameraLookAt.SetTarget(lookAtTarget);
		}
	}
}
