using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDEV.DefenseGameBasic
{ 
    public class ShopManager : MonoBehaviour
    {
        public static ShopManager Ins;
        public ShopItem[] items; // Array of shop items

        public void Awake()
        {
            MakeSingleton();
        }
        private void MakeSingleton()
        {
            if (Ins == null)
            {
                Ins = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }
        private void Init()
        {
            if (items == null || items.Length <= 0) return;

            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                string dataKey = Const.PLAYER_PREFIX_PREF + i;  // player_0, player_1, player_2....

                if (item != null)
                {
                    if (i == 0)
                        Pref.SetBool(dataKey, true);
                    else
                    {
                        if (!PlayerPrefs.HasKey(dataKey))
                            Pref.SetBool(dataKey, false);
                    }
                }
            }
        }
    }
}

