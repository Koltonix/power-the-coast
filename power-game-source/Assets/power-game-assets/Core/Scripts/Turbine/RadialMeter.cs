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
        [SerializeField]
        private Renderer arrowRenderer = null;

        [SerializeField]
        private Gradient colourScheme = new Gradient();

        
        private void Start()
        {
            SetTarget(1.0f);
        }

        private void FixedUpdate()
        {
            Rotate();
            SetColour();
        }

        private void Rotate()
        {
            arrow.transform.up = Vector3.Lerp(arrow.transform.up, new Vector3(GetTarget(), lastDirection.y, lastDirection.z), Time.deltaTime * speed);
        }

        private float GetTarget()
        {
            return Mathf.Lerp(directionBounds.x, directionBounds.y, t);
        }

        public void SetTarget(float t)
        {
            this.t = t;
            lastDirection = arrow.transform.up;
        }

        private void SetColour()
        {
            if (!arrowRenderer)
                return;

                // My head hurts too much to do the actual inverse match. Oh well.
                float t = (arrow.transform.up.x + 1) / 2;

                Color32 colour = colourScheme.Evaluate(t);
                arrowRenderer.material.SetColor("_BaseColor", (Color)colour);
        }
    }
}