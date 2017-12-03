using System;
using UnityEngine;

namespace CCore
{
	// NOTE: CURRENTLY UNUSED
	[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/Player", order = 2)]
	public class PlayerConfig : ScriptableObject
	{
		[SerializeField] private GameObject playerMesh;

		public GameObject PlayerMesh { get { return playerMesh; } }
	}
}
