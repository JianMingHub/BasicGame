using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UDEV.DefenseGameBasic
{
    public class GameManager : MonoBehaviour
    {
        public float spawnTime;
        public Enemy[] enemyPrefabs;
        private bool m_isGameOver;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        // Update is called once per frame
        void Update()
        {

        }
        
        IEnumerator SpawnEnemies()
        {
            while (!m_isGameOver)
            {
                if (enemyPrefabs != null && enemyPrefabs.Length > 0)
                {
                    int randomIndex = Random.Range(0, enemyPrefabs.Length);

                    Enemy enemyPrefab = enemyPrefabs[randomIndex];

                    if (enemyPrefab)
                    {
                        Instantiate(enemyPrefab, new Vector3(8, 0, 0), Quaternion.identity);
                    }
                }

                yield return new WaitForSeconds(spawnTime);
            }
        }
    }
}

