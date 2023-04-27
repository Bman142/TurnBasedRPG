using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace RPG.Items
{
    
    public class Item : MonoBehaviour
    {
        [SerializeField] string m_ItemName;
        [SerializeField] List<StatMods> m_Modifications;
        [SerializeField] GameObject m_Prefab;

        [SerializeField] TMP_Text m_NameTextBox;

        [SerializeField, Multiline] string m_Description;
        
        public string Name { get { return m_ItemName; } }
        public List<StatMods> Modifications { get { return m_Modifications; } }
        public string Description { get { return m_Description; } }
        public GameObject Prefab { get { return m_Prefab; } }

        private void OnEnable()
        {
            m_NameTextBox.text = m_ItemName;
        }

        public void LogDebug()
        {
            Debug.Log("You Selected: " + m_ItemName);
        }

    }
    /// <summary>
    /// Class to contain the effects of an item
    /// </summary>
    [Serializable]
    public struct StatMods
    {
        public Stats m_Stat;
        public int m_Modification;

        public StatMods(Stats stat, int mod)
        {
            m_Modification = mod;
            m_Stat = stat;
        }
    }
}