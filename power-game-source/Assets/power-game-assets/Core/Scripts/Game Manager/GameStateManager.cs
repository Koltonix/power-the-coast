using UnityEngine;
using UnityEngine.UI;
using power.turbine;

namespace power.manager
{
    public class GameStateManager : MonoBehaviour
    {
        public static float hourSpeed = 5.0f;
        private float elapsed = 0.0f;

        [SerializeField]
        private Image hourImage = null;

        private void FixedUpdate()
        {
            if (elapsed >= hourSpeed)
            {
                InvokePowerCollection();
                elapsed = 0.0f;
            }

            elapsed += Time.deltaTime;

            hourImage.fillAmount = elapsed / hourSpeed;
        }

        private void InvokePowerCollection()
        {
            Turbine[] turbines =  GameObject.FindObjectsOfType<Turbine>();
            foreach (Turbine turbine in turbines)
                turbine.CollectPower();
        }
    }
}