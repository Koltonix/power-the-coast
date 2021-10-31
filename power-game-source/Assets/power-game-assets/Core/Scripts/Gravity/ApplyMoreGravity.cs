using UnityEngine;

namespace power.utilities
{
    [RequireComponent(typeof(Rigidbody))]
    public class ApplyMoreGravity : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody rb = null;

        [SerializeField]
        private float force = 50.0f;

        private void Start()
        {
            rb = this.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rb.AddForce(Vector3.down * force, ForceMode.Acceleration);
        }
    }
}