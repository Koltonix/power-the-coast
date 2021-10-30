using UnityEngine;

namespace power.utilities
{
    public class RotateRandomly : MonoBehaviour
    {
        [SerializeField]
        private Vector2 speedRange = new Vector2(0.5f, 2.5f);
        private float currentSpeed = 1.25f;

        private Vector3 direction = Vector3.zero;
        [SerializeField]
        private Vector2 directionRange = new Vector2(-1.0f, 1.0f);

        private void Start() => SetRandomRotation();

        private void FixedUpdate()
        {
            this.transform.forward = Vector3.Slerp(this.transform.forward, direction, currentSpeed * Time.deltaTime);

            if (Vector3.Dot(this.transform.forward.normalized, direction) >= 0.9f)
                SetRandomRotation();
        }

        public void SetRandomRotation()
        {
            currentSpeed = GetRandomSpeed();
            direction = GetRandomDirection();
        }

        private float GetRandomSpeed()
        {
            return Random.Range(speedRange.x, speedRange.y);
        }

        private Vector3 GetRandomDirection()
        {
            return new Vector3(Random.Range(directionRange.x, directionRange.y), 0, 0).normalized;
        }
    }
}