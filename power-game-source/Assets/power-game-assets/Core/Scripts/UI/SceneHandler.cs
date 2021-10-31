using UnityEngine;
using UnityEngine.SceneManagement;

namespace power.utilities
{
    [CreateAssetMenu(fileName = "SceneHandler", menuName = "ScriptableObjects/Utilities/SceneHandler")]
    public class SceneHandler : ScriptableObject
    {
        public int transitionScene = 2;

        public void ChangeScene(int scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void TransitionToScene(int scene)
        {
            PlayerPrefs.SetInt("SCENE", scene);
            PlayerPrefs.Save();

            ChangeScene(transitionScene);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void RestartTransition()
        {
            TransitionToScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}