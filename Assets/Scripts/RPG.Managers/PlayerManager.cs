using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Items;
using RPG.Characters;

namespace RPG.Managers
{
    

    public class PlayerManager : MonoBehaviour
    {
        static PlayerManager m_Instance;

        public GameObject InventoryCanvas;

        [SerializeField] List<Item> m_Inventory;

        [SerializeField] List<Player> m_Players = new();

        Scene m_CurrentScene;

        public Scene CurrentScene { get { return m_CurrentScene; } set { m_CurrentScene = value; } }

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

        public void AddPlayer(Player player)
        {
            m_Players.Add(player);
        }

        public void StartPlayerCoroutine(string MethodName)
        {
            foreach(Player player in m_Players)
            {
                player.InvokeRepeating(MethodName, 0.1f,0.1f);
            }
        }
    }
}
