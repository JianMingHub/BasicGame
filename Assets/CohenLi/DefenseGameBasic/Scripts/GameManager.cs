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
        public ShopManager shopMng;
        public AudioController auCtr;
        private Player m_curPlayer;
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

        public bool IsComponentNull()
        {
            return guiMng == null || shopMng == null || auCtr == null;
        }

        public void PlayGame()
        {
            if (IsComponentNull()) return;

            ActivePlayer();

            StartCoroutine(SpawnEnemies());

            guiMng.ShowGameGUI(true);
            guiMng.UpdateGameplayCoins();
            auCtr.PlayBgm();
        }

        public void ActivePlayer()
        {
            if (IsComponentNull()) return;

            if (m_curPlayer)
                Destroy(m_curPlayer.gameObject);

            var shopItems = shopMng.items;

            if (shopItems == null || shopItems.Length <= 0) return;

            var newPlayerPb = shopItems[Pref.curPlayerId].playerPrefab;

            if (newPlayerPb)
                m_curPlayer = Instantiate(newPlayerPb, new Vector3(-7f, -1f, 0f), Quaternion.identity);
        }

        public void GameOver()
        {
            if (m_isGameOver) return;

            m_isGameOver = true;

            Pref.bestScore = m_score;

            if (guiMng.gameOverDialog)
                guiMng.gameOverDialog.Show(true);

            auCtr.PlaySound(auCtr.gameOver);
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

