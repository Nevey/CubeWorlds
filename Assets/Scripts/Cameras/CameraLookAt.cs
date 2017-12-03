using System;
using UnityEngine;

namespace CCore.CubeWorlds.Cameras
{
	public class CameraLookAt : MonoBehaviour
	{
		private Transform target;

		private void Update()
		{
			if (target == null)
			{
				return;
			}

			LookAt();
		}

		private void LookAt()
		{
			transform.LookAt(target, target.up);
		}

		public void SetTarget(Transform target)
		{
			this.target = target;
		}
	}
}
