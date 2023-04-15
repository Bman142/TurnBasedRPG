using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace RPG
{
    public enum stats { Health, Defense, Magic}
    public class Item : MonoBehaviour
    {

        [SerializeField] List<StatMods> m_Modifications;

        [SerializeField, Multiline] string m_Description;
        
        public List<StatMods> Modifications { get { return m_Modifications; } }
        public string Description { get { return m_Description; } }
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