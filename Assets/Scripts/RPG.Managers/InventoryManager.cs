using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using RPG.Items;
using UnityEngine.SceneManagement;
using System.Linq;

namespace RPG.Managers
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] List<RectTransform> InventorySlots;
        [SerializeField] GameObject StatPanel;

        List<Item> items;

        List<GameObject> InstansiatedItems = new();

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnLoad;
        }
        void OnLoad(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 1) 
            { 
                GameObject[] tmp;
                tmp = GameObject.FindGameObjectsWithTag("Inventory Slot");
                
                tmp.OrderBy(x => x.name);
                for (int i = 0; i < tmp.Length; i++)
                {
                    InventorySlots.Add(tmp[i].GetComponent<RectTransform>());
                }

                StatPanel = FindFirstObjectByType<StatPanel>(FindObjectsInactive.Include).gameObject;
            }
        }

        public void OpenInventory()
        {
            items = PlayerManager.Instance.Inventroy;

            for(int i = 0; i < items.Count; i++)
            {
                var tmp = Instantiate(items[i], InventorySlots[i]);
                InstansiatedItems.Add(tmp.GameObject());
            }
            StatPanel.SetActive(true);

            FindObjectOfType<EventSystem>().SetSelectedGameObject(InstansiatedItems[0]);
        }

        public void DestroyItems()
        {
            foreach (GameObject @object in InstansiatedItems)
            {
                Destroy(@object);
            }
            StatPanel.SetActive(false);
            InstansiatedItems = new List<GameObject>();
        }
    }
}
