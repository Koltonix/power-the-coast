using UnityEngine;
using UnityEngine.InputSystem;

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

        private void Start()
        {
            if (mainCamera)
                mainCamera = Camera.main;
        }

        private void Update()
        {
                
        }
        

        private RaycastHit GetHitFromMouse()
        {
            
            Vector3 screenPos = Mouse.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(screenPos);

            RaycastHit hit;
            Physics.Raycast(ray, out hit, rayDistance, hitMask);
            
            return hit;
        }
    }
}