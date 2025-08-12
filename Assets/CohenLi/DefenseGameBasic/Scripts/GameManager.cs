using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UDEV.DefenseGameBasic
{
    public class GameManager : MonoBehaviour, IComponentCheking
    {
        public float spawnTime;
        public Enemy[] enemyPrefabs;
        public GUIManager guiMng;
        private bool m_isGameOver;
        private int m_score;

        public int Score { get => m_score; set => m_score = value; }

        // Start is called before the first frame update
        void Start()
        {
            if (IsComponentNull()) return;

            guiMng.ShowGameGUI(false);
            guiMng.UpdateMainCoins();
        }

        public void PlayGame()
        {
            StartCoroutine(SpawnEnemies());

            guiMng.ShowGameGUI(true);
            guiMng.UpdateGameplayCoins();
        }

        public bool IsComponentNull()
        {
            return guiMng == null;
        }

        public void GameOver()
        {
            if (m_isGameOver) return;

            m_isGameOver = true;

            Pref.bestScore = m_score;

            if (guiMng.gameOverDialog)
                guiMng.gameOverDialog.Show(true);
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

