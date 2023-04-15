using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG
{
    
    public class Character : MonoBehaviour
    {

        //Base Stats
        [SerializeField]  public int m_Initiative;
        [SerializeField]  int m_Health;
        [SerializeField]  int m_MagicPoints;
        [SerializeField]  int m_Defense;
        [SerializeField] int m_Attack;
        [SerializeField] string m_Name;

        //TODO: Integrate into targeting system
        public Character m_Target;

        [SerializeField] Weapon m_Weapon;
        [SerializeField] List<Item> inventory = new List<Item>();

        [SerializeField] Action m_QueuedAction;

        [SerializeField] TMP_Text playerHealth;

        //Public getters and Setters
        public int Initiative { get { return m_Initiative; } }
        public int Health { get { return m_Health; } }
        public int MagicPoint { get { return m_MagicPoints; } }
        public int Defense { get { return m_Defense; } }
        public int Attack { get { return m_Attack; } }
        public string Name { get { return m_Name; } set { m_Name = value; } }
        public Weapon Weapon { get => m_Weapon; set => m_Weapon = value; }

        public void ExecuteAction()
        {
            m_QueuedAction.Target.TakeDamage(m_QueuedAction.TargetStat, m_QueuedAction.TargetMod);
        }


        /// <summary>
        /// Take Damage to a Specified Stat
        /// </summary>
        /// <param name="stat">The Stat to be affected</param>
        /// <param name="mod">The amount to modify the specified stat</param>
        public void TakeDamage(stats stat, int mod)
        {
            switch (stat)
            {
                case stats.Health:
                    m_Health -= mod;
                    break;
            }
        }

        /// <summary>
        /// Queue the Action the character will take on their next turn
        /// </summary>
        /// <param name="target">Target of the Action</param>

        //TODO: Integrate into targeting system
        public void QueueAction(Character target)
        {
            m_QueuedAction.Target = target;
            m_QueuedAction.TargetStat = m_Weapon.Modifications[0].m_Stat;
            m_QueuedAction.TargetMod = m_Weapon.Modifications[0].m_Modification;
        }

        private void Update()
        {
            playerHealth.text = m_Health.ToString();
        }

    }
    /// <summary>
    /// Class to contain all information for an action
    /// </summary>
    [Serializable]
    public class Action
    {
        [SerializeField] Character m_Target;
        [SerializeField] stats m_TargetEffect;
        [SerializeField] int m_TargetMod;

        public Character Target { get { return m_Target; } set { m_Target = value; } }
        public stats TargetStat { get { return m_TargetEffect; } set { m_TargetEffect = value; } }
        public int TargetMod { get { return m_TargetMod; } set { m_TargetMod = value; } }
    }

    
}