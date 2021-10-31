using UnityEngine;
using UnityEngine.Events;

namespace power.utilities
{
    public class OnInvoke : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onInvoke = null;

        public void CustomInvoke() => onInvoke?.Invoke();
    }
}