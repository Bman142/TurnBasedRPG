using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{

    public class Character : MonoBehaviour
    {

        [SerializeField]  public int m_Initiative;
        [SerializeField]  int m_Health;
        [SerializeField]  int m_MagicPoints;
        [SerializeField]  int m_Defense;

        [SerializeField] string m_Name;

        List<Item> inventory = new List<Item>();
        

        public int Initiative { get { return m_Initiative; } }
        public int Health { get { return m_Health; } }
        public int MagicPoint { get { return m_MagicPoints; } }
        public int Defense { get { return m_Defense; } }



    }
}