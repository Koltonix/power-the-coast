using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using power.controller;
using power.data;
using power.turbine;
using power.utilities;

namespace power.manager
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance = null;

        [SerializeField]
        private PowerData data = null;

        [SerializeField]
        private float gameTimer = 180;
        [SerializeField]
        private TMP_Text timer = null;

        [SerializeField]
        private bool isGameOver = false;

        public static float hourSpeed = 5.0f;
        private float elapsed = 0.0f;

        [SerializeField]
        private Image hourImage = null;

        [SerializeField]
        private UnityEvent onEnd = null;

        private float originalPower = 0;

        private void Awake()
        {
            if (!Instance)
                Instance = this;

            else
                Destroy(this);
        }

        private void Start()
        {
            data.Reset();
            PowerDataToText.Instance.UpdateValue(0);
        }

        private void FixedUpdate()
        {
            if (elapsed >= hourSpeed)
            {
                InvokePowerCollection();
                elapsed = 0.0f;
            }

            if (!isGameOver)
            {
                elapsed += Time.deltaTime;  
                gameTimer -= Time.deltaTime;

                if (gameTimer <= 0)
                    EndGame();

                timer.text = Mathf.RoundToInt(gameTimer).ToString();
            }

            hourImage.fillAmount = elapsed / hourSpeed;
        }

        private void InvokePowerCollection()
        {
            originalPower = data.powerCollected;

            Turbine[] turbines =  GameObject.FindObjectsOfType<Turbine>();
            foreach (Turbine turbine in turbines)
                turbine.CollectPower();
        }

        public void UpdatePowerText()
        {
            Turbine[] turbines =  GameObject.FindObjectsOfType<Turbine>();

            int sendingTurbines = 0;
            foreach (Turbine turbine in turbines)
                if (!turbine.numbersFinishedSending)
                    sendingTurbines++;

            if (sendingTurbines <= 1)
            { 
                foreach (Turbine turbine in turbines)
                    turbine.numbersFinishedSending = false;

                PowerDataToText.Instance.UpdateValue(originalPower);
            }
        }
        
        public void EndGame()
        {
            if (isGameOver)
                return;
            
            PlayerController input = FindObjectOfType<PlayerController>();
            input.enabled = false;

            isGameOver = true;
            onEnd?.Invoke();
        }
    }
}