using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace RPG
{
    public enum stats { Health, Defense, Magic}
    public class Item : MonoBehaviour
    {
        [SerializeField] string m_Name;
        [SerializeField] List<StatMods> m_Modifications;
        [SerializeField] GameObject m_Prefab;

        [SerializeField] TMP_Text m_NameTextBox;

        [SerializeField, Multiline] string m_Description;
        
        public string Name { get { return m_Name; } }
        public List<StatMods> Modifications { get { return m_Modifications; } }
        public string Description { get { return m_Description; } }
        public GameObject Prefab { get { return m_Prefab; } }

        private void OnEnable()
        {
            m_NameTextBox.text = m_Name;
        }

        public void LogDebug()
        {
            Debug.Log("You Selected: " + m_Name);
        }

    }
    /// <summary>
    /// Class to contain the effects of an item
    /// </summary>
    [Serializable]
    public class StatMods
    {
        public stats m_Stat;
        public int m_Modification;

        public StatMods(stats stat, int mod)
        {
            m_Modification = mod;
            m_Stat = stat;
        }
    }
}