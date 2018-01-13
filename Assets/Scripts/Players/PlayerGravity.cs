using UnityEngine;

namespace CCore.CubeWorlds.Players
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerGravity : MonoBehaviour
    {
        private float gravity = 0.5f;

        private new Rigidbody rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            rigidbody.AddRelativeForce(0f, -gravity, 0f, ForceMode.Acceleration);
        }

        public void SetGravityValue(float value)
        {
            gravity = value;
        }
    }
}