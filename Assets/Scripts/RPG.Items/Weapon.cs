using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Items
{
    public class Weapon : Item
    {
        [SerializeField] int m_Attack;

        public int Attack { get => m_Attack; }


    }
}
