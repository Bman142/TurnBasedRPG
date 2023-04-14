using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG {
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] Character? m_CurrentCharacter;
        [SerializeField] List<Character> characters = new List<Character>();
        [SerializeField] List<Character> m_PlayerCharacters = new List<Character>();
        [SerializeField] List<Character> m_EnemyCharacters = new List<Character>();
        
        public List<Character> PlayerCharacters { get { return m_PlayerCharacters; } }
        public List<Character> EnemyCharacters { get { return m_EnemyCharacters; } }

        public void CalculateInitaitve()
        {
            characters = characters.OrderByDescending(x => x.Initiative).ToList();
            m_CurrentCharacter = characters[0];
        }

        public void SeperatePlayerAndEnemy()
        {
            m_PlayerCharacters = characters.OfType<Player>().ToList<Character>();
            m_EnemyCharacters = characters.OfType<Enemy>().ToList<Character>();
        }

        public void GetCharacters()
        {
            characters.AddRange(FindObjectsOfType<Character>().ToList());
        }

        public void ClearLists()
        {
            characters = new List<Character>();
            m_PlayerCharacters = new List<Character>();
            m_EnemyCharacters = new List<Character>();
            m_CurrentCharacter = null;
        }

        public void RandomInit()
        {
            foreach(Character character in characters)
            {
                character.m_Initiative = Random.Range(0, 10);
            }
        }

        public void NextTurn()
        {
            characters.Remove(m_CurrentCharacter);
            characters.Add(m_CurrentCharacter);
            m_CurrentCharacter = characters[0];
        }

    }
}