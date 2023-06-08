using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RPG.Items;
using System.Xml.Schema;
using Unity.VisualScripting;

namespace RPG.Characters
{
    
    public class Character : MonoBehaviour
    {

        //Base Stats
        [SerializeField] protected int m_Initiative;
        [SerializeField] protected int m_Health;
        [SerializeField] protected int m_MaxHealth;
        [SerializeField] protected int m_MagicPoints;
        [SerializeField] protected int m_MaxMagicPoints;
        [SerializeField] protected int m_Defense;
        [SerializeField] protected int m_Attack;
        [SerializeField] protected string m_CharacterName;

        [SerializeField] protected Sprite m_Sprite;


        [SerializeField] Weapon m_Weapon;

        [SerializeField] Action m_QueuedAction;

        

        //Public getters and Setters
        public int Initiative { get { return m_Initiative; } set { m_Initiative = value; } }
        public int Health { get { return m_Health; } }
        public int MaxHealth { get { return m_MaxHealth; } }
        public int MagicPoint { get { return m_MagicPoints; } }
        public int MaxMagicPoint { get { return m_MaxMagicPoints; } }
        public int Defense { get { return m_Defense; } }
        public int Attack { get { return m_Attack; } }
        public string Name { get { return m_CharacterName; } set { m_CharacterName = value; } }
        public Weapon Weapon { get => m_Weapon; set => m_Weapon = value; }
        public Sprite Sprite { get => m_Sprite; }
        
        public Character ReturnCharacter()
        {
            return this;
        }

        private void Start()
        {
            if (this.GetComponent<SpriteRenderer>())
            {
                m_Sprite = this.GetComponent<SpriteRenderer>().sprite;
            }
        }

        public void ExecuteAction()
        {
            for (int i = 0; i < m_QueuedAction.StatMods.Count; i++)
            {
                Debug.Log(m_QueuedAction.ToString());
                m_QueuedAction.Target.TakeDamage(m_QueuedAction.StatMods[i].m_Stat, m_QueuedAction.StatMods[i].m_Modification);
            }
        }



        /// <summary>
        /// Take Damage to a Specified Stat
        /// </summary>
        /// <param name="stat">The Stat to be affected</param>
        /// <param name="mod">The amount to modify the specified stat</param>
        public void TakeDamage(Stats stat, int mod)
        {
            switch (stat)
            {
                case Stats.Health:
                    m_Health -= mod;
                    break;
            }
        }

        /// <summary>
        /// Queue the Action the character will take on their next turn
        /// </summary>
        /// <param name="target">Target of the Action</param>

        public void QueueAction(Character target, List<StatMods> statMods)
        {
            m_QueuedAction.Target = target;
            m_QueuedAction.StatMods = statMods;
        }
        public bool ValidateHealth()
        {
            if(m_Health < 0) { m_Health = 0; }
            if(m_Health == 0)
            {
                //Kill Character
                this.GetComponent<SpriteRenderer>().enabled = false;
                return false;
            }
            else
            {
                return true;
            }
        }
        

    }
    /// <summary>
    /// Class to contain all information for an action
    /// </summary>
    [Serializable]
    public struct Action
    {
        [SerializeField] Character m_Target;
        [SerializeField] List<StatMods> m_StatMods;

        public Character Target { get { return m_Target; } set { m_Target = value; } }
        public List<StatMods> StatMods { get => m_StatMods; set => m_StatMods = value; }

        public Action(Character target, List<StatMods> statMods)
        {
            m_Target = target;
            m_StatMods = statMods;
        }
        /// <summary>
        /// Returns a Formatted Version of the Action
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = "Target: " + m_Target;
            foreach(Items.StatMods statmod in m_StatMods)
            {
                output += "\nStatMod:\n " + statmod.ToString();
            }
            return output;
        }
        
    }

    
}