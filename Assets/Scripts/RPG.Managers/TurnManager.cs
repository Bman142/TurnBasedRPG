using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Characters;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.EventSystems;

namespace RPG.Managers {
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] Button TopOfMenu;

        [SerializeField] Character m_CurrentCharacter;

        //Character Lists
        [Header("Character Lists")]
        [SerializeField] List<Character> m_Characters = new List<Character>();
        [SerializeField] List<Character> m_PlayerCharacters = new List<Character>();
        [SerializeField] List<Character> m_EnemyCharacters = new List<Character>();


        [SerializeField] GameObject m_PlayerHealthPrefab;
        [SerializeField] GameObject m_EnemyPrefab;
        [SerializeField] Button m_ButtonPrefab;

        //Item Instanstiation Location Lists
        [Header("Item Spawn Location Lists")]
        [SerializeField] List<RectTransform> m_HealthLocations;
        [SerializeField] List<RectTransform> m_AttackLocations;
        [SerializeField] List<Transform> m_PlayerSpawns;
        [SerializeField] List<Transform> m_EnemySpawns;

        public List<Character> PlayerCharacters { get { return m_PlayerCharacters; } }
        public List<Character> EnemyCharacters { get { return m_EnemyCharacters; } }

        List<GameObject> TemporaryButtons = new();

        private void Awake()
        {
            ClearLists();
            GetPlayers();
            
            InstansiateEnemies();

            m_Characters.AddRange(m_PlayerCharacters);
            m_Characters.AddRange(m_EnemyCharacters);

            CalculateInitaitve();
            EstablishHealthBars();
            InstantiatePlayers();
            

        }

        void EstablishHealthBars()
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
        /// Find All Characters in the scene
        /// </summary>
        public void GetPlayers()
        {
            m_PlayerCharacters.AddRange(PlayerManager.Instance.Players);

        }

        public void GetEnemies()
        {
            m_EnemyCharacters.AddRange(FindObjectsOfType<Enemy>().ToList<Enemy>());
        }

        public void InstantiatePlayers()
        {
            for (int i = 0; i < m_PlayerCharacters.Count; i++)
            {
                m_PlayerCharacters[i].transform.position = m_PlayerSpawns[i].transform.position;
            }
        }

        public void InstansiateEnemies()
        {
            for (int i = 0; i <= 3; i++)
            {
                GameObject NewEnemy = Instantiate(m_EnemyPrefab, m_EnemySpawns[i]);
                m_EnemyCharacters.Add(NewEnemy.GetComponent<Enemy>());
                NewEnemy.GetComponent<Enemy>().Name = "Enemy " + (i + 1).ToString();
            }

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
            foreach (Character character in m_Characters)
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

        public void SetCurrentTarget(string TargetName)
        {
            //Debug.LogError("");
            //Debug.Log("Button " + this.name + " Pressed, Target Number " + TargetName.ToString());
            Character Target = m_Characters.Find(x => x.Name == TargetName);
            //Debug.Log("Target Set: " + Target.Name);
            m_CurrentCharacter.SetTarget(Target, m_CurrentCharacter.Weapon.Modifications[0].m_Stat, m_CurrentCharacter.Weapon.Modifications[0].m_Modification);
            foreach(GameObject item in TemporaryButtons)
            {
                Destroy(item);
            }
            TemporaryButtons = new();
            FindObjectOfType<EventSystem>().SetSelectedGameObject(TopOfMenu.gameObject);
        }


        /// <summary>
        /// Execute the queued action of the current character
        /// </summary>
        public void ExecuteCurrentTurn()
        {
            m_CurrentCharacter.ExecuteAction();
        }

        public void Attacking()
        {
            for (int i = 0; i < m_EnemyCharacters.Count; i++)
            {
                GameObject NewButton = Instantiate(m_ButtonPrefab.gameObject, m_AttackLocations[i]);
                NewButton.name = m_EnemyCharacters[i].Name;
                NewButton.GetComponentInChildren<TMP_Text>().text = m_EnemyCharacters[i].Name;
                NewButton.GetComponent<Button>().onClick.AddListener(delegate { SetCurrentTarget(NewButton.name); }) ;
                TemporaryButtons.Add(NewButton);
            }
        }
    } 
}