using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Characters
{

    public class Enemy : Character
    {
        public new string Name { get => m_CharacterName; set => m_CharacterName = value; }
    }
}