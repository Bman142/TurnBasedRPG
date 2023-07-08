using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Characters;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace RPG.Managers {
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] Button TopOfMenu;

        [SerializeField] Character m_CurrentCharacter;

        //Character Lists
        [Header("Character Lists")]
        [SerializeField] List<Character> m_Characters = new();
        [SerializeField] List<Character> m_PlayerCharacters = new();
        [SerializeField] List<Character> m_EnemyCharacters = new();


        [SerializeField] GameObject m_PlayerHealthPrefab;
        [SerializeField] List<GameObject> m_EnemyPrefabs;
        [SerializeField] Button m_ButtonPrefab;

        //Item Instanstiation Location Lists
        [Header("Item Spawn Location Lists")]
        [SerializeField] List<RectTransform> m_HealthLocations;
        [SerializeField] List<RectTransform> m_AttackLocations;
        [SerializeField] List<Transform> m_PlayerSpawns;
        [SerializeField] List<Transform> m_EnemySpawns;
        [SerializeField] Transform m_MagicButtonLocation;


        GameObject m_MagicButtonObject;
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
            InitHealthBars();

            for (int i = 0; i < m_Characters.Count; i++)
            {
                if (m_Characters[i] is Player)
                {
                    m_Characters[i].GetComponent<Player>().PlayerHealthText.color = Color.red;
                    break;
                }
            }

            InstantiatePlayers();
            InstantiateMagicButton();
            

        }
        /// <summary>
        /// Initalises Player Health Bars in the GUI
        /// </summary>
        void InitHealthBars()
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
        /// Import Players from the player manager
        /// </summary>
        public void GetPlayers()
        {
            m_PlayerCharacters.AddRange(PlayerManager.Instance.Players);

        }

       
        /// <summary>
        /// Move Player Icons into approprate positions and set them to active
        /// </summary>
        public void InstantiatePlayers()
        {
            for (int i = 0; i < m_PlayerCharacters.Count; i++)
            {
                m_PlayerCharacters[i].transform.position = m_PlayerSpawns[i].transform.position;
                m_PlayerCharacters[i].gameObject.SetActive(true);
            }
        }
        /// <summary>
        /// Create Enemies based off a random predefined prefab in the available list
        /// </summary>
        
        public void InstansiateEnemies()
        {
            for (int i = 0; i <= 3; i++)
            {
                GameObject NewEnemy = Instantiate(m_EnemyPrefabs[UnityEngine.Random.Range(0,m_EnemyPrefabs.Count)], m_EnemySpawns[i]);
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
        /// Randomise the Initative of the charcters in the Character List (Deprecated)
        /// </summary>
        [System.Obsolete("No Longer Used, initative should be defined per character")]
        public void RandomInit()
        {
            foreach (Character character in m_Characters)
            {
                character.Initiative = UnityEngine.Random.Range(0, 10);
            }
        }
        /// <summary>
        /// Progress the Game to the next character's turn
        /// </summary>
        public void NextTurn()
        {

            List<Character> ToBeRemoved = new();
            m_Characters.Remove(m_CurrentCharacter);
            m_Characters.Add(m_CurrentCharacter);
            foreach (Character character in m_Characters)
            {
                if (!character.ValidateHealth())
                {
                    ToBeRemoved.Add(character);
                    if(character is Player)
                    {
                        m_PlayerCharacters.Remove(character);
                    }
                    else if(character is Enemy)
                    {
                        m_EnemyCharacters.Remove(character);
                    }
                }
            }
            if (!m_EnemyCharacters.Any())
            {
                foreach(Player player in m_PlayerCharacters.Cast<Player>())
                {
                    if (!player.GetComponent<GameControls>())
                    {
                        player.gameObject.SetActive(false);
                        continue;
                    }
                    else
                    {
                        player.Camera.gameObject.SetActive(true);
                    }
                }
                PlayerManager.Instance.LoadScene(1, Scene.Overworld);
            }
            foreach(Character character in ToBeRemoved)
            {
                m_Characters.Remove(character);
            }
            m_CurrentCharacter = m_Characters[0];

            

            foreach(Player player in m_PlayerCharacters.Cast<Player>())
            {
                player.UpdateHealthSliders();
                player.PlayerHealthText.color = Color.black;
            }

            CheckCharacterTurn();
            

        }

        void InstantiateMagicButton()
        {
            if(m_CurrentCharacter is Player player && player.MaxMagicPoint != 0)
            {
                m_MagicButtonObject = Instantiate(m_ButtonPrefab.gameObject, m_MagicButtonLocation);
                m_MagicButtonObject.name = "Magic Button";
                m_MagicButtonObject.GetComponentInChildren<TMP_Text>().text = "Magic";
                m_MagicButtonObject.GetComponent<Button>().onClick.AddListener(delegate { SpellSelector(); });
            }
        }


        //TODO: Fix Recursion
        private void CheckCharacterTurn()
        {
            if(m_CurrentCharacter is Player player)
            {
                Debug.Log("It is " + m_CurrentCharacter.Name + "'s Turn");
                Player tmp = player;
                tmp.PlayerHealthText.color = new Color(1f, 0f, 0f);
                //If the character is capable of using magic, create a new targeting button to allow them to.
                if(tmp.MaxMagicPoint != 0)
                {
                    //Create Magic button
                    
                    if(m_MagicButtonObject == null)
                    {
                        InstantiateMagicButton();
                    }
                    
                }
                else if(tmp.MaxMagicPoint == 0 && m_MagicButtonObject != null)
                {
                    Destroy(m_MagicButtonObject);
                }

            }
            else if(m_CurrentCharacter is Enemy)
            {
                Character target = m_PlayerCharacters[UnityEngine.Random.Range(0, m_PlayerCharacters.Count)];
                Debug.Log(m_CurrentCharacter.Name + " is targeting " + target.Name);
                m_CurrentCharacter.QueueAction(target, m_CurrentCharacter.Weapon.Modifications);
                ExecuteCurrentTurn();
                NextTurn();
            }
        }

        /// <summary>
        /// Instantiate Buttons to Select Spell 
        /// </summary>
        public void SpellSelector()
        {
            for(int i = 0; i < m_CurrentCharacter.Spells.Count; i++)
            {
                GameObject NewButton = Instantiate(m_ButtonPrefab.gameObject, m_AttackLocations[i]);
                NewButton.name = i.ToString();
                NewButton.GetComponentInChildren<TMP_Text>().text = m_CurrentCharacter.Spells[i].name;
                NewButton.GetComponent<Button>().onClick.AddListener(delegate { SpellTargeting(int.Parse(NewButton.name)); });
                TemporaryButtons.Add(NewButton);
            }
        }
        /// <summary>
        /// Instantiate Buttons to target a spell at enemies
        /// </summary>
        /// <param name="SpellIndex">Index of the Spell in Character Spell List</param>
        public void SpellTargeting(int SpellIndex)
        {
            foreach(GameObject obj in TemporaryButtons)
            {
                Destroy(obj);
            }


            for (int i = 0; i < m_EnemyCharacters.Count; i++)
            {
                GameObject NewButton = Instantiate(m_ButtonPrefab.gameObject, m_AttackLocations[i]);
                NewButton.name = m_EnemyCharacters[i].Name;
                NewButton.GetComponentInChildren<TMP_Text>().text = m_EnemyCharacters[i].Name;
                NewButton.GetComponent<Button>().onClick.AddListener(delegate { SetCurrentTargetMagic(NewButton.name, SpellIndex); });
                TemporaryButtons.Add(NewButton);
            }
            FindObjectOfType<EventSystem>().SetSelectedGameObject(TemporaryButtons[1]);
        }

        /// <summary>
        /// Sets the Current Target and Progresses the next turn
        /// </summary>
        /// <param name="TargetName">Target Name (As in the Character.cs)</param>
        public void SetCurrentTarget(string TargetName)
        {
            //Debug.LogError("");
            //Debug.Log("Button " + this.name + " Pressed, Target Number " + TargetName.ToString());
            Character Target = m_Characters.Find(x => x.Name == TargetName);
            Debug.Log("Target Set: " + Target.Name);
            m_CurrentCharacter.QueueAction(Target, m_CurrentCharacter.Weapon.Modifications);
            foreach(GameObject item in TemporaryButtons)
            {
                Destroy(item);
            }
            TemporaryButtons = new();
            FindObjectOfType<EventSystem>().SetSelectedGameObject(TopOfMenu.gameObject);
            ExecuteCurrentTurn();
            NextTurn();
            
        }
        /// <summary>
        /// Targeting System for Magic
        /// </summary>
        /// <param name="TargetName">Name (As in Character.cs) to target</param>
        /// <param name="SpellIndex">Index of the Spell in Character Spell List</param>
        public void SetCurrentTargetMagic(string TargetName, int SpellIndex)
        {
            //Debug.LogError("");
            //Debug.Log("Button " + this.name + " Pressed, Target Number " + TargetName.ToString());
            Character Target = m_Characters.Find(x => x.Name == TargetName);
            Debug.Log("Target Set: " + Target.Name);
            m_CurrentCharacter.QueueAction(Target, m_CurrentCharacter.Spells[SpellIndex].Modifications);
            m_CurrentCharacter.TakeDamage(Stats.Magic, m_CurrentCharacter.Spells[SpellIndex].SpellCost);
            foreach(GameObject item in TemporaryButtons)
            {
                Destroy(item);
            }
            Destroy(GameObject.Find("Magic Button"));
            TemporaryButtons = new();
            FindObjectOfType<EventSystem>().SetSelectedGameObject(TopOfMenu.gameObject);
            ExecuteCurrentTurn();
            NextTurn();
            
        }


        /// <summary>
        /// Execute the queued action of the current character
        /// </summary>
        public void ExecuteCurrentTurn()
        {
            m_CurrentCharacter.ExecuteAction();
        }

        public void AttackButton()
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