using UnityEngine;

namespace power.turbine
{
    public class RadialMeter : MonoBehaviour
    {
        public float t = 0.0f;
        [SerializeField]
        private float speed = 1.25f;
        [SerializeField]
        private Vector2 rotateBounds = new Vector2(0.0f, -180.0f);

        [SerializeField]
        private Transform arrow = null;
        private Vector3 lastDirection = Vector3.zero;

        private void FixedUpdate()
        {
            Rotate();
        }

        private void Rotate()
        {
            float z = Mathf.Lerp(rotateBounds.x, rotateBounds.y, t);
            Vector3 rot = arrow.transform.eulerAngles;

            float lerpZ = Mathf.Lerp(rot.z, z, Time.deltaTime * speed);
            arrow.transform.rotation = Quaternion.Euler(new Vector3(rot.x, rot.y, lerpZ));
        }

        private float GetTarget()
        {
            return Mathf.Lerp(rotateBounds.x, rotateBounds.y, t);
        }
    }
}