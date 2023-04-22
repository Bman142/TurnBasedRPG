using RPG.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Characters
{
    public class Player : Character
    {
        [SerializeField] TMP_Text playerHealth;
        [SerializeField] Slider playerHealthSlider;
        Slider playerMagicSlider;

        Camera m_Camera;

        public Camera Camaera { get { return m_Camera; } }


        private void Start()
        {
            m_Camera = this.GetComponentInChildren<Camera>();
            Debug.Log("Player Awake");
            PlayerManager.Instance.AddPlayer(this);

            DontDestroyOnLoad(this);
        }
        public void SetTextAndSlider(GameObject obj)
        {
            playerHealth = obj.GetComponentInChildren<TMP_Text>();
            List<Slider> sliders = obj.GetComponentsInChildren<Slider>().ToList();
            foreach(Slider slid in sliders)
            {
                if(slid.name == "PlayerMagicSlider")
                {
                    playerMagicSlider = slid;
                }
                else if(slid.name == "PlayerHealthSlider")
                {
                    playerHealthSlider = slid;
                }

                else
                {

                    Debug.LogError("Warning: Item name " + slid.name + " is invalid");
                }
            }
        }

        private void Update()
        {
            if (PlayerManager.Instance.CurrentScene == Scene.Battle)
            {
                //Battle Scene
                playerHealth.text = m_CharacterName + ": " + m_Health.ToString();
                playerHealthSlider.value = m_Health;
                playerMagicSlider.value = m_MagicPoints;
            }
            else if (PlayerManager.Instance.CurrentScene == Scene.Overworld)
            {
                //Overworld

            }
        }
    }
}