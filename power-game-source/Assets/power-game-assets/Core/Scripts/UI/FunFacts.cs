using UnityEngine;
using TMPro;

namespace power.utilities
{
    [RequireComponent(typeof(TMP_Text))]
    public class FunFacts : MonoBehaviour
    {
        [SerializeField]
        [TextArea(0, 5)]
        private string[] funFacts = null;
        
        private TMP_Text text = null;

        private void Start()
        {
            text = this.GetComponent<TMP_Text>();
            text.text = GetRandomFact();
        }

        private string GetRandomFact()
        {
            return funFacts[Random.Range(0, funFacts.Length)];
        }
    }
}