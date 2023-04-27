using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using RPG.Characters;
using RPG.Managers;
using UnityEngine.SceneManagement;

namespace RPG
{
    public class GameControls : MonoBehaviour
    {

        [SerializeField] int m_Speed;
        [SerializeField] float m_JumpForce;
        [SerializeField] Animator m_Animator;
        Controls inputActions;

        bool inventoryOpen = false;

        private void Awake()
        {
            inputActions = new Controls();
            inputActions.Enable();

            inputActions.Player.OpenInventory.performed += _ => OpenInventory();
            inputActions.Player.Battle.performed += _ => TurnBased();

            if(m_Animator == null) { m_Animator = this.GetComponent<Animator>(); }
        }

        void TurnBased()
        {
            PlayerManager.Instance.CurrentScene = Scene.Battle;
            PlayerManager.Instance.StartPlayerCoroutine("UpdateHealthSliders");
            foreach (Player player in PlayerManager.Instance.Players)
            {
                //player.GetComponent<SpriteRenderer>().enabled = false;
                player.Camaera.gameObject.SetActive(false);
                
            }
            
            SceneManager.LoadScene(0);
        }
        private void Update()
        {
            if(inventoryOpen) 
            {
                
                return; 
            }
            Vector2 keyboardInput = inputActions.Player.Move.ReadValue<Vector2>();
            if (PlayerManager.Instance.CurrentScene == Scene.Overworld)
            {
                if (keyboardInput.x > 0)
                {
                    this.transform.position += (this.transform.right * m_Speed * Time.deltaTime);
                    m_Animator.SetInteger("Direction", 2);
                }
                else if (keyboardInput.x < 0)
                {
                    this.transform.position -= (this.transform.right * m_Speed * Time.deltaTime);
                    m_Animator.SetInteger("Direction", 3);
                }

                if (keyboardInput.y > 0)
                {
                    this.transform.position += (this.transform.up * m_Speed * Time.deltaTime);
                    m_Animator.SetInteger("Direction", 1);
                }
                else if (keyboardInput.y < 0)
                {
                    this.transform.position -= (this.transform.up * m_Speed * Time.deltaTime);
                    m_Animator.SetInteger("Direction", 0);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(this.gameObject.name + " Collided with " + collision.gameObject.name);
        }

        void OpenInventory()
        {
            if (inventoryOpen)
            {
                PlayerManager.Instance.GetComponent<InventoryManager>().DestroyItems();
            }
            else
            {
                PlayerManager.Instance.GetComponent<InventoryManager>().ListItems();
            }

            inventoryOpen = !inventoryOpen;
        }

        
    }
}