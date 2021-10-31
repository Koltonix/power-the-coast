using System.Collections.Generic;
using UnityEngine;

namespace power.utilities
{
    // Source: https://www.youtube.com/watch?v=aLpixrPvlB8
    // Forever in our hearts <3
    [RequireComponent(typeof(Camera))]
    public class TargetBoundCamera : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> targets = null;

        [SerializeField]
        private float smoothTime = 0.5f;
        [SerializeField]
        private float zoomSpeed = 1.25f;

        [SerializeField]
        private Vector2 zoom = new Vector2(15, 50);
        [SerializeField]
        private float zoomLimiter = 50.0f;

        [SerializeField]
        private Vector3 offset = Vector3.zero;

        private Camera mainCamera = null;

        private void Start()
        {
            offset = this.transform.position;

            mainCamera = this.GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if (targets.Count == 0)
                return;

            Vector3 centre = GetCentreInTargets();
            Vector3 v = Vector3.zero;
            transform.position = Vector3.SmoothDamp(this.transform.position, centre + offset, ref v, smoothTime);

            Zoom();
        }

        private void Zoom()
        {
            float newZoom = Mathf.Lerp(zoom.x, zoom.y, GetGreatestDistance() / zoomLimiter);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newZoom, zoomSpeed * Time.deltaTime);
        }

        float GetGreatestDistance()
        {
            var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
            for(int i = 0; i < targets.Count; i++)
                bounds.Encapsulate(targets[i].transform.position);

            return bounds.size.x;
        }

        private Vector3 GetCentreInTargets()
        {
            if (targets.Count == 1)
                return targets[0].transform.position;

            var bounds = new Bounds(targets[0].transform.position, Vector3.zero);

            for(int i = 0; i < targets.Count; i++)
                bounds.Encapsulate(targets[i].transform.position);

            return bounds.center;
        }
    }
}