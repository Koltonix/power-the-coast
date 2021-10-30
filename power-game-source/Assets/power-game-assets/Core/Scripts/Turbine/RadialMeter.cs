using UnityEngine;

namespace power.turbine
{
    public class RadialMeter : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1.25f;
        [SerializeField]
        private Vector2 directionBounds = new Vector2(-1.0f, 1.0f);

        private float t = 0.0f;
        private Vector3 lastDirection = Vector3.zero;

        [SerializeField]
        private Transform arrow = null;
        
        private void Start()
        {
            SetTarget(1.0f);
        }

        private void FixedUpdate()
        {
            Rotate();
        }

        private void Rotate()
        {
            arrow.transform.up = Vector3.Slerp(arrow.transform.up, new Vector3(GetTarget(), lastDirection.y, lastDirection.z), Time.deltaTime * speed);
        }

        private float GetTarget()
        {
            return Mathf.Lerp(directionBounds.x, directionBounds.y, t);
        }

        private void SetTarget(float t)
        {
            this.t = t;
            lastDirection = arrow.transform.up;
        }
    }
}