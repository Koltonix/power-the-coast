using System;
using UnityEngine;
using TMPro;
using power.data;

namespace power.utilities
{
    // Source: https://www.cse.org.uk/advice/advice-and-support/how-much-electricity-am-i-using
    [Serializable]
    public struct CommonAppliance
    {
        public string name;
        public int powerRating;
    }

    public class Highscore : MonoBehaviour
    {
        [SerializeField]
        private PowerData data = null;

        [SerializeField]
        private CommonAppliance[] appliances = null;

        [SerializeField]
        private GameObject newHighScoreUI = null;

        [SerializeField]
        private GameObject endScreen = null;
        [SerializeField]
        private TMP_Text highscoreText = null;
        [SerializeField]
        private TMP_Text scoreText = null;
        
        public bool scoresCalculated = false;

        public void GameEnd()
        {
            scoresCalculated = true;
            endScreen.SetActive(true);

            float score = data.powerCollected;
            float highscore = PlayerPrefs.GetFloat("HIGHSCORE");

            if (score >= highscore)
            {
                PlayerPrefs.SetFloat("HIGHSCORE", score);
                PlayerPrefs.Save();

                newHighScoreUI.SetActive(true);
                highscore = score;
            }

            CommonAppliance appliance = appliances[UnityEngine.Random.Range(0, appliances.Length)];

            scoreText.text = Math.Round(MWToAppliance(score, appliance.powerRating), 2) +  " " + appliance.name + "s for an hour"; 
            highscoreText.text = Math.Round(highscore, 2) + "MW";
        }

        // 800-1500 Watts for a Toaster = https://www.cse.org.uk/advice/advice-and-support/how-much-electricity-am-i-using
        private float MWToAppliance(float mw, int rating)
        {
            float kw = mw * 1000;
            float w = kw * 1000;

            return w / rating;
        }
    }
}