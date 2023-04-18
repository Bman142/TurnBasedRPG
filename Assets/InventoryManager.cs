using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] List<RectTransform> InventorySlots;

        List<Item> items;

        List<GameObject> InstansiatedItems = new List<GameObject>();

        public void ListItems()
        {
            items = PlayerManager.Instance.Inventroy;

            for(int i = 0; i < items.Count; i++)
            {
                var tmp = Instantiate(items[i], InventorySlots[i]);
                InstansiatedItems.Add(tmp.GameObject());
            }

            FindObjectOfType<EventSystem>().SetSelectedGameObject(InstansiatedItems[0]);
        }

        public void DestroyItems()
        {
            foreach (GameObject @object in InstansiatedItems)
            {
                Destroy(@object);
            }
        }
    }
}
