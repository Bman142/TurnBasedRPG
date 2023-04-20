using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using RPG.Characters;
using RPG.Managers;

namespace RPG
{
    public class Movement : MonoBehaviour
    {

        [SerializeField] int m_Speed;
        [SerializeField] float m_JumpForce;
        Controls inputActions;

        bool inventoryOpen = false;

        private void Awake()
        {
            inputActions = new Controls();
            inputActions.Enable();

            inputActions.Player.Jump.performed += _ => Jump();
            inputActions.Player.OpenInventory.performed += _ => OpenInventory();
        }
        private void Update()
        {
            if(inventoryOpen) 
            {
                inputActions.Player.Jump.Disable();
                return; 
            }
            inputActions.Player.Jump.Enable();
            Vector2 keyboardInput = inputActions.Player.Move.ReadValue<Vector2>();

            if(keyboardInput.x > 0)
            {
                this.transform.position += (this.transform.right * m_Speed * Time.deltaTime);
            }
            else if (keyboardInput.x < 0)
            {
                this.transform.position -= (this.transform.right * m_Speed * Time.deltaTime);
            }
        }

        void OpenInventory()
        {
            if (inventoryOpen)
            {
                PlayerManager.Instance.InventoryCanvas.GetComponent<InventoryManager>().DestroyItems();
            }
            else
            {
                PlayerManager.Instance.InventoryCanvas.GetComponent<InventoryManager>().ListItems();
            }

            inventoryOpen = !inventoryOpen;
        }

        void Jump()
        {
            this.GetComponent<Rigidbody2D>().AddForce(this.transform.up * m_JumpForce);
        }
    }
}