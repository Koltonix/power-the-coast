using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace power.utilities
{
    // Still miss him: https://www.youtube.com/watch?v=YMj2qPq9CP8
    public class AsynchronousSceneLoading : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text loadingText = null;
        [SerializeField]
        private Slider slider = null;

        [SerializeField]
        private float fakeWait = 2.0f;

        private Coroutine loading = null;

        private void Start()
        {
            int index = PlayerPrefs.GetInt("SCENE");
            loading = StartCoroutine(LoadAsync(index));
        }

        private IEnumerator LoadAsync(int index)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(index);
            operation.allowSceneActivation = false; 

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                loadingText.text = (progress * 100) + "% Loaded";
                slider.value = progress;

                if (progress >= 1.0f)
                {
                    // Love me some fake waiting.
                    yield return new WaitForSeconds(fakeWait);
                    
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}