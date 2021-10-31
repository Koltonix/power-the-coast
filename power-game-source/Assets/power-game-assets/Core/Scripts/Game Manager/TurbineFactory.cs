using System.Collections.Generic;
using UnityEngine;

namespace power.manager
{
    public class TurbineFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject turbinePrefab = null;
        private GameObject[,] turbines = null;

        [SerializeField]
        private Vector2Int maxSize = new Vector2Int(5, 5);

        [SerializeField]
        private Vector3 offset = new Vector3(5.0f, 0.0f, 5.0f);

        private int turbinesToShow = 0;

        private void Start()
        {
            SpawnTurbines();       

            for (int i = 0; i < maxSize.x; i++)
                EnableTurbines();
        }

        private void SpawnTurbines()
        {
            turbines = new GameObject[maxSize.x, maxSize.y];

            for (int y = 0; y < maxSize.y; y++)
            {
                for (int x = 0; x < maxSize.x; x++)
                {
                    Vector3 pos = new Vector3(x * offset.x, 0.0f, y * offset.z);
                    GameObject turbine = Instantiate(turbinePrefab, pos, Quaternion.identity);

                    turbine.SetActive(false);
                    turbines[x,y] = turbine;
                }
            }
        }

        private void EnableTurbines()
        {
            turbinesToShow++;

            for (int y = 0; y < turbinesToShow; y++)
            {
                for (int x = 0; x < turbinesToShow; x++)
                {
                    turbines[x,y].SetActive(true);
                }
            }
        }
    }
}