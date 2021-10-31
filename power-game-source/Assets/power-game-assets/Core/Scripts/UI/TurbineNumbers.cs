using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using power.data;
using power.manager;
using power.turbine;

namespace power.utilities
{
    [Serializable]
    public struct TurbineData
    {
        public TurbineData(Vector3 pos, float value) {this.pos = pos; this.value = value;}

        public Vector3 pos;
        public float value;
    }

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

        private List<TurbineData> turbineData = new List<TurbineData>();

        public bool finished = true;

        private void Awake()
        {
            if (!Instance)
                Instance = this;

            else
                Destroy(this);
        }

        public void QueueValue(Vector3 pos, float value)
        {
            turbineData.Add(new TurbineData(pos, value));
        }

        public void InvokeTextCreation()
        {   
            finished = false;
            for (int i = turbineData.Count - 1; i >= 0 ; i--)
            {
                TurbineData data = turbineData[i];
                CreateText(data.pos, data.value, i == 0);
                turbineData.Remove(data);
            }
        }

        private void CreateText(Vector3 pos, float value, bool isFinal)
        {
            StartCoroutine(SpawnText(mainCamera.WorldToScreenPoint(pos), value, isFinal));
        }

        private IEnumerator SpawnText(Vector3 screenPos, float value, bool isFinal)
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

            data.powerCollected += value;

            if (isFinal)
            {
                GameStateManager.Instance.UpdatePowerText();
                finished = true;
            }
                

            uiPowerAnim.SetTrigger("BOUNCE");
            Destroy(text.gameObject);
        }
    }
}