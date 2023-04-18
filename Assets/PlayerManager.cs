using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{

    public class PlayerManager : MonoBehaviour
    {
        static PlayerManager m_Instance;

        public GameObject InventoryCanvas;

        [SerializeField] List<Item> m_Inventory;

        List<Player> m_Players;

        public static PlayerManager Instance { get { return m_Instance; } }
        public List<Item> Inventroy { get { return m_Inventory; } }
        public List<Player> Players { get { return m_Players; } }
       
        public void AddToInventory(Item item)
        {
            m_Inventory.Add(item);
        }

        private void Awake()
        {
            if(m_Instance != null) 
            { 
                if(m_Instance != this)
                {
                    Destroy(this);
                }
            
            }
            else
            {
                m_Instance = this;
            }

            DontDestroyOnLoad(this.gameObject);
        }

        private void OnEnable()
        {
            m_Players.AddRange(FindObjectsOfType<Player>());
        }
    }
}
