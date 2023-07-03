using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Items
{
    public class Spell : Item
    {
        [SerializeField] int m_SpellCost;


        public int SpellCost { get => m_SpellCost; }
    }
}
