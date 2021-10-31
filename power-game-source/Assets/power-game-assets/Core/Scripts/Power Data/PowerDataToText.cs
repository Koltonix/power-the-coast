using System;
using System.Collections;
using UnityEngine;
using TMPro;
using power.data;

namespace power.utilities
{   
    [RequireComponent(typeof(TMP_Text))]
    public class PowerDataToText : MonoBehaviour
    {
        public static PowerDataToText Instance = null;

        [SerializeField]
        private PowerData data = null;
        private TMP_Text text = null;

        [SerializeField]
        private string preconcatenated = "Power:";
        [SerializeField]
        private string unit = "MW";

        [SerializeField]
        private float delay = 0.1f;
        private Coroutine coroutine;

        // I know Singletons are bad. But I don't have time to implement an event system.
        private void Awake()
        {
            if (Instance)
                Destroy(this);

            else
                Instance = this;
        }

        private void Start()
        {
            text = this.GetComponent<TMP_Text>();
        }

        public void UpdateValue(float originalPower)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(LerpValue(originalPower, data.powerCollected));
        }

        private IEnumerator LerpValue(float original, float target)
        {
            for (float i = original; i <= target; i++)
            {
                text.text = preconcatenated + "\n" + Math.Round(i, 2) + unit;
                yield return new WaitForSeconds(0.1f);
            }

            text.text = preconcatenated + "\n" + Math.Round(target, 2) + unit;
            yield return null;
        }
    }
}