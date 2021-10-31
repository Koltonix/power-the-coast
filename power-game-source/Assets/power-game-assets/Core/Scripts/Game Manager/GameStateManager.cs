using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using power.data;
using power.turbine;
using power.utilities;

namespace power.manager
{
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField]
        private PowerData data = null;

        [SerializeField]
        private bool canCollect = true;

        public static float hourSpeed = 5.0f;
        private float elapsed = 0.0f;

        [SerializeField]
        private Image hourImage = null;

        [SerializeField]
        private UnityEvent onEnd = null;

        private void Start()
        {
            data.Reset();
            PowerDataToText.Instance.UpdateValue(0);
        }

        private void FixedUpdate()
        {
            if (elapsed >= hourSpeed)
            {
                InvokePowerCollection();
                elapsed = 0.0f;
            }

            if (canCollect)
                elapsed += Time.deltaTime;

            hourImage.fillAmount = elapsed / hourSpeed;
        }

        private void InvokePowerCollection()
        {
            float originalPower = data.powerCollected;
            Turbine[] turbines =  GameObject.FindObjectsOfType<Turbine>();
            foreach (Turbine turbine in turbines)
                turbine.CollectPower();

            PowerDataToText.Instance.UpdateValue(originalPower);
        }
        
        public void EndGame()
        {
            canCollect = false;
            onEnd?.Invoke();
        }
    }
}