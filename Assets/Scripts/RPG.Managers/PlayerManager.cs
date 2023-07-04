using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Items;
using RPG.Characters;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Dependencies.NCalc;

namespace RPG.Managers
{
    

    public class PlayerManager : MonoBehaviour
    {
        static PlayerManager m_Instance;

        public GameObject InventoryCanvas;

        [SerializeField] List<Item> m_Inventory;

        [SerializeField] List<Player> m_Players = new();
        [SerializeField] Player m_OverworldPlayer;

        [SerializeField] Vector3 m_PlayerLocation = new();

        Scene m_CurrentScene = Scene.Overworld;

        public Scene CurrentScene { get { return m_CurrentScene; } set { m_CurrentScene = value; } }

        public static PlayerManager Instance { get { return m_Instance; } }
        public List<Item> Inventroy { get { return m_Inventory; } }
        public List<Player> Players { get { return m_Players; } }
        public Player OverworldPlayer { set { m_OverworldPlayer = value; } }
       
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

        public void LoadScene(int sceneIndex, Scene sceneType)
        {
            if(m_CurrentScene == Scene.Overworld)
            {
                m_PlayerLocation = m_OverworldPlayer.transform.position;
            }
            else if(m_CurrentScene == Scene.Battle)
            {
                // Fixes Issue where character imeditately retriggers battle scene on return to overworld
                // HACK: Moves player to the nearest 10px hopefully away from the trigger. To figure out better way to implement
                m_OverworldPlayer.transform.position = new Vector3(Mathf.Round(m_PlayerLocation.x / 10) * 10, m_PlayerLocation.y, Mathf.Round(m_PlayerLocation.z / 10)*10);
                m_PlayerLocation = Vector3.zero;
            }

            m_CurrentScene = sceneType;
            SceneManager.LoadScene(sceneIndex);
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
