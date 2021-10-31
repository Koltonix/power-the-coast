using System;
using UnityEngine;
using TMPro;

namespace power.utilities
{
    public class MenuHighscore : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text highscoreText = null;


        

        private void Start()
        {
            UpdateText();
        }

        public void ResetScore()
        {
            PlayerPrefs.SetFloat("HIGHSCORE", 0.0f);
            PlayerPrefs.Save();

            UpdateText();
        }

        private void UpdateText()
        {
            highscoreText.text = Math.Round(PlayerPrefs.GetFloat("HIGHSCORE"), 2) + "MW";
        }
    }
}
