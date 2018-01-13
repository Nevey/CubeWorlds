using UnityEngine;

// TODO: Move to generic namespace and folder
namespace CCore.CubeWorlds.Players
{
    [RequireComponent(typeof(Rigidbody))]
    public class ObjectGravity : MonoBehaviour
    {
        [SerializeField] private float gravityStrength = 1f;

        private new Rigidbody rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            rigidbody.AddRelativeForce(0f, -gravityStrength, 0f, ForceMode.Force);
        }

        public void OverrideGravityStrength(float strength)
        {
            gravityStrength = strength;
        }
    }
}