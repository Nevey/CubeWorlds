using System;
using UnityEngine;

namespace CCore.CubeWorlds.Cameras
{
	public class CameraSlot : MonoBehaviour
	{
		[SerializeField] private float fieldOfView;

		private new Camera camera;

		public Camera Camera { get { return camera; } }

		/// <summary>
		/// Adds the Camera's transform to this slot
		/// </summary>
		/// <param name="camera"></param>
		public void AddCamera(Camera camera)
		{
			this.camera = camera;
			
			camera.fieldOfView = fieldOfView;

			camera.transform.parent = transform;

			camera.transform.position = transform.position;
		}
	}
}
