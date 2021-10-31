using UnityEngine;
using UnityEngine.InputSystem;
using power.turbine;

namespace power.controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera = null;

        [SerializeField]
        [Range(0.0f, 5000.0f)]
        private float rayDistance = 1000.0f;

        [SerializeField]
        private LayerMask hitMask = new LayerMask();

        [SerializeField]
        [Range(0, 1)]
        private float speedChange = 0.1f;

        private void Start()
        {
            if (mainCamera)
                mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
                ChangeTurbineSpeed(1);

            if (Input.GetKey(KeyCode.Mouse1))
                ChangeTurbineSpeed(-1);
        }

        private void ChangeTurbineSpeed(int dir)
        {
            RaycastHit hit = GetHitFromMouse();
            if (!hit.collider)
                return;

            RotateTurbine turbineSpeed = hit.collider.GetComponent<RotateTurbine>();

            if (turbineSpeed)
                turbineSpeed.t += speedChange * dir;
        }

        private RaycastHit GetHitFromMouse()
        {
            
            Vector3 screenPos = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(screenPos);

            RaycastHit hit;
            Physics.Raycast(ray, out hit, rayDistance, hitMask);
            
            return hit;
        }
    }
}