using UnityEngine;
using power.data;
using power.manager;

namespace power.turbine
{
    // Units will be in MW (Megawatt) and MWh (Megawatt Hour)
    // Each Rampion Turbine produces 3.45MW (presumably at max)
    // Source: https://www.rampionoffshore.com/about/questions-and-answers/another-qa-category/
    [RequireComponent(typeof(RotateTurbine))]
    public class Turbine : MonoBehaviour
    {
        [SerializeField]
        private PowerData data = null;

        // If memory serves turbines are non-efficient for 33% of the time. 
        // Or efficient only 30% of the time. One of them.
        // Doesn't make for fun gameplay though
        // Going to use some artistic licensing here...
        private float maxMegaWatt = 3.45f;
        private float minMegaWatt = 0.0f;

        private float heldPower = 0.0f;

        [SerializeField]
        private RadialMeter meter = null;
        private RotateTurbine turbineRotate = null;

        private void Start()
        {
            turbineRotate = this.GetComponent<RotateTurbine>();
        }

        private void FixedUpdate()
        {
            IncreasePower();             
            meter.SetTarget(turbineRotate.t);
        }

        private void IncreasePower()
        {
            // Increases the amount of power by the current wattage per in game hour.
            heldPower += (Mathf.Lerp(minMegaWatt, maxMegaWatt, turbineRotate.t) * Time.deltaTime) / GameStateManager.hourSpeed;
        }

        public void CollectPower()
        {
            data.powerCollected += heldPower;
            heldPower = 0.0f;
        }
    }
}