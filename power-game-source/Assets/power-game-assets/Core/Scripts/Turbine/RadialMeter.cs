using UnityEngine;

namespace power.turbine
{
    public class RadialMeter : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1.25f;
        [SerializeField]
        private Vector2 rotationBounds = new Vector2(90.0f, -90.0f);

        public float t = 0.0f;
        [SerializeField]
        private Transform arrow = null;
        [SerializeField]
        private Renderer arrowRenderer = null;

        [SerializeField]
        private Gradient colourScheme = new Gradient();

        private void FixedUpdate()
        {
            Rotate();
            SetColour();
        }

        // This whole method is stupid and I hate it.
        // But it works.
        private void Rotate()
        {
            Vector3 velocity = Vector3.zero;

            Vector3 startRot = arrow.transform.eulerAngles;
            startRot.x = 0; startRot.y = 0;
            Vector3 targetRot = new Vector3(0, 0, GetTarget());

            arrow.transform.rotation = Quaternion.Lerp(Quaternion.Euler(startRot), Quaternion.Euler(targetRot), speed * Time.deltaTime);
        }

        private float GetTarget()
        {
            return Mathf.Lerp(rotationBounds.x, rotationBounds.y, t);
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