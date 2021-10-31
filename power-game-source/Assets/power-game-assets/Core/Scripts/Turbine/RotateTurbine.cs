using UnityEngine;

namespace power.turbine
{
    public class RotateTurbine : MonoBehaviour
    {
        [SerializeField]
        private Vector2 speed = new Vector2(0, 10.0f);
        private float currentSpeed = 0.0f;
        [SerializeField]
        private Vector2 increaseSpeedRange = new Vector2(0.01f, 0.05f);
        [SerializeField]
        private float increaseSpeed = 0.01f;

        [SerializeField]
        private Vector2 speedChangeTimeRange = new Vector2(2.0f, 10.0f);
        [SerializeField]
        private float timeToChangeSpeed = 2.5f;

        [Range(0, 1)]
        public float t = 0.5f;

        [SerializeField]
        private Transform objToRotate = null;

        [SerializeField]
        private bool graduallyIncreaseSpeed = true;
        [SerializeField]
        private bool canRotate = true;
        [SerializeField]
        private bool invertDirection = true;

        private void Start()
        {
            Randomise();
        }

        private void FixedUpdate()
        {
            timeToChangeSpeed -= Time.deltaTime;
            if (timeToChangeSpeed <= 0)
                Randomise();

            if (graduallyIncreaseSpeed)
                t += increaseSpeed * Time.deltaTime;

            t = Mathf.Clamp01(t);

            SetSpeed(t);

            if (canRotate)
                Rotate();
        }

        private void Randomise()
        {
            increaseSpeed = GetRandomIncreaseSpeed();
            timeToChangeSpeed = GetRandomTime();
        }

        private void Rotate()
        {
            int dir = invertDirection ? -1 : 1;

            Vector3 rot = objToRotate.rotation.eulerAngles;
            objToRotate.transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z + (currentSpeed * Time.deltaTime) * dir);
        }

        private void SetSpeed(float t)
        {
            currentSpeed = Mathf.Lerp(speed.x, speed.y, t);
        }

        private float GetRandomIncreaseSpeed()
        {
            return Random.Range(increaseSpeedRange.x, increaseSpeedRange.y);
        }

        private float GetRandomTime()
        {
            return Random.Range(speedChangeTimeRange.x, speedChangeTimeRange.y);
        }
    }
}
