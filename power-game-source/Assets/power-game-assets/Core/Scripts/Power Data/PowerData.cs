using UnityEngine;

namespace power.data
{
    [CreateAssetMenu(fileName = "Power Data", menuName = "ScriptableObjects/Data/PowerData")]
    public class PowerData : ScriptableObject
    {
        public float powerCollected = 0.0f;

        public void Reset()
        {
            powerCollected = 0.0f;
        }
    }
}