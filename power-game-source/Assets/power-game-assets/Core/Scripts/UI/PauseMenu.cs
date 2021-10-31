using UnityEngine;

namespace power.utilities
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject pauseMenu = null;

        private bool isPaused = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                Pause(!isPaused);
        }

        public void Pause(bool isPaused)
        {
            this.isPaused = isPaused;

            Time.timeScale = isPaused ? 0.0f : 1.0f;
            pauseMenu.SetActive(isPaused);
        }

        private void OnDestroy()
        {
            Pause(false);
        }
    }
}