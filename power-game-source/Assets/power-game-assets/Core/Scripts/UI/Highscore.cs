using UnityEngine;
using TMPro;
using power.data;

namespace power.utilities
{
    public class Highscore : MonoBehaviour
    {
        [SerializeField]
        private PowerData data = null;

        [SerializeField]
        private GameObject newHighScoreUI = null;

        [SerializeField]
        private TMP_Text highscoreText = null;
        [SerializeField]
        private TMP_Text scoreText = null;

        public void GameEnd()
        {
            float score = data.powerCollected;
            float highscore = PlayerPrefs.GetFloat("HIGHSCORE");
            if (score >= highscore)
            {
                PlayerPrefs.SetFloat("HIGHSCORE", score);
                PlayerPrefs.Save();

                newHighScoreUI.SetActive(true);
            }
        }
    }
}