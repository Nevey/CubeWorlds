using System;
using UnityEngine;

namespace CCore.CubeWorlds.Cameras
{
	public class CameraSlot : MonoBehaviour
	{
		[SerializeField] private float fieldOfView;

		public float FieldOfFiew { get { return fieldOfView; } }
	}
}
