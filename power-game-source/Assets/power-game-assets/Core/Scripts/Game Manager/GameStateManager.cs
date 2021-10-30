using UnityEngine;
using power.turbine;

namespace power.manager
{
    public class GameStateManager : MonoBehaviour
    {
        public static float hourSpeed = 5.0f;
        private float elapsed = 0.0f;

        private void FixedUpdate()
        {
            if (elapsed >= hourSpeed)
            {
                InvokePowerCollection();
                elapsed = 0.0f;
            }

            elapsed += Time.deltaTime;
        }

        private void InvokePowerCollection()
        {
            Turbine[] turbines =  GameObject.FindObjectsOfType<Turbine>();
            foreach (Turbine turbine in turbines)
                turbine.CollectPower();
        }
    }
}