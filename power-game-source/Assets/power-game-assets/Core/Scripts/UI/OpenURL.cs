using UnityEngine;

namespace power.utilities
{
    [CreateAssetMenu(fileName = "URLHandler", menuName = "ScriptableObjects/Utilities/URLHandler")]
    public class OpenURL : ScriptableObject
    {
        public static void Open(string url) => Application.OpenURL(url);
    }
}