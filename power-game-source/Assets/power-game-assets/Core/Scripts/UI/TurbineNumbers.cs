using System;
using System.Collections;
using UnityEngine;
using TMPro;
using power.data;
using power.manager;
using power.turbine;

namespace power.utilities
{
    public class TurbineNumbers : MonoBehaviour
    {
        public static TurbineNumbers Instance = null;

        [SerializeField]
        private PowerData data = null;

        [SerializeField]
        private Camera mainCamera = null;
        [SerializeField]
        private Transform numberParent = null;
        [SerializeField]
        private GameObject uiPower = null;
        [SerializeField]
        private Animator uiPowerAnim = null;

        [SerializeField]
        private GameObject textPrefab = null;
        [SerializeField]
        private float moveSpeed = 1.25f;
        [SerializeField]
        private float initialDelay = 0.5f;

        private void Awake()
        {
            if (!Instance)
                Instance = this;

            else
                Destroy(this);
        }

        public void CreateText(Vector3 pos, float value, Turbine turbine)
        {
            StartCoroutine(SpawnText(mainCamera.WorldToScreenPoint(pos), value, turbine));
        }

        private IEnumerator SpawnText(Vector3 screenPos, float value, Turbine turbine)
        {
            TMP_Text text = Instantiate(textPrefab, screenPos, Quaternion.identity, numberParent).GetComponent<TMP_Text>();
            text.text = Math.Round(value, 2).ToString();

            yield return new WaitForSeconds(initialDelay);

            float t = 0.0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime * moveSpeed;
                text.transform.position = Vector3.Slerp(text.transform.position, uiPower.transform.position, t);

                yield return new WaitForFixedUpdate();
            }

            turbine.numbersFinishedSending = true;
            data.powerCollected += value;
            GameStateManager.Instance.UpdatePowerText();

            uiPowerAnim.SetTrigger("BOUNCE");
            Destroy(text.gameObject);
        }
    }
}