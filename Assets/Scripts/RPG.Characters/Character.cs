using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RPG.Items;

namespace RPG.Characters
{
    
    public class Character : MonoBehaviour
    {
        #region Variables
        //Base Stats
        [SerializeField] protected string m_CharacterName;

        [Header("Base Stats")]
        [SerializeField] protected int m_Health;
        [SerializeField] protected int m_MaxHealth;
        [SerializeField] protected int m_MagicPoints;
        [SerializeField] protected int m_MaxMagicPoints;
        [Header("Complimenting Stats")]
        [SerializeField] protected int m_Defense;
        [SerializeField] protected int m_Accuracy;
        [SerializeField] protected int m_Evasion;
        [Header("Modifier Stats")]
        [SerializeField] protected int m_Dexterity;
        [SerializeField] protected int m_Strength;
        [Header("Inheritated Stats")]
        [SerializeField] protected int m_Initiative;

        [Space]
        [SerializeField] protected Sprite m_Sprite;

        [Header("Items and Spells")]
        [SerializeField] Weapon m_Weapon;

        [SerializeField] protected List<Spell> m_Spells;
        [Header("Actions")]

        [SerializeField] Action m_QueuedAction;

        #endregion
        #region Getters and Setters
        //Public getters and Setters

        public string Name { get { return m_CharacterName; } set { m_CharacterName = value; } }
        //Base Stats
        public int Health { get { return m_Health; } }
        public int MaxHealth { get { return m_MaxHealth; } }
        public int MagicPoint { get { return m_MagicPoints; } }
        public int MaxMagicPoint { get { return m_MaxMagicPoints; } }
        //Complimenting Stats    
        
        public int Defense { get { return m_Defense; } }
        public int Accuracy { get => m_Accuracy; }
        public int Evasion { get => m_Evasion; }

        //Modifier Stats
        public int Dexterity { get => m_Dexterity; }
        public int Strength { get => m_Strength; }

        public int Initiative { get { return m_Initiative; } set { m_Initiative = value; } }
        public Weapon Weapon { get => m_Weapon; set => m_Weapon = value; }
        public Sprite Sprite { get => m_Sprite; }
        public List<Spell> Spells { get => m_Spells; }
        
        public Character ReturnCharacter()
        {
            return this;
        }
        #endregion
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
                case Stats.Magic:
                    m_MagicPoints -= mod;
                    break;
                case Stats.Defense:
                    m_Defense -= mod;
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

                this.gameObject.SetActive(false);
                return false;
            }
            else
            {
                return true;
            }
        }
        

    }
    /// <summary>
    /// Struct to contain all information for an action
    /// </summary>
    [Serializable]
    public struct Action
    {
        [SerializeField] Character m_Target;
        [SerializeField] List<StatMods> m_StatMods;

        public Character Target { get => m_Target; set { m_Target = value; } }
        public List<StatMods> StatMods { get { return m_StatMods; } set => m_StatMods = value; }

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
            foreach(StatMods statmod in m_StatMods)
            {
                output += "\nStatMod:\n " + statmod.ToString();
            }
            return output;
        }
        
    }

    
}