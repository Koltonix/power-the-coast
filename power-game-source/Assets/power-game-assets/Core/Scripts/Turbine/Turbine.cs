using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using power.data;
using power.manager;
using power.utilities;

namespace power.turbine
{
    [Serializable]
    struct MaterialChange
    {
        public Material material;
        public Renderer rend;
    }

    // Units will be in MW (Megawatt) and MWh (Megawatt Hour)
    // Each Rampion Turbine produces 3.45MW (presumably at max)
    // Source: https://www.rampionoffshore.com/about/questions-and-answers/another-qa-category/
    [RequireComponent(typeof(RotateTurbine))]
    public class Turbine : MonoBehaviour
    {
        [SerializeField]
        private PowerData data = null;

        [SerializeField]
        private MaterialChange[] animatedMaterials = null;
        [SerializeField]
        private Material destroyingMaterial = null;

        [SerializeField]
        private float force = 50.0f;
        [SerializeField]
        private GameObject[] physicsExplode = null;

        [SerializeField]
        [Range(0, 1)]
        private float destroyPercentage = 0.85f;

        [SerializeField]
        private bool isDamaged = false;
        [SerializeField]
        private Vector2 redDelayRange = new Vector2(0.01f, 0.5f);

        [SerializeField]
        private float destroyTime = 5.0f;
        private float _destroyTime = 0.0f;


        // If memory serves turbines are non-efficient for 33% of the time. 
        // Or efficient only 30% of the time. One of them.
        // Doesn't make for fun gameplay though
        // Going to use some artistic licensing here...
        private float maxMegaWatt = 3.45f;
        private float minMegaWatt = 0.0f;

        private float heldPower = 0.0f;

        [SerializeField]
        private RadialMeter meter = null;
        private RotateTurbine turbineRotate = null;

        [SerializeField]
        private UnityEvent onExplode = null;

        private Coroutine destroyCoroutine = null;

        private void Start()
        {
            turbineRotate = this.GetComponent<RotateTurbine>();
            _destroyTime = destroyTime;
        }

        private void FixedUpdate()
        {
            IncreasePower();             
            meter.t = turbineRotate.t;

            if (meter.t > destroyPercentage)
                CheckForDestruction();

            else    
            {
                isDamaged = false;
                _destroyTime = destroyTime;
            }
        }

        private void CheckForDestruction()
        {
            isDamaged = true;

            if (destroyCoroutine == null)
                destroyCoroutine = StartCoroutine(DestroyAnimation());

            _destroyTime -= Time.deltaTime;
            if (_destroyTime <= 0)
            {
                Explode();
            }
        }

        private void IncreasePower()
        {
            // Increases the amount of power by the current wattage per in game hour.
            heldPower += (Mathf.Lerp(minMegaWatt, maxMegaWatt, turbineRotate.t) * Time.deltaTime) / GameStateManager.hourSpeed;
        }

        public void CollectPower()
        {
            data.powerCollected += heldPower;
            heldPower = 0.0f;
        }

        private void Explode()
        {
            onExplode?.Invoke();

            ResetMaterials();
            PhysicsTime();

            GameStateManager.Instance.EndGame();

            Destroy(meter.gameObject);
            Destroy(this);
            Destroy(turbineRotate);
        }

        private void PhysicsTime()
        {
            foreach (GameObject obj in physicsExplode)
            {
                obj.transform.SetParent(null);
                Rigidbody rb = obj.AddComponent<Rigidbody>();
                rb.angularDrag = 0;

                obj.AddComponent<ApplyMoreGravity>();
                rb.AddForce(UnityEngine.Random.insideUnitSphere * force, ForceMode.Impulse);
                Destroy(obj, 5.0f);
            }
        }

        private IEnumerator DestroyAnimation()
        {
            while (isDamaged)
            {
                SetMaterials(destroyingMaterial);
                yield return new WaitForSeconds(GetDelay());
                ResetMaterials();
                yield return new WaitForSeconds(GetDelay());
            }

            ResetMaterials();
            StopCoroutine(destroyCoroutine);
            destroyCoroutine = null;
            
            yield return null;
        }

        private float GetDelay()
        {
            return Mathf.Lerp(redDelayRange.x, redDelayRange.y, _destroyTime / destroyTime);
        }

        private void SetMaterials(Material mat)
        {
            foreach (MaterialChange obj in animatedMaterials)
                obj.rend.material = mat;
        }

        private void ResetMaterials()
        {
            foreach (MaterialChange obj in animatedMaterials)
                obj.rend.material = obj.material;
        }
    }
}