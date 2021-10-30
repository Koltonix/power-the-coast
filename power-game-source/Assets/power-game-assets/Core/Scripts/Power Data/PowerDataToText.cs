using System;
using UnityEngine;
using TMPro;
using power.data;

namespace power.utilities
{   
    [RequireComponent(typeof(TMP_Text))]
    public class PowerDataToText : MonoBehaviour
    {
        [SerializeField]
        private PowerData data = null;
        private TMP_Text text = null;

        [SerializeField]
        private string preconcatenated = "Power:";
        [SerializeField]
        private string unit = "MW";

        private void Start()
        {
            text = this.GetComponent<TMP_Text>();
        }

        private void FixedUpdate()
        {
            if (data && text)
                text.text = preconcatenated + "\n" + Math.Round(data.powerCollected, 2) + unit;
        }
    }
}