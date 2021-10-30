using UnityEngine;

namespace power.turbine
{
    public class RotateTurbine : MonoBehaviour
    {
        [SerializeField]
        private Vector2 speed = new Vector2(0, 10.0f);
        private float currentSpeed = 0.0f;
        private float tStart = 0.5f;

        [SerializeField]
        private Transform objToRotate = null;

        [SerializeField]
        private bool canRotate = true;
        [SerializeField]
        private bool invertDirection = true;

        private void Start()
        {
            SetSpeed(tStart);
        }

        private void FixedUpdate()
        {
            if (canRotate)
                Rotate();
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

        public float GetEfficiency()
        {
            return currentSpeed / (speed.x + speed.y);
        }
    }
}
