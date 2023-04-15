using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG {
    public class TurnManager : MonoBehaviour
    {

        [SerializeField] Character m_CurrentCharacter;
        [SerializeField] List<Character> m_Characters = new List<Character>();
        [SerializeField] List<Character> m_PlayerCharacters = new List<Character>();
        [SerializeField] List<Character> m_EnemyCharacters = new List<Character>();
        [SerializeField] GameObject m_PlayerHealthPrefab;
        [SerializeField] List<RectTransform> m_HealthLocations;
        
        public List<Character> PlayerCharacters { get { return m_PlayerCharacters; } }
        public List<Character> EnemyCharacters { get { return m_EnemyCharacters; } }

        private void OnEnable()
        {
            ClearLists();
            GetCharacters();
            SeperatePlayerAndEnemy();
            CalculateInitaitve();

            
        }

        private void Start()
        {
            for (int i = 0; i < m_PlayerCharacters.Count; i++)
            {
                GameObject tmp = Instantiate(m_PlayerHealthPrefab, m_HealthLocations[i]);
                Player tmp2 = (Player)m_PlayerCharacters[i];
                tmp2.SetTextAndSlider(tmp);
            }
        }
        /// <summary>
        /// Sort the list of Characters in order of Highest Initative to Lowest
        /// </summary>
        public void CalculateInitaitve()
        {
            m_Characters = m_Characters.OrderByDescending(x => x.Initiative).ToList();
            m_CurrentCharacter = m_Characters[0];
        }
        /// <summary>
        /// Filter Enemy and Player characters into seperate lists for targeting
        /// </summary>
        public void SeperatePlayerAndEnemy()
        {
            m_PlayerCharacters = m_Characters.OfType<Player>().ToList<Character>();
            m_EnemyCharacters = m_Characters.OfType<Enemy>().ToList<Character>();
        }
        /// <summary>
        /// Find All Characters in the scene
        /// </summary>
        public void GetCharacters()
        {
            m_Characters.AddRange(FindObjectsOfType<Character>().ToList());
        }
        /// <summary>
        /// Clear All Lists
        /// </summary>
        public void ClearLists()
        {
            m_Characters = new List<Character>();
            m_PlayerCharacters = new List<Character>();
            m_EnemyCharacters = new List<Character>();
            m_CurrentCharacter = null;
        }
        /// <summary>
        /// Randomise the Initative of the charcters in the Character List
        /// </summary>
        public void RandomInit()
        {
            foreach(Character character in m_Characters)
            {
                character.Initiative = Random.Range(0, 10);
            }
        }
        /// <summary>
        /// Progress the Game to the next character's turn
        /// </summary>
        public void NextTurn()
        {
            m_Characters.Remove(m_CurrentCharacter);
            m_Characters.Add(m_CurrentCharacter);
            m_CurrentCharacter = m_Characters[0];
            
        }

        /// <summary>
        /// Execute the queued action of the current character
        /// </summary>
        public void ExecuteCurrentTurn()
        {
            m_CurrentCharacter.ExecuteAction();
        }

        private void Update()
        {
        }
    }
}